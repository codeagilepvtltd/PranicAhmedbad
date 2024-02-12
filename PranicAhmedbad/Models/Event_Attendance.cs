﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Models
{
    internal class Event_Attendance
    {
        public long intGlCode { get; set; }

        public long? ref_EventID { get; set; }

        public long? ref_EventSlotID { get; set; }

        public long? ref_UserID { get; set; }

        public string chrAttended { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
