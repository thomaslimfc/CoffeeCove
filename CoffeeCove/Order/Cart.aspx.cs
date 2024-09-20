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
        decimal subTotal = 0;
        decimal linePrice = 0;

        string orderId = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //string orderId = Session["OrderId"].ToString();

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"SELECT * 
                            FROM OrderedItem I JOIN Product P 
                            ON I.ProductId = P.ProductId
                            WHERE OrderId = @ID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", "1");
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
                cmd1.Parameters.AddWithValue("@id", "1");
                int count = (int)cmd1.ExecuteScalar();

                //if no record for this order
                if (count <= 0)
                {
                    Response.Redirect("../Menu/Menu.aspx");
                }

                

                

                conn.Close();
            }

            

        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            //save total into the db


            //string orderId = Session["OrderId"].ToString();
            Response.Redirect("../Payment/paymentOpt.aspx?id=" + orderId);
        }

        
        protected void rptOrdered_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
            int quantity = int.Parse(lblQuantity.Text);
            Label lblPrice = (Label)e.Item.FindControl("lblPrice");
            decimal price = decimal.Parse(lblPrice.Text);
            Label lblLineTotal = (Label)e.Item.FindControl("lblLineTotal");

            Label lblSize = (Label)e.Item.FindControl("lblSize");

            Panel panelSize = (Panel)e.Item.FindControl("panelSize");
            Panel panelFlavour = (Panel)e.Item.FindControl("panelFlavour");
            Panel panelIce = (Panel)e.Item.FindControl("panelIce");
            Panel panelAddon = (Panel)e.Item.FindControl("panelAddon");

            if (string.IsNullOrEmpty(lblSize.Text))
            {
                panelSize.Visible = false;
                panelFlavour.Visible = false;
                panelIce.Visible = false;
                panelAddon.Visible = false;
            }
            

            linePrice = quantity * price;
            subTotal += linePrice;

            decimal tax = subTotal * (decimal)0.06;
            decimal total = subTotal + tax;

            lblSubtotal.Text = subTotal.ToString("C"); // "C" formats the number as currency
            lblTax.Text = tax.ToString("C");
            lblTotal.Text = total.ToString("C");
            lblLineTotal.Text = linePrice.ToString("C");
        }

        protected void rptOrdered_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {

                // Retrieve the ID of the item to edit
                string orderedItemID = e.CommandArgument.ToString();

                //string id = Request.QueryString["id"] ?? "";

                SqlConnection conn = new SqlConnection(cs);
                string sql = @"DELETE FROM OrderedItem
                            WHERE OrderedItemID = @orderedItemID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@orderedItemID", orderedItemID);
                conn.Open();

                cmd.ExecuteNonQuery();

                //Response.Redirect("cart.aspx");

                string sql1 = @"SELECT COUNT(*) 
                            FROM OrderedItem I JOIN Product P 
                            ON I.ProductId = P.ProductId
                            WHERE OrderId = @id";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@id", "1");
                int count = (int)cmd1.ExecuteScalar();

                //if no record for this order
                if (count <= 0)
                {
                    Response.Redirect("../Menu/Menu.aspx");
                }




                conn.Close();

            }
        }
    }
}