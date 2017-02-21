using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index(string name)
        {
            return @"
                    <html>
                        <head>
                            <title>Hello World</title>
                        </head>
                        <body>
                            <p>Hello <span style=""color:red"">" + name + @"</span></p>
                        </body>
                    </html>";
        }
    }
}