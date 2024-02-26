using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Lib.DA
{
    internal class General_DA
    {
        private StringBuilder sqlQuery;
        private DataSet resultSet;
        public DataSet Select_EntityTypeList(int intGlCode)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { intGlCode };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_EntityTypeList, objParamName, objParamValue);
            }
            catch 
            {
                throw;
            }
            return resultSet;

        }
      
    }
}
