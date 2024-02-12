using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class LoginForm_Details
    {
        public int intGlCode { get; set; }

        public int? ref_PageID { get; set; }

        public DateTime? dtPageIn { get; set; }

        public DateTime? dtPageOut { get; set; }

        public long? ref_LoginDetailID { get; set; }

    }
}
