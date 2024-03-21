using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EMS_Web_App.Controllers
{
    [Authorize("Permission")]
    public class StateController : Controller
    {
        private readonly IAPIHandler<election_state> _api;
        private readonly APISettings _apiSettings;
        private readonly string _token;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StateController(IAPIHandler<election_state> api, IOptions<APISettings> apiSettings,
                                                            IHttpContextAccessor httpContextAccessor)
        {
            _api = api;
            _apiSettings = apiSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _token = _httpContextAccessor.HttpContext!.Session.GetString(Constant.__TOKEN__)!;
        }

        public async Task<IActionResult> Index()
        {
            List<election_state> states = await _api.GetAllData(_token, _apiSettings.BaseURL, Constant.StateGetAll);

            return View(states);
        }

        public async Task<IActionResult> Edit(int id)
        {
            election_state states = await _api.GetDataById(_token, _apiSettings.BaseURL, Constant.StateGetById, id);

            return View(states);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] election_state model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.StateADD, model);

                    if (result != "")
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result);
                    else
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, "Unknown error");

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid data.");
                    return View();
                }                                 
            }
            catch
            {
                return View();
            }
              
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] election_state model)
        {
            try
            {
                string result = "";

                if (ModelState.IsValid)
                {
                    result = await _api.UpdateData(_token, _apiSettings.BaseURL, Constant.StateUPDATE, model);

                    if (result != "")
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result);
                    else
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, "Unknown error");

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid data.");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}

