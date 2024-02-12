using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Forgot_Password_History
    {
        public int intGlCode { get; set; }

        public long? ref_UserID { get; set; }

        public string varResetPasswordURL { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

    }
}
