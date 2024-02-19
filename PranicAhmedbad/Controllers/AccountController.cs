using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PranicAhmedbad.Common;
using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.Repository.ModuleErrorLog;
using PranicAhmedbad.Lib.ViewModels;
using System.Data;
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
            return View("Admin/State_Master");
        }



        [HttpPost]
        public async Task<ActionResult> Save_States(StateViewModel stateView)
        {
            try
            {
                //stateView.ref_EntryBy = 1;
                //stateView.ref_CountryID = 1;
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


        public IActionResult GetStateList()
        {

            List<State_Master> state_Masters = new List<State_Master>();
            DataSet dsResult = new DataSet();
            try
            {
                state_Masters = accountRepository.GetStateList();
                var resultJson = JsonConvert.SerializeObject(state_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        public IActionResult GetCountryList()
        {

            List<Country_Master> Country_Master = new List<Country_Master>();
            DataSet dsResult = new DataSet();
            try
            {
                Country_Master = accountRepository.GetCountryList();
                var resultJson = JsonConvert.SerializeObject(Country_Master);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }
        [HttpPut]
        public IActionResult UpdateStateDetails([FromForm] int key, [FromForm] string values)
        {
           // SessionManager sessionManager = new SessionManager(HttpContextAccessor);
            try
            {
                //PersonViewModel objPersonViewModel = new PersonViewModel();
                //SessionPersonMst objSessionPersonMst = new SessionPersonMst(HttpContextAccessor);
                //objPersonViewModel.lst_Model = objSessionPersonMst.lstPerson_Details;

                //if (objPersonViewModel.lst_Model.Where(x => x.intGlCode == key).Any() == true)
                //{
                //    Person_Mst data = objPersonViewModel.lst_Model.First(o => o.intGlCode == key);
                //    JsonConvert.PopulateObject(values, data);
                //}
                //else
                //{
                //    return Ok();
                //}
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("Dummy_Shipment", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
            return Ok();

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

        public IActionResult Roles()
        {
            return View("~/Views/Account/Admin/Role_Master.cshtml");
        }

        public IActionResult GetRolesList()
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            Role_Master Roles_mst = new Role_Master();
            DataSet dsResult = new DataSet();
            try
            {
                RoleMasterViewModel objViewModel = new RoleMasterViewModel();
                objViewModel = accountRepository.GetRoles();


                var resultJson = JsonConvert.SerializeObject(objViewModel.Role_MasterList);
                return Content(resultJson, "application/json");
                //return DataSourceLoader.Load(objViewModel.lst_Model, loadOptions);
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("Supplier_Mst", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        [HttpPost]
        public IActionResult Save_Roles(RoleMasterViewModel roleView)
        {
            try
            {
                roleView.ref_EntryBy = 1;
                roleView.ref_UpdateBy = 1;
                DataSet result = accountRepository.InsertUpdate_roles(roleView);
                var resultJson = JsonConvert.SerializeObject(result);
               
                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Role");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Role");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

    }
}
