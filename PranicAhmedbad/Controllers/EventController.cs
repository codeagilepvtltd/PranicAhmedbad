using Microsoft.AspNetCore.Components;
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
    public class EventController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;


        public EventController(IEventRepository _eventRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            eventRepository = _eventRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        #region Event
        public ActionResult Index()
        {
            return View("Admin/Event_Master");
        }

        public IActionResult Edit(int id=0)
        {
            EventMasterViewModel EventViewModel = new EventMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                EventViewModel = eventRepository.GetEventlist(id);
                return View("Admin/Event_Master", EventViewModel);
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        public IActionResult GetEventsList()
        {
            EventMasterViewModel EventViewModel = new EventMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                EventViewModel = eventRepository.GetEventlist();
                var resultJson = JsonConvert.SerializeObject(EventViewModel.event_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        public IActionResult GetTraineeList(int intGlCode = 0, string varRoleName = "")
        {
            List<User_Role_Mapping> UserRoleMapping = new List<User_Role_Mapping>();
            DataSet dsResult = new DataSet();
            try
            {
                UserRoleMapping = eventRepository.TraineeList(intGlCode,varRoleName);
                var resultJson = JsonConvert.SerializeObject(UserRoleMapping);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }
        public IActionResult GetEventTypeList(string varEventType)
        {
            EventMasterViewModel EventViewModel = new EventMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                EventViewModel.EntityTypeMaster = eventRepository.GetEventTypeList(varEventType);
                var resultJson = JsonConvert.SerializeObject(EventViewModel.EntityTypeMaster);
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

        #region Event
        public ActionResult EventSlotDetails()
        {
            return View("Admin/EventDetails");
        }

        public ActionResult Save_EventSlot(EventSlotDetailViewModel eventslotMasterView)
        {
            try
            {
                eventslotMasterView.event_slotDetail.ref_EntryBy = 1;
                eventslotMasterView.event_slotDetail.ref_UpdateBy = 1;
                eventslotMasterView.event_slotDetail.chrActive = eventslotMasterView.event_slotDetail.chrActive == "true" ? "Y" : "N";
                DataSet result = eventRepository.InsertUpdate_EventSlot(eventslotMasterView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Event");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Event");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetEventSlotList(long ref_EventId)
        {
            EventSlotDetailViewModel EventSlotViewModel = new EventSlotDetailViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                EventSlotViewModel.event_slotDetails = eventRepository.GetEventSlotList(ref_EventId);
                var resultJson = JsonConvert.SerializeObject(EventSlotViewModel.event_slotDetails);
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
    }
}
