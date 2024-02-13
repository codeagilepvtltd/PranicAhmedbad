using PranicAhmedbad.DA;
using PranicAhmedbad.ViewModels;
using System.Data;

namespace PranicAhmedbad.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        public AccountLoginViewModel CheckAuthentication(string UserName, string Password)
        {
            AccountLoginViewModel accountLoginViewModel = new AccountLoginViewModel();
            Account_DA accountDA = new Account_DA();

            try
            {
                DataSet dsResult = accountDA.Check_Login(UserName,Password);

               if(dsResult.Tables.Count>0 && dsResult.Tables[0].Rows.Count>0)
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
                return accountLoginViewModel;
            }
            return accountLoginViewModel;
        }
    }
}
