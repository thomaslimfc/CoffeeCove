using System;
using System.Linq;
using System.Web.UI;

using CoffeeCove.Securities;

namespace CoffeeCove.Security
{
    //@asdfghjklASDFGHJKL12345#
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void SignInButton_SI_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = Username_SI.Text;
                string password = HashPassword(Password_SI.Text);

                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    // Check for a matching customer
                    var customer = db.Customers.SingleOrDefault(c => c.Username == username && c.HashedPassword == password);

                    // Check for a matching admin
                    var admin = db.Admins.SingleOrDefault(a => a.Username == username && a.HashedPassword == password);

                    // If not identity of customer or admin
                    if (customer != null || admin != null)
                    {
                        // Store user role and username in session
                        Session["Username"] = username;
                        Session["UserRole"] = customer != null ? "Customer" : "Admin";

                        // need to check with customer only, becoz admin no CusID
                        if (customer != null)
                        {
                            Session["CusID"] = customer.CusID;
                            Session["ContactNo"] = customer.ContactNo;
                        }

                        // Set a flag indicating that 2FA is required
                        Session["2FARequired"] = true;

                        // Redirect to the two-factor authentication page
                        Response.Redirect("TwoFactorAuthentication.aspx");
                    }
                    else
                    {
                        // cannot access to 2-factor
                    }
                }
            }
        }

        private string HashPassword(string password)
        {
            // Placeholder: implement a real password hashing mechanism
            // This is just an example using SHA256. You should ideally use a library like BCrypt.Net for stronger security.
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
