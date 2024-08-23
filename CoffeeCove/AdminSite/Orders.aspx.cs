using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        dbCoffeeCoveEntities db = new dbCoffeeCoveEntities();
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOrderNo.Text = "1";
            lblDate.Text = "21/07/2024";
            lblAmount.Text = "RM66.70";
            lblDelPick.Text = "1";
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            // Define the SQL query with JOIN
            string query = @"SELECT [OrderID], [OrderDateTime], [TotalAmount], [DeliveryNo],
                            [PickUpNo], [UserName]
                            FROM [OrderPlaced]
                            INNER JOIN [Customer] ON OrderPlaced.CustomerID =
                            Customers.CustomerID";

            // Create a connection and data adapter
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    // Fill the DataTable with data
                    da.Fill(dt);

                    // Bind the DataTable to the GridView
                    gvOrder.DataSource = dt;
                    gvOrder.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log error or show message)
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "viewOrder")
            {
                string orderId = (string)e.CommandArgument;
                LoadOrder(orderId);

                lblOrderNo.Text = "1";
                lblDate.Text = "21/07/2024";
                lblAmount.Text = "RM66.70";
                lblDelPick.Text = "1";


            }
        }

        private void LoadOrder(string orderId)
        {
            int orderNum = int.Parse(orderId);
            var o = db.OrderPlaceds.SingleOrDefault(x => x.OrderID == orderNum);
            if (o != null)
            {
                lblOrderNo.Text = o.OrderID.ToString();
                lblDate.Text = o.OrderDateTime.ToString();
                lblAmount.Text = o.TotalAmount.ToString();
                lblDelPick.Text = o.DeliveryNo ?? o.PickUpNo ?? "N/A";
            }
            else
            {
                lblOrderNo.Text = "Data not found.";
            }
        }
    }
}