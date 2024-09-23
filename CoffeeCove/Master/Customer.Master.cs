﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                int orderId = (int)Session["OrderId"];
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

    }
}