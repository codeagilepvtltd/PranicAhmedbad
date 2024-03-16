using NUnit.Framework;
using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedBad.Test
{
    internal class City_Test
    {


        [Test]
        [Explicit]
        public void Insert_City()
        {
            IAccountRepository accountRepository = new AccountRepository();
            CityViewModel cityViewModel = new CityViewModel();
            cityViewModel.city_Master = new PranicAhmedbad.Lib.Models.City_Master();
            cityViewModel.city_Master.varCityCode = "CT1001";
            cityViewModel.city_Master.varCityName = "Ahmedbad";
            cityViewModel.city_Master.ref_CountryID = 1;
            cityViewModel.city_Master.ref_StateID = 1;
            cityViewModel.city_Master.chrActive = "Y";
            cityViewModel.city_Master.intGlCode = 0;
            cityViewModel.city_Master.ref_EntryBy = 1;
            DataSet country = accountRepository.InsertUpdate_city(cityViewModel);
            if (country.Tables.Count > 0 && Convert.ToString(country.Tables[0].Rows[0]["intStatus"]) == "1")
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
        [Test]
        [Explicit]
        public void Update_City()
        {
            IAccountRepository accountRepository = new AccountRepository();
            CityViewModel cityViewModel = new CityViewModel();
            cityViewModel.city_Master = new PranicAhmedbad.Lib.Models.City_Master();
            cityViewModel.city_Master.varCityCode = "CT1001";
            cityViewModel.city_Master.varCityName = "Ahmedbad West";
            cityViewModel.city_Master.ref_CountryID = 1;
            cityViewModel.city_Master.ref_StateID = 1;
            cityViewModel.city_Master.chrActive = "Y";
            cityViewModel.city_Master.intGlCode = 1;
            cityViewModel.city_Master.ref_UpdateBy = 1; ;
            DataSet country = accountRepository.InsertUpdate_city(cityViewModel);
            if (country.Tables.Count > 0 && Convert.ToString(country.Tables[0].Rows[0]["intStatus"]) == "1")
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
        [Test]
        [Explicit]
        public void Get_City()
        {
            IAccountRepository accountRepository = new AccountRepository();
            List<City_Master> city = accountRepository.GetCityList(1);
            if (city.Count > 0)
            {
                //Assert.A(accountLoginViewModel.UserName, "Mitesh");
            }
        }
    }
}
