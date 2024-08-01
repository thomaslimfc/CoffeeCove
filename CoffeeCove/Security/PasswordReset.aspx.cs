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
        protected void ResetPasswordBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignIn.aspx");
        }

        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            // Add your reset password logic here
            string newPassword = password3.Text;
            string confirmPassword = passwordConfirm.Text;

            if (newPassword == confirmPassword)
            {
                // Assume we have a method to reset the password
                ResetUserPassword(newPassword);
                // Redirect to a success page or display a success message
                Response.Redirect("PasswordResetSuccess.aspx");
            }
            else
            {
                // Display an error message
                // For example, using a Label control to show the message
                // errorMessage.Text = "Passwords do not match!";
                // errorMessage.Visible = true;
            }
        }

        private void ResetUserPassword(string newPassword)
        {
            // Implement the logic to reset the user's password here
            // This might involve updating the password in the database
        }

        protected void Password3_TextChanged(object sender, EventArgs e)
        {
            // Handle Password TextChanged event
        }

        protected void PasswordConfirm_TextChanged(object sender, EventArgs e)
        {
            // Handle Password Re-enter TextChanged event
        }
    }
}