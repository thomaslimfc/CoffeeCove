using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace CoffeeCove.Order
{
    public partial class orderOption : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStoreName.Text = " ";
            lblStoreAdd.Text = " ";

            if (!Page.IsPostBack)
            {
                string id = Request.QueryString["id"] ?? "";

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

                conn.Close();
            }
        }

        protected void rptStoreList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lbStoreList")
            {
                // Retrieve the ID of the item to edit
                string storeId = e.CommandArgument.ToString();

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"SELECT * 
                            FROM Store
                            WHERE StoreId = @storeId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@storeId", storeId);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                lblStoreName.Text = dr["StoreName"].ToString();
                lblStoreAdd.Text = dr["StoreAddress"].ToString();


                //Response.Redirect("orderOption.aspx");

                conn.Close();

            }
        }
    }
}