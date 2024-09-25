using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.Text;

//role based authentication
using System.Security.Principal;
using System.Threading;
using System.Web.Security;


namespace CoffeeCove
{
    public class UserSecurity
    {
        //handle login with role authorization
        public static void LoginUser(string username, string role, bool rememberMe)
        {
            HttpContext ctx = HttpContext.Current;

            // Retrieving old cookie/ticket
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(username, rememberMe);
            // Decrypt old cookie/ticket
            FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(authCookie.Value);

            // Adding role into ticket and form new ticket
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                oldTicket.Version,
                oldTicket.Name,
                oldTicket.IssueDate,
                oldTicket.Expiration,
                oldTicket.IsPersistent,
                role
            );
            // Encrypt new ticket
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            // Pass the new ticket to client
            ctx.Response.Cookies.Add(authCookie);

            // Custom redirect logic based on role
            string redirectUrl = role == "Admin" ? "~/AdminSite/Dashboard.aspx" : "~/Home/Home.aspx";
            ctx.Response.Redirect(redirectUrl);
        }


        public static void ProcessRoles()
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.User != null && ctx.User.Identity.IsAuthenticated && ctx.User.Identity is FormsIdentity)
            {
                FormsIdentity identity = (FormsIdentity)ctx.User.Identity;
                string[] roles = identity.Ticket.UserData.Split(',');

                GenericPrincipal principal = new GenericPrincipal(identity, roles);
                ctx.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }










        //create method to perform hash calculation
        public static string GetHash(string strPass)
        {
            //based on original password and encode it to become binary
            byte[] binPass = Encoding.Default.GetBytes(strPass);

            //generate hash function
            //crytography library
            SHA256 sha = SHA256.Create();

            //perform hash function
            byte[] binHash = sha.ComputeHash(binPass);

            //in order to store the hash value into DB, need to convert from bin to string
            string strHash = Convert.ToBase64String(binHash);

            return strHash;
        }



    }
}