using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.ErrorPages
{
    public partial class AdminGeneralError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["exception"] != null)
            {
                Exception ex = new Exception();
                ex = (Exception)Application["exception"];
                string FileUrl = (string)Application["Location"];

                Response.Write("Application Error");
                Response.Write("Error Detected");
                Response.Write("<p>Error found in " + FileUrl + ex.Message + "</p>");
            }
        }
    }
}