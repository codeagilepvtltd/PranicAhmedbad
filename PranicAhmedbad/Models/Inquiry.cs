using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Inquiry
    {
        public long intGlCode { get; set; }

        public string varName { get; set; }

        public string varContactNumber { get; set; }

        public string varEmailId { get; set; }

        public string varMessage { get; set; }

        public string varInquiryType { get; set; }

        public int? ref_StatusID { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
