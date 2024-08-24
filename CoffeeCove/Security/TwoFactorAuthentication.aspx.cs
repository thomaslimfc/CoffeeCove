using System;
using System.Web.UI;

namespace CoffeeCove.Security
{
    public partial class TwoFactorAuthentication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure 2FA is required
            if (Session["2FARequired"] == null || !(bool)Session["2FARequired"])
            {
                // If no 2FA is required or the flag is not set, redirect to sign-in
                Response.Redirect("SignIn.aspx");
            }
        }

        protected void VerifyButton_TFA_Click(object sender, EventArgs e)
        {
            // Assuming 2FA is successfully verified

            // Mark 2FA as completed
            Session["2FARequired"] = false; // Or simply remove the session key with Session.Remove("2FARequired");

            // Get user role from session
            string userRole = Session["UserRole"] as string;

            if (userRole == "Customer")
            {
                // Redirect to the customer home page
                Response.Redirect("~/Home/Home.aspx");
            }
            else if (userRole == "Admin")
            {
                // Redirect to the admin dashboard
                Response.Redirect("~/AdminSite/Dashboard.aspx");
            }
            else
            {
                // Handle unexpected role or session timeout
                Response.Redirect("SignIn.aspx");
            }
        }
    }
}
