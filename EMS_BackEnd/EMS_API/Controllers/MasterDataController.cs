using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_Domain.Entities;
using EMS_Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterOperation<election_state> _election_state;
        private readonly IMasterOperation<election_city> _election_city;
        private readonly IMasterOperation<election_symbols> _election_symbols;
        private readonly IMasterOperation<election_user> _election_user;
        private readonly IMasterOperation<election_parties> _election_party;

        public MasterDataController(IMasterOperation<election_state> election_state,
                                                IMasterOperation<election_city> election_city,
                                                IMasterOperation<election_symbols> election_symbols,
                                                IMasterOperation<election_user> election_user,
                                                IMasterOperation<election_parties> election_party)
        {
            _election_state = election_state;
            _election_city = election_city;
            _election_symbols = election_symbols;
            _election_user = election_user;
            _election_party = election_party;
        }

        #region State Master

        [HttpPost]
        public async Task<ActionResult> AddState(election_state state)
        {
            return Ok(await _election_state.Add(state));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateState(election_state state)
        {
            return Ok(await _election_state.Update(state));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<election_state>> GetStateById(int id)
        {
            return Ok(await _election_state.GetById(id));
        }
        [HttpGet]
        [EnableCors("HandleCORS")]
        public async Task<ActionResult<List<election_state>>> GetStateAll()
        {
            return Ok(await _election_state.GetAll());
        }

        #endregion

        #region City Master

        [HttpPost]
        public async Task<ActionResult> AddCity(election_city city)
        {
            return Ok(await _election_city.Add(city));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCity(election_city city)
        {
            return Ok(await _election_city.Update(city));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<election_city>> GetCityById(int id)
        {
            return Ok(await _election_city.GetById(id));
        }
        [HttpGet]
        public async Task<ActionResult<List<election_city>>> GetCityAll()
        {
            return Ok(await _election_city.GetAll());
        }

        #endregion

        #region Symbol Master

        [HttpPost]
        public async Task<ActionResult> AddSymbol(election_symbols symbols)
        {
            return Ok(await _election_symbols.Add(symbols));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateSymbol(election_symbols symbols)
        {
            return Ok(await _election_symbols.Update(symbols));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<election_symbols>> GetSymbolById(int id)
        {
            return Ok(await _election_symbols.GetById(id));
        }
        [HttpGet]
        public async Task<ActionResult<List<election_symbols>>> GetSymbolAll()
        {
            return Ok(await _election_symbols.GetAll());
        }

        #endregion

        #region User Master

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddUser(election_user users)
        {
            return Ok(await _election_user.Add(users));
        }

        #endregion

        #region Party Master

        [HttpPost]
        public async Task<ActionResult> AddParty(election_parties party)
        {
            return Ok(await _election_party.Add(party));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateParty(election_parties party)
        {
            return Ok(await _election_party.Update(party));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<election_parties>> GetPartyById(int id)
        {
            return Ok(await _election_party.GetById(id));
        }
        [HttpGet]
        public async Task<ActionResult<List<election_parties>>> GetPartiesAll()
        {
            return Ok(await _election_party.GetAll());
        }

        #endregion
    }
}

