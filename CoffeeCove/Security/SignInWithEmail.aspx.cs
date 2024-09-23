using System;
using System.Linq;
using System.Web.UI;
using CoffeeCove.Securities;

namespace CoffeeCove.Security
{
    public partial class SignInWithEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void SignInButton_SI2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = EmailAdd_SI2.Text;
                string password = HashPassword(Password_SI2.Text);

                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    var customer = db.Customers.SingleOrDefault(c => c.EmailAddress == email && c.HashedPassword == password);

                    if (customer != null)
                    {
                        // Store user details in session
                        Session["Username"] = customer.Username; // Assuming you have a Username field
                        Session["UserRole"] = "Customer";
                        Session["CusID"] = customer.CusID;
                        Session["ContactNo"] = customer.ContactNo;
                    }
                    else
                    {
                         //Optionally, display an error message
                        //InvalidCredentialsLabel.Text = "Invalid email or password.";
                        //InvalidCredentialsLabel.Visible = true;

                        // Log this attempt for security reasons
                        //System.Diagnostics.Debug.WriteLine("Failed login attempt for user: " + email);
                    }
                    // Set a flag indicating that 2FA is required
                    Session["2FARequired"] = true;

                    // Redirect to the two-factor authentication page
                    Response.Redirect("TwoFactorAuthentication2.aspx");
                }
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
