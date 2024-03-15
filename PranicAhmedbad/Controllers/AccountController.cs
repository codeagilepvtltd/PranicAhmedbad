using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PranicAhmedbad.Common;
using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.Repository.General;
using PranicAhmedbad.Lib.Repository.ModuleErrorLog;
using PranicAhmedbad.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace PranicAhmedbad.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountRepository accountRepository;
        private readonly IMasterRepository masterRepository;

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        // GET: Controller


        public AccountController(IAccountRepository _accountRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor, IMasterRepository _masterRepository)
        {
            accountRepository = _accountRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
            masterRepository = _masterRepository;
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

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

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
                if (accountLoginView.LoginMaster != null && !string.IsNullOrEmpty(accountLoginView.LoginMaster.varUserName))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, accountLoginViewModel.UserName));
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    /*Set Session*/
                    sessionManager.IntGlCode = accountLoginView.LoginMaster.intGlCode;
                    sessionManager.UserName = accountLoginView.LoginMaster.varUserName;
                    sessionManager.Email = accountLoginView.LoginMaster.varEmailID;
                    sessionManager.varFirstName = accountLoginView.LoginMaster.varUserName;

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

        public IActionResult GetStateListActive(int CountryID = 0)
        {

            List<State_Master> state_Masters = new List<State_Master>();
            DataSet dsResult = new DataSet();
            try
            {
                if (CountryID > 0)
                {
                    state_Masters = accountRepository.GetStateList().Where(m => m.chrActive == "Active" && m.ref_CountryId == CountryID).ToList();
                }
                else
                {
                    state_Masters = accountRepository.GetStateList().Where(m => m.chrActive == "Active").ToList();
                }
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

        public IActionResult GetCityList(int StateId = 0)
        {

            List<City_Master> cityViewModel = new List<City_Master>();
            DataSet dsResult = new DataSet();
            try
            {
                if (StateId > 0)
                {
                    cityViewModel = accountRepository.GetCityList().Where(m => m.chrActive == "Active" && m.ref_StateID == StateId).ToList();
                }
                else
                {
                    cityViewModel = accountRepository.GetCityList();
                }
                var resultJson = JsonConvert.SerializeObject(cityViewModel);
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

            List<Gender_Master> gender_Masters = new List<Gender_Master>();
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

            List<Entity_Type_Master> entity_Type_Masters = new List<Entity_Type_Master>();
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

        #region Customer Upload
        public ActionResult CustomerUpload()
        {
            if (TempData["ButtonType"] == null)
            {
                TempData["ButtonType"] = "2";
            }
            ViewBag.Message = TempData["Message"];
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.ButtonType = TempData["ButtonType"];
            return View("Admin/Customer_Upload");
        }

        [HttpPost]
        public async Task<IActionResult> CheckData(IFormFile formFile, IFormCollection frm)
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            int fk_SupplierGlCode = 0;
            int fk_SupplierGSTDetailGlCode = 0;

            try
            {
                TempData["ButtonType"] = "1";

                if (formFile == null || formFile.Length == 0)
                {
                    TempData["Message"] = "File not selected.";
                    TempData["MessageType"] = "Error";
                    TempData["ButtonType"] = "2";
                    return RedirectToAction(nameof(CustomerUpload));
                }

                var fileName = Path.GetFileName(formFile.FileName);
                string sFileExtension = Path.GetExtension(fileName).ToLower();
                if (sFileExtension == ".xls" || sFileExtension == ".xlsx")
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Contents\Customers\", fileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Contents\Customers\")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Contents\Customers\"));
                    }
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    IWorkbook workbook = null;
                    DataTable dtData = new DataTable();
                    DataSet dsResult = new DataSet();

                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                            stream.Position = 0;

                            if (sFileExtension == ".xlsx")
                            {
                                workbook = new XSSFWorkbook(stream);
                            }
                            else if (sFileExtension == ".xls")
                            {
                                workbook = new HSSFWorkbook(stream);
                            }
                            else
                            {
                                throw new Exception("This format is not supported");
                            }

                            ISheet sheet = workbook.GetSheetAt(0);
                            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                            IRow headerRow = sheet.GetRow(0);
                            int cellCount = headerRow.LastCellNum;

                            if (cellCount != 10)
                            {
                                TempData["Message"] = "File is not in proper format.";
                                TempData["MessageType"] = "Error";
                                TempData["ButtonType"] = "2";
                                return RedirectToAction(nameof(CustomerUpload));
                            }

                            for (int j = 0; j < cellCount; j++)
                            {
                                ICell cell = headerRow.GetCell(j);
                                dtData.Columns.Add(cell.ToString().Trim());
                            }

                            dtData.Columns.Add("ref_EntryBy");
                            Int64 ref_EntryBy = sessionManager.IntGlCode;
                            dtData.Columns["ref_EntryBy"].DefaultValue = ref_EntryBy;

                            dtData.Columns.Add("dtEntryDate");
                            dtData.Columns["dtEntryDate"].DefaultValue = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss");

                            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                            {
                                IRow row = sheet.GetRow(i);
                                DataRow dataRow = dtData.NewRow();
                                if (row == null)
                                {
                                    break;
                                }
                                for (int j = row.FirstCellNum; j < cellCount; j++)
                                {
                                    if (row.GetCell(j) != null && !string.IsNullOrEmpty(row.GetCell(j).ToString()))
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                    }
                                    else
                                    {
                                        dataRow[j] = DBNull.Value;
                                    }
                                }

                                dtData.Rows.Add(dataRow);
                            }

                            if (dtData == null)
                            {
                                TempData["Message"] = "File is not in proper format.";
                                TempData["MessageType"] = "Error";
                                TempData["ButtonType"] = "2";
                                return RedirectToAction(nameof(CustomerUpload));
                            }

                            accountRepository.Customer_Upload(ref_EntryBy, "", "DeleteData");

                            if (dtData == null || dtData.Rows.Count == 0)
                            {
                                TempData["fk_SupplierGlCode"] = fk_SupplierGlCode;
                                TempData["fk_SupplierGSTDetailGlCode"] = fk_SupplierGSTDetailGlCode;
                                TempData["Message"] = "No Record found for Uploaded File.";
                                TempData["MessageType"] = "Information";
                                TempData["ButtonType"] = "2";
                                return RedirectToAction(nameof(CustomerUpload));
                            }
                            else
                            {
                                string strXML = string.Empty;
                                strXML = "<Customers>";
                                for (int i = 0; i < dtData.Rows.Count; i++)
                                {
                                    strXML += "<Entry>";
                                    strXML += "<FirstName>" + Convert.ToString(dtData.Rows[i]["FirstName"]) + "</FirstName>";
                                    strXML += "<MiddleName>" + Convert.ToString(dtData.Rows[i]["MiddleName"]) + "</MiddleName>";
                                    strXML += "<LastName>" + Convert.ToString(dtData.Rows[i]["LastName"]) + "</LastName>";
                                    strXML += "<MobileNo>" + Convert.ToString(dtData.Rows[i]["MobileNo"]) + "</MobileNo>";
                                    strXML += "<CityName>" + Convert.ToString(dtData.Rows[i]["CityName"]) + "</CityName>";
                                    strXML += "<EmailId>" + Convert.ToString(dtData.Rows[i]["EmailId"]) + "</EmailId>";
                                    strXML += "<Address1>" + Convert.ToString(dtData.Rows[i]["Address1"]) + "</Address1>";
                                    strXML += "<PostalCode>" + Convert.ToString(dtData.Rows[i]["PostalCode"]) + "</PostalCode>";
                                    strXML += "<Gender>" + Convert.ToString(dtData.Rows[i]["Gender"]) + "</Gender>";
                                    strXML += "<DOB>" + Convert.ToString(dtData.Rows[i]["DOB"]) + "</DOB>";
                                    strXML += "</Entry>";
                                }
                                strXML += "</Customers>";

                                dsResult = accountRepository.Customer_Upload(ref_EntryBy, strXML, "Bulk_Insert");
                                if (dsResult == null)
                                {
                                    TempData["Message"] = "File is not in proper format.";
                                    TempData["MessageType"] = "Information";
                                    TempData["ButtonType"] = "2";
                                    return RedirectToAction(nameof(CustomerUpload));
                                }
                            }

                            dsResult = accountRepository.Customer_Upload(ref_EntryBy, "", "CheckData");
                            if (dsResult != null && dsResult.Tables != null)
                            {
                                if (dsResult.Tables.Count > 1)
                                {
                                    if (dsResult.Tables[1].Rows.Count > 0)
                                    {
                                        CustomerMasterTempViewModel customerMasterTempViewModel = new CustomerMasterTempViewModel();
                                        customerMasterTempViewModel.temp_Customer_Uploads = new List<Temp_Customer_Upload>();
                                        //pOViewModel.lstPO_UploadModel = Common_Functions.ConvertDataTable<PO_DetailModel>(dsResult.Tables[1]);
                                        customerMasterTempViewModel.temp_Customer_Uploads = dsResult.Tables[1].AsEnumerable().Select(m => new Temp_Customer_Upload()
                                        {
                                            varFirstName = m.Field<string>("varFirstName"),
                                            varMiddleName = m.Field<string>("varMiddleName"),
                                            varLastName = m.Field<string>("varLastName"),
                                            varMobileNo = m.Field<string>("varMobileNo"),
                                            varCityName = m.Field<string>("varCityName"),
                                            varEmailID = m.Field<string>("varEmailID"),
                                            varAddressLine1 = m.Field<string>("varAddressLine1"),
                                            varGender = m.Field<string>("varGender"),
                                            varPostalCode = m.Field<string>("varPostalCode"),
                                            dtDOB = m.Field<DateTime>("dtDOB"),
                                            ref_EntryBy = m.Field<long>("ref_EntryBy")
                                        }).ToList();

                                        if (dsResult.Tables[0].Rows.Count > 0)
                                        {
                                            TempData["Message"] = Convert.ToString(dsResult.Tables[0].Rows[0]["varMessage"]);
                                            if (Convert.ToInt32(dsResult.Tables[0].Rows[0]["intStatus"]) == 1)
                                            {
                                                TempData["MessageType"] = "Success";
                                                TempData["ButtonType"] = "1";
                                            }
                                            else
                                            {

                                                TempData["MessageType"] = "Error";
                                                TempData["ButtonType"] = "2";
                                            }
                                        }

                                        ViewBag.Message = TempData["Message"];
                                        ViewBag.MessageType = TempData["MessageType"];
                                        ViewBag.ButtonType = TempData["ButtonType"];
                                        return View(nameof(CustomerUpload), customerMasterTempViewModel.temp_Customer_Uploads);
                                    }
                                }
                                else if (dsResult.Tables.Count > 0)
                                {
                                    TempData["Message"] = "Data checked successfully.";
                                    TempData["MessageType"] = "Success";
                                    TempData["ButtonType"] = "1";
                                }
                                else
                                {
                                    TempData["Message"] = "File is not in proper format.";
                                    TempData["MessageType"] = "Error";
                                    TempData["ButtonType"] = "2";
                                }
                            }
                            else
                            {
                                TempData["Message"] = "File is not in proper format.";
                                TempData["MessageType"] = "Error";
                                TempData["ButtonType"] = "2";
                            }
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "Invalid file type!";
                    TempData["MessageType"] = "Error";
                    TempData["ButtonType"] = "2";
                    return RedirectToAction(nameof(CustomerUpload));
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "File is not in proper format.";
                TempData["MessageType"] = "Error";
                TempData["ButtonType"] = "2";
                //  ModuleErrorLogRepository.Insert_Modules_Error_Log("POUpload", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                return RedirectToAction(nameof(CustomerUpload));
            }
            return RedirectToAction(nameof(CustomerUpload));
        }

        public async Task<IActionResult> DownloadFormat()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Contents\Download\", "Customers_Download_Format.xlsx");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), string.Concat(Path.GetFileNameWithoutExtension(path), "_", DateTime.Now.ToString("yyyyMMdd"), ".xlsx"));
        }


        [NonAction]
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        [NonAction]
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        #endregion

    }
}
