using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kuroneko.Pages.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Pages.Database.Entities.User user = Pages.Database.ConnectionPlayerDB.LoginUser(txtUserName.Text, txtPassword.Text);

            if (user != null)
            {
                if (user.user_type != "wait_time")
                {
                    Session["login"] = user.username;
                    Session["type"] = user.user_type;
                    lblResult.Text = "login successful! "+user.username;
                    
                    FormsAuthentication.SetAuthCookie(user.username, true);
                    Response.Redirect("~/Default.aspx");
                }
                else
                {

                    lblResult.Text = user.username +": please wait: " + user.wait_time.ToString() + " before trying again"; 
                }

            }
            else
            {

                lblResult.Text = "login Failed!";            
            }
               
        }
    }
}