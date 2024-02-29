using PranicAhmedbad.Common.SessionManagement;
using PranicAhmedbad.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PranicAhmedbad.Common
{
    public class SessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public long IntGlCode
        {
            set
            {
                _session.SetData("intGlCode", Convert.ToInt64(value));
            }
            get
            {
                return _session.GetData<Int64>("intGlCode");
            }
        }

        public int fk_SubModuleGlCode
        {
            set
            {
                _session.SetData("fk_SubModuleGlCode", Convert.ToInt32(value));
            }
            get
            {
                return _session.GetData<Int32>("fk_SubModuleGlCode");
            }
        }

        public string UserName
        {
            set
            {
                _session.SetData("UserName", Convert.ToString(value));
            }
            get
            {
                return _session.GetData<string>("UserName");
            }
        }

        public string Email
        {
            set
            {
                _session.SetData("Email", Convert.ToString(value));
            }
            get
            {
                return _session.GetData<string>("Email");
            }
        }

        // Added By Romi

        public string varFirstName
        {
            set
            {
                _session.SetData("varFirstName", Convert.ToString(value));
            }
            get
            {
                return _session.GetData<string>("varFirstName");
            }
        }

        public string varLoginType
        {
            set
            {
                _session.SetData("varLoginType", Convert.ToString(value));
            }
            get
            {
                return _session.GetData<string>("varLoginType");
            }
        }

        public string version
        {
            set
            {
                _session.SetData("varVersion", Convert.ToString(value));
            }
            get
            {
                return _session.GetData<string>("varVersion");
            }
        }

        public int FK_LGLGlCode
        {
            set
            {
                _session.SetData("fk_LDLGlCode", Convert.ToInt32(value));
            }
            get
            {
                return _session.GetData<Int32>("fk_LDLGlCode");
            }
        }
        // End Changes


    }

   
}
