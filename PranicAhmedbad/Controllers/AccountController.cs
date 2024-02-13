using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PranicAhmedbad.Common;
using PranicAhmedbad.Repository.Account;
using PranicAhmedbad.Repository.ModuleErrorLog;
using PranicAhmedbad.ViewModels;
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

        public AccountController(IAccountRepository _accountRepository,IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
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
                    return RedirectToAction(nameof(Index));
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
