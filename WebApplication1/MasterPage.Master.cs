using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kuroneko
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] != null)
            {
                lblLogin.Text = "Welcome: " + Session["login"].ToString();
                lblLogin.Visible = true;
                LinkButton1.Text = "Logout";
            }
            else
            {
                if (lblLogin != null)
                {
                    lblLogin.Text = "";
                    lblLogin.Visible = false;
                    LinkButton1.Text = "Login";
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (LinkButton1.Text == "Login")
            {
                Response.Redirect("~/Pages/Account/Login.aspx");

            }
            else
            {
                //user log out 
                Session.Clear();
                Response.Redirect("~/Default.aspx");

            }
        }
    }
}