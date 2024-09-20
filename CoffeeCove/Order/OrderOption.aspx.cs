using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace CoffeeCove.Order
{
    public partial class orderOption : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                SqlConnection conn = new SqlConnection(cs);
                string sql = @"SELECT * 
                            FROM Store";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                rptStoreList.DataSource = ds;
                rptStoreList.DataBind();

                


            }

            if (string.IsNullOrWhiteSpace(lblStoreAdd.Text)) //if lblStoreAdd is empty
            {
                lbConfirmPickUp.Enabled = false;
                lbConfirmPickUp.CssClass = "btnCont-disabled";
            }

            
        }

        protected void rptStoreList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "storeList")
            {
                // Retrieve the ID of the item to edit
                string storeId = e.CommandArgument.ToString();

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"SELECT * FROM Store WHERE StoreID = @storeId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@storeId", storeId);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read()) 
                {
                    // Assign values to label controls
                    lblStoreName.Text = dr["StoreName"].ToString();
                    lblStoreAdd.Text = dr["StoreAddress"].ToString();

                }
                else
                {
                    // Handle the case where no data is found
                    lblStoreName.Text = "Store not found.";
                    lblStoreAdd.Text = string.Empty;
                }

                lbConfirmPickUp.Enabled = true;
                lbConfirmPickUp.CssClass = "btnCont";

                conn.Close();
            }
        }

        protected void lbConfirmMap_Click(object sender, EventArgs e)
        {
            //txtAddress1.Text = "Jalan Seri Tanjung Pinang 1";
            //txtAddress2.Text = "Seri Tanjung Pinang";
            //txtPostCode.Text = "10470";
            //txtUnit.Text = "8A-15-1";
        }

        protected void lbConfirmPickUp_Click(object sender, EventArgs e)
        {
            
            Session["orderOpt"] = "PickUp";
            Response.Redirect("../Menu/Menu.aspx");
        }

        protected void lbConfirmDelivery_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) //means it choose delivery
            {
                Session["orderOpt"] = "Delivery";
                Response.Redirect("../Menu/Menu.aspx");
            }
        }
    }
}