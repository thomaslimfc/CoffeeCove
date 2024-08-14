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
        decimal subtotal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request.QueryString["id"] ?? "";

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"SELECT * 
                            FROM OrderedItem I JOIN Product P 
                            ON I.ProductId = P.ProductId
                            JOIN Order O ON O.OrderId = I.OrderId
                            WHERE OrderId = @ID";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", "12345");
                conn.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                rptOrdered.DataSource = ds;
                rptOrdered.DataBind();


                conn.Close();
            }
        }

        protected void rptOrdered_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //assign label
            Label lblId = e.Item.FindControl("lblId") as Label;
            Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;
            string productId = lblId.Text;
            decimal quantity = Convert.ToDecimal(lblQuantity.Text);

            string id = Request.QueryString["id"] ?? "";

            SqlConnection conn = new SqlConnection(cs);
            string sql = @"SELECT * 
                            FROM OrderedItem OI JOIN Product P 
                            ON OI.ProductId = P.ProductId
                            JOIN Order O
                            ON O.OrderId = OI.OrderId
                            WHERE OrderId = @ID AND ProductId = @prodID";


            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", "12345");
            cmd.Parameters.AddWithValue("@prodID", productId);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            //get price of each items and add them up
            decimal linePrice = (decimal)dr["UnitPrice"]* (int)dr["Quantity"];
            subtotal += linePrice;

            lblSubtotal.Text = subtotal.ToString();





            conn.Close();
        }
    }
}