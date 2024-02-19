using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace PranicAhmedbad.Lib.Repository.Account
{
    public interface IAccountRepository
    {
        AccountLoginViewModel CheckAuthentication(string UserName, string Password);

        StateViewModel GetStateList(StateViewModel stateViewModel);

        int InsertUpdate_states(StateViewModel stateViewModel);

        DataSet InsertUpdate_roles(RoleMasterViewModel roleViewModel);

        List<Country_Master> GetCountryList(int intGlCode = 0);

        RoleMasterViewModel GetRoles();

    }
}
