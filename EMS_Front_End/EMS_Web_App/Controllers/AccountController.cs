using System;
using System.Reflection;
using System.Security.Principal;
using System.Text.Json;
using EMS_Common.Handler;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using EMS_Web_App.StaticFunc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMS_Web_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITransactionHandler _api;
        private readonly APISettings _apiSettings;
        private readonly IAuthHandler _apiAuth;
        private readonly IAPIHandler<election_user> _apiUser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(ITransactionHandler api, IOptions<APISettings> apiSettings,
                                                        IAuthHandler apiAuth,
                                                        IAPIHandler<election_user> apiUser,
                                                        IHttpContextAccessor httpContextAccessor)
        {
            _api = api;
            _apiSettings = apiSettings.Value;
            _apiAuth = apiAuth;
            _apiUser = apiUser;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            if (StaticValue.CheckUserActive(_httpContextAccessor))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                await LoadState();
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Login(Login_Register_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiAuth.Login(_apiSettings.BaseURL, Constant.AuthLogin, model.LoginViewModel!);

                if(model.LoginViewModel!.UserName == null || model.LoginViewModel.Password == null || response == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
                else
                {

                    HttpContext.Session.SetString(Constant.__USERNAME__, response.UName);
                    HttpContext.Session.SetString(Constant.__TOKEN__, response.Token);
                    HttpContext.Session.SetString(Constant.__REFRESH_TOKEN__, response.RefreshToken);

                    return RedirectToAction("Index", "Home");
                }            
            }
            else
            {
                return View("Index");
            }            
        }
        [HttpPost]
        public async Task<ActionResult> Register(Login_Register_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiUser.AddDataWithOutToken(_apiSettings.BaseURL, Constant.AddUser, model.RegisterViewModel!);

                if (model.RegisterViewModel!.election_voter_id == null || model.RegisterViewModel.election_voter_password == null || response == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
                else
                {
                    await LoadState();
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }
        public async Task<IActionResult> LogOut()
        {
            var response = await _apiAuth.LogOut(StaticValue.GetToken(_httpContextAccessor), _apiSettings.BaseURL, Constant.AuthLogOut);
            HttpContext.Session.Clear();

            return LocalRedirect("/");
        }
        private async Task<bool> LoadState()
        {
            var states = await _api.GetActiveList(_apiSettings.BaseURL, Constant.GetDDState);

            if (states != null)
            {
                ViewBag.StateDrpList = new SelectList(states, "election_state_id", "election_state_name");
                return true;
            }
            else
                return false;
        }
        public async Task<string> GetActiveCities(int id)
        {
            var result = await _api.GetActiveCity(_apiSettings.BaseURL, Constant.GetDDCity, id);
            return JsonConvert.SerializeObject(result);
        }
    }
}

