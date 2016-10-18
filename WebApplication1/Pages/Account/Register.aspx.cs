using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kuroneko.Pages.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Pages.Database.Entities.User  user = new Database.Entities.User(txtUserName.Text, "user", txtPassword.Text);

            lblResult.Text = Pages.Database.ConnectionPlayerDB.Register(user); 
        }
    }
}