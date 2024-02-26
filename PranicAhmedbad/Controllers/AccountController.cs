using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PranicAhmedbad.Common;
using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.Repository.General;
using PranicAhmedbad.Lib.Repository.ModuleErrorLog;
using PranicAhmedbad.Lib.ViewModels;
using System.Data;
using System.Net;

namespace PranicAhmedbad.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountRepository accountRepository;
        private readonly IMasterRepository masterRepository;

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        // GET: Controller


        public AccountController(IAccountRepository _accountRepository,IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor,IMasterRepository _masterRepository)
        {
            accountRepository = _accountRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
            masterRepository=_masterRepository;
        }

        #region Index
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

        #endregion

        #region Login
        public ActionResult Login()
        {
            return View("Admin/Login");
        }

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

        #endregion

        #region State
        public ActionResult States()
        {
            return View("Admin/State_Master");
        }

        [HttpPost]
        public ActionResult Save_States(StateViewModel stateView)
        {
            try
            {
                stateView.state_Master.ref_EntryBy = 1;
                stateView.state_Master.ref_UpdateBy = 1;
                stateView.state_Master.chrActive = stateView.state_Master.chrActive == "true" ? "Y" : "N";
                DataSet result = accountRepository.InsertUpdate_states(stateView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "State");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "State");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return Content(JsonConvert.SerializeObject(0));
            }
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
        #endregion

        #region Country

        public ActionResult Country()
        {
            return View("Admin/Country_Master");
        }

        [HttpPost]
        public ActionResult Save_Country(CountryViewModel countryView)
        {
            try
            {
                countryView.country_Master.ref_EntryBy = 1;
                countryView.country_Master.ref_UpdateBy = 1;
                countryView.country_Master.chrActive = countryView.country_Master.chrActive == "true" ? "Y" : "N";
                DataSet result = accountRepository.InsertUpdate_country(countryView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Country");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Country");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetCountryList()
        {

            CountryViewModel Country_Master = new CountryViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                Country_Master.county_Masters = accountRepository.GetCountryList();
                var resultJson = JsonConvert.SerializeObject(Country_Master.county_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }
        public IActionResult GetActiveCountryList()
        {

            CountryViewModel Country_Master = new CountryViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                Country_Master.county_Masters = accountRepository.GetCountryList().Where(m => m.chrActive == "Active").ToList();
                var resultJson = JsonConvert.SerializeObject(Country_Master.county_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion

        #region Role
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
                roleView.chrActive = roleView.chrActive == "Active" ? "Y" : "N";

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

        #endregion

        #region City

        public ActionResult City()
        {
            return View("Admin/City_Master");
        }

        [HttpPost]
        public ActionResult Save_City(CityViewModel cityView)
        {
            try
            {
                cityView.city_Master.ref_EntryBy = 1;
                cityView.city_Master.ref_UpdateBy = 1;
                cityView.city_Master.chrActive = cityView.city_Master.chrActive == "true" ? "Y" : "N";
                DataSet result = accountRepository.InsertUpdate_city(cityView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "City");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "City");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetStateListActive()
        {

            List<State_Master> state_Masters = new List<State_Master>();
            DataSet dsResult = new DataSet();
            try
            {
                state_Masters = accountRepository.GetStateList().Where(m => m.chrActive == "Active").ToList();
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

        public IActionResult GetCityList()
        {

            CityViewModel cityViewModel = new CityViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                cityViewModel = accountRepository.GetCityList();
                var resultJson = JsonConvert.SerializeObject(cityViewModel.city_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }
        #endregion

        #region Customer


        public ActionResult Customers()
        {
            return View("Admin/Customer_Master");
        }

        public IActionResult GetGenders()
        {

            List<Gender_Master>  gender_Masters = new List<Gender_Master>();
            DataSet dsResult = new DataSet();
            try
            {
                gender_Masters = accountRepository.GetGenders();
                var resultJson = JsonConvert.SerializeObject(gender_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }
        public IActionResult GetEntityTypes()
        {

            List<Entity_Type_Master > entity_Type_Masters = new List<Entity_Type_Master>();
            DataSet dsResult = new DataSet();
            try
            {
                entity_Type_Masters = masterRepository.Select_EntityTypeList(0);
                var resultJson = JsonConvert.SerializeObject(entity_Type_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }


        public IActionResult GetCustomerList()
        {

            CustomerMasterViewModel customerViewModel = new CustomerMasterViewModel();
             DataSet dsResult = new DataSet();
            try
            {
                customerViewModel = accountRepository.GetCustomerlist();
                var resultJson = JsonConvert.SerializeObject(customerViewModel.customer_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        public ActionResult Save_Customer(CustomerMasterViewModel customerView)
        {
            try
            {
                customerView.customer_Master.ref_EntryBy = 1;
                customerView.customer_Master.ref_UpdateBy = 1;
                customerView.customer_Master.chrActive = customerView.customer_Master.chrActive == "true" ? "Y" : "N";
                DataSet result = accountRepository.InsertUpdate_Customer(customerView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Customer");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Customer");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return Content(JsonConvert.SerializeObject(0));
            }
        }
        #endregion


    }
}
