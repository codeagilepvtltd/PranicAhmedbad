using PranicAhmedbad.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.DA
{
    internal class Account_DA
    {
        private StringBuilder ?sqlQuery;
        private DataSet ?resultSet;
        public DataSet Check_Login(string UserId, string Password)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "UserId","Password"  };
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
    }
}
