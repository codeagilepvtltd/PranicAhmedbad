using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace PranicAhmedbad.Lib.Repository.Account
{
    public interface IAccountRepository
    {
        AccountLoginViewModel CheckAuthentication(string UserName, string Password);

        int InsertUpdate_states(StateViewModel stateViewModel);

        List<Country_Master> GetCountryList(int CountryId = 0);

        DataSet InsertUpdate_roles(RoleMasterViewModel roleViewModel);

        List<State_Master> GetStateList(int StateId = 0);

        RoleMasterViewModel GetRoles();

    }
}
