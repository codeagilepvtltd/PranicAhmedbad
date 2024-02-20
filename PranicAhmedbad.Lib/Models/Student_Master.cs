using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    public class Customer_Master
    {
        public long intGlCode { get; set; }

        public long ref_LoginID { get; set; }

        public string varFirstName { get; set; }

        public string varMiddleName { get; set; }

        public string varLasteName { get; set; }

        public int ref_EntityTypeID { get; set; }
        public int ref_AddressId { get; set; }

        public string chrGender { get; set; }

        public DateTime dtDOB { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

        public Address_Master address_Master { get; set; }

    }
}
