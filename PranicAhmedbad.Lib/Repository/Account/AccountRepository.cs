using PranicAhmedbad.Lib.DA;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PranicAhmedbad.Lib.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        public AccountLoginViewModel CheckAuthentication(string UserName, string Password)
        {
            AccountLoginViewModel accountLoginViewModel = new AccountLoginViewModel();
            Account_DA accountDA = new Account_DA();

            try
            {
                DataSet dsResult = accountDA.Check_Login(UserName, Password);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    accountLoginViewModel.UserName = Convert.ToString(dsResult.Tables[0].Rows[0]["varUserName"]);
                    accountLoginViewModel.Password = Convert.ToString(dsResult.Tables[0].Rows[0]["varPassword"]);
                }
                else
                {
                    accountLoginViewModel.UserName = "";
                }
            }
            catch
            {
                throw;
            }
            return accountLoginViewModel;
        }
        public List<State_Master> GetStateList(int StateId=0)
        {
            Account_DA accountDA = new Account_DA();
            List <State_Master> states = new List<State_Master>();
            try
            {
                DataSet dsResult = accountDA.GetStateList(StateId);
                foreach (DataRow dataRow in dsResult.Tables[0].Rows)
                {
                    State_Master state_Master = new State_Master();
                    state_Master.intGlCode = Convert.ToInt32(dataRow["intGlCode"]);
                    state_Master.varStateName = Convert.ToString(dataRow["varStateName"]);
                    state_Master.chrActive = Convert.ToString(dataRow["chrActive"]);
                    state_Master.dtEntryDate = Convert.ToDateTime(dataRow["dtEntryDate"]);
                    state_Master.ref_CountryID = Convert.ToInt32(dataRow["CountryId"]);
                    state_Master.varCountryName = Convert.ToString(dataRow["CountryName"]);
                    states.Add(state_Master);
                }
                return states;

            }
            catch
            {
                throw;
            }
        }
        public int InsertUpdate_states(StateViewModel stateViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_states(stateViewModel);

            }
            catch
            {
                throw;
            }
        }

        public DataSet InsertUpdate_roles(RoleMasterViewModel roleViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_Role(roleViewModel);

            }
            catch
            {
                throw;
            }
        }
        public List<Country_Master> GetCountryList(int intGlCode=0)
        {
            Account_DA accountDA = new Account_DA();
            List<Country_Master> country_Masters = new List<Country_Master>();

            try
            {
                DataSet dsResult = accountDA.GetCountryList(intGlCode);
                foreach (DataRow dataRow in dsResult.Tables[0].Rows)
                {
                    Country_Master country_Master = new Country_Master();

                    country_Master.intGlCode = Convert.ToInt32(dataRow["intGlCode"]);
                    country_Master.varCountryName = Convert.ToString(dataRow["varCountryName"]);

                    country_Masters.Add(country_Master);
                }
            }
            catch
            {
                throw;
            }
            return country_Masters;
        }

        public RoleMasterViewModel GetRoles()
        {

            RoleMasterViewModel roleMasterViewModel = new RoleMasterViewModel();
            Account_DA accountDA = new Account_DA();

            try
            {
                DataSet dsResult = accountDA.GetRolesList();

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    roleMasterViewModel.Role_MasterList = dsResult.Tables[0].AsEnumerable().Select(row => new Role_Master()
                    {
                        intGlCode = row.Field<int>("intGlCode"),
                        varRoleName = row.Field<string>("varRoleName"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();
                    
                }

            }
            catch
            {
                return roleMasterViewModel;
            }

            return roleMasterViewModel;
        }

    }
}
