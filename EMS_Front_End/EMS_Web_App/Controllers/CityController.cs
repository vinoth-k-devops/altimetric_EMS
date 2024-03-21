using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace EMS_Web_App.Controllers
{
    [Authorize("Permission")]
    public class CityController : Controller
    {
        private readonly IAPIHandler<election_city> _api;
        private readonly ITransactionHandler _apiDD;
        private readonly APISettings _apiSettings;
        private readonly string _token;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CityController(ILogger<HomeController> logger, IAPIHandler<election_city> api,
                                            IOptions<APISettings> apiSettings,
                                            ITransactionHandler apiDD,
                                            IHttpContextAccessor httpContextAccessor)
        {
            _api = api;
            _apiSettings = apiSettings.Value;
            _apiDD = apiDD;
            _httpContextAccessor = httpContextAccessor;
            _token = _httpContextAccessor.HttpContext!.Session.GetString(Constant.__TOKEN__)!;
        }

        public async Task<IActionResult> Index()
        {
            List<election_city> states = await _api.GetAllData(_token, _apiSettings.BaseURL, Constant.CityGetAll);

            return View(states);
        }

        public async Task<IActionResult> Edit(int id)
        {
            await LoadState();
            election_city states = await _api.GetDataById(_token, _apiSettings.BaseURL, Constant.CityGetById, id);

            return View(states);
        }
        public async Task<IActionResult> Add()
        {
            await LoadState();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] election_city model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.CityADD, model);

                    if (result != "")
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result);
                    else
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, "Unknown error");
                }
                else
                    ModelState.AddModelError("", "Invalid data.");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] election_city model)
        {
            try
            {
                string result = "";

                if (ModelState.IsValid)
                {
                    result = await _api.UpdateData(_token, _apiSettings.BaseURL, Constant.CityUPDATE, model);

                    if (result != "")
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result);
                    else
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, "Unknown error");
                }
                else
                    ModelState.AddModelError("", "Invalid data.");


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private async Task<bool> LoadState()
        {
            var states = await _apiDD.GetActiveList(_apiSettings.BaseURL, Constant.GetDDState);

            if (states != null)
            {
                ViewBag.StateDrpList = new SelectList(states, "election_state_id", "election_state_name");
                return true;
            }
            else
                return false;
        }
    }
}

