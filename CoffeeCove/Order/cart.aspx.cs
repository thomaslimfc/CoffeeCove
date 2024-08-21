using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace CoffeeCove.Order
{
    public partial class orderCart : System.Web.UI.Page
    {
        string cs = Global.CS;
        double subTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string orderId = Session["OrderId"].ToString();

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"SELECT * 
                            FROM OrderedItem I JOIN Product P 
                            ON I.ProductId = P.ProductId
                            WHERE OrderId = @ID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", "12345");
                conn.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                rptOrdered.DataSource = ds;
                rptOrdered.DataBind();

                string sql1 = @"SELECT COUNT(*) 
                            FROM OrderedItem I JOIN Product P 
                            ON I.ProductId = P.ProductId
                            WHERE OrderId = @id";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@id", "12345");
                int count = (int)cmd1.ExecuteScalar();


                //if no record for this order
                if (count <= 0)
                {
                    Response.Redirect("../Menu/Menu.aspx");
                }

                conn.Close();
            }

            

        }
        protected void rptOrdered_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
            
            

        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            string orderId = Session["OrderId"].ToString();
            Response.Redirect("../Payment/paymentOpt.aspx?id=" + orderId);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string orderId = Session["OrderId"].ToString();
            Response.Redirect("cartEdit.aspx?id=" + orderId);
        }

        protected void rptOrdered_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Assign label controls
            Label lblId = (Label)e.Item.FindControl("lblId");
            string productId = lblId.Text;

            // Get the Order ID from the query string
            string orderId = Session["OrderId"].ToString();
            SqlConnection conn = new SqlConnection(cs);
            // SQL query to get the product details for the specific order and product ID
            string sql = @"SELECT P.UnitPrice 
                   FROM OrderedItem OI 
                   JOIN Product P ON OI.ProductId = P.ProductId
                   JOIN Order O ON O.OrderId = OI.OrderId
                   WHERE O.OrderId = @ID AND P.ProductId = @prodID";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", "12345");
            cmd.Parameters.AddWithValue("@prodID", productId);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                // Calculate the line price based on the UnitPrice and quantity

                double linePrice = (double)dr["UnitPrice"] * (int)dr["Quantity"];

                // Add the line price to the subtotal
                subTotal += linePrice;
                lblSubtotal.Text = "X";
            }
            else
            {
                // Handle the case where no data is found
                lblSubtotal.Text = "X";
            }

            // Display the subtotal
            lblSubtotal.Text = subTotal.ToString("C"); // "C" formats the number as currency
        }
    }
}