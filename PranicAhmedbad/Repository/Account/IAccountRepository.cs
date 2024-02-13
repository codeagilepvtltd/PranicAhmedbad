using PranicAhmedbad.Models;
using PranicAhmedbad.ViewModels;

namespace PranicAhmedbad.Repository.Account
{
    public interface IAccountRepository
    {
        AccountLoginViewModel CheckAuthentication(string UserName, string Password);

    }
}
