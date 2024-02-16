using System;

namespace PranicAhmedbad.Lib.Models
{
    public class ModuleErrorLogModel
    {
        public int Id { get; set; }
        public string varPageName { get; set; }
        public string varMethodName { get; set; }
        public string varUserId { get; set; }
        public string varStackTrace { get; set; }
        public string varModuleName { get; set; }
        public string varSourceSystem { get; set; }
        public string varExtra1 { get; set; }
        public string varExtra2 { get; set; }
        public string varExtraa3 { get; set; }
        public DateTime dtUpdatedDate { get; set; }
        public string varErrorMessage { get; set; }
        public int fk_Status { get; set; }
        public string varMessage { get; set; }
    }
}
