﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
using CoffeeCove.Master;

namespace CoffeeCove.Order
{
    public partial class orderOption : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                using(SqlConnection conn = new SqlConnection(cs)){

                    string sql = @"SELECT * 
                            FROM Store";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        try
                        {
                            conn.Open();

                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                            rptStoreList.DataSource = ds;
                            rptStoreList.DataBind();

                        }
                        catch (Exception ex)
                        {
                            Response.Write("Oops! An error occurred: " + ex.Message);
                        }
                    }
                }



            }

            if (string.IsNullOrWhiteSpace(lblStoreAdd.Text)) //if the store address is empty
            {
                lbConfirmPickUp.Enabled = false;
                lbConfirmPickUp.CssClass = "btnCont-disabled";
            }

            
        }

        protected void rptStoreList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "storeList")
            {
                
                string storeId = e.CommandArgument.ToString();

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    string sql = @"SELECT * FROM Store WHERE StoreID = @storeId";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@storeId", storeId);
                            conn.Open();

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                hfStoreID.Value = storeId;
                                lblStoreName.Text = dr["StoreName"].ToString();
                                lblStoreAdd.Text = dr["StoreAddress"].ToString();

                            }
                            else
                            {

                                lblStoreName.Text = "Store not found.";
                                lblStoreAdd.Text = string.Empty;
                            }
                            lbConfirmPickUp.Enabled = true;
                            lbConfirmPickUp.CssClass = "btnCont";
                        }
                        catch (Exception ex)
                        {
                            Response.Write("Oops! An error occurred: " + ex.Message);
                        }
                    }
                        
                }
            }
        }

        protected void lbConfirmMap_Click(object sender, EventArgs e)
        {
            //txtAddress1.Text = "Jalan Seri Tanjung Pinang 1";
            //txtAddress2.Text = "Seri Tanjung Pinang";
            //txtPostCode.Text = "10470";
            //txtUnit.Text = "8A-15-1";
        }

        protected void createOrderID()
        {
            //retrieve cusID from session
            string cusID = Session["CusID"].ToString();
            //create an orderID for it
            using (SqlConnection conn3 = new SqlConnection(cs))
            {
                string sql3 = @"INSERT INTO OrderPlaced(CusID,OrderDateTime,TotalAmount,OrderType) 
                                VALUES (@cusID,@dateTime,0,'Pick Up');
                                SELECT SCOPE_IDENTITY();";

                string sql4 = @"INSERT INTO PaymentDetail(PaymentStatus,OrderID) 
                                VALUES ('Pending',@orderId)";

                using (SqlCommand cmd3 = new SqlCommand(sql3, conn3))
                {
                    try
                    {
                        cmd3.Parameters.AddWithValue("@cusID", cusID);
                        cmd3.Parameters.AddWithValue("@dateTime", DateTime.Now);
                        conn3.Open();

                        object newOrderID = cmd3.ExecuteScalar();
                        int orderId = Convert.ToInt32(newOrderID);

                        using (SqlCommand cmd4 = new SqlCommand(sql4, conn3))
                        {
                            cmd4.Parameters.AddWithValue("@orderId", orderId.ToString());
                            cmd4.ExecuteNonQuery();

                            Session["OrderID"] = orderId;

                            //create cookies
                            HttpCookie coo = new HttpCookie("OrderID", orderId.ToString());
                            //coo.Expires = DateTime.Now.AddMinutes(1);

                            //send the cookie to client pc
                            Response.Cookies.Add(coo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                    
                }
            }
                
        }

        protected void lbConfirmPickUp_Click(object sender, EventArgs e)
        {
            if (Session["OrderID"] == null) //if its their firstTime come here 
            {
                createOrderID();
            }

            string orderID = Session["OrderID"].ToString();
            //put the pickup Store inside the database
            string storeID = hfStoreID.Value;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"UPDATE OrderPlaced 
                                SET StoreID = @storeID
                                WHERE OrderID = @orderID;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@storeID", storeID);
                        cmd.Parameters.AddWithValue("@orderID", orderID);
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        //clean the deliveryAddress
                        string sql2 = @"UPDATE OrderPlaced 
                                SET DeliveryAddress = NULL
                                WHERE OrderID = @orderID;";

                        using (SqlCommand cmd2 = new SqlCommand(sql2, conn))
                        {
                            try
                            {
                                cmd2.Parameters.AddWithValue("@orderID", orderID);

                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Oops! An error occurred: " + ex.Message);
                            }
                            
                        }
                            
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }
            }
            Session["orderOpt"] = "PickUp";
            Response.Redirect("../Menu/Menu.aspx");
        }

        protected void lbConfirmDelivery_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) //means it choose delivery
            {
                if (Session["OrderID"] == null) //if its their firstTime come here 
                {

                    createOrderID();
                }

                string orderID = Session["OrderID"].ToString();
                //put the pickup Store inside the database
                string storeID = hfStoreID.Value;

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    string address = "";
                    if (txtUnit.Text.Length > 0) //if got write unit
                    {
                        address = txtUnit.Text + "," + txtAddress1.Text + "," + txtPostCode.Text;
                    }
                    else
                    {
                        address = txtAddress1.Text + "," + txtPostCode.Text;
                    }

                    //save the address into the database
                    string sql = @"UPDATE OrderPlaced 
                                SET DeliveryAddress = @address
                                WHERE OrderID = @orderID;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@address", address);
                            cmd.Parameters.AddWithValue("@orderID", orderID);
                            conn.Open();
                            cmd.ExecuteNonQuery();

                            //clean the pickupStore
                            string sql2 = @"UPDATE OrderPlaced 
                                SET StoreID = NULL
                                WHERE OrderID = @orderID;";

                            using (SqlCommand cmd2 = new SqlCommand(sql2, conn))
                            {
                                try
                                {
                                    cmd2.Parameters.AddWithValue("@orderID", orderID);

                                    cmd2.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("Oops! An error occurred: " + ex.Message);
                                }
                            }
                                
                        }
                        catch(Exception ex)
                        {
                            Response.Write("Oops! An error occurred: " + ex.Message);
                        }
                    }
                }
                Session["orderOpt"] = "Delivery";
                Response.Redirect("../Menu/Menu.aspx");
            }
        }
    }
}