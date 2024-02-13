using PranicAhmedbad.Common;
using PranicAhmedbad.Models;
using System.Data;
using System.Text;

namespace PranicAhmedbad.DA
{
    public class ModuleErrorLogDA
    {
        DataSet resultSet = new DataSet();

        private StringBuilder sqlQuery;
        public void Insert_Modules_Error_Log(string varPageName, string varMethodName, string varUserId, string varStackTrace, string varModuleName, string varSourceSystem, string varExtra1, string varExtra2, string varExtraa3, string varErrorMessage)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "varPageName", "varMethodName", "varUserId", "varStackTrace", "varModuleName", "varSourceSystem", "varExtra1", "varExtra2", "varExtraa3", "varErrorMessage" };
            object[] objParamValue = { varPageName, varMethodName, varUserId, varStackTrace, varModuleName, varSourceSystem, varExtra1, varExtra2, varExtraa3, varErrorMessage };

            try
            {
                SQLHelper.GetData(StoredProcedures.USP_Insert_Modules_Error_Log, objParamName, objParamValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet Insert_Modules_Error_Log(ModuleErrorLogModel moduleErrorLogModel)
        {
            moduleErrorLogModel.varPageName = moduleErrorLogModel.varPageName == null ? "" : moduleErrorLogModel.varPageName;
            moduleErrorLogModel.varMethodName = moduleErrorLogModel.varMethodName == null ? "" : moduleErrorLogModel.varMethodName;
            moduleErrorLogModel.varUserId = moduleErrorLogModel.varUserId == null ? "" : moduleErrorLogModel.varUserId;
            moduleErrorLogModel.varStackTrace = moduleErrorLogModel.varStackTrace == null ? "" : moduleErrorLogModel.varStackTrace;
            moduleErrorLogModel.varModuleName = moduleErrorLogModel.varModuleName == null ? "" : moduleErrorLogModel.varModuleName;
            moduleErrorLogModel.varSourceSystem = moduleErrorLogModel.varSourceSystem == null ? "" : moduleErrorLogModel.varSourceSystem;
            moduleErrorLogModel.varExtra1 = moduleErrorLogModel.varExtra1 == null ? "" : moduleErrorLogModel.varExtra1;
            moduleErrorLogModel.varExtra2 = moduleErrorLogModel.varExtra2 == null ? "" : moduleErrorLogModel.varExtra2;
            moduleErrorLogModel.varExtraa3 = moduleErrorLogModel.varExtraa3 == null ? "" : moduleErrorLogModel.varExtraa3;
            moduleErrorLogModel.varErrorMessage = moduleErrorLogModel.varErrorMessage == null ? "" : moduleErrorLogModel.varErrorMessage;

            sqlQuery = new StringBuilder();
            object[] objParamName = { "varPageName", "varMethodName", "varUserId", "varStackTrace", "varModuleName", "varSourceSystem", "varExtra1", "varExtra2", "varExtraa3", "varErrorMessage" };
            object[] objParamValue = { moduleErrorLogModel.varPageName, moduleErrorLogModel.varMethodName, moduleErrorLogModel.varUserId, moduleErrorLogModel.varStackTrace, moduleErrorLogModel.varModuleName, moduleErrorLogModel.varSourceSystem, moduleErrorLogModel.varExtra1, moduleErrorLogModel.varExtra2, moduleErrorLogModel.varExtraa3, moduleErrorLogModel.varErrorMessage };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Insert_Modules_Error_Log, objParamName, objParamValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultSet;

        }

    }
}
