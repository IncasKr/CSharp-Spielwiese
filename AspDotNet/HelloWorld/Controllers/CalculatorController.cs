using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        public string Add(int value1, int value2)
        {
            return (value1 + value2).ToString();
        }

        public string Diff(int value1, int value2)
        {
            return (value1 - value2).ToString();
        }

        public string Div(int value1, int value2)
        {
            return (value1 / value2).ToString();
        }

        public string Mult(int value1, int value2)
        {
            return (value1 * value2).ToString();
        }
    }
}