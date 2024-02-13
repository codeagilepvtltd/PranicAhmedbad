using Microsoft.AspNetCore.Http;
using PranicAhmedbad.Common;
using PranicAhmedbad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PranicAhmedbad.Common
{
    public class SessionPersonMst
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public SessionPersonMst(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Login_Master> lstPerson_Details
        {
            set
            {
                _session.SetData("Login_Master", value);
            }
            get
            {
                return _session.GetData<List<Login_Master>>("Login_Master");
            }
        }
    }
}
