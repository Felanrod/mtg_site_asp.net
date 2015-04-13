using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace MTGPlayers
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the focus to the Username textbox
            Page.Form.DefaultFocus = txtPlayerName.ClientID;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //instantiate the user class
            BusinessRules.CPlayer objUser = new BusinessRules.CPlayer();

            //call the login function from the user class
            string role = objUser.login(txtPlayerName.Text, txtPass.Text);

            //try logining in, show an error if it fails
            if (role == "")
            {
                lblError.Text = "Invalid login";
            }
            else
            {
                //login is valid
                //create an authentication cookie
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, //version
                    txtPlayerName.Text, //identity
                    DateTime.Now, //issue date
                    DateTime.Now.AddMinutes(30), //30 minute default expiry
                    false, //don't persist across browser sessions
                    role, //user role
                    FormsAuthentication.FormsCookiePath);

                //encrypt the cookie
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                //save cookie to browser
                Response.Cookies.Add(cookie);

                //send user back to the home page
                Response.Redirect("Home.aspx", true);
            }
        }
    }
}