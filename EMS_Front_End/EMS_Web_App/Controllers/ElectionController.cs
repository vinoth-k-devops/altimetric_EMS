using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using EMS_Web_App.StaticFunc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EMS_Web_App.Controllers
{
    public class ElectionController : Controller
    {
        private readonly ITransactionHandler _api;
        private readonly APISettings _apiSettings;
        private readonly string _token;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ElectionController(ILogger<HomeController> logger, 
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
            List<election_year_to_date> data =  await _api.GetElectionList(_token, _apiSettings.BaseURL, Constant.GetElection);
            return View(data);
        }
        public IActionResult Add()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            APIResponse response = await _api.GetDataById(_token, _apiSettings.BaseURL, Constant.GetElectionById, id.ToString());
            election_year_to_date result = GenericFunc.ExtractJsonData<election_year_to_date>(response.Data!.ToString());            
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] election_year_to_date model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    APIResponse result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.AddElection, model);

                    if (result.IsSuccess)
                    {
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result.Message);
                    }
                    else
                    {
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, result.Message);
                        return View(model);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
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
        public async Task<IActionResult> Edit([FromForm] election_year_to_date model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    APIResponse result = await _api.UpdateData(_token, _apiSettings.BaseURL, Constant.UpdateElection, model);

                    if (result.IsSuccess)
                    {
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result.Message);
                    }
                    else
                    {
                        TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, result.Message);
                        return View(model);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid data.");
                    return View(model);
                }
            }
            catch
            {
                return View();
            }

        }
    }
}

