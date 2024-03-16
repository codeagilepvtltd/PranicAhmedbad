using PranicAhmedbad.Lib.Models;
using System;
using System.Collections.Generic;

namespace PranicAhmedbad.Lib.ViewModels
{

    public class EventMasterViewModel
    {
        public Event_Master event_Master { get; set; }

        public List<Event_Master> event_Masters { get; set; }

        public List<Entity_Type_Master> EntityTypeMaster { get; set; }
    }

    public class EventSlotDetailViewModel
    {
        public Event_SlotDetails event_slotDetail { get; set; }

        public List<Event_SlotDetails> event_slotDetails { get; set; }
    }
}


