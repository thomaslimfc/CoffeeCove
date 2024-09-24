using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Payment
{
    public partial class paymentOpt : System.Web.UI.Page
    {
        string cs = Global.CS;

        decimal totalAmountWithTax;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["OrderID"] != null)
                {
                    string orderId = Session["OrderID"].ToString();

                    // Handle PayPal return
                    if (Request.QueryString["PayerID"] != null && Request.QueryString["paymentId"] != null)
                    {
                        var payerId = Request.QueryString["PayerID"];
                        var paymentId = Request.QueryString["paymentId"];

                        ExecutePayment(payerId, paymentId, orderId);

                        // After successful payment, update payment details in the database
                        UpdatePaymentStatus(orderId, "Paypal", "Complete");

                        // Update OrderPlaced table (OrderStatus, OrderDateTime, TotalAmount)
                        UpdateOrderPlaced(orderId, totalAmountWithTax);

                        // Clear the specific session (OrderID)
                        Session.Remove("OrderID");
                        Session.Remove("access");

                        Response.Redirect("~/Payment/paymentSuccess.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Order/cart.aspx");
                }
            }
        }

        protected void btnBackToCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Order/cart.aspx");
        }

        protected void btnCreditCardPayment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Session["OrderID"] != null && Page.IsValid)
                {
                    string orderId = Session["OrderID"].ToString();

                    try
                    {
                        // Update payment method and mark status as "Complete" for Credit Card Payment
                        UpdatePaymentStatus(orderId, "Credit/Debit Card", "Complete");

                        // Update OrderPlaced table (OrderStatus, OrderDateTime, TotalAmount)
                        decimal totalAmount = CalculateTotalAmount(orderId);
                        UpdateOrderPlaced(orderId, totalAmount);

                        // Clear session for OrderID after successful order processing
                        Session.Remove("OrderID");
                        Session.Remove("access");

                        // Redirect to a success page
                        Response.Redirect("~/Payment/paymentSuccess.aspx");
                    }
                    catch (Exception ex)
                    {
                        // Handle errors (e.g., log the error)
                        Response.Write("An error occurred: " + ex.Message);
                    }
                }
                else
                {
                    Response.Redirect("~/Order/cart.aspx");
                }
            }
        }

        protected void btnCOD_Click(object sender, EventArgs e)
        {
            if (Session["OrderID"] != null)
            {
                string orderId = Session["OrderID"].ToString();

                try
                {
                    // Update payment method and mark status as "Pending" for COD
                    UpdatePaymentStatus(orderId, "Cash on Delivery", "Pending");

                    // Update OrderPlaced table (OrderStatus, OrderDateTime, TotalAmount)
                    decimal totalAmount = CalculateTotalAmount(orderId);
                    UpdateOrderPlaced(orderId, totalAmount);

                    // Clear session for OrderID after successful order processing
                    Session.Remove("OrderID");
                    Session.Remove("access");

                    // Redirect to a success page
                    Response.Redirect("~/Payment/paymentSuccess.aspx");
                }
                catch (Exception ex)
                {
                    // Handle errors (e.g., log the error)
                    Response.Write("An error occurred: " + ex.Message);
                }
            }
            else
            {
                Response.Redirect("~/Order/cart.aspx");
            }
        }

        protected void btnPaypal_Click(object sender, EventArgs e)
        {
            var apiContext = GetApiContext();
            string orderId = Session["OrderID"].ToString();

            try
            {
                // Create a PayPal payment by fetching the order details
                var payment = CreatePayment(apiContext, Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/paymentOpt.aspx", orderId, out totalAmountWithTax);

                // Redirect the user to PayPal for approval
                var approvalUrl = payment.GetApprovalUrl();
                Response.Redirect(approvalUrl);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private APIContext GetApiContext()
        {
            var config = new Dictionary<string, string>
            {
                { "mode", System.Configuration.ConfigurationManager.AppSettings["PayPalMode"] }
            };

            var accessToken = new OAuthTokenCredential(
                System.Configuration.ConfigurationManager.AppSettings["PayPalClientId"],
                System.Configuration.ConfigurationManager.AppSettings["PayPalClientSecret"],
                config).GetAccessToken();

            return new APIContext(accessToken);
        }

        // Create a PayPal payment
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl, string orderId, out decimal totalAmountWithTax)
        {
            var payer = new Payer() { payment_method = "paypal" };

            // Retrieve product information from the database
            List<Item> paypalItems = new List<Item>();
            decimal subtotal = 0;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT P.ProductName, P.UnitPrice AS Price, I.Quantity 
                       FROM OrderedItem I 
                       JOIN Product P ON I.ProductId = P.ProductId
                       WHERE I.OrderId = @orderId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader["ProductName"].ToString();
                            decimal price = Convert.ToDecimal(reader["Price"]);
                            int quantity = Convert.ToInt32(reader["Quantity"]);

                            // Add item to PayPal item list
                            paypalItems.Add(new Item()
                            {
                                name = productName,
                                currency = "MYR",
                                price = price.ToString("F2"),  // Format the price to 2 decimal places
                                quantity = quantity.ToString(),
                                sku = "COFFEE_" + productName // Create an SKU based on product name
                            });

                            subtotal += price * quantity;
                        }
                    }
                }
            }

            // Calculate tax (6%)
            decimal tax = subtotal * 0.06m;
            totalAmountWithTax = subtotal + tax; // Assign totalAmountWithTax here

            // Create item list for PayPal
            var itemList = new ItemList() { items = paypalItems };

            var amount = new Amount()
            {
                currency = "MYR",
                total = totalAmountWithTax.ToString("F2"),  // Format the total to 2 decimal places
                details = new Details()
                {
                    subtotal = subtotal.ToString("F2"),
                    tax = tax.ToString("F2")
                }
            };

            var transaction = new Transaction()
            {
                description = "Coffee purchase from Coffee Cove",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            };

            var payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = new List<Transaction>() { transaction },
                redirect_urls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "?cancel=true",
                    return_url = redirectUrl
                }
            };

            return payment.Create(apiContext);
        }

        private void ExecutePayment(string payerId, string paymentId, string orderId)
        {
            var apiContext = GetApiContext();
            var paymentExecution = new PaymentExecution() { payer_id = payerId };

            // Use the fully qualified PayPal.Api.Payment class to avoid namespace conflict
            var payment = new PayPal.Api.Payment() { id = paymentId };

            // Execute the payment
            payment.Execute(apiContext, paymentExecution);
        }

        // Update the PaymentMethod and PaymentStatus in the database
        private void UpdatePaymentStatus(string orderId, string paymentMethod, string paymentStatus)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string updateSql = @"UPDATE PaymentDetail 
                             SET PaymentMethod = @PaymentMethod, PaymentStatus = @PaymentStatus 
                             WHERE OrderID = @OrderID";

                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update OrderPlaced (OrderStatus, OrderDateTime, TotalAmount)
        private void UpdateOrderPlaced(string orderId, decimal totalAmountWithTax)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string updateOrderSql = @"UPDATE OrderPlaced
                                          SET OrderStatus = @OrderStatus, OrderDateTime = @OrderDateTime, TotalAmount = @TotalAmount
                                          WHERE OrderID = @OrderID";

                using (SqlCommand cmd = new SqlCommand(updateOrderSql, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderStatus", "Order Received");
                    cmd.Parameters.AddWithValue("@OrderDateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmountWithTax);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private decimal CalculateTotalAmount(string orderId)
        {
            decimal subtotal = 0;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT P.UnitPrice AS Price, I.Quantity 
                       FROM OrderedItem I 
                       JOIN Product P ON I.ProductId = P.ProductId
                       WHERE I.OrderId = @orderId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal price = Convert.ToDecimal(reader["Price"]);
                            int quantity = Convert.ToInt32(reader["Quantity"]);
                            subtotal += price * quantity;
                        }
                    }
                }
            }

            // Calculate tax (6%)
            decimal tax = subtotal * 0.06m;
            decimal totalAmountWithTax = subtotal + tax;

            return totalAmountWithTax;
        }
    }
}
