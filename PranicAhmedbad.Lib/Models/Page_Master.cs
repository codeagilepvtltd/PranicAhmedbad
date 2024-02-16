using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    internal class Page_Master
    {
        public int intGlCode { get; set; }

        public string varPageName { get; set; }

        public string varDisplayName { get; set; }

        public int? ref_ParentPageID { get; set; }

        public string varURL { get; set; }

        public int? intMenu_Level { get; set; }

        public string chrElement_Type { get; set; }

        public int? intDisplay_Order { get; set; }

        public string varIconPath { get; set; }

        public string chrDisplay { get; set; }

        public string varTransactionCode { get; set; }

        public string varHeadType { get; set; }

        public string chrMenuType { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }

}
