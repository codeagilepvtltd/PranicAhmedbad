using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    public class Customer_Master
    {
        public long intGlCode { get; set; }

        public long? ref_LoginID { get; set; }

        public string FullName { get; set; }
        public string UserType { get; set; }

        public string varFirstName { get; set; }

        public string varMiddleName { get; set; }

        public string varLasteName { get; set; }

        public int ref_EntityTypeID { get; set; }
        public long ref_AddressId { get; set; }

        public string chrGender { get; set; }

        [DataType(DataType.Date)]
        public DateTime dtDOB { get; set; }

        public string StatusName { get; set; }

        public string chrActive { get; set; }
        
        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

        public int ref_CountryID { get; set; }
        public int ref_StateID { get; set; }
        public long? ref_CityId { get; set; }

        public string CityName { get; set; }


        public string varAddressLine1 { get; set; }

        public string varAddressLine2 { get; set; }

        public string varEmailAddress { get; set; }

        public string varContactNo { get; set; }

        public string varGMapLocation { get; set; }
        public long? varPostalCode { get; set; }
        public Address_Master address_Master { get; set; }


    }
}
