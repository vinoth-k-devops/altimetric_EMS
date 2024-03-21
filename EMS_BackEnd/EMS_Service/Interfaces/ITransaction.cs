using EMS_Domain.Model;
using EMS_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EMS_Service.Interfaces
{
	public interface ITransaction
	{
        Task<List<election_state>> GetActiveListForState();
        Task<List<election_city>> GetActiveListForCity(int election_state_id);
        Task<Dashboard> GetDashboardData();
        Task<string> Approve(string keyvField);
        Task<List<election_user>> GetActiveUser();
        Task<List<election_symbols>> GetActiveListForSymbol();

        Task<List<election_year_to_date>> GetListForElection();
        Task<Response> AddElection(election_year_to_date model);
        Task<Response> EditElection(election_year_to_date model);
        Task<Response> GetElectionById(int id);
        Task<Response> GetActiveElection();

        Task<Response> GetListForMPSeat();
        Task<Response> AddMPSeat(election_mp_seat_by_state model);
        Task<Response> EditMPSeat(election_mp_seat_by_state model);
        Task<Response> GetMPSeatById(int id);

        Task<Response> AddCanditure(election_contesting_candidate model);
        Task<Response> EditCanditure(election_contesting_candidate model);
        Task<Response> GetCanditureById(int id);
        Task<Response> GetListCanditure();

        Task<Response> GetActiveUsers();
        Task<Response> GetUserDashboard(string token);
        Task<Response> RightToVote(ToVote model);
        Task<Response> GetCompletedorInProgressElectionList();
        Task<Response> GetElectionResult(ElectionInput model);
    }
}

