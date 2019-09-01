using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginSsoAdfs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            });
            app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            {
                MetadataAddress = "https://adfs.incas.de/federationmetadata/2007-06/federationmetadata.xml",
                Wtrealm = "https://localhost/weblogon.asp"
            });

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}