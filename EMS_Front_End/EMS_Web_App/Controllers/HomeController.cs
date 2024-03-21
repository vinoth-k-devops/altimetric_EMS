using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EMS_Web_App.Models;
using EMS_Common.Handler;
using EMS_Common.Variables;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using EMS_Web_App.StaticFunc;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EMS_Web_App.Controllers;

[Authorize("Permission")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITransactionHandler _api;
    private readonly APISettings _apiSettings;
    private readonly string _token;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, ITransactionHandler api,
                                                IOptions<APISettings> apiSettings,
                                                IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _api = api;
        _apiSettings = apiSettings.Value;
        _httpContextAccessor = httpContextAccessor;
        _token = _httpContextAccessor.HttpContext!.Session.GetString(Constant.__TOKEN__)!;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string Type = StaticValue.UserType(_httpContextAccessor, Constant.RoleClaimType);
        if (Type == Constant.AType)
        {
            var data = await _api.GetDashboard(_token, _apiSettings.BaseURL, Constant.GetDashboard);
            data.activate = new ApproveUser() { election_user_id = 0 };
            return View(data);
        }
        else if (Type == Constant.UType)
            return RedirectToAction("UserDashboard");       
        else
            return RedirectToAction("Index", "Account");
    }
    [HttpGet]
    public async Task<IActionResult> UserDashboard()
    {
       APIResponse response = await _api.GetUserDashboard(_token, _apiSettings.BaseURL, Constant.GetUserDashboard);
       return View(response);
    }
    [HttpGet]
    public async Task<IActionResult> EResult()
    {
        await LoadData();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Approve([FromForm] Dashboard model)
    {
        try
        {
            string result = "";
            if(model.activate?.election_user_id > 0)
            {
                result = await _api.UpdateUserToActive(_token, _apiSettings.BaseURL, Constant.ActiveUser, model.activate?.election_user_id.ToString()!);

                if (result != "")
                    TempData["alertMsg"] = Constant.ShowAlert(Alerts.Success, result);
                else
                    TempData["alertMsg"] = Constant.ShowAlert(Alerts.Danger, "Unknown error");
            }
            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Vote([FromForm] string rdVote)
    {
        try
        {
            if (ModelState.IsValid)
            {
                ToVote model = new ToVote() { contest_id = Convert.ToInt32(rdVote), voter_user_name = StaticValue.UserType(_httpContextAccessor, Constant.NameClaimType) };
                APIResponse result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.RIGHT_TO_VOTE, model);

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
                return RedirectToAction("UserDashboard");
            }
        }
        catch
        {
            return View();
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EResult([FromForm] string election_id)
    {
        try
        {
            await LoadData();

            if (ModelState.IsValid)
            {
                ElectionInput model = new ElectionInput() { election_id = Convert.ToInt32(election_id) };
                APIResponse result = await _api.AddData(_token, _apiSettings.BaseURL, Constant.GetElectionResult, model);

                List<ElectionResult> data = GenericFunc.ExtractJsonData<List<ElectionResult>>(result.Data!.ToString());
                if (result.IsSuccess)
                { 
                    return View(data);
                }
                else
                    return View(data);
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
    private async Task<bool> LoadData()
    {
        APIResponse elections = await _api.GetDataList(_token, _apiSettings.BaseURL, Constant.GetCompletedorInProgressElectionList);
        List<election_year_to_date> result = GenericFunc.ExtractJsonData<List<election_year_to_date>>(elections.Data!.ToString());

        if (result != null)
            ViewBag.ElectionDrpList = new SelectList(result, "election_id", "election_name");

            return true;
  
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous ,ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

