using System;
using System.Linq;
using System.Web.UI;
using CoffeeCove.Securities;

namespace CoffeeCove.Security
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SignInButton_SI_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = Username_SI.Text;
                string password = Password_SI.Text;

                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    // Check for a matching customer
                    var customer = db.Customers.SingleOrDefault(c => c.Username == username && c.HashedPassword == password);

                    // Check for a matching admin
                    var admin = db.Admins.SingleOrDefault(a => a.Username == username && a.HashedPassword == password);

                    if (customer != null || admin != null)
                    {
                        // Store user role and username in session
                        Session["Username"] = username;
                        Session["UserRole"] = customer != null ? "Customer" : "Admin";

                        // Set a flag indicating that 2FA is required
                        Session["2FARequired"] = true;

                        // Redirect to the two-factor authentication page
                        Response.Redirect("~/Security/TwoFactorAuthentication.aspx");
                    }
                    else
                    {
                        // Handle invalid credentials
                        System.Diagnostics.Debug.WriteLine($"Failed login attempt for user: {username}");
                    }
                }
            }
        }
    }
}
