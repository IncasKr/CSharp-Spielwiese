using System;
using System.Collections.Generic;
using System.Web.Http;

namespace OwinAPI.Controller
{
    public class TestController : ApiController
    {
        public IEnumerable<string> GetTest()
        {
            return new string[] { "One", "Two", "Three" };
        }

        public string GetFacultyOf(int id)
        {
            return $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")} - Faculty of {id} = {Faculty(id)}";
        }

        private int Faculty(int n)
        {
            return n == 0 ? 1 : n * Faculty(n - 1);
        }
    }
}
