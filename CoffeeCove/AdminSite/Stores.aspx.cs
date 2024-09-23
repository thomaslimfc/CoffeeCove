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
                    try
                    {
                        conn.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvStoreList.DataSource = dt;
                        gvStoreList.DataBind();
                    }
                    catch (SqlException ex)
                    {

                    }
                    catch (Exception ex)
                    {

                    }
                    
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

                string sql = @"DELETE FROM Store
                                WHERE StoreID = @StoreId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StoreId", StoreId);
                    cmd.ExecuteNonQuery();
                }

                string sql2 = @"SELECT ISNULL(MAX(StoreID), 0) 
                                FROM Store";
                int nextId = 0;

                using (SqlCommand cmd = new SqlCommand(sql2, conn))
                {
                    nextId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string sql3 = @"DBCC CHECKIDENT ('Store', RESEED, @nextId)";
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
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT * 
                        FROM Store 
                        WHERE StoreID = @StoreId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StoreId", StoreId);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtStoreName.Text = dr["StoreName"].ToString();
                            txtStoreAddress.Text = dr["StoreAddress"].ToString();
                            //txtPostCode.Text = dr["StorePostCode"].ToString();
                            hdnId.Value = StoreId;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Response.Write("An error occurred: " + ex.Message);
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

            //if it is "ADD"
            if(btnAdd.Text == "Add")
            {
                if (Page.IsValid)
                {
                    //get data from textbox
                    string storeName = txtStoreName.Text;
                    string storeAddress = txtStoreAddress.Text;

                    using (SqlConnection conn = new SqlConnection(cs))
                    {
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
                    }
                        
                    //clear the textbox after insert
                    txtStoreName.Text = "";
                    txtStoreAddress.Text = "";

                }
            }
            else if(btnAdd.Text == "Update") //if it is update
            {
                if (Page.IsValid)
                {
                    //get data from textbox
                    string storeName = txtStoreName.Text;
                    string storeAddress = txtStoreAddress.Text;
                    string storeId = hdnId.Value;

                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        string sql = @"UPDATE Store
                                SET StoreName = @storeName, StoreAddress = @storeAdd
                                WHERE StoreID = @storeId;";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@storeName", storeName);
                            cmd.Parameters.AddWithValue("@storeAdd", storeAddress);
                            cmd.Parameters.AddWithValue("@storeId", storeId);

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
                    }
                        
                    //clear the textbox
                    txtStoreName.Text = "";
                    txtStoreAddress.Text = "";

                }

                btnAdd.Text = "Add";
            }
            
        }
    }

}