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
        string strSubtotal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request.QueryString["id"] ?? "";

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
                    Response.Redirect("../Menu/Menu.aspx?id=" + id);
                }

                conn.Close();
            }
        }
        protected void rptOrdered_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            strSubtotal = "50";


            lblSubtotal.Text = "WOW";


            //assign label
            Label lblId = (Label)e.Item.FindControl("lblId");
            Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
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
            decimal linePrice = (decimal)dr["UnitPrice"] * 5;
            strSubtotal = "50";


            lblSubtotal.Text = strSubtotal;

            conn.Close();
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] ?? "";
            Response.Redirect("../Payment/paymentOpt.aspx?id=" + id);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] ?? "";
            Response.Redirect("cartEdit.aspx?id=" + id);
        }
    }
}