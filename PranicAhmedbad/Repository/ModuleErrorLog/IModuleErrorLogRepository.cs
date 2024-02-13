using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PranicAhmedbad.Models;

namespace PranicAhmedbad.Repository.ModuleErrorLog
{
    public interface IModuleErrorLogRepository
    {
        void Insert_Modules_Error_Log(string varPageName, string varMethodName, string varUserId, string varStackTrace, string varModuleName, string varSourceSystem, string varExtra1, string varExtra2, string varExtraa3, string varErrorMessage);

        ModuleErrorLogModel Insert_Modules_Error_Log(ModuleErrorLogModel moduleErrorLogModel);


    }
}
