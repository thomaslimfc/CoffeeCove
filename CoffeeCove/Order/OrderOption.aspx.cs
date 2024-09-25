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

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"SELECT * FROM Store WHERE StoreID = @storeId";

                SqlCommand cmd = new SqlCommand(sql, conn);
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

        protected void createOrderID()
        {
            //retrieve cusID from session
            string cusID = Session["CusID"].ToString();
            //create an orderID for it
            SqlConnection conn3 = new SqlConnection(cs);
            string sql3 = @"INSERT INTO OrderPlaced(CusID,OrderDateTime,TotalAmount,OrderType) 
                                VALUES (@cusID,@dateTime,0,'Pick Up');
                                SELECT SCOPE_IDENTITY();";

            SqlCommand cmd3 = new SqlCommand(sql3, conn3);
            cmd3.Parameters.AddWithValue("@cusID", cusID);
            cmd3.Parameters.AddWithValue("@dateTime", DateTime.Now);
            conn3.Open();

            object newOrderID = cmd3.ExecuteScalar();


            int orderId = Convert.ToInt32(newOrderID);

            string sql4 = @"INSERT INTO PaymentDetail(PaymentStatus,OrderID) 
                                VALUES ('Pending',@orderId)";

            SqlCommand cmd4 = new SqlCommand(sql4, conn3);
            cmd4.Parameters.AddWithValue("@orderId", orderId.ToString());
            cmd4.ExecuteNonQuery();

            Session["OrderID"] = orderId;

            conn3.Close();
        }

        protected void lbConfirmPickUp_Click(object sender, EventArgs e)
        {
            if (Session["access"] == null) //if its their firstTime come here or second time but dont have order ID
            {
                Session["access"] = 1; //set its session

                createOrderID();
            }

            if (Session["OrderID"] == null)
            {
                //if dont have orderID even thou its their second time enter
                createOrderID();
            }

            int orderID = (int)Session["OrderID"];
            //put the pickup Store inside the database
            string storeID = hfStoreID.Value;

            SqlConnection conn = new SqlConnection(cs);
            string sql = @"UPDATE OrderPlaced 
                                SET StoreID = @storeID
                                WHERE OrderID = @orderID;";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@storeID", storeID);
            cmd.Parameters.AddWithValue("@orderID", orderID);
            conn.Open();

            cmd.ExecuteNonQuery();

            //clean the deliveryAddress
            string sql2 = @"UPDATE OrderPlaced 
                                SET DeliveryAddress = NULL
                                WHERE OrderID = @orderID;";

            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@orderID", orderID);

            cmd2.ExecuteNonQuery();




            conn.Close();

            Session["orderOpt"] = "PickUp";
            Response.Redirect("../Menu/Menu.aspx");
        }

        protected void lbConfirmDelivery_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) //means it choose delivery
            {
                if (Session["access"] == null) //if its their firstTime come here
                {
                    Session["access"] = 1; //set its session

                    //retrieve cusID from session
                    string cusID = Session["CusID"].ToString();
                    //create an orderID for it
                    SqlConnection conn3 = new SqlConnection(cs);
                    string sql3 = @"INSERT INTO OrderPlaced(CusID,OrderDateTime,TotalAmount,OrderType) 
                                VALUES (@cusID,@dateTime,0,'Delivery');
                                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd3 = new SqlCommand(sql3, conn3);
                    cmd3.Parameters.AddWithValue("@cusID", cusID);
                    cmd3.Parameters.AddWithValue("@dateTime", DateTime.Now);
                    conn3.Open();

                    object newOrderID = cmd3.ExecuteScalar();


                    int orderId = Convert.ToInt32(newOrderID);

                    string sql4 = @"INSERT INTO PaymentDetail(PaymentStatus,OrderID) 
                                VALUES ('Pending',@orderId)";

                    SqlCommand cmd4 = new SqlCommand(sql4, conn3);
                    cmd4.Parameters.AddWithValue("@orderId", orderId.ToString());
                    cmd4.ExecuteNonQuery();

                    Session["OrderID"] = orderId;

                    conn3.Close();
                }


                int orderID = (int)Session["OrderID"];

                //get the address from textbox then combine them into one address
                string address = "";
                if(txtUnit.Text.Length > 0) //if got write unit
                {
                    address = txtUnit.Text + "," + txtAddress1.Text + "," + txtPostCode.Text;
                }
                else
                {
                    address = txtAddress1.Text + "," + txtPostCode.Text;
                }
                
                

                //save the address into the database
                SqlConnection conn = new SqlConnection(cs);
                string sql = @"UPDATE OrderPlaced 
                                SET DeliveryAddress = @address
                                WHERE OrderID = @orderID;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@orderID", orderID);
                conn.Open();

                cmd.ExecuteNonQuery();

                //clean the pickupStore
                string sql2 = @"UPDATE OrderPlaced 
                                SET StoreID = NULL
                                WHERE OrderID = @orderID;";

                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@orderID", orderID);

                cmd2.ExecuteNonQuery();



                conn.Close();

                Session["orderOpt"] = "Delivery";
                Response.Redirect("../Menu/Menu.aspx");
            }
        }
    }
}