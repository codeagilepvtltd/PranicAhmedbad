using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.DA
{
    public class EventMaster_DA
    {
        private StringBuilder sqlQuery;
        private DataSet resultSet;

        public DataSet InsertUpdate_EventMaster(EventMasterViewModel eventMasterViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "ref_EntityID", "varEventName", "varEventDescription", "varEventContent", "varContactPerson", "varContactMobileNo", "dtEventPublishDate",
                "dtStartDate", "dtEndDate", "intNoofDays", "varPaymentType", "intFollowUp", "varRegistrationLink", "ref_StatusID", "varAddressLine1", "varAddressLine2 ", "ref_AddressID",
                "ref_CityId",  "varPostalCode",  "varContactNo", "varEmailAddress", "varGMapLocation", "chrActive", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { eventMasterViewModel.event_Master.intGlCode, eventMasterViewModel.event_Master.ref_EntityID, eventMasterViewModel.event_Master.varEventName,
                eventMasterViewModel.event_Master.varEventDescription,
                eventMasterViewModel.event_Master.varEventContent, eventMasterViewModel.event_Master.varContactPerson,eventMasterViewModel.event_Master.varContactMobileNo,
                eventMasterViewModel.event_Master.dtEventPublishDate,eventMasterViewModel.event_Master.dtStartDate,eventMasterViewModel.event_Master.dtEndDate,
                eventMasterViewModel.event_Master.intNoofDays,eventMasterViewModel.event_Master.varPaymentType,eventMasterViewModel.event_Master.intFollowUp,eventMasterViewModel.event_Master.varRegistrationLink,
                eventMasterViewModel.event_Master.ref_StatusID, eventMasterViewModel.event_Master.address_Master.varAddressLine1,eventMasterViewModel.event_Master.address_Master.varAddressLine2,
                eventMasterViewModel.event_Master.ref_AddressID,eventMasterViewModel.event_Master.ref_CityId,eventMasterViewModel.event_Master.address_Master.varPostalCode
                ,eventMasterViewModel.event_Master.varContactMobileNo,eventMasterViewModel.event_Master.address_Master.varEmailAddress,eventMasterViewModel.event_Master.address_Master.varGMapLocation,
                eventMasterViewModel.event_Master.chrActive,eventMasterViewModel.event_Master.ref_EntryBy,eventMasterViewModel.event_Master.ref_UpdateBy };


            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Event_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet GetEventList(long intGlCode = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { intGlCode };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_EventList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet GetEventTypeList(string varEventType = "")
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "varEventType" };
            object[] objParamValue = { varEventType };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_EntityTypeList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet GeTraineeist(int intGlCode = 0, string varRoleName = "")
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varRoleName" };
            object[] objParamValue = { intGlCode , varRoleName };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_UserRoleMappingList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet InsertUpdate_EventSlot(EventSlotDetailViewModel eventslotMasterView)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "ref_EventId", "dtDate", "varTimeFrom", "varTimeTo", "ref_TrainerID", "intNoofSeats", "ref_StatusID",
                                     "chrActive", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { eventslotMasterView.event_slotDetail.intGlCode, eventslotMasterView.event_slotDetail.ref_EventID, eventslotMasterView.event_slotDetail.dtDate,
                               eventslotMasterView.event_slotDetail.varTimeFrom,eventslotMasterView.event_slotDetail.varTimeTo,eventslotMasterView.event_slotDetail.ref_TrainerID,
                               eventslotMasterView.event_slotDetail.intNoofSeats,eventslotMasterView.event_slotDetail.ref_StatusID,eventslotMasterView.event_slotDetail.chrActive,
                               eventslotMasterView.event_slotDetail.ref_EntryBy,eventslotMasterView.event_slotDetail.ref_UpdateBy};


            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_EventSlotDetails, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

    }
}
