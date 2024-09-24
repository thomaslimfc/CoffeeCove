using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalculateTodayRecords();
                LoadDashboardData();
                BindTopSellingProducts();
            }
        }

        private void CalculateTodayRecords()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string sql = @"SELECT ISNULL(SUM(oi.Quantity), 0) AS TodaySales,
                                ISNULL(SUM(op.TotalAmount), 0) AS TodayRevenue,  
                                ISNULL(COUNT(op.OrderID), 0) AS TodayOrders 
                                FROM OrderedItem oi
                                JOIN OrderPlaced op ON oi.OrderID = op.OrderID
                                JOIN PaymentDetail pd ON op.OrderID = pd.OrderID
                                WHERE pd.PaymentStatus = 'complete'
                                AND CAST(op.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // total sales 
                            int salesRecord = reader.GetInt32(0);
                            lblSalesToday.Text = salesRecord.ToString();

                            // total revenue
                            decimal revenueRecord = reader.GetDecimal(1);
                            lblRevenueToday.Text = "RM " + revenueRecord.ToString("F2");

                            // total order 
                            int orderRecord = reader.GetInt32(2);
                            lblOrdersToday.Text = orderRecord.ToString();
                        }
                    }
                }
            }
        }

        private void LoadDashboardData()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Category", con))
                {
                    int totalCategories = (int)cmd.ExecuteScalar();
                    lblTotalCategory.Text = totalCategories.ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Product", con))
                {
                    int totalProducts = (int)cmd.ExecuteScalar();
                    lblTotalProduct.Text = totalProducts.ToString();
                }

                // Get total feedbacks
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Review", con))
                {
                    int totalFeedback = (int)cmd.ExecuteScalar();
                    lblTotalFeedback.Text = totalFeedback.ToString();
                }
            }
        }

        private void BindTopSellingProducts()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT TOP 5 p.ImageUrl, p.ProductName, p.UnitPrice, SUM(op.Quantity) AS TotalSold
                                FROM Product p JOIN OrderPlaced op ON p.ProductId = op.ProductId 
                                GROUP BY p.ImageUrl, p.ProductName, p.UnitPrice ORDER BY TotalSales DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptTopSellingProducts.DataSource = reader;
                    rptTopSellingProducts.DataBind();
                }
            }
        }

    }
}