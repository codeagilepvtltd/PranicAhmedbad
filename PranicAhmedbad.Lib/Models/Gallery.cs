using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.Models
{
    internal class Gallery
    {
        public long intGlCode { get; set; }

        public long? fk_EventID { get; set; }

        public string  varGalleryType { get; set; }

        public string  varGalleryCode { get; set; }

        public string varGalleryName { get; set; }

        public string varDescription { get; set; }

        public byte[] varFileContent { get; set; }

        public string varContentURL { get; set; }

        public string varContentType { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
