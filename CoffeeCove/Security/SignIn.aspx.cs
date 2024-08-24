using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoffeeCove.Models;

namespace CoffeeCove.Security
{
    public partial class SignIn : System.Web.UI.Page
    {
        //string cs = Global.CS;

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
                    var customerQuery = db.Customers;
                    var adminQuery = db.Admins;

                    Customer cust = customerQuery.SingleOrDefault(c => c.Username == username && c.HashedPassword == password);

                    Admin admin = adminQuery.SingleOrDefault(a => a.Username == username && a.HashedPassword == password);

                    if (cust != null)
                    {
                        Response.Redirect("~/Home/Home.aspx");
                    }
                    else if (admin != null)
                    {
                        Response.Redirect("~/AdminSite/Dashboard.aspx");
                    }
                    else
                    {
                        //InvalidCredentialsLabel.Text = "Invalid username or password.";
                        //InvalidCredentialsLabel.Visible = true;
                        // Optionally, log this attempt for security reasons
                        System.Diagnostics.Debug.WriteLine("Failed login attempt for user: " + username);
                    }

                }
            }
        }
    }
}