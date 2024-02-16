using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.DA
{
    internal class Account_DA
    {
        private StringBuilder sqlQuery;
        private DataSet resultSet;
        public DataSet Check_Login(string UserId, string Password)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "varUserID", "varPassword" };
            object[] objParamValue = { UserId, Password};

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Check_Login, objParamName, objParamValue);
            }
            catch 
            {
                throw;
            }
            return resultSet;

        }
        public DataSet GetRolesList()
        {
            sqlQuery = new StringBuilder();

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_RoleList);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet GetStateList(int StateId=0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { StateId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_StateList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet GetCountryList(int CountryId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { CountryId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_CountryList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public int InsertUpdate_states(StateViewModel   stateViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varStateName" , "ref_EntryBy", "ref_CountryId", "chrActive" };
            object[] objParamValue = { stateViewModel.intGlCode, stateViewModel.varStateName, stateViewModel.ref_EntryBy, stateViewModel.ref_CountryID, stateViewModel.chrActive };

            try
            {
                return SQLHelper.ExecuteQuery(StoredProcedures.USP_InsertUpdate_State_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }
    }
}
