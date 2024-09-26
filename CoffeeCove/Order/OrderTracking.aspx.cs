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
                    OrderIdLiteral.Text = $"{orderId}";

                    // Fetch and display the progress bar based on the order status
                    LoadOrderStatus(Convert.ToInt32(orderId));
                }
            }
        }

        private void LoadOrderStatus(int orderId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT OrderStatus, OrderType FROM OrderPlaced WHERE OrderID = @OrderID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string orderStatus = reader["OrderStatus"].ToString();
                            string orderType = reader["OrderType"].ToString();

                            // Update the progress bar
                            SetProgressBar(orderStatus, orderType);
                        }
                    }
                }
            }
        }

        private void SetProgressBar(string orderStatus, string orderType)
        {
            // Define the wording based on OrderType
            string readyText = orderType == "Pick Up" ? "Your Order<br>is Ready" : "Your Order is<br>Out for Delivery";
            string pickedUpText = orderType == "Pick Up" ? "Order<br>Picked Up" : "Order<br>Delivered";

            // Update the progress bar steps based on the OrderStatus
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
                case "Your Order is Ready":  // Handle for Pick Up case
                    progressbarStep1.Attributes.Add("class", "active step0");
                    progressbarStep2.Attributes.Add("class", "active step0");
                    progressbarStep3.Attributes.Add("class", "active step0");
                    progressbarStep4.Attributes.Add("class", "step0");
                    break;

                case "Order Delivered":
                case "Order Picked Up":  // Handle for Pick Up case
                    progressbarStep1.Attributes.Add("class", "active step0");
                    progressbarStep2.Attributes.Add("class", "active step0");
                    progressbarStep3.Attributes.Add("class", "active step0");
                    progressbarStep4.Attributes.Add("class", "active step0");
                    break;
            }

            // Update the labels dynamically
            LabelStep3.Text = readyText;
            LabelStep4.Text = pickedUpText;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            if (Request.UrlReferrer != null)
            {
                string referrerUrl = Request.UrlReferrer.ToString();
                if (!referrerUrl.Contains("OrderTracking.aspx"))
                {
                    Response.Redirect(referrerUrl);
                }
                else
                {
                    Response.Redirect("orderHistory.aspx");
                }
            }
            else
            {
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