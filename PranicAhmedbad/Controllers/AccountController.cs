using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PranicAhmedbad.Common;
using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.Repository.ModuleErrorLog;
using PranicAhmedbad.Lib.ViewModels;
using System.Net;

namespace PranicAhmedbad.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountRepository accountRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        // GET: Controller
        public ActionResult Login()
        {
            return View("Admin/Login");
        }

        #region State
        public ActionResult States()
        {
            StateViewModel stateViewModel = new StateViewModel();

            stateViewModel = accountRepository.GetStateList(stateViewModel);

            stateViewModel.county_Masters.Insert(0, new Lib.Models.Country_Master { intGlCode = 0, varCountryName = "-Select-" });
            ViewBag.countryList = stateViewModel.county_Masters;

            return View("Admin/State_Master", stateViewModel);
        }



        [HttpPost]
        public async Task<ActionResult> Save_States(StateViewModel stateView)
        {
            try
            {
                stateView.ref_EntryBy = 1;
                stateView.ref_CountryID = 1;
                int id = accountRepository.InsertUpdate_states(stateView);
                if (id == 0)
                {
                    return RedirectToAction("Account", "Admin/State_Master");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "State");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return View();
            }
        }

        #endregion
        public AccountController(IAccountRepository _accountRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            accountRepository = _accountRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        // POST: HomeController/Create
        [HttpPost]
        public async Task<ActionResult> ValidateLogin(AccountLoginViewModel accountLoginViewModel, string returnUrl)
        {
            try
            {
                string decodedUrl = "";

                if (!string.IsNullOrEmpty(returnUrl))
                    decodedUrl = WebUtility.UrlDecode(returnUrl);

                if (string.IsNullOrEmpty(accountLoginViewModel.UserName))
                {
                    TempData["ErrorMessage"] = "Please Enter User Name";
                    return RedirectToAction(nameof(Index));
                }

                if (string.IsNullOrEmpty(accountLoginViewModel.Password))
                {
                    TempData["ErrorMessage"] = "Please Enter Password";
                    return RedirectToAction(nameof(Index));
                }

                AccountLoginViewModel accountLoginView = accountRepository.CheckAuthentication(accountLoginViewModel.UserName, accountLoginViewModel.Password);
                if (accountLoginView != null && !string.IsNullOrEmpty(accountLoginView.UserName))
                {
                    return RedirectToAction("Index", "DashBoard");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid User Name/Password, Please Try Again!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return View();
            }
        }
        public IActionResult Index(string returnUrl)
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            try
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                if (string.IsNullOrEmpty(sessionManager.UserName))
                {
                    return View("~/Views/Account/Index.cshtml");
                }
                if (string.IsNullOrEmpty(returnUrl) && string.IsNullOrEmpty(Request.Headers["Referer"]))
                    returnUrl = WebUtility.UrlEncode(Request.Headers["Referer"]);

                if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
                {
                    ViewBag.ReturnURL = returnUrl;
                }

                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                return View("~/Views/Account/Index.cshtml");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                //  ModuleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
