using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    public class Address_Master
    {
        public int intGlCode { get; set; }

        public string  varAddressLine1 { get; set; }

        public string  varAddressLine2 { get; set; }

        public string  varEmailAddress { get; set; }

        public string  varContactNo { get; set; }

        public string  varGMapLocation { get; set; }
        public long? ref_CityId { get; set; }
        public string CityName { get; set; }
        public long? ref_StateId { get; set; }
        public string StateName { get; set; }
        public long? ref_CountryId { get; set; }
        public string CountryName { get; set; }

        public long? varPostalCode { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

      

    }


}
