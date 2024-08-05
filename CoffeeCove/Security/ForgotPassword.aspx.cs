using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Security
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void NextBtn_FP_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Proceed with the logic when usernameEmail is provided
                Response.Redirect("PasswordReset.aspx");
            }

        }

        protected void BackBtn_FP_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignIn.aspx");

        }
    }
}