using System;

namespace CoffeeCove.Security
{
    public partial class PasswordResetSuccess : System.Web.UI.Page
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