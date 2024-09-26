using CoffeeCove.Security;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Master
{
    public partial class Customer : System.Web.UI.MasterPage
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            //retrieve cookie
            HttpCookie coo = Request.Cookies["CusID"];

            if (coo != null && !string.IsNullOrEmpty(coo.Value))//if got cookies
            {
                Session["CusID"] = coo.Value;
                string cusId = coo.Value;
                //use sql to retrieve cusUsername
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    string sql = @"SELECT Username FROM Customer WHERE CusID = @cusId";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@CusID", cusId);
                            conn.Open();

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                //put username inside session
                                Session["Username"] = dr["Username"].ToString();

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            if (Context.User.Identity.IsAuthenticated || coo != null)
            {
                
                if (Session["CusID"] == null) //means its admin
                {
                    pnlLoggedIn.Visible = false;
                    pnlGuest.Visible = true;
                    //sign out when it coming back to customer page
                    FormsAuthentication.SignOut();
                }
                else
                {
                    pnlLoggedIn.Visible = true;
                    pnlGuest.Visible = false;
                    
                    string profilePicturePath = GetProfilePicture(Session["CusID"].ToString());

                    if (!string.IsNullOrEmpty(profilePicturePath))
                    {
                        ImgUser.ImageUrl = $"/UserManagement/UserProfilePictures/{profilePicturePath}";
                    }
                    else
                    {
                        ImgUser.ImageUrl = "/img/user.png";
                    }
                }
            }
            else
            {
                pnlLoggedIn.Visible = false;
                pnlGuest.Visible = true;
            }

            string username = Session["Username"] as string;
            //bool statusTFA = Session["statusTFA"] as bool? ?? false;
            if (!string.IsNullOrEmpty(username))
            {
                lblUserName.Text = username;
            }
            else
            {
                lblUserName.Text = "Login Now";
            }


            if (IsPostBack)
            {
                pnlDropdownMenu.Visible = ViewState["DropdownVisible"] != null && (bool)ViewState["DropdownVisible"];
            }
            UpdateCartCount();
        }

        private void UpdateCartCount()
        {
            if (Session["OrderId"] != null)
            {
                string orderId = Session["OrderId"].ToString();
                string sql = "SELECT COUNT(*) FROM OrderedItem WHERE OrderId = @OrderId";

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@OrderId", orderId); 
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    lblCount.Text = count.ToString();
                }
            }
            else
            {
                lblCount.Text = "0";
            }
        }

        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of the dropdown menu panel
            pnlDropdownMenu.Visible = !pnlDropdownMenu.Visible;
            ViewState["DropdownVisible"] = pnlDropdownMenu.Visible;
        }

        protected void lbLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Security/SignIn.aspx");
        }

        private string GetProfilePicture(string cusID)
        {
            string profilePicturePath = string.Empty;
            string sql = "SELECT ProfilePicturePath FROM Customer WHERE CusID = @CusID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@CusID", cusID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    profilePicturePath = reader["ProfilePicturePath"].ToString();
                }
            }

            return profilePicturePath;
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            HttpCookie cookieCus = Request.Cookies["CusID"];
            if (cookieCus != null)
            {
                // Set the cookie's expiration date to a time in the past
                cookieCus.Expires = DateTime.Now.AddDays(-1);

                // Add the cookie to the Response to overwrite the existing cookie
                Response.Cookies.Add(cookieCus);
            }
            Response.Redirect("../Security/SignIn.aspx");
        }
    }
}