using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Order
{
    public partial class OrderTracking : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderId = Request.QueryString["OrderID"];
                if (!string.IsNullOrEmpty(orderId))
                {
                    // Set the OrderID in the literal
                    OrderIdLiteral.Text = $"#{orderId}";

                    // Fetch and display the progress bar based on the order status
                    LoadOrderStatus(Convert.ToInt32(orderId));
                }
            }
        }

        private void LoadOrderStatus(int orderId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT OrderStatus FROM OrderPlaced WHERE OrderID = @OrderID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    con.Open();
                    string orderStatus = cmd.ExecuteScalar()?.ToString();

                    if (!string.IsNullOrEmpty(orderStatus))
                    {
                        SetProgressBar(orderStatus);
                    }
                }
            }
        }

        private void SetProgressBar(string orderStatus)
        {
            switch (orderStatus)
            {
                case "Order Received":
                    progressbarStep1.Attributes.Add("class", "active step0");
                    progressbarStep2.Attributes.Add("class", "step0");
                    progressbarStep3.Attributes.Add("class", "step0");
                    progressbarStep4.Attributes.Add("class", "step0");
                    break;

                case "Preparing Your Meal":
                    progressbarStep1.Attributes.Add("class", "active step0");
                    progressbarStep2.Attributes.Add("class", "active step0");
                    progressbarStep3.Attributes.Add("class", "step0");
                    progressbarStep4.Attributes.Add("class", "step0");
                    break;

                case "Your Order is Out for Delivery":
                    progressbarStep1.Attributes.Add("class", "active step0");
                    progressbarStep2.Attributes.Add("class", "active step0");
                    progressbarStep3.Attributes.Add("class", "active step0");
                    progressbarStep4.Attributes.Add("class", "step0");
                    break;

                case "Order Delivered":
                    progressbarStep1.Attributes.Add("class", "active step0");
                    progressbarStep2.Attributes.Add("class", "active step0");
                    progressbarStep3.Attributes.Add("class", "active step0");
                    progressbarStep4.Attributes.Add("class", "active step0");
                    break;
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            // Check if the UrlReferrer is not null and redirect
            if (Request.UrlReferrer != null)
            {
                // Additional check to prevent looping back to the same page
                string referrerUrl = Request.UrlReferrer.ToString();
                if (!referrerUrl.Contains("OrderTracking.aspx"))
                {
                    Response.Redirect(referrerUrl);
                }
                else
                {
                    // Fallback to orderHistory.aspx if the referrer is the current page
                    Response.Redirect("orderHistory.aspx");
                }
            }
            else
            {
                // Fallback if UrlReferrer is null
                Response.Redirect("orderHistory.aspx");
            }
        }

        protected void InvoiceButton_Click(object sender, EventArgs e)
        {
            string orderId = Request.QueryString["OrderID"];
            if (!string.IsNullOrEmpty(orderId))
            {
                Response.Redirect($"OrderInvoice.aspx?OrderID={orderId}");
            }
        }
    }
}