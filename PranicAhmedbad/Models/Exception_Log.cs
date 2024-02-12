using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Exception_Log
    {
        public long intGlCode { get; set; }

        public string varPageName { get; set; }

        public string varMethodName { get; set; }

        public string varUserId { get; set; }

        public string varExceptionMessage { get; set; }

        public string varStackTrace { get; set; }

        public string varModuleName { get; set; }

        public string varSourceSystem { get; set; }

        public string varExtra1 { get; set; }

        public string varExtra2 { get; set; }

        public string varExtra3 { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

    }

}
