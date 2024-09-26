using System;

namespace CoffeeCove.Security
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.AddHeader("Refresh", "3; URL=SignIn.aspx");
            }
        }
    }
}