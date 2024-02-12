using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Email_Master
    {
        public int intGlCode { get; set; }

        public string varEmail { get; set; }

        public string varDescription { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
