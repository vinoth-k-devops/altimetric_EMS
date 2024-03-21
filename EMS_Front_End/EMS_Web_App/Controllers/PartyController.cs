using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS_Web_App.Controllers
{
    public class PartyController : Controller
    {
        private readonly IAPIHandler<election_parties> _api;
        private readonly ITransactionHandler _apiDD;
        private readonly APISettings _apiSettings;
        private readonly string _token;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartyController(ILogger<HomeController> logger, IAPIHandler<election_parties> api,
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
            List<election_parties> parties = await _api.GetAllData(_token, _apiSettings.BaseURL, Constant.PartyGetAll);

            return View(parties);
        }

        public async Task<IActionResult> Edit(int id)
        {
            await LoadState();
            election_parties states = await _api.GetDataById(_token, _apiSettings.BaseURL, Constant.PartyGetById, id);

            return View(states);
        }
        public async Task<IActionResult> Add()
        {
            await LoadState();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] election_parties model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.PartyADD, model);

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
        public async Task<IActionResult> Edit([FromForm] election_parties model)
        {
            try
            {
                string result = "";

                if (ModelState.IsValid)
                {
                    result = await _api.UpdateData(_token, _apiSettings.BaseURL, Constant.PartyUPDATE, model);

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
            var data = await _apiDD.GetActiveList(_token, _apiSettings.BaseURL, Constant.GetDDSymbol);

            if (data != null)
            {
                ViewBag.SymDrpList = new SelectList(data, "election_sym_id", "election_sym_name");
                return true;
            }
            else
                return false;
        }
    }
}

