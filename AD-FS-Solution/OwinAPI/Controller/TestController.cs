using System;
using System.Collections.Generic;
using System.Web.Http;

namespace OwinAPI.Controller
{
    public class TestController : NDController
    {
        private static int counter = 0;
        private static IList<string> testList;

        public TestController()
        {
            if (testList == null)
            {
                testList = new List<string>();
            }           
        }

        public IEnumerable<string> GetTest()
        {
            testList.Add($"Text{++counter}");
            return testList;
        }

        public string GetFacultyOf(int id = 0)
        {
            return $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")} - Faculty of {id} = {Faculty(id)}";
        }

        [HttpPost]
        public string PostData([FromBody]string val)
        {
            int id = 0;
            Console.WriteLine($"Data: {id} and {val}");
            return "OK";
        }

        public override string Register(int id, int val)
        {
            Console.WriteLine($"Data to register: {id} and {val}");
            return "OK";
        }

        private int Faculty(int n)
        {
            return n == 0 ? 1 : n * Faculty(n - 1);
        }

        //[Authorize]
        public override string GetAuthenticate(int id, int val = 0)
        {
            var test = this;
            Console.WriteLine($"Request from {test.Request.RequestUri.Host} on port {test.Request.RequestUri.Port}");
            return id.Equals(val).ToString();
        }

    }
}
