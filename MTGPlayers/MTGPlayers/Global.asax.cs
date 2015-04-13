using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Security.Principal;

namespace MTGPlayers
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //identify the current user if we can
            var user = HttpContext.Current.User;

            if (user == null || !User.Identity.IsAuthenticated)
            {
                return;
            }
            else
            {
                //read the roles from the cookie and store in the principal
                var fi = (FormsIdentity)HttpContext.Current.User.Identity;
                var fat = fi.Ticket;
                var astrRoles = fat.UserData.Split('|');
                HttpContext.Current.User = new GenericPrincipal(fi, astrRoles);
            }
        }

    }
}
