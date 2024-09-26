using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
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
                BindMonthlyRevenue();
                BindTopSellingProducts();
            }
        }

        private void CalculateTodayRecords()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(@"SELECT ISNULL(SUM(oi.Quantity), 0) AS TotalSales   
                                                        FROM OrderPlaced op JOIN PaymentDetail pd ON op.OrderID = pd.OrderID
                                                        JOIN OrderedItem oi ON op.OrderID = oi.OrderID  
                                                        WHERE CAST(op.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)  
                                                        AND pd.PaymentStatus = 'Complete'", con))
                {
                    int totalSales = (int)cmd.ExecuteScalar();
                    lblSalesToday.Text = totalSales.ToString();
                }

                using (SqlCommand cmd = new SqlCommand(@"SELECT ISNULL(SUM(op.TotalAmount), 0) AS TotalRevenues   
                                                        FROM OrderPlaced op
                                                        JOIN PaymentDetail pd ON op.OrderID = pd.OrderID 
                                                        WHERE CAST(op.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)  
                                                        AND pd.PaymentStatus = 'Complete'", con))
                {
                    decimal totalRevenues = (decimal)cmd.ExecuteScalar();
                    lblRevenueToday.Text = totalRevenues.ToString("C");
                }

                using (SqlCommand cmd = new SqlCommand(@"SELECT COUNT(DISTINCT op.OrderID) AS TotalOrders 
                                                        FROM OrderPlaced op 
                                                        JOIN PaymentDetail pd ON op.OrderID = pd.OrderID 
                                                        WHERE CAST(op.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)", con))
                {
                    int totalOrders = (int)cmd.ExecuteScalar();
                    lblOrdersToday.Text = totalOrders.ToString();
                }
            }
        }

        private void LoadDashboardData()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // total category
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Category", con))
                {
                    int totalCategories = (int)cmd.ExecuteScalar();
                    lblTotalCategory.Text = totalCategories.ToString();
                }
                // total product
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Product", con))
                {
                    int totalProducts = (int)cmd.ExecuteScalar();
                    lblTotalProduct.Text = totalProducts.ToString();
                }
                // total feedback
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Review", con))
                {
                    int totalFeedback = (int)cmd.ExecuteScalar();
                    lblTotalFeedback.Text = totalFeedback.ToString();
                }
            }
        }

        private void BindMonthlyRevenue()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"WITH AllMonths AS (  
                                SELECT 1 AS OrderMonth UNION ALL  
                                SELECT 2 UNION ALL  
                                SELECT 3 UNION ALL  
                                SELECT 4 UNION ALL  
                                SELECT 5 UNION ALL  
                                SELECT 6 UNION ALL  
                                SELECT 7 UNION ALL  
                                SELECT 8 UNION ALL  
                                SELECT 9 UNION ALL  
                                SELECT 10 UNION ALL  
                                SELECT 11 UNION ALL  
                                SELECT 12  
                            )  
                            SELECT am.OrderMonth, COALESCE(YEAR(op.OrderDateTime), YEAR(GETDATE())) AS OrderYear, COALESCE(SUM(op.TotalAmount), 0) AS TotalRevenue  
                            FROM AllMonths am 
                            LEFT JOIN OrderPlaced op ON MONTH(op.OrderDateTime) = am.OrderMonth  
                            LEFT JOIN PaymentDetail p ON op.OrderID = p.OrderID AND p.PaymentStatus = 'Complete'  
                            WHERE (op.OrderDateTime IS NULL OR p.PaymentStatus = 'Complete') 
                            GROUP BY am.OrderMonth, YEAR(op.OrderDateTime)  
                            ORDER BY OrderYear, am.OrderMonth;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvMonthlyRevenue.DataSource = reader;
                    gvMonthlyRevenue.DataBind();
                }
            }
        }

        private void BindTopSellingProducts()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT TOP 5 p.ImageUrl, p.ProductName, p.UnitPrice, SUM(oi.Quantity) AS TotalSold, SUM(oi.Quantity * p.UnitPrice) AS TotalSales
                                FROM Product p JOIN OrderedItem oi ON p.ProductId = oi.ProductId JOIN OrderPlaced op ON oi.OrderID = op.OrderID JOIN PaymentDetail pd ON op.OrderID = pd.OrderID
                                WHERE pd.PaymentStatus = 'Complete'
                                GROUP BY p.ImageUrl, p.ProductName, p.UnitPrice ORDER BY TotalSales DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        gvTopSellingProducts.DataSource = reader;
                        gvTopSellingProducts.DataBind();
                    }
                    else
                    {
                        // if no data show EmptyDataText
                        gvTopSellingProducts.DataBind();
                    }
                }
            }
        }
    }
}