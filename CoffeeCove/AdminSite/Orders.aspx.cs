﻿using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoffeeCove.Securities;

namespace CoffeeCove.AdminSite
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        //dbCoffeeCoveEntities db = new dbCoffeeCoveEntities();
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //string orderId = Session["OrderId"].ToString();
                BindGridView();
            }
        }

        protected void rptOrdered_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int quantity = 0;
            decimal price = 0;
            decimal subTotal = 0;
            decimal linePrice = 0;
            decimal tax = 0;
            decimal total = 0;

            System.Web.UI.WebControls.Label lblQuantity = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblQuantity");
            quantity = int.Parse(lblQuantity.Text);
            System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrice");
            price = decimal.Parse(lblPrice.Text);
            System.Web.UI.WebControls.Label lblLineTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblLineTotal");

            System.Web.UI.WebControls.Label lblSize = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblSize");

            Panel panelSize = (Panel)e.Item.FindControl("panelSize");
            Panel panelFlavour = (Panel)e.Item.FindControl("panelFlavour");
            Panel panelIce = (Panel)e.Item.FindControl("panelIce");
            Panel panelAddon = (Panel)e.Item.FindControl("panelAddon");

            Panel panelTable = (Panel)e.Item.FindControl("panelTable");

            if (string.IsNullOrEmpty(lblSize.Text))
            {
                panelTable.Visible = false;
                panelSize.Visible = false;
                panelFlavour.Visible = false;
                panelIce.Visible = false;
                panelAddon.Visible = false;
            }

            linePrice = quantity * price;
            subTotal += linePrice;

            tax = subTotal * (decimal)0.06;
            total = subTotal + tax;

            lblSubtotal.Text = subTotal.ToString("C"); // "C" formats the number as currency
            lblTax.Text = tax.ToString("C");
            lblTotal.Text = total.ToString("C");
            lblLineTotal.Text = linePrice.ToString("C");
        }

        private void BindGridView()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT O.OrderID, O.OrderDateTime, O.TotalAmount, P.PaymentMethod, O.OrderStatus, C.Username
                    FROM OrderPlaced O 
                    JOIN PaymentDetail P ON O.OrderID = P.OrderID
                    JOIN Customer C ON O.CusID = C.CusID";


                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvOrder.DataSource = dt;
                        gvOrder.DataBind();
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewOrder")
            {
                string orderId = e.CommandArgument.ToString();
                bindRepeater(orderId);
                displayDetail(orderId);
                
            }
            else if(e.CommandName == "deleteOrder")
            {
                string orderId = e.CommandArgument.ToString();
                deleteOrder(orderId);
            }
        }

        private void deleteOrder(string orderId)
        {
            //delete store then reset the identity() to max num
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string paymentId = "";
                //get paymentID
                string sql5 = @"SELECT PaymentID
                                FROM PaymentDetail
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql5, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    try
                    {
                        conn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            paymentId = dr["PaymentID"].ToString();

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    conn.Close();
                }

                conn.Open();

                string sql = @"DELETE FROM OrderedItem
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }

                //delete review first then only payment
                string sql4 = @"DELETE FROM Review
                                WHERE PaymentID = @paymentId";
                using (SqlCommand cmd = new SqlCommand(sql4, conn))
                {
                    cmd.Parameters.AddWithValue("@paymentId", paymentId);
                    cmd.ExecuteNonQuery();
                }

                string sql2 = @"DELETE FROM PaymentDetail
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql2, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }

                string sql3 = @"DELETE FROM OrderPlaced
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql3, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }

            }

            //rebind the gridview
            BindGridView();
        }

        private void bindRepeater(string orderId)
        {
            SqlConnection conn = new SqlConnection(cs);
            string sql = @"SELECT * 
                            FROM OrderedItem I JOIN Product P 
                            ON I.ProductId = P.ProductId
                            WHERE OrderId = @orderId";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@orderId", orderId);
            conn.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            //clear the repeater to null
            rptOrdered.DataSource = null;
            rptOrdered.DataBind();

            rptOrdered.DataSource = ds;
            rptOrdered.DataBind();

            conn.Close();
        }

        private void displayDetail(string orderId)
        {
            //get data from db
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT *
                    FROM OrderPlaced O 
                    JOIN PaymentDetail P ON O.OrderID = P.OrderID
                    JOIN Customer C ON O.CusID = C.CusID
                    WHERE O.OrderID = @orderId";


                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    try
                    {
                        conn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            lblOrderNo.Text = orderId;
                            lblDate.Text = dr["OrderDateTime"].ToString();

                            //if it is delivery then display delivery and vice versa
                            if (dr["StoreID"] == DBNull.Value)//if store ID = null, means that it is using delivery
                            {
                                lblDelPick.Text = "Delivery";
                                lblDelivery.Text = dr["DeliveryAddress"].ToString();
                                lblPickUp.Text = "-";
                            }
                            else if (dr["DeliveryAddress"] == DBNull.Value)
                            {
                                lblDelPick.Text = "Pick Up";
                                lblPickUp.Text = dr["StoreName"].ToString();
                                lblDelivery.Text = "-";
                            }
                            else
                            {
                                lblDelPick.Text = " ";
                            }

                            lblPaymentMethod.Text = dr["PaymentMethod"].ToString();
                            lblUsername.Text = dr["Username"].ToString();
                            lblEmail.Text = dr["EmailAddress"].ToString();



                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }




        }
    }
}
