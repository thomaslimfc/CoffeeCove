using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {

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
            Order o = db.Orders.SingleOrDefault(x => x.OrderId == orderId);

            if (o != null)
            {
                lblOrderNo.Text = orderId;
                lblDate.Text = o.OrderDate;
                lblAmount.Text = o.TotalAmount;
                if(o.DeliveryNo != null)
                {
                    lblDelPick.Text = o.DeliveryNo;
                }
                else if(o.PickUpNo != null)
                {
                    lblDelPick.Text = o.PickUpNo;
                }
            }
            else
            {
                lblOrderNo.Text = "Data not found.";
            }
        }
    }
}