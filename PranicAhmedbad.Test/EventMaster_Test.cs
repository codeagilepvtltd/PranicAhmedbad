using NUnit.Framework;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedBad.Test
{
    internal class EventMaster_Test
    {


        [Test]
        [Explicit]
        public void Insert_City()
        {
            IEventRepository eventRepository = new EventRepository();
            EventMasterViewModel eventMasterViewModel = new EventMasterViewModel();
            eventMasterViewModel.event_Master = new PranicAhmedbad.Lib.Models.Event_Master();
            eventMasterViewModel.event_Master.address_Master = new PranicAhmedbad.Lib.Models.Address_Master();

            eventMasterViewModel.event_Master.intGlCode = 0;
            eventMasterViewModel.event_Master.ref_EntityID = 1;
            eventMasterViewModel.event_Master.varEventName = "Healing Camp 2022-02-22";
            eventMasterViewModel.event_Master.varEventDescription = "Healing Camp 2022-02-22 Description";
            eventMasterViewModel.event_Master.varEventContent = "Healing Camp 2022-02-22 Content";
            eventMasterViewModel.event_Master.varContactPerson = "Mitesh Patel";
            eventMasterViewModel.event_Master.varContactMobileNo = "9067971934";
            eventMasterViewModel.event_Master.dtEventPublishDate = DateTime.Now.AddDays(-5);
            eventMasterViewModel.event_Master.dtStartDate = DateTime.Now;
            eventMasterViewModel.event_Master.dtEndDate = DateTime.Now.AddDays(5);
            eventMasterViewModel.event_Master.intNoofDays = 5;
            eventMasterViewModel.event_Master.varPaymentType = "Cash";
            eventMasterViewModel.event_Master.intFollowUp = 2;
            eventMasterViewModel.event_Master.varRegistrationLink = "https://pranicahmedabad.com/";
            eventMasterViewModel.event_Master.ref_StatusID = 1;
            eventMasterViewModel.event_Master.ref_AddressID = 0;
            eventMasterViewModel.event_Master.ref_StatusID = 1;

            eventMasterViewModel.event_Master.address_Master.varAddressLine1 = "B 402 Ganesh greens";
            eventMasterViewModel.event_Master.address_Master.varAddressLine2 = "Opp Ganesh dwar";
            eventMasterViewModel.event_Master.address_Master.ref_CityId = 1;
            eventMasterViewModel.event_Master.address_Master.varPostalCode = 382470;
            eventMasterViewModel.event_Master.address_Master.varContactNo = "9067971934";
            eventMasterViewModel.event_Master.address_Master.varEmailAddress = "miteshmca38@gmail.com";
            eventMasterViewModel.event_Master.address_Master.varGMapLocation = "http://google.com";

            eventMasterViewModel.event_Master.chrActive = "Y";
            eventMasterViewModel.event_Master.ref_EntryBy = 1;
            eventMasterViewModel.event_Master.ref_UpdateBy = 1;

            DataSet country = eventRepository.InsertUpdate_EventMaster(eventMasterViewModel);
            if (country.Tables.Count > 0 && Convert.ToString(country.Tables[0].Rows[0]["intStatus"]) == "1")
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
        [Test]
        [Explicit]
        public void Update_Event()
        {
            IEventRepository eventRepository = new EventRepository();
            EventMasterViewModel eventMasterViewModel = new EventMasterViewModel();
            eventMasterViewModel.event_Master = new PranicAhmedbad.Lib.Models.Event_Master();
            eventMasterViewModel.event_Master.address_Master = new PranicAhmedbad.Lib.Models.Address_Master();

            eventMasterViewModel.event_Master.intGlCode = 3;
            eventMasterViewModel.event_Master.ref_EntityID = 1;
            eventMasterViewModel.event_Master.varEventName = "Healing Camp 2025-02-23";
            eventMasterViewModel.event_Master.varEventDescription = "Healing Camp 2026-02-22 Description 23";
            eventMasterViewModel.event_Master.varEventContent = "Healing Camp 2026-02-25 Content";
            eventMasterViewModel.event_Master.varContactPerson = "Mitesh Patel";
            eventMasterViewModel.event_Master.varContactMobileNo = "9067971934";
            eventMasterViewModel.event_Master.dtEventPublishDate = DateTime.Now.AddDays(-5);
            eventMasterViewModel.event_Master.dtStartDate = DateTime.Now;
            eventMasterViewModel.event_Master.dtEndDate = DateTime.Now.AddDays(5);
            eventMasterViewModel.event_Master.intNoofDays = 5;
            eventMasterViewModel.event_Master.varPaymentType = "Cash";
            eventMasterViewModel.event_Master.intFollowUp = 2;
            eventMasterViewModel.event_Master.varRegistrationLink = "https://pranicahmedabad.com/";
            eventMasterViewModel.event_Master.ref_StatusID = 1;
            eventMasterViewModel.event_Master.ref_AddressID = 6;
            eventMasterViewModel.event_Master.ref_StatusID = 1;

            eventMasterViewModel.event_Master.address_Master.varAddressLine1 = "B 452 Ganesh greens";
            eventMasterViewModel.event_Master.address_Master.varAddressLine2 = "Opp Ganesh dwar";
            eventMasterViewModel.event_Master.address_Master.ref_CityId = 1;
            eventMasterViewModel.event_Master.address_Master.varPostalCode = 382470;
            eventMasterViewModel.event_Master.address_Master.varContactNo = "9067971934";
            eventMasterViewModel.event_Master.address_Master.varEmailAddress = "miteshmca38@gmail.com";
            eventMasterViewModel.event_Master.address_Master.varGMapLocation = "http://google.com";

            eventMasterViewModel.event_Master.chrActive = "Y";
            eventMasterViewModel.event_Master.ref_EntryBy = 1;
            eventMasterViewModel.event_Master.ref_UpdateBy = 1;

            DataSet country = eventRepository.InsertUpdate_EventMaster(eventMasterViewModel);
            if (country.Tables.Count > 0 && Convert.ToString(country.Tables[0].Rows[0]["intStatus"]) == "1")
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
        [Test]
        [Explicit]
        public void Get_Events()
        {
            IEventRepository eventRepository = new EventRepository();
            EventMasterViewModel events = eventRepository.GetEventlist(1);
            if (events.event_Masters.Count > 0)
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
    }
}
