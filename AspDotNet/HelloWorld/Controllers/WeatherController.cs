using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        public string Displya(int day, int month, int year)
        {
            return $"Es ist wölkig am {day}.{month}.{year}";
        }
    }
}