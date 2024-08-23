using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Security
{
    public partial class SignIn : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    //if (!IsPostBack)
        //    //{
        //    //    InitializeForm();
        //    //}
        //}

        //private void InitializeForm()
        //{
        //    username2.Text = string.Empty;
        //    password2.Text = string.Empty;
        //}

        protected void UsernameOrEmailValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string input = args.Value.Trim();

            // Regular expression for a valid email address
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Regular expression for a valid username (at least 10 alphanumeric characters)
            string usernamePattern = @"^[a-zA-Z0-9]{10,}$";

            // Check if the input matches either the email pattern or the username pattern
            if (System.Text.RegularExpressions.Regex.IsMatch(input, emailPattern) ||
                System.Text.RegularExpressions.Regex.IsMatch(input, usernamePattern))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void SignInButton_SI_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}