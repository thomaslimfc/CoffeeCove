using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void AdminNextBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Proceed with the logic when usernameEmail is provided
                Response.Redirect("AdminPasswordReset.aspx");
            }
        }

        protected void AdminBackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminSignIn.aspx");
        }
        protected void AdminUsernameEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}