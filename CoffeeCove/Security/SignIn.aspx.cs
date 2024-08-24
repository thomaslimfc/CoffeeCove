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
            //if (!IsPostBack)
            //{
            //    InitializeForm();
            //}
        }

        protected void SignInButton_SI_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = Username_SI.Text;
                string password = Password_SI.Text;

                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    // Ensure Customers and Admins are IQueryable or IEnumerable
                    var customerQuery = db.Customers;
                    var adminQuery = db.Admins;

                    // Checking for Customer
                    Customer cust = customerQuery.SingleOrDefault(x => x.Username == username && x.HashedPassword == password);

                    if (cust != null)
                    {
                        Response.Redirect("~/Home/Home.aspx");
                    }
                    else
                    {
                        // Checking for Admin
                        Admin admin = adminQuery.SingleOrDefault(x => x.Username == username && x.HashedPassword == password);

                        if (admin != null)
                        {
                            Response.Redirect("~/AdminSite/Dashboard.aspx");
                        }
                        else
                        {
                            //InvalidCredentialsLabel.Text = "Invalid username or password.";
                            //InvalidCredentialsLabel.Visible = true;
                        }
                    }
                }
            }
        }
    }
}