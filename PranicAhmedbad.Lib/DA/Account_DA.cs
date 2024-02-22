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

        public DataSet GetCityList(int CityId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { CityId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_CityList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet InsertUpdate_states(StateViewModel  stateViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varStateName" , "ref_EntryBy", "ref_CountryId", "chrActive" };
            object[] objParamValue = { stateViewModel.state_Master.intGlCode, stateViewModel.state_Master.varStateName, stateViewModel.state_Master.ref_EntryBy, stateViewModel.state_Master.ref_CountryId, stateViewModel.state_Master.chrActive };

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_State_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }
        public DataSet InsertUpdate_City(CityViewModel cityViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varCityCode", "varCityName", "ref_CountryID", "ref_StateID", "chrActive", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { cityViewModel.city_Master.intGlCode, cityViewModel.city_Master.varCityCode, cityViewModel.city_Master.varCityName, cityViewModel.city_Master.ref_CountryID, cityViewModel.city_Master.ref_StateID, cityViewModel.city_Master.chrActive, cityViewModel.city_Master.ref_EntryBy, cityViewModel.city_Master.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_City_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet InsertUpdate_Role(RoleMasterViewModel roleViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varRoleName", "chrActive", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { roleViewModel.intGlCode, roleViewModel.varRoleName,roleViewModel.chrActive, roleViewModel.ref_EntryBy, roleViewModel.ref_UpdateBy };

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Role_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }
        public DataSet InsertUpdate_Country(CountryViewModel countryViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varCountryCode","varCountryName", "chrActive", "ref_EntryBy", "ref_UpdatedBy" };
            object[] objParamValue = { countryViewModel.country_Master.intGlCode, countryViewModel.country_Master.varCountryCode, countryViewModel.country_Master.varCountryName, countryViewModel.country_Master.chrActive, countryViewModel.country_Master.ref_EntryBy, countryViewModel.country_Master.ref_UpdateBy };

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Country_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet InsertUpdate_Customer(CustomerMasterViewModel customerMasterViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "ref_LoginID", "varFirstName", "varMiddleName", "varLastName", "varAddressLine1", "varAddressLine2", "ref_AddressId", "ref_EntityTypeID", "ref_CityId", "varPostalCode", "varGender", "varContactNo", "varEmailAddress", "varGMapLocation", "dtDOB", "chrActive", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { customerMasterViewModel.customer_Master.intGlCode, customerMasterViewModel.customer_Master.ref_LoginID, customerMasterViewModel.customer_Master.varFirstName, customerMasterViewModel.customer_Master.varMiddleName, customerMasterViewModel.customer_Master.varLasteName
                    , customerMasterViewModel.customer_Master.address_Master.varAddressLine1,customerMasterViewModel.customer_Master.address_Master.varAddressLine2,customerMasterViewModel.customer_Master.ref_AddressId,customerMasterViewModel.customer_Master.ref_EntityTypeID,customerMasterViewModel.customer_Master.ref_CityId,
                    customerMasterViewModel.customer_Master.address_Master.varPostalCode,customerMasterViewModel.customer_Master.chrGender,customerMasterViewModel.customer_Master.address_Master.varContactNo,customerMasterViewModel.customer_Master.address_Master.varEmailAddress,customerMasterViewModel.customer_Master.address_Master.varGMapLocation,
                    customerMasterViewModel.customer_Master.dtDOB,customerMasterViewModel.customer_Master.chrActive,customerMasterViewModel.customer_Master.ref_EntryBy,customerMasterViewModel.customer_Master.dtEntryDate};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Customer_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet GetCustomerList(int AddId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { AddId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_CustomerList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
    }
}
