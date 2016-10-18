using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace kuroneko.Pages.Managment
{
    public partial class Movie_add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["login"] != "mikael")
            {
                Response.Redirect("~/Pages/Account/Login.aspx");
            }
           
            if (!Page.IsPostBack)
            {

                FillList();
                string play = "~\\Movies\\" + ddlMovies.SelectedValue.ToString();
               
                videoTag.Attributes["src"] = play;
             
            }
            
        }
        private void FillList()
        {
            ddlMovies.Items.Clear();
            //C:\Users\admin\Documents\Visual Studio 2013\Projects\MainKuroneko\WebApplication1\Movies\
           
            string dirpath = MapPath("~/Movies");
            DirectoryInfo dir = new DirectoryInfo(dirpath);
            foreach (FileInfo files in dir.GetFiles())
            {
                ddlMovies.Items.Add(files.Name);            
            }
        }

        protected void ddlMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
     
            
                string play = "~\\Movies\\" + ddlMovies.SelectedValue.ToString();
         
            videoTag.Attributes["src"] = play;
          
        }
    }
}