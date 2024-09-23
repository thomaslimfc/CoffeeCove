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
            //lblOrderNo.Text = "1";
            //lblDate.Text = "22/7/2024 12:00:00 AM";
            //lblDelPick.Text = "Pick Up";
            //lblPaymentMethod.Text = "Cash";
            //lblUsername.Text = "goldfishyyy";
            //lblEmail.Text = "goldfizh@gmail.com";
            //lblPickUp.Text = "CoffeeCove Karpal Singh";


            if (!Page.IsPostBack)
            {
                //string orderId = Session["OrderId"].ToString();
                BindGridView();
            }
        }

        protected void rptOrdered_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            System.Web.UI.WebControls.Label lblQuantity = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblQuantity");
            int quantity = int.Parse(lblQuantity.Text);
            System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrice");
            decimal price = decimal.Parse(lblPrice.Text);
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

            decimal subTotal = 0;
            decimal linePrice = 0;

            linePrice = quantity * price;
            subTotal += linePrice;

            decimal tax = subTotal * (decimal)0.06;
            decimal total = subTotal + tax;

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
                    INNER JOIN PaymentDetail P ON O.OrderID = P.OrderID
                    INNER JOIN Customer C ON O.CusID = C.CusID";


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
                    catch (SqlException ex)
                    {

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
                int orderId = Convert.ToInt32(e.CommandArgument);
                bindRepeater(orderId.ToString());

                
            }
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


    }
}
