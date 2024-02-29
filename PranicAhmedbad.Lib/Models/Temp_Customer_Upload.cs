using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    public class Temp_Customer_Upload
    {
        public string varFirstName { get; set; }
        public string varMiddleName { get; set; }
        public string varLastName { get; set; }
        public string varMobileNo { get; set; }
        public string varEmailID { get; set; }
        public string varCityName { get; set; }
        public int fk_CustomerGlCode { get; set; }
        public int fk_AddressGlCode { get; set; }
        public int fk_CityGlCode { get; set; }
        public string varAddressLine1 { get; set; }
        public string varPostalCode { get; set; }
        public string varGender { get; set; }
        public string chrValid { get; set; }
        public string varRemarks { get; set; }
        public DateTime ?dtDOB { get; set; }
        public DateTime dtEntryDate { get; set; }

        public long ref_EntryBy { get; set; }

    }
}
