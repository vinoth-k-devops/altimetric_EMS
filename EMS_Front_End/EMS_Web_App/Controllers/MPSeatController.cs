using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using EMS_Web_App.StaticFunc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace EMS_Web_App.Controllers
{
    [Authorize("Permission")]
    public class MPSeatController : Controller
    {
        private readonly ITransactionHandler _api;
        private readonly APISettings _apiSettings;
        private readonly string _token;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MPSeatController(ILogger<HomeController> logger,
                                            IOptions<APISettings> apiSettings,
                                            ITransactionHandler api,
                                            IHttpContextAccessor httpContextAccessor)
        {
            _apiSettings = apiSettings.Value;
            _api = api;
            _httpContextAccessor = httpContextAccessor;
            _token = _httpContextAccessor.HttpContext!.Session.GetString(Constant.__TOKEN__)!;
        }
        public async Task<IActionResult> Index()
        {
            APIResponse response = await _api.GetDataList(_token, _apiSettings.BaseURL, Constant.GetMPSeat);
            List<election_mp_seat_lists> result = GenericFunc.ExtractJsonData<List<election_mp_seat_lists>>(response.Data!.ToString());
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
            APIResponse response = await _api.GetDataById(_token, _apiSettings.BaseURL, Constant.GetMPSeatyId, id.ToString());
            election_mp_seat_by_state result = GenericFunc.ExtractJsonData<election_mp_seat_by_state>(response.Data!.ToString());
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] election_mp_seat_by_state model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    APIResponse result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.AddMPSeat, model);

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
        public async Task<IActionResult> Edit([FromForm] election_mp_seat_by_state model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    APIResponse result = await _api.UpdateData(_token, _apiSettings.BaseURL, Constant.UpdateMPSeat, model);

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
        private async Task<bool> LoadData()
        {
            var states = await _api.GetActiveList(_apiSettings.BaseURL, Constant.GetDDState);
            APIResponse elections = await _api.GetDataList(_token, _apiSettings.BaseURL, Constant.GetActiveElectionList);
            List<election_year_to_date> result = GenericFunc.ExtractJsonData<List<election_year_to_date>>(elections.Data!.ToString());

            if (states != null || result != null)
            {
                if(states != null)
                    ViewBag.StateDrpList = new SelectList(states, "election_state_id", "election_state_name");

                if (result != null)
                    ViewBag.ElectionDrpList = new SelectList(result, "election_id", "election_name");

                return true;
            }
            else
                return false;
        }
    }
}

