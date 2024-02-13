using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    public class Login_Master
    {
        public long intGlCode { get; set; }

        public string ?varUserName { get; set; }

        public string ?varMobileNo { get; set; }

        public string ?varEmailID { get; set; }

        public string ?varPassword { get; set; }

        public char ?chrLock { get; set; }

        public char ?chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }

}
