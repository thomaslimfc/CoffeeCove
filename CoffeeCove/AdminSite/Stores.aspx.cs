using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Configuration;

namespace CoffeeCove.AdminSite
{
    public partial class StoreList : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();


            }
        }

        private void BindGridView()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM Store";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvStoreList.DataSource = dt;
                    gvStoreList.DataBind();
                }
            }
        }

        protected void gvStoreList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditStore")
            {
                string StoreId = (string)e.CommandArgument;
                LoadStoreForEdit(StoreId);
            }
            else if (e.CommandName == "DeleteStore")
            {
                int StoreId = Convert.ToInt32(e.CommandArgument);
                deleteStore(StoreId);
            }
        }

        private void deleteStore(int StoreId)
        {
            //delete store then reset the identity() to max num
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string sql = "DELETE FROM Store WHERE StoreID = @StoreId";
                using (SqlCommand deleteCmd = new SqlCommand(sql, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@StoreId", StoreId);
                    deleteCmd.ExecuteNonQuery();
                }

                string sql2 = "SELECT ISNULL(MAX(StoreID), 0) FROM Store";
                int nextId = 0;

                using (SqlCommand cmd = new SqlCommand(sql2, conn))
                {
                    nextId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string sql3 = "DBCC CHECKIDENT ('Store', RESEED, @nextId)";
                using (SqlCommand cmd = new SqlCommand(sql3, conn))
                {
                    cmd.Parameters.AddWithValue("@nextId", nextId);
                    cmd.ExecuteNonQuery();
                }
            }

            //rebind the gridview
            BindGridView();
        }

        private void LoadStoreForEdit(string StoreId)
        {
            string sql = "SELECT * FROM Store WHERE StoreID = @StoreId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StoreId", StoreId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtStoreName.Text = dr["StoreName"].ToString();
                        txtStoreAddress.Text = dr["StoreAddress"].ToString();
                        //txtPostCode.Text = dr["StorePostCode"].ToString();
                        hdnId.Value = StoreId;
                    }
                }
            }
            btnAdd.Text = "Update";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Stores.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get data from textbox
                string storeName = txtStoreName.Text;
                string storeAddress = txtStoreAddress.Text;

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"INSERT INTO Store(StoreName,StoreAddress) 
                                VALUES (@storeName,@storeAdd);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@storeName", storeName);
                    cmd.Parameters.AddWithValue("@storeAdd", storeAddress);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        BindGridView(); //bind the gridview again after adding in new data
                    }
                    catch (SqlException ex)
                    {
                        Response.Write("An error occurred: " + ex.Message);
                    }
                }

                //clear the textbox after insert
                txtStoreName.Text = "";
                txtStoreAddress.Text = "";

            }
        }
    }

}