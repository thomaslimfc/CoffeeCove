using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                conn.Close();
            }
        }
    }
}