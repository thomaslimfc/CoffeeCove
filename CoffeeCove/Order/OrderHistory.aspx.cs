using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CoffeeCove.Order
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderHistory();
            }
        }

        private void BindOrderHistory()
        {
            int cusId = (int)(Session["CusID"] ?? 0);

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"
            SELECT O.*, PD.PaymentID
            FROM [OrderPlaced] O
            LEFT JOIN PaymentDetail PD ON O.OrderID = PD.OrderID
            WHERE O.CusID = @CusID
            AND O.OrderStatus IS NOT NULL
            AND O.OrderStatus IN ('Order Delivered', 'Preparing Your Meal', 'Your Order is Out for Delivery', 'Order Received')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Pass the CusID as a parameter
                    cmd.Parameters.AddWithValue("@CusID", cusId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    OrderHistoryList.DataSource = reader;
                    OrderHistoryList.DataBind();
                }
            }
        }

        protected void OrderHistoryList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Get the current OrderID, OrderStatus, and OrderDateTime
                int orderId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "OrderID"));
                string orderStatus = DataBinder.Eval(e.Item.DataItem, "OrderStatus").ToString();
                DateTime orderDateTime = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "OrderDateTime"));

                // Find the RatingButton and CancelOrderButton controls
                Button ratingButton = (Button)e.Item.FindControl("RatingButton");
                Button cancelOrderButton = (Button)e.Item.FindControl("CancelOrderButton");

                // Check if the order status is 'Order Delivered'
                if (orderStatus == "Order Delivered")
                {
                    // Show the Rating button for orders that are delivered
                    ratingButton.Visible = true;
                }

                // If the order status is 'Order Received', check the order time
                if (orderStatus == "Order Received")
                {
                    TimeSpan timeDifference = DateTime.Now - orderDateTime;

                    // If the order was placed more than 5 minutes ago, update the status to 'Preparing Your Meal'
                    if (timeDifference.TotalSeconds > 20)
                    {
                        UpdateOrderStatus(orderId, "Preparing Your Meal");
                        cancelOrderButton.Visible = false;
                    }
                    else
                    {
                        cancelOrderButton.Visible = true;
                    }
                }

                if (orderStatus == "Preparing Your Meal")
                {
                    TimeSpan timeDifference = DateTime.Now - orderDateTime;

                    // If the order was prepared more than 5 minutes ago, update the status to 'Your Order is Out for Delivery'
                    if (timeDifference.TotalSeconds > 30)
                    {
                        UpdateOrderStatus(orderId, "Your Order is Out for Delivery");
                    }
                }

                if (orderStatus == "Your Order is Out for Delivery")
                {
                    TimeSpan timeDifference = DateTime.Now - orderDateTime;

                    // If the order was prepared more than 5 minutes ago, update the status to 'Order Delivered'
                    if (timeDifference.TotalSeconds > 40)
                    {
                        UpdateOrderStatus(orderId, "Order Delivered");
                    }
                }

                // Bind the product list for the current order
                Repeater productListRepeater = (Repeater)e.Item.FindControl("ProductListRepeater");
                BindProductList(orderId, productListRepeater);

                // Check if there is a review for the current order
                CheckForReview(orderId, e);
            }
        }

        private void UpdateOrderStatus(int orderId, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"UPDATE [OrderPlaced] 
                    SET OrderStatus = @OrderStatus
                    WHERE OrderID = @OrderID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderStatus", newStatus);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void BindProductList(int orderId, Repeater productListRepeater)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"SELECT OI.ProductID, OI.Quantity, OI.Price, P.ProductName, P.ImageUrl
                                 FROM OrderedItem OI
                                 INNER JOIN Product P ON OI.ProductID = P.ProductId
                                 WHERE OI.OrderID = @OrderID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    productListRepeater.DataSource = reader;
                    productListRepeater.DataBind();
                }
            }
        }

        private void CheckForReview(int orderId, RepeaterItemEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"
            SELECT R.RatingScore, R.ReviewContent, R.RatingReviewDateTime, PD.PaymentID 
            FROM Review R 
            JOIN PaymentDetail PD ON R.PaymentID = PD.PaymentID 
            WHERE PD.OrderID = @OrderID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        // Review exists, bind the review data
                        Literal ratingScore = (Literal)e.Item.FindControl("RatingScore");
                        Literal reviewContent = (Literal)e.Item.FindControl("ReviewContent");
                        PlaceHolder phStars = (PlaceHolder)e.Item.FindControl("phStars");
                        HtmlGenericControl reviewSection = (HtmlGenericControl)e.Item.FindControl("ReviewSection");

                        int score = Convert.ToInt32(reader["RatingScore"]);
                        reviewContent.Text = reader["ReviewContent"].ToString();

                        for (int i = 0; i < score; i++)
                        {
                            phStars.Controls.Add(new Literal
                            {
                                Text = "<i class='fa fa-star'></i>"
                            });
                        }

                        // Add empty stars if the rating is less than 5
                        for (int i = score; i < 5; i++)
                        {
                            phStars.Controls.Add(new Literal
                            {
                                Text = "<i class='fa fa-star-o'></i>"
                            });
                        }

                        reviewSection.Visible = true;
                    }
                }
            }
        }

        protected void CancelOrderButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int orderId = Convert.ToInt32(button.CommandArgument);

            // Update the order status to "Order Cancelled"
            UpdateOrderStatus(orderId, "Order Cancelled");

            BindOrderHistory();
        }

        protected void TrackOrderButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string orderId = button.CommandArgument;
            Response.Redirect($"OrderTracking.aspx?OrderID={orderId}");
        }

        protected void RatingButton_Click(object sender, EventArgs e)
        {
            Button ratingButton = (Button)sender;
            string paymentId = ratingButton.CommandArgument;
            Response.Redirect($"~/RatingReview/comment.aspx?PaymentID={paymentId}");
        }
    }
}
