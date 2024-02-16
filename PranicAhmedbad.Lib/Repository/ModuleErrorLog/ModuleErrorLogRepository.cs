using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.DA;
using PranicAhmedbad.Models;

namespace PranicAhmedbad.Lib.Repository.ModuleErrorLog
{
    public class ModuleErrorLogRepository : IModuleErrorLogRepository
    {
        DataSet dsResult = new DataSet();

        public void Insert_Modules_Error_Log(string varPageName, string varMethodName, string varUserId, string varStackTrace, string varModuleName, string varSourceSystem, string varExtra1, string varExtra2, string varExtraa3, string varErrorMessage)
        {
            try
            {
                ModuleErrorLogDA objModuleErrorLogDA = new ModuleErrorLogDA();
                objModuleErrorLogDA.Insert_Modules_Error_Log(varPageName, varMethodName, varUserId, varStackTrace, varModuleName, varSourceSystem, varExtra1, varExtra2, varExtraa3, varErrorMessage);

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public Models.ModuleErrorLogModel Insert_Modules_Error_Log(Models.ModuleErrorLogModel moduleErrorLogModel)
        {
            ModuleErrorLogDA objModuleErrorLogDA = new ModuleErrorLogDA();
            List<Models.ModuleErrorLogModel> lstModuleErrorLogViewModel = new List<Models.ModuleErrorLogModel>();
            dsResult = objModuleErrorLogDA.Insert_Modules_Error_Log(moduleErrorLogModel);
            lstModuleErrorLogViewModel = dsResult.Tables[0].AsEnumerable().Select(m => new Models.ModuleErrorLogModel()
            {
                fk_Status = m.Field<int>("fk_Status"),
                varMessage = m.Field<string>("varMessage")
            }).ToList();
            moduleErrorLogModel.fk_Status = lstModuleErrorLogViewModel.Select(x => x.fk_Status).FirstOrDefault();
            moduleErrorLogModel.varMessage = lstModuleErrorLogViewModel.Select(x => x.varMessage).FirstOrDefault();

            return moduleErrorLogModel;
        }
    }
}
