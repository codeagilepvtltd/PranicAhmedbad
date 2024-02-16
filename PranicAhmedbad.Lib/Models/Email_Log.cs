using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Email_Log
    {
        public long intGlCode { get; set; }

        public int? ref_EmailID { get; set; }

        public string varBody { get; set; }

        public string varTo { get; set; }

        public string varCC { get; set; }

        public string varBCC { get; set; }

        public DateTime? dtSendDate { get; set; }

        public string chrSend { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
