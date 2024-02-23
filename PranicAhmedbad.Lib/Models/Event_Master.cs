using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    public class Event_Master
    {
        public long intGlCode { get; set; }

        public int? ref_EventTypeID { get; set; }

        public string varEventName { get; set; }

        public string varEventDescription { get; set; }

        public string varEventContent { get; set; }

        public int? ref_CityID { get; set; }
        public long? ref_StateId { get; set; }
        public string StateName { get; set; }
        public long? ref_CountryId { get; set; }
        public string CountryName { get; set; }
        public string varContactPerson { get; set; }

        public string varContactMobileNo { get; set; }

        public DateTime? dtEventPublishDate { get; set; }

        public DateTime? dtStartDate { get; set; }

        public DateTime? dtEndDate { get; set; }

        public int? intNoofDays { get; set; }

        public string varPaymentType { get; set; }

        public int? intFollowUp { get; set; }

        public string varRegistrationLink { get; set; }

        public long? ref_AddressID { get; set; }

        public int? ref_StatusID { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

        public Address_Master address_Master { get; set; }
    }

}
