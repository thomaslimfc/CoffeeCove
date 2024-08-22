using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Order
{
    public partial class OrderTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderId = Request.QueryString["OrderID"];
                if (!string.IsNullOrEmpty(orderId))
                {
                    // Set the OrderID in the label or literal
                    OrderIdLiteral.Text = $"#{orderId}";
                }
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            // Check if the UrlReferrer is not null and redirect
            if (Request.UrlReferrer != null)
            {
                // Additional check to prevent looping back to the same page
                string referrerUrl = Request.UrlReferrer.ToString();
                if (!referrerUrl.Contains("OrderTracking.aspx"))
                {
                    Response.Redirect(referrerUrl);
                }
                else
                {
                    // Fallback to orderHistory.aspx if the referrer is the current page
                    Response.Redirect("orderHistory.aspx");
                }
            }
            else
            {
                // Fallback if UrlReferrer is null
                Response.Redirect("orderHistory.aspx");
            }
        }

        protected void InvoiceButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderInvoice.aspx");
        }
    }
}