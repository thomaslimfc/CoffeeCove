using System;
using System.Data.SqlClient;
using System.Web.UI;
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
                // Get the current OrderID
                int orderId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "OrderID"));

                // Find the nested repeater (ProductListRepeater)
                Repeater productListRepeater = (Repeater)e.Item.FindControl("ProductListRepeater");

                // Bind product data for this order
                BindProductList(orderId, productListRepeater);
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

        protected void TrackOrderButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string orderId = button.CommandArgument;
            Response.Redirect($"OrderTracking.aspx?OrderID={orderId}");
        }
    }
}
