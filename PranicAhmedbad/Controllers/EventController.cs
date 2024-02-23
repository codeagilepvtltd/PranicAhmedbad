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

        public ActionResult Save_Events(EventMasterViewModel eventMasterView)
        {
            try
            {
                eventMasterView.event_Master.ref_EntryBy = 1;
                eventMasterView.event_Master.ref_UpdateBy = 1;
                eventMasterView.event_Master.chrActive = eventMasterView.event_Master.chrActive == "true" ? "Y" : "N";
                DataSet result = eventRepository.InsertUpdate_EventMaster(eventMasterView);
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

        #endregion
    }
}
