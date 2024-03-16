using PranicAhmedbad.Lib.DA;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace PranicAhmedbad.Lib.Repository.Account
{
    public class EventRepository : IEventRepository
    {
        #region Event Master
        public EventMasterViewModel GetEventlist(long intGlCode = 0)
        {
            EventMasterViewModel eventMasterViewModel = new EventMasterViewModel();
            EventMaster_DA eventMaster_DA = new EventMaster_DA();

            try
            {
                eventMasterViewModel.event_Masters = new List<Event_Master>();
                DataSet dsResult = eventMaster_DA.GetEventList(intGlCode);
                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (intGlCode > 0)
                    {
                        eventMasterViewModel.event_Master = new Event_Master();
                        DataRow dataRow = dsResult.Tables[0].Rows[0];
                        eventMasterViewModel.event_Master.intGlCode = Convert.ToInt64(dataRow["intGlCode"]);
                        eventMasterViewModel.event_Master.ref_EntityID = Convert.ToInt32(dataRow["ref_EntityID"]);
                        eventMasterViewModel.event_Master.varEventName = Convert.ToString(dataRow["varEventName"]);
                        eventMasterViewModel.event_Master.varEventDescription = Convert.ToString(dataRow["varEventDescription"]);
                        eventMasterViewModel.event_Master.varEventContent = Convert.ToString(dataRow["varEventContent"]);
                        eventMasterViewModel.event_Master.ref_AddressID = Convert.ToInt64(dataRow["ref_AddressID"]);
                        eventMasterViewModel.event_Master.varContactPerson = Convert.ToString(dataRow["varContactPerson"]);
                        eventMasterViewModel.event_Master.varContactMobileNo = Convert.ToString(dataRow["varContactMobileNo"]);
                        eventMasterViewModel.event_Master.dtEventPublishDate = Convert.ToDateTime(dataRow["dtEventPublishDate"]);
                        eventMasterViewModel.event_Master.dtStartDate = Convert.ToDateTime(dataRow["dtStartDate"]);
                        eventMasterViewModel.event_Master.dtEndDate = Convert.ToDateTime(dataRow["dtEndDate"]);
                        eventMasterViewModel.event_Master.intNoofDays = Convert.ToInt32(dataRow["intNoofDays"]);
                        eventMasterViewModel.event_Master.varPaymentType = Convert.ToString(dataRow["varPaymentType"]);
                        eventMasterViewModel.event_Master.intFollowUp = Convert.ToInt32(dataRow["intFollowUp"]);
                        eventMasterViewModel.event_Master.varRegistrationLink = Convert.ToString(dataRow["varRegistrationLink"]);
                        eventMasterViewModel.event_Master.chrActive = Convert.ToString(dataRow["chrActive"]);
                        eventMasterViewModel.event_Master.dtEntryDate = Convert.ToDateTime(dataRow["dtEntryDate"]);
                        eventMasterViewModel.event_Master.address_Master = new Address_Master();
                        eventMasterViewModel.event_Master.address_Master.varAddressLine1 = Convert.ToString(dataRow["varAddressLine1"]);
                        eventMasterViewModel.event_Master.address_Master.varAddressLine2 = Convert.ToString(dataRow["varAddressLine2"]);
                        eventMasterViewModel.event_Master.address_Master.varEmailAddress = Convert.ToString(dataRow["varEmailAddress"]);
                        eventMasterViewModel.event_Master.address_Master.varContactNo = Convert.ToString(dataRow["varContactNo"]);
                        eventMasterViewModel.event_Master.address_Master.varGMapLocation = Convert.ToString(dataRow["varGMapLocation"]);
                        eventMasterViewModel.event_Master.ref_CityId = Convert.ToInt32(dataRow["ref_CityId"]);
                        eventMasterViewModel.event_Master.address_Master.varPostalCode = Convert.ToInt64(dataRow["varPostalCode"]);
                        eventMasterViewModel.event_Master.address_Master.CityName = Convert.ToString(dataRow["CityName"]);
                        eventMasterViewModel.event_Master.ref_StateId = Convert.ToInt32(dataRow["ref_StateId"]);
                        eventMasterViewModel.event_Master.StateName = Convert.ToString(dataRow["StateName"]);
                        eventMasterViewModel.event_Master.ref_CountryId = Convert.ToInt32(dataRow["ref_CountryId"]);
                        eventMasterViewModel.event_Master.CountryName = Convert.ToString(dataRow["CountryName"]);

                    }

                    else
                    {

                        foreach (DataRow dataRow in dsResult.Tables[0].Rows)
                        {
                            Event_Master event_Master = new Event_Master();

                            event_Master.intGlCode = Convert.ToInt64(dataRow["intGlCode"]);
                            event_Master.ref_EntityID = Convert.ToInt32(dataRow["ref_EntityID"]);
                            event_Master.varEventName = Convert.ToString(dataRow["varEventName"]);
                            event_Master.varEventDescription = Convert.ToString(dataRow["varEventDescription"]);
                            event_Master.varEventContent = Convert.ToString(dataRow["varEventContent"]);
                            event_Master.ref_AddressID = Convert.ToInt64(dataRow["ref_AddressID"]);
                            event_Master.varContactPerson = Convert.ToString(dataRow["varContactPerson"]);
                            event_Master.varContactMobileNo = Convert.ToString(dataRow["varContactMobileNo"]);
                            event_Master.dtEventPublishDate = Convert.ToDateTime(dataRow["dtEventPublishDate"]);
                            event_Master.dtStartDate = Convert.ToDateTime(dataRow["dtStartDate"]);
                            event_Master.dtEndDate = Convert.ToDateTime(dataRow["dtEndDate"]);
                            event_Master.intNoofDays = Convert.ToInt32(dataRow["intNoofDays"]);
                            event_Master.varPaymentType = Convert.ToString(dataRow["varPaymentType"]);
                            event_Master.intFollowUp = Convert.ToInt32(dataRow["intFollowUp"]);
                            event_Master.varRegistrationLink = Convert.ToString(dataRow["varRegistrationLink"]);
                            event_Master.chrActive = Convert.ToString(dataRow["chrActive"]);
                            event_Master.dtEntryDate = Convert.ToDateTime(dataRow["dtEntryDate"]);
                            event_Master.address_Master = new Address_Master();
                            event_Master.varAddressLine1 = Convert.ToString(dataRow["varAddressLine1"]);
                            event_Master.varAddressLine2 = Convert.ToString(dataRow["varAddressLine2"]);
                            event_Master.varEmailAddress = Convert.ToString(dataRow["varEmailAddress"]);
                            event_Master.address_Master.varContactNo = Convert.ToString(dataRow["varContactNo"]);
                            event_Master.address_Master.varGMapLocation = Convert.ToString(dataRow["varGMapLocation"]);
                            event_Master.ref_CityId = Convert.ToInt32(dataRow["ref_CityId"]);
                            event_Master.varPostalCode = Convert.ToInt64(dataRow["varPostalCode"]);

                            event_Master.address_Master.CityName = Convert.ToString(dataRow["CityName"]);
                            event_Master.ref_StateId = Convert.ToInt32(dataRow["ref_StateId"]);
                            event_Master.StateName = Convert.ToString(dataRow["StateName"]);
                            event_Master.ref_CountryId = Convert.ToInt32(dataRow["ref_CountryId"]);
                            event_Master.CountryName = Convert.ToString(dataRow["CountryName"]);

                            eventMasterViewModel.event_Masters.Add(event_Master);
                        }
                    }

                }
                return eventMasterViewModel;
            }
            catch
            {
                throw;
            }
        }

        public DataSet InsertUpdate_EventMaster(EventMasterViewModel customerMasterViewModel)
        {
            EventMaster_DA eventMaster_DA = new EventMaster_DA();
            try
            {
                return eventMaster_DA.InsertUpdate_EventMaster(customerMasterViewModel);

            }
            catch
            {
                throw;
            }
        }

        public List<Entity_Type_Master> GetEventTypeList(string varEventType)
        {
            EventMaster_DA EventMasterDA = new EventMaster_DA();
            List<Entity_Type_Master> EntityTypeMaster = new List<Entity_Type_Master>();

            try
            {
                DataSet dsResult = EventMasterDA.GetEventTypeList(varEventType);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    EntityTypeMaster = dsResult.Tables[0].AsEnumerable().Select(row => new Entity_Type_Master()
                    {
                        intGlCode = row.Field<int>("intGlCode"),
                        varEntityType = row.Field<string>("varEntityName"),
                        varEntityName = row.Field<string>("varEntityName"),
                        chrActive = row.Field<string>("chrActive")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return EntityTypeMaster;
        }

        public List<User_Role_Mapping> TraineeList(int intGlCode = 0, string varRoleName = "")
        {
            EventMaster_DA EventMasterDA = new EventMaster_DA();
            List<User_Role_Mapping> UserRoleMapping = new List<User_Role_Mapping>();

            try
            {
                DataSet dsResult = EventMasterDA.GeTraineeist(intGlCode, varRoleName);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    UserRoleMapping = dsResult.Tables[0].AsEnumerable().Select(row => new User_Role_Mapping()
                    {
                        intGlCode = row.Field<Int64>("intGlCode"),
                        varRoleName = row.Field<string>("varRoleName"),
                        UserFullName = row.Field<string>("UserFullName"),
                        ref_RoleID = row.Field<int>("ref_RoleID"),
                        ref_UserID = row.Field<Int64>("ref_UserID"),
                        chrActive = row.Field<string>("chrActive")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return UserRoleMapping;
        }

        public DataSet InsertUpdate_EventSlot(EventSlotDetailViewModel eventslotMasterView)
        {
            EventMaster_DA eventMaster_DA = new EventMaster_DA();
            try
            {
                return eventMaster_DA.InsertUpdate_EventSlot(eventslotMasterView);

            }
            catch
            {
                throw;
            }
        }
        //public List<Event_SlotDetails> GetEventSlotList(string varEventType)
        //{
        //    EventMaster_DA EventMasterDA = new EventMaster_DA();
        //    List<Event_SlotDetails> EntitySlot = new List<Event_SlotDetails>();

        //    try
        //    {
        //        DataSet dsResult = EventMasterDA.GetEventTypeList(varEventType);

        //        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        //        {
        //            EntitySlot = dsResult.Tables[0].AsEnumerable().Select(row => new Event_SlotDetails()
        //            {
        //                intGlCode = row.Field<int>("intGlCode"),
        //                varEntityType = row.Field<string>("varEntityName"),
        //                varEntityName = row.Field<string>("varEntityName"),
        //                chrActive = row.Field<string>("chrActive")

        //            }).ToList();

        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return EntityTypeMaster;
        //}
        #endregion
    }
}
