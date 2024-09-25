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
            if (Context.User.Identity.IsAuthenticated) // Use Context.User if User is not accessible
            {
                pnLoggedIn.Visible = true;  // Show logged-in panel
                pnGuest.Visible = false; // Hide logged-out panel
            }
            else
            {
                pnLoggedIn.Visible = false; // Hide logged-in panel
                pnGuest.Visible = true;  // Show logged-out panel
            }

            //retrieve cookie
            HttpCookie coo = Request.Cookies["CusID"];
            if (coo != null)
            {
                Session["CusID"] = coo.Value;
            }

            string username = Session["Username"] as string;
            bool statusTFA = Session["statusTFA"] as bool? ?? false;
            if (!string.IsNullOrEmpty(username) && statusTFA)
            {
                lblUserName.Text = username;
            }
            else
            {
                lblUserName.Text = "Register Now";
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
    }
}