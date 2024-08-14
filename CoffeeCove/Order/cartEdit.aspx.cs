using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Order
{
    public partial class cartEdit : System.Web.UI.Page
    {
        string cs = Global.CS;
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

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] ?? "";
            Response.Redirect("cart.aspx?id=" + id);
        }


        protected void rptOrdered_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {
                // Retrieve the ID of the item to edit
                string productId = e.CommandArgument.ToString();

                string id = Request.QueryString["id"] ?? "";

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"DELETE FROM OrderedItem
                            WHERE OrderId = @ID AND ProductID = @prodID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", "12345");
                cmd.Parameters.AddWithValue("@prodID", productId);
                conn.Open();

                cmd.ExecuteNonQuery();

                Response.Redirect("cartEdit.aspx");


                conn.Close();

            }
        }
    }
}