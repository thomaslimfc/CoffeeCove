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
        decimal subTotal = 0;
        decimal linePrice = 0;

        string orderId = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOrderNo.Text = "1";
            lblDate.Text = "22/7/2024 12:00:00 AM";
            lblDelPick.Text = "Pick Up";
            lblPaymentMethod.Text = "Cash";
            lblUsername.Text = "goldfishyyy";
            lblEmail.Text = "goldfizh@gmail.com";
            lblPickUp.Text = "CoffeeCove Karpal Singh";
            

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

                conn.Close();
            }
        }

        protected void rptOrdered_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            System.Web.UI.WebControls.Label lblQuantity = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblQuantity");
            int quantity = int.Parse(lblQuantity.Text);
            System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrice");
            decimal price = decimal.Parse(lblPrice.Text);
            System.Web.UI.WebControls.Label lblLineTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblLineTotal");

            linePrice = quantity * price;
            subTotal += linePrice;

            decimal tax = subTotal * (decimal)0.06;
            decimal total = subTotal + tax;

            lblSubtotal.Text = subTotal.ToString("C"); // "C" formats the number as currency
            lblTax.Text = tax.ToString("C");
            lblTotal.Text = total.ToString("C");
            lblLineTotal.Text = linePrice.ToString("C");
        }

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "viewOrder")
            {
                string orderId = (string)e.CommandArgument;
                LoadOrder(orderId);



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
            }
            else
            {
                lblOrderNo.Text = "Data not found.";
            }
        }
    }
}