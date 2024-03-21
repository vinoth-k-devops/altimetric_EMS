using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using EMS_Web_App.StaticFunc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EMS_Web_App.Controllers
{
    [Authorize("Permission")]
    public class CanditureController : Controller
    {
        private readonly ITransactionHandler _api;
        private readonly IAPIHandler<election_parties> _apiPDD;
        private readonly APISettings _apiSettings;
        private readonly string _token;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanditureController(ILogger<HomeController> logger,
                                            IOptions<APISettings> apiSettings,
                                            ITransactionHandler api,
                                            IAPIHandler<election_parties> apiPDD,
                                            IHttpContextAccessor httpContextAccessor)
        {
            _apiSettings = apiSettings.Value;
            _api = api;
            _apiPDD = apiPDD;
            _httpContextAccessor = httpContextAccessor;
            _token = _httpContextAccessor.HttpContext!.Session.GetString(Constant.__TOKEN__)!;
        }
        public async Task<IActionResult> Index()
        {
            APIResponse response = await _api.GetDataList(_token, _apiSettings.BaseURL, Constant.GetCanditure);
            List<election_contesting_candidate_list> result = GenericFunc.ExtractJsonData<List<election_contesting_candidate_list>>(response.Data!.ToString());
            return View(result);
        }
        public async Task<IActionResult> Add()
        {
            await LoadData();
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            await LoadData();
            APIResponse response = await _api.GetDataById(_token, _apiSettings.BaseURL, Constant.GetCanditureById, id.ToString());
            election_contesting_candidate result = GenericFunc.ExtractJsonData<election_contesting_candidate>(response.Data!.ToString());
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] election_contesting_candidate model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    APIResponse result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.AddCanditure, model);

                    if (result.IsSuccess)
                    {
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result.Message);
                    }
                    else
                    {
                        await LoadData();
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, result.Message);
                        return View(model);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    await LoadData();
                    ModelState.AddModelError("", "Invalid data.");
                    return View(model);
                }
            }
            catch
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] election_contesting_candidate model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    APIResponse result = await _api.UpdateData(_token, _apiSettings.BaseURL, Constant.UpdateCanditure, model);

                    if (result.IsSuccess)
                    {
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result.Message);
                    }
                    else
                    {
                        await LoadData();
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, result.Message);
                        return View(model);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    await LoadData();
                    ModelState.AddModelError("", "Invalid data.");
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }
        public async Task<string> GetActiveCities(int id)
        {
            var result = await _api.GetActiveCity(_apiSettings.BaseURL, Constant.GetDDCity, id);
            return JsonConvert.SerializeObject(result);
        }
        private async Task<bool> LoadData()
        {
            var states = await _api.GetActiveList(_apiSettings.BaseURL, Constant.GetDDState);

            var parties = await _apiPDD.GetAllData(_token, _apiSettings.BaseURL, Constant.PartyGetAll);

            APIResponse elections = await _api.GetDataList(_token, _apiSettings.BaseURL, Constant.GetActiveElectionList);
            List<election_year_to_date> result = GenericFunc.ExtractJsonData<List<election_year_to_date>>(elections.Data!.ToString());

            APIResponse users = await _api.GetDataList(_token, _apiSettings.BaseURL, Constant.GetActiveUsers);
            List<election_user> Rusers = GenericFunc.ExtractJsonData<List<election_user>>(users.Data!.ToString());

            if (states != null || result != null || parties != null || Rusers != null)
            {
                if (states != null)
                    ViewBag.StateDrpList = new SelectList(states, "election_state_id", "election_state_name");

                if (result != null)
                    ViewBag.ElectionDrpList = new SelectList(result, "election_id", "election_name");

                if (parties != null)
                    ViewBag.PartyDrpList = new SelectList(parties, "election_party_id", "election_party_name");

                if (Rusers != null)
                    ViewBag.UserDrpList = new SelectList(Rusers, "election_user_id", "election_voter_name");

                return true;
            }
            else
                return false;
        }
    }
}

