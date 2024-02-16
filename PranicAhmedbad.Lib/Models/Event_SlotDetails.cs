using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    internal class Event_SlotDetails
    {
        public long intGlCode { get; set; }

        public long? ref_EventID { get; set; }

        public DateTime? dtDate { get; set; }

        public string varTimeFrom { get; set; }

        public string varTimeTo { get; set; }

        public long? ref_TrainerID { get; set; }

        public int? intNoofSeats { get; set; }

        public int? ref_StatusID { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
