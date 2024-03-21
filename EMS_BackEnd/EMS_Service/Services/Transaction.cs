using System;
using System.Linq;
using System.Security.Principal;
using EMS_Common;
using EMS_Domain.EMS;
using EMS_Domain.Entities;
using EMS_Domain.Model;
using EMS_Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EMS_Service.Services
{
	public class Transaction : ITransaction
    {
        private readonly EMSContext _context;
        private readonly ITokenService _tokenService;
        private readonly JWTSettings _jwtSettings;
        public Transaction(EMSContext context, ITokenService tokenService, IOptions<JWTSettings> options)
        {
            _context = context;
            _tokenService = tokenService;
            _jwtSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
        }
        public async Task<List<election_state>> GetActiveListForState()
        {
            return await _context.election_states.Where(x => x.election_state_active == true).ToListAsync();
        }
        public async Task<List<election_city>> GetActiveListForCity(int election_state_id)
        {
            return await _context.election_citys.Where(x => x.election_city_active == true && x.election_state_id == election_state_id).ToListAsync();
        }
        public async Task<List<election_symbols>> GetActiveListForSymbol()
        {
            return await _context.election_symbols_lists.Where(x => x.election_sym_active == true).ToListAsync();
        }
        public async Task<List<election_user>> GetActiveUser()
        {
            return await _context.election_users.Where(x => x.election_user_approve_status == true).ToListAsync();
        }
        public async Task<Dashboard> GetDashboardData()
        {
            return await Task.FromResult(new Dashboard()
            {
                total_user_count = _context.election_users.Count().ToString(),
                parties_count = _context.election_parties_lists.Count().ToString(),
                candidate_count = _context.election_contesting_candidates.Count().ToString(),
                awaiting_approval_count = _context.election_users.Where(x => x.election_user_approve_status == false).Count().ToString(),
                approval_user =
                (from a in _context.election_users
                 join b in _context.election_states on a.election_state_id equals b.election_state_id
                 join c in _context.election_citys on a.election_city_id equals c.election_city_id
                 where a.election_user_approve_status == false
                 select new e_user
                 {
                     election_user_id = a.election_user_id,
                     election_voter_id = a.election_voter_id,
                     election_voter_name = a.election_voter_name,
                     election_city_name = c.election_city_name,
                     election_state_name = b.election_state_name
                 }).ToList()
            });        
         }

        public async Task<string> Approve(string keyvField)
        {
            var user = await _context.election_users.Where(x => x.election_user_id == Convert.ToInt32(keyvField)).FirstOrDefaultAsync();

            if(user != null)
            {
                user.election_user_approve_status = true;
                await _context.SaveChangesAsync();

                return Common.APPROVE.Replace("{0}", user.election_voter_name);
            }
            return Common.ErrorApprove;
        }

        public async Task<List<election_year_to_date>> GetListForElection()
        {
            return await _context.election_year_to_dates.OrderByDescending(x => x.election_id).ToListAsync();
        }
        public async Task<Response> GetElectionById(int id)
        {
            Response response = new Response()
            {
                IsSuccess = true,
                Data = await _context.election_year_to_dates.Where(x => x.election_id == id).FirstOrDefaultAsync()
            };
            return await Task.FromResult(response);
        }
        public async Task<Response> AddElection(election_year_to_date model)
        {
            Response response = new Response();

            if(await _context.election_year_to_dates.Where(x => x.election_current_status == 1).AnyAsync())
            {
                response.Message = Common.ElectionAddError;
                return await Task.FromResult(response);
            }                
            else
            {
                response.IsSuccess = true;
                response.Message = Common.INSERT;

                await _context.election_year_to_dates.AddAsync(model);
                await _context.SaveChangesAsync();
                return await Task.FromResult(response);
            }               
        }
        public async Task<Response> EditElection(election_year_to_date model)
        {
            Response response = new Response();

            if (await _context.election_year_to_dates.Where(x => x.election_id == model.election_id && x.election_current_status == 1).AnyAsync())
            {
                response.IsSuccess = true;
                response.Message = Common.UPDATE;

                _context.election_year_to_dates.Update(model);
                await _context.SaveChangesAsync();
                return await Task.FromResult(response);
            }
            else
            {
                response.Message = Common.ElectionUpdateError;
                return await Task.FromResult(response);
            }
        }
        public async Task<Response> GetListForMPSeat()
        {
            var result = (from a in _context.election_mp_seat_by_states
                          join b in _context.election_states on a.election_state_id equals b.election_state_id
                          join c in _context.election_year_to_dates on a.election_id equals c.election_id
                          select new election_mp_seat_lists
                          {
                              election_mp_seat_id = a.election_mp_seat_id,
                              election_name = c.election_name,
                              election_state_name = b.election_state_name,
                              election_mp_seat_no = a.election_mp_seat_no,
                              election_seat_to_edit = c.election_current_status != 1 ? false : true
                          }).OrderByDescending(x => x.election_mp_seat_id).ToList();
            Response response = new Response()
            {
                IsSuccess = true,
                Data = await Task.FromResult(result)
            };
            return await Task.FromResult(response);
        }
        public async Task<Response> AddMPSeat(election_mp_seat_by_state model)
        {
            Response response = new Response();
            
            if (await _context.election_mp_seat_by_states.Where(x => x.election_state_id == model.election_state_id
                && x.election_id == model.election_id).AnyAsync())
            {
                response.Message = Common.MPSeatError;
                return await Task.FromResult(response);
            }
            else if (await _context.election_citys.Where(x => x.election_state_id == model.election_state_id).CountAsync() < model.election_mp_seat_no)
            {
                response.Message = Common.MPSeatGreaterError;
                return await Task.FromResult(response);
            }
            else
            {
                response.IsSuccess = true;
                response.Message = Common.INSERT;

                await _context.election_mp_seat_by_states.AddAsync(model);
                await _context.SaveChangesAsync();
                return await Task.FromResult(response);
            }
        }
        public async Task<Response> EditMPSeat(election_mp_seat_by_state model)
        {
            Response response = new Response();

            if (await _context.election_mp_seat_by_states.Where(x => x.election_state_id == model.election_state_id
                    && x.election_id == model.election_id && x.election_mp_seat_id != model.election_mp_seat_id).AnyAsync())
            {
                response.Message = Common.MPSeatError;
                return await Task.FromResult(response);
            }
            else if (await _context.election_citys.Where(x => x.election_state_id == model.election_state_id).CountAsync() < model.election_mp_seat_no)
            {
                response.Message = Common.MPSeatGreaterError;
                return await Task.FromResult(response);
            }
            else
            {
                response.IsSuccess = true;
                response.Message = Common.UPDATE;

                _context.election_mp_seat_by_states.Update(model);
                await _context.SaveChangesAsync();
                return await Task.FromResult(response);
            }
        }
        public async Task<Response> GetMPSeatById(int id)
        {
            Response response = new Response()
            {
                IsSuccess = true,
                Data = await _context.election_mp_seat_by_states.Where(x => x.election_mp_seat_id == id).FirstOrDefaultAsync()
            };
            return await Task.FromResult(response);
        }
        public async Task<Response> GetActiveElection()
        {
            Response response = new Response()
            {
                IsSuccess = true,
                Data = await _context.election_year_to_dates.Where(x => x.election_current_status == 1).ToListAsync()
            };
            return await Task.FromResult(response);
        }

        public async Task<Response> AddCanditure(election_contesting_candidate model)
        {
            Response response = new Response();

            if (await _context.election_contesting_candidates.Where(x => x.election_state_id == model.election_state_id && x.election_city_id == model.election_city_id
               && x.election_id == model.election_id && x.election_party_id == model.election_party_id).AnyAsync())
            {
                response.Message = Common.CanditureError;
                return await Task.FromResult(response);
            }
            else if (await _context.election_contesting_candidates.Where(x => x.election_id == model.election_id && x.election_user_id == model.election_user_id).AnyAsync())
            {
                response.Message = Common.CanditureExistsError;
                return await Task.FromResult(response);
            }
            else
            {
                response.IsSuccess = true;
                response.Message = Common.INSERT;

                await _context.election_contesting_candidates.AddAsync(model);
                await _context.SaveChangesAsync();
                return await Task.FromResult(response);
            }
        }
        public async Task<Response> EditCanditure(election_contesting_candidate model)
        {
            Response response = new Response();

            if (await _context.election_contesting_candidates.Where(x => x.election_state_id == model.election_state_id && x.election_city_id == model.election_city_id
               && x.election_id == model.election_id && x.election_party_id == model.election_party_id && x.election_contest_id != model.election_contest_id).AnyAsync())
            {
                response.Message = Common.CanditureError;
                return await Task.FromResult(response);
            }
            else if (await _context.election_contesting_candidates.Where(x => x.election_id == model.election_id && x.election_user_id == model.election_user_id && x.election_contest_id != model.election_contest_id).AnyAsync())
            {
                response.Message = Common.CanditureExistsError;
                return await Task.FromResult(response);
            }
            else
            {
                response.IsSuccess = true;
                response.Message = Common.UPDATE;

                 _context.election_contesting_candidates.Update(model);
                await _context.SaveChangesAsync();
                return await Task.FromResult(response);
            }
        }
        public async Task<Response> GetCanditureById(int id)
        {
            Response response = new Response()
            {
                IsSuccess = true,
                Data = await _context.election_contesting_candidates.Where(x => x.election_contest_id == id).FirstOrDefaultAsync()
            };
            return await Task.FromResult(response);
        }
        public async Task<Response> GetListCanditure()
        {

            var result = (from a in _context.election_contesting_candidates
                         join b in _context.election_states on a.election_state_id equals b.election_state_id
                         join c in _context.election_year_to_dates on a.election_id equals c.election_id
                         join d in _context.election_citys on a.election_city_id equals d.election_city_id
                         join e in _context.election_users on a.election_user_id equals e.election_user_id
                         join f in _context.election_parties_lists on a.election_party_id equals f.election_party_id
                         select new election_contesting_candidate_list
                         {
                             election_contest_id = a.election_contest_id,
                             election_name = c.election_name,
                             election_party_name = f.election_party_name,
                             election_voter_name = e.election_voter_name,
                             election_state_name = b.election_state_name,
                             election_city_name = d.election_city_name,
                             election_canditure_edit = c.election_current_status != 1 ? false : true
                         }).ToList();

            Response response = new Response()
            {
                IsSuccess = true,
                Data = await Task.FromResult(result)
            };
            return await Task.FromResult(response);
        }
        public async Task<Response> GetActiveUsers()
        {
            Response response = new Response()
            {
                IsSuccess = true,
                Data = await _context.election_users.Where(x => x.election_user_approve_status == true && x.election_user_id != 1).ToListAsync()
            };
            return await Task.FromResult(response);
        }
        public async Task<Response> GetUserDashboard(string token)
        {
            Response response = new Response();

            DateTime dt = DateTime.Now;
            bool UpComming = await _context.election_year_to_dates.Where(x => x.election_current_status == 1).AnyAsync();            

            if (UpComming)
            {
                dt = await _context.election_year_to_dates.Where(x => x.election_current_status == 1).Select(x => x.election_start_date_time).FirstAsync();

                if(!(dt > DateTime.Now))
                {
                    var res = await _context.election_year_to_dates.Where(x => x.election_current_status == 1).FirstOrDefaultAsync();
                    UpComming = false;

                    res!.election_current_status = 2;
                   await _context.SaveChangesAsync();
                }
            }

            bool InProgress = await _context.election_year_to_dates.Where(x => x.election_current_status == 2).AnyAsync();

            if (UpComming)
            {                
                response.Message = "Election will start on " + dt.ToString("MM/dd/yyyy hh:mm tt") + "..";
            }
            else if (InProgress)
            {
                response.IsSuccess = true;

                UserDashBoard userDash = new UserDashBoard();
                userDash.showPollCount = true;

               userDash.pollCount = await _context.election_poll_datas.Join(_context.election_year_to_dates, a => a.election_id, b => b.election_id, (a, b) => new
               {
                    poll = a.election_party_id,
                    election_status = b.election_current_status
               }).Where(x => x.election_status == 2).CountAsync();

                var principal = await _tokenService.GetPrincipalFromExpiredToken(_jwtSettings, token);

                var username = principal.Identity!.Name!; //this is mapped to the Name claim by default

                userDash.PolledByUser = await _context.election_users.Join(_context.election_poll_datas, u => u.election_user_id, v => v.election_user_id, (u, v) => new
                {
                    election_voter_id = u.election_voter_id 
                }).Where(x => x.election_voter_id == username).AnyAsync();

                if(!userDash.PolledByUser)
                {
                  var data =   (from a in _context.election_users
                                join b in _context.election_contesting_candidates on a.election_state_id equals b.election_state_id 
                                join c in _context.election_users on b.election_user_id equals c.election_user_id
                                join d in _context.election_parties_lists on b.election_party_id equals d.election_party_id
                                join e in _context.election_symbols_lists on d.election_sym_id equals e.election_sym_id
                                where ( a.election_voter_id == username) && (a.election_city_id == b.election_city_id)
                                select new VotingInfo
                                {
                                   candidate_id = c.election_user_id,
                                   candidate_name = c.election_voter_name,
                                   party_name = d.election_party_name,
                                   symbol = e.election_sym_font_name
                                }).ToList();

                    userDash.votings = await Task.FromResult(data);
                }
                response.Data = await Task.FromResult(userDash);
            }
            else
            {
                response.Message += "No Election is available..";
            }            
            return await Task.FromResult(response);
        }
        public async Task<Response> RightToVote(ToVote model)
        {
            var result = await (from a in _context.election_contesting_candidates
                                join b in _context.election_users on model.voter_user_name equals b.election_voter_id
                          where a.election_user_id == model.contest_id
                          select new election_poll_data
                          {
                              election_poll_id = 0,
                              election_id = a.election_id,
                              election_party_id = a.election_party_id,
                              election_state_id = a.election_state_id,
                              election_city_id = a.election_city_id,
                              election_user_id = b.election_user_id
                          }).FirstOrDefaultAsync();

            await _context.election_poll_datas.AddAsync(result!);
            await _context.SaveChangesAsync();

            return await Task.FromResult(new Response() { IsSuccess = true, Message = "Voted Successfully.." });
        }
        public async Task<Response> GetCompletedorInProgressElectionList()
        {
            Response response = new Response()
            {
                IsSuccess = true,
                Data = await _context.election_year_to_dates.Where(x => x.election_current_status == 2 || x.election_current_status == 3).ToListAsync()
            };
            return await Task.FromResult(response);
        }
        public async Task<Response> GetElectionResult(ElectionInput model)
        {
            if(await _context.election_year_to_dates.Where(x => x.election_id == model.election_id && x.election_current_status == 2).AnyAsync())
            {
                var data = await _context.election_year_to_dates.Where(x => x.election_id == model.election_id && x.election_current_status == 2).FirstOrDefaultAsync();
                data!.election_current_status = 3;
                await _context.SaveChangesAsync();
            }

            var data1 = (from a in _context.election_contesting_candidates
                        join c in _context.election_users on a.election_user_id equals c.election_user_id
                        join d in _context.election_parties_lists on a.election_party_id equals d.election_party_id
                        join e in _context.election_symbols_lists on d.election_sym_id equals e.election_sym_id
                        join b in _context.election_year_to_dates on a.election_id equals b.election_id
                        where (b.election_id == model.election_id) && (a.election_city_id == a.election_city_id)
                        select new ElectionResult
                        {
                            candidate_id = c.election_user_id,
                            candidate_name = c.election_voter_name,
                            party_name = d.election_party_name,
                            symbol = e.election_sym_font_name,
                            pollcount = _context.election_poll_datas
                                            .Where(x =>
                                                x.election_id == a.election_id &&
                                                x.election_party_id == a.election_party_id &&
                                                x.election_state_id == a.election_state_id &&
                                                x.election_city_id == a.election_city_id
                                             ).Count(),
                            election_winner = false
                        }).ToList();

            var dd = data1.Max(x => x.pollcount);

            var final = (from a in data1
                         select new ElectionResult
                         {
                             candidate_id = a.candidate_id,
                             candidate_name = a.candidate_name,
                             party_name = a.party_name,
                             symbol = a.symbol,
                             pollcount = a.pollcount,
                             election_winner = a.pollcount == dd ? true : false 
                         }).OrderByDescending(x => x.pollcount).ToList();


            Response response = new Response()
            {
                IsSuccess = true,
                Data = await Task.FromResult(final)
            };
            return await Task.FromResult(response);
        }
    }       
}

