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
using System.Security.Cryptography;
using System.Data;

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
        [Test]
        public void EncryptPassword()
        {

            string cleanString = "Admin@2024";
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            string retvalue= BitConverter.ToString(hashedBytes);
        }

        [Test]
        [Explicit]
        public void Insert_Country()
        {
            IAccountRepository accountRepository = new AccountRepository();
            CountryViewModel countryViewModel = new CountryViewModel();
            countryViewModel.country_Master = new Lib.Models.Country_Master();
            countryViewModel.country_Master.varCountryCode = "C1001";
            countryViewModel.country_Master.varCountryName = "Demo1";
            countryViewModel.country_Master.chrActive = "Y";
            countryViewModel.country_Master.intGlCode = 0;
            countryViewModel.country_Master.ref_EntryBy = 1;
            DataSet country = accountRepository.InsertUpdate_country(countryViewModel);
            if (country.Tables.Count>0 && Convert.ToString(country.Tables[0].Rows[0]["intStatus"]) =="1")
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
        [Test]
        [Explicit]
        public void Update_Country()
        {
            IAccountRepository accountRepository = new AccountRepository();
            CountryViewModel countryViewModel = new CountryViewModel();
            countryViewModel.country_Master = new Lib.Models.Country_Master();

            countryViewModel.country_Master.varCountryCode = "C1002";
            countryViewModel.country_Master.varCountryName = "United State";
            countryViewModel.country_Master.chrActive = "Y";
            countryViewModel.country_Master.intGlCode = 1;
            countryViewModel.country_Master.ref_UpdateBy = 1;
            DataSet country = accountRepository.InsertUpdate_country(countryViewModel);
            if (country.Tables.Count > 0 && Convert.ToString(country.Tables[0].Rows[0]["intStatus"]) == "1")
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
        [Test]
        [Explicit]
        public void Get_Country()
        {
            IAccountRepository accountRepository = new AccountRepository();
            List<Lib.Models.Country_Master> country = accountRepository.GetCountryList(1);
            if (country.Count>0)
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
        
    }
}
