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
        #region Login
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
        #endregion

        #region State
        public List<State_Master> GetStateList(int StateId = 0)
        {
            Account_DA accountDA = new Account_DA();
            List<State_Master> states = new List<State_Master>();
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
                    state_Master.ref_CountryId = Convert.ToInt32(dataRow["CountryId"]);
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
        public DataSet InsertUpdate_states(StateViewModel stateViewModel)
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

        #endregion

        #region Country
        public List<Country_Master> GetCountryList(int intGlCode = 0)
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
                    country_Master.varCountryCode = Convert.ToString(dataRow["varCountryCode"]);
                    country_Master.chrActive = Convert.ToString(dataRow["chrActive"]);
                    country_Master.dtEntryDate = Convert.ToDateTime(dataRow["dtEntryDate"]);
                    country_Masters.Add(country_Master);
                }
            }
            catch
            {
                throw;
            }
            return country_Masters;
        }

        public DataSet InsertUpdate_country(CountryViewModel countryViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_Country(countryViewModel);

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Roles
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

        #endregion

        #region City
        public DataSet InsertUpdate_city(CityViewModel cityViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_City(cityViewModel);

            }
            catch
            {
                throw;
            }
        }

        public CityViewModel GetCityList(int CityId = 0)
        {
            CityViewModel cityViewModel = new CityViewModel();
            Account_DA accountDA = new Account_DA();

            try
            {
                cityViewModel.city_Masters = new List<City_Master>();
                DataSet dsResult = accountDA.GetCityList();
                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    cityViewModel.city_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new City_Master()
                    {
                        intGlCode = row.Field<int>("intGlCode"),
                        varCityCode = row.Field<string>("varCityCode"),
                        varCityName = row.Field<string>("varCityName"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate"),
                        StateName = row.Field<string>("StateName"),
                        CountryName = row.Field<string>("CountryName"),
                        ref_CountryID = row.Field<int>("ref_CountryID"),
                        ref_StateID = row.Field<int>("ref_StateID")

                    }).ToList();

                }
                return cityViewModel;
            }
            catch
            {
                throw;
            }


        }
        #endregion

        #region Customer
        public DataSet InsertUpdate_Customer(CustomerMasterViewModel customerMasterViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_Customer(customerMasterViewModel);

            }
            catch
            {
                throw;
            }
        }

        public CustomerMasterViewModel GetCustomerlist(int AddId = 0)
        {
            CustomerMasterViewModel customerViewModel = new CustomerMasterViewModel();
            Account_DA accountDA = new Account_DA();

            try
            {
                customerViewModel.customer_Masters = new List<Customer_Master>();
                DataSet dsResult = accountDA.GetCustomerList(AddId);
                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    customerViewModel.customer_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new Customer_Master()
                    {
                        intGlCode = row.Field<Int64>("intGlCode"),
                        ref_LoginID = row.Field<Int64>("ref_LoginID"),
                        varFirstName = row.Field<string>("varFirstName"),
                        varMiddleName = row.Field<string>("varMiddleName"),
                        varUserName = row.Field<string>("varUserName"),
                        varPassword = row.Field<string>("varPassword"),
                        FullName = row.Field<string>("FullName"),
                        UserType = row.Field<string>("UserType"),
                        varLasteName = row.Field<string>("varLasteName"),
                        ref_EntityTypeID = row.Field<int>("ref_EntityTypeID"),
                        StatusName = row.Field<string>("StatusName"),
                        ref_AddressId = row.Field<Int64>("ref_AddressId"),
                        chrGender = row.Field<string>("chrGender"),
                        dtDOB = row.Field<DateTime?>("dtDOB"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate"),
                        ref_EntryBy = row.Field<Int64>("ref_EntryBy"),
                        ref_CityId = row.Field<Int64>("ref_CityId"),
                        ref_StateID = row.Field<int>("ref_StateID"),
                        ref_CountryID = row.Field<int>("ref_CountryID"),
                        CityName = row.Field<string>("varCityName"),
                        varAddressLine1 = row.Field<string>("varAddressLine1"),
                        varContactNo = row.Field<string>("varContactNo"),
                        varEmailAddress = row.Field<string>("varEmailAddress"),
                        varGMapLocation = row.Field<string>("varGMapLocation"),
                        varPostalCode = row.Field<Int64>("varPostalCode"),


                    }).ToList();

                }
                return customerViewModel;
            }
            catch
            {
                throw;
            }
        }

        public List<Gender_Master> GetGenders(int StateId = 0)
        {
            List<Gender_Master> gender_Masters = new List<Gender_Master>();

            string[] Genders = { "Male", "Female" };
            int intGlCode = 1;
            for (int i = 0; i < Genders.Length; i++)
            {
                Gender_Master gender_Master = new Gender_Master();
                gender_Master.intGlCode = intGlCode;
                gender_Master.GenderName = Genders[i];
                gender_Masters.Add(gender_Master);
                intGlCode++;
            }
            return gender_Masters;
        }
        #endregion
    }
}
