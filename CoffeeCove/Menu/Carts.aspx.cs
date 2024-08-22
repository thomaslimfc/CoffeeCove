using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Menu
{
    public partial class Carts : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrder();
            }
        }

        private void BindOrder()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                // Ensure to use a valid SQL query with proper joins
                string sql = @"SELECT oi.ProductID, p.ProductName, p.ImageUrl, oi.Size, oi.Flavour, oi.IceLevel, oi.AddOn, oi.Instruction, oi.Quantity, oi.Price 
                                FROM OrderedItem oi
                                JOIN Product p ON oi.ProductID = p.ProductID"; // Fixed the JOIN clause

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    rptCart.DataSource = dr; // Make sure ID matches the ASPX ID
                    rptCart.DataBind();
                }
            }
        }
    }
}