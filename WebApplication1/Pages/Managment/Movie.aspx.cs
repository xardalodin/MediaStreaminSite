using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kuroneko.Pages.Managment
{
    public partial class Movie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["login"] != "mikael")
            {
                Response.Redirect("~/Pages/Account/Login.aspx");
            }


            FillPage();
        }

        private void FillPage()
        {
            ArrayList movieList = new ArrayList();

            movieList = Database.ConnectionPlayerDB.GetFiles();
            string style = "style='border: 3px solid  #E3E3E3;  border-collapse: collapse;'";

            StringBuilder sb = new StringBuilder();
            sb.Append("<h3>list of Files</h3>");
            sb.Append(string.Format(@"<table {0}>",style));
           

            foreach(Pages.Database.Entities.ListOfFiles movie in movieList)
            {
                sb.Append(String.Format(@"<tr>
                                          <td width='10px'{0}>{1}</td>
                                          <td width='100px'{0}>{2}</td>
                                          <td width='10px'{0}>{3}</td>  
                                        </tr> ", style,movie.id,movie.filename,movie.media_type));
            }


            sb.Append("</table>");
            labelOutput.Text = sb.ToString();
        }

    }
}