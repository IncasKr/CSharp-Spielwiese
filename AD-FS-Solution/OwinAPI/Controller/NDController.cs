using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinAPI.Controller
{
    public abstract class NDController : ApiController
    {
        public abstract string GetAuthenticate(int id, int val = 0);

        [HttpGet]
        public abstract string Register(int id, int val);
    }
}
