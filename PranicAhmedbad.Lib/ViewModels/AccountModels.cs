﻿using PranicAhmedbad.Lib.Models;
using System;
using System.Collections.Generic;

namespace PranicAhmedbad.Lib.ViewModels
{
    public class AccountLoginViewModel
    {
        
        public string  UserName { get; set; }

        public string  Password { get; set; }

        public Login_Master LoginMaster { get; set; }
    }
    public class StateViewModel 
    {
    
        public State_Master state_Master{ get; set; }

        public List<State_Master>  state_Masters { get; set; }

        public List<Country_Master> county_Masters { get; set; }

    }
    public class CountryViewModel
    {
        public int intGlCode { get; set; }

        public string varCountryCode { get; set; }

        public string varCountryName { get; set; }

        public string chrActive { get; set; }

        public List<Country_Master> county_Masters { get; set; }

    }

    public class RoleMasterViewModel : CommonFieldModel
    {
        public int intGlCode { get; set; }

        public string varRoleName { get; set; }       

        public List<Role_Master>  Role_MasterList { get; set; }
    }



}
