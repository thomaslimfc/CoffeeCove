using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Security
{
    public partial class PasswordReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ResetPasswordBtn_PR_Click(object sender, EventArgs e)
        {
            Response.Redirect("TwoFactorAuthentication.aspx");
        }

        protected void BackBtn_PR_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }

        //protected void ResetPassword_Click(object sender, EventArgs e)
        //{
        //    // Add your reset password logic here
        //    string newPassword = password3.Text;
        //    string confirmPassword = passwordConfirm.Text;

        //    if (newPassword == confirmPassword)
        //    {
        //        // Assume we have a method to reset the password
        //        ResetUserPassword(newPassword);
        //        // Redirect to a success page or display a success message
        //        Response.Redirect("PasswordResetSuccess.aspx");
        //    }
        //    else
        //    {
        //        // Display an error message
        //        // For example, using a Label control to show the message
        //        // errorMessage.Text = "Passwords do not match!";
        //        // errorMessage.Visible = true;
        //    }
        //}



    }
}