using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Order
{
    public partial class OrderInvoice : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderId = Request.QueryString["OrderID"];
                if (!string.IsNullOrEmpty(orderId))
                {
                    // Set the OrderID in the literal
                    OrderIdLiteral.Text = $"{orderId}";
                }
                else
                {
                    // Handle the case where no OrderID is passed
                    OrderIdLiteral.Text = "Invalid Order ID";
                }
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderTracking.aspx");
        }
    }
}