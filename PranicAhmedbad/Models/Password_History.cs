using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Password_History
    {
        public long intGlCode { get; set; }

        public long? ref_UserID { get; set; }

        public string varPassword { get; set; }

        public DateTime? dt_EntryDate { get; set; }

    }
}
