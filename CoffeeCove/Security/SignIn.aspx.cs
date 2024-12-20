﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CoffeeCove.Security
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            //remove the cookie
            HttpCookie cookieCus = Request.Cookies["CusID"];
            if (cookieCus != null)
            {
                // Set the cookie's expiration date to a time in the past
                cookieCus.Expires = DateTime.Now.AddDays(-1);

                // Add the cookie to the Response to overwrite the existing cookie
                Response.Cookies.Add(cookieCus);
            }
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

                    // Should be identity of customer or admin
                    if (customer != null || admin != null)
                    {
                        // Store user role and username in session
                        Session["Username"] = username;
                        Session["UserRole"] = customer != null ? "User" : "Admin";

                        // need to check with customer only, becoz admin no CusID
                        if (customer != null)
                        {
                            Session["CusID"] = customer.CusID;
                            ////create cookies
                            //HttpCookie coo = new HttpCookie("CusID", customer.CusID.ToString());
                            ////coo.Expires = DateTime.Now.AddMinutes(1);

                            ////send the cookie to client pc
                            //Response.Cookies.Add(coo);

                        }

                        if (customer != null && !string.IsNullOrEmpty(customer.ContactNo))
                        {
                            Session["ContactNo"] = customer.ContactNo;
                        }
                        else if (admin != null && !string.IsNullOrEmpty(admin.ContactNo))
                        {
                            Session["ContactNo"] = admin.ContactNo;
                        }
                        else
                        {
                            // Redirect if contact number is missing for both customer and admin
                            Response.Redirect("~/Security/SignIn.aspx");
                            return; // Stop further processing if no contact number is found
                        }

                        // Set a flag indicating that 2FA is required
                        Session["2FARequired"] = true;

                        // Redirect to the two-factor authentication page
                        Response.Redirect("TwoFactorAuthentication.aspx");
                    }
                    else
                    {
                        lblPassword_SI.Visible = true;
                    }
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
