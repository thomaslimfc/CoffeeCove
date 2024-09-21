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
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM [OrderPlaced]";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
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
                // Get the current OrderID and OrderStatus
                int orderId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "OrderID"));
                string orderStatus = DataBinder.Eval(e.Item.DataItem, "OrderStatus").ToString();

                // Find the RatingButton and TrackOrderButton controls
                Button ratingButton = (Button)e.Item.FindControl("RatingButton");

                // Check if the order status is 'Order Delivered'
                if (orderStatus == "Order Delivered")
                {
                    // Show the Rating button for orders that are delivered
                    ratingButton.Visible = true;
                }

                // Find the nested repeater (ProductListRepeater) and bind product data for this order
                Repeater productListRepeater = (Repeater)e.Item.FindControl("ProductListRepeater");
                BindProductList(orderId, productListRepeater);

                CheckForReview(orderId, e);
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
                string query = @"SELECT RatingScore, ReviewContent, RatingReviewDateTime 
                         FROM Review 
                         WHERE PaymentID IN (SELECT PaymentID FROM PaymentDetail WHERE OrderID = @OrderID)";

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

                        // Generate star icons based on rating score
                        for (int i = 0; i < score; i++)
                        {
                            phStars.Controls.Add(new Literal
                            {
                                Text = "<i class='fa fa-star'></i>" // FontAwesome star icon (or use your own star image)
                            });
                        }

                        // Add empty stars if the rating is less than 5
                        for (int i = score; i < 5; i++)
                        {
                            phStars.Controls.Add(new Literal
                            {
                                Text = "<i class='fa fa-star-o'></i>" // FontAwesome empty star icon
                            });
                        }

                        // Show the review section
                        reviewSection.Visible = true;
                    }
                }
            }
        }

        protected void TrackOrderButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string orderId = button.CommandArgument;
            Response.Redirect($"OrderTracking.aspx?OrderID={orderId}");
        }

        protected void RatingButton_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button ratingButton = (Button)sender;

            // Get the OrderID from the CommandArgument
            string orderId = ratingButton.CommandArgument;

            // Redirect to the rating page, passing the OrderID as a query parameter
            Response.Redirect($"~/RatingReview/comment.aspx?OrderID={orderId}");
        }
    }
}
