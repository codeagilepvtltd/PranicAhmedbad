using NUnit.Framework;
using PranicAhmedbad.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.ViewModels;
using NUnit.Framework.Internal;

namespace PranicAhmedbad.Test
{
    public class AccountControllerTest
    {

        [Test]
        [Explicit]
        public void CheckAuthentication()
        {
            IAccountRepository accountRepository = new AccountRepository();
            AccountLoginViewModel accountLoginViewModel = accountRepository.CheckAuthentication("Mitesh", "9067971934");
            if (accountLoginViewModel == null || accountLoginViewModel.UserName == null)
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
    }
}
