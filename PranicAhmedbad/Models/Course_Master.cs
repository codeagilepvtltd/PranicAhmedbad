using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Course_Master
    {
        public long intGlCode { get; set; }

        public long? ref_FoundationID { get; set; }

        public string varCourseName { get; set; }

        public string varShortDescription { get; set; }

        public string varDescription { get; set; }

        public string varContent { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
