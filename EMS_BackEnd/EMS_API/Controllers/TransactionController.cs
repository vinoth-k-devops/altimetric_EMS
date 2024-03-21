using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_Common;
using EMS_Domain.Entities;
using EMS_Domain.Model;
using EMS_Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransaction _transaction;

        public TransactionController(ITransaction transaction)
        {
            _transaction = transaction;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<election_state>>> GetActiveState()
        {
            return Ok(await _transaction.GetActiveListForState());
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<election_state>>> GetActiveCities(int id)
        {
            return Ok(await _transaction.GetActiveListForCity(id));
        }
        [HttpGet]
        public async Task<ActionResult<List<election_state>>> GetDashboard()
        {
            return Ok(await _transaction.GetDashboardData());
        }

        [HttpPost]
        public async Task<ActionResult<string>> Approve([FromBody] string keyvField)
        {
            return Ok(await _transaction.Approve(keyvField));
        }
        [HttpGet]
        public async Task<ActionResult<List<election_user>>> GetActiveUser()
        {
            return Ok(await _transaction.GetActiveUser());
        }
        [HttpGet]
        public async Task<ActionResult<List<election_symbols>>> GetActiveSymbols()
        {
            return Ok(await _transaction.GetActiveListForSymbol());
        }

        [HttpGet]
        public async Task<ActionResult<List<election_year_to_date>>> GetElection()
        {
            return Ok(await _transaction.GetListForElection());
        }
        [HttpPost]
        public async Task<ActionResult<Response>> AddElection([FromBody] election_year_to_date model)
        {
            return Ok(await _transaction.AddElection(model));
        }
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateElection([FromBody] election_year_to_date model)
        {
            return Ok(await _transaction.EditElection(model));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetElectionById(int Id)
        {
            return Ok(await _transaction.GetElectionById(Id));
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetMPSeatList()
        {
            return Ok(await _transaction.GetListForMPSeat());
        }
        [HttpPost]
        public async Task<ActionResult<Response>> AddMPSeat([FromBody] election_mp_seat_by_state model)
        {
            return Ok(await _transaction.AddMPSeat(model));
        }
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateMPSeat([FromBody] election_mp_seat_by_state model)
        {
            return Ok(await _transaction.EditMPSeat(model));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetMPSeatById(int Id)
        {
            return Ok(await _transaction.GetMPSeatById(Id));
        }
        [HttpGet]
        public async Task<ActionResult<Response>> GetActiveElectionList()
        {
            return Ok(await _transaction.GetActiveElection());
        }


        [HttpGet]
        public async Task<ActionResult<Response>> GetCanditureList()
        {
            return Ok(await _transaction.GetListCanditure());
        }
        [HttpPost]
        public async Task<ActionResult<Response>> AddCanditure([FromBody] election_contesting_candidate model)
        {
            return Ok(await _transaction.AddCanditure(model));
        }
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateCanditure([FromBody] election_contesting_candidate model)
        {
            return Ok(await _transaction.EditCanditure(model));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetCanditureById(int Id)
        {
            return Ok(await _transaction.GetCanditureById(Id));
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetActiveUsers()
        {
            return Ok(await _transaction.GetActiveUsers());
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<Response>> GetUserDashboard(string token)
        {
            return Ok(await _transaction.GetUserDashboard(token));
        }

        [HttpPost]
        public async Task<ActionResult<Response>> RIGHTTOVOTE([FromBody] ToVote model)
        {
            return Ok(await _transaction.RightToVote(model));
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetCompletedorInProgressElectionList()
        {
            return Ok(await _transaction.GetCompletedorInProgressElectionList());
        }
        [HttpPost]
        public async Task<ActionResult<Response>> GetElectionResult(ElectionInput model)
        {
            return Ok(await _transaction.GetElectionResult(model));
        }
    }
}

