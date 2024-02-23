using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace PranicAhmedbad.Lib.Repository.Account
{
    public interface IAccountRepository
    {
        #region Login
        AccountLoginViewModel CheckAuthentication(string UserName, string Password);

        #endregion

        #region Roles
        RoleMasterViewModel GetRoles();

        DataSet InsertUpdate_roles(RoleMasterViewModel roleViewModel);

        #endregion

        #region State
        List<State_Master> GetStateList(int StateId = 0);
        DataSet InsertUpdate_states(StateViewModel stateViewModel);

        #endregion

        #region Country
        DataSet InsertUpdate_country(CountryViewModel countryViewModel);
        List<Country_Master> GetCountryList(int CountryId = 0);

        #endregion

        #region City
        DataSet InsertUpdate_city(CityViewModel cityViewModel);
        CityViewModel GetCityList(int CityId = 0);

        #endregion

        #region Customer
        DataSet InsertUpdate_Customer(CustomerMasterViewModel customerMasterViewModel);
        CustomerMasterViewModel GetCustomerlist(int CityId = 0);

        List<Gender_Master> GetGenders(int StateId = 0);

        #endregion
    }
}
