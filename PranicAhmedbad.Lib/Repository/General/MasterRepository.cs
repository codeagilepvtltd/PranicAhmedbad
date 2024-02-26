using PranicAhmedbad.Lib.DA;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PranicAhmedbad.Lib.Repository.General
{
    public class MasterRepository : IMasterRepository
    {
        #region EntityTypeList

        public List<Entity_Type_Master> Select_EntityTypeList(int intGlCode)
        {
            List<Entity_Type_Master> entity_Type_Masters = new List<Entity_Type_Master>();
            General_DA general_DA = new General_DA();

            try
            {
                DataSet dsResult = general_DA.Select_EntityTypeList(intGlCode);
                foreach (DataRow dataRow in dsResult.Tables[0].Rows)
                {
                    Entity_Type_Master entity_Type_Master = new Entity_Type_Master();
                    entity_Type_Master.intGlCode = Convert.ToInt32(dataRow["intGlCode"]);
                    entity_Type_Master.varEntityType = Convert.ToString(dataRow["varEntityType"]);
                    entity_Type_Masters.Add(entity_Type_Master);
                }
            }
            catch
            {
                throw;
            }
            return entity_Type_Masters;
        }
        #endregion

    }
}
