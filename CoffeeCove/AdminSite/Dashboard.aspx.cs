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
            }
        }

        private void CalculateTodayRecords()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string sql = @"SELECT ISNULL(SUM(OI.Quantity), 0) AS TotalSales,
                                ISNULL(SUM(OP.TotalAmount), 0) AS TotalRevenue,  
                                ISNULL(COUNT(OP.OrderID), 0) AS TotalOrders 
                                FROM OrderedItem OI
                                JOIN OrderPlaced OP ON OI.OrderID = OP.OrderID
                                JOIN PaymentDetail PD ON OP.OrderID = PD.OrderID
                                WHERE PD.PaymentStatus = 'complete'
                                AND CAST(OP.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)";

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
    }
}