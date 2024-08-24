using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoffeeCove.Models;

namespace CoffeeCove.Security
{
    public partial class SignInWithEmail : System.Web.UI.Page
    {
        //string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignInButton_SI2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = EmailAdd_SI2.Text;
                string password = Password_SI2.Text;

                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    var customerQuery = db.Customers;

                    Customer cust = customerQuery.SingleOrDefault(c => c.EmailAddress == email && c.HashedPassword == password);

                    if (cust != null)
                    {
                        Response.Redirect("~/Home/Home.aspx");
                    }
                    else
                    {
                        //InvalidCredentialsLabel.Text = "Invalid username or password.";
                        //InvalidCredentialsLabel.Visible = true;
                        // Optionally, log this attempt for security reasons
                        System.Diagnostics.Debug.WriteLine("Failed login attempt for user: " + email);
                    }
                }
            }
        }
    }
}