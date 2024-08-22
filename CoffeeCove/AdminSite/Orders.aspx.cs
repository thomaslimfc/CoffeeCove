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
            lblOrderNo.Text = "1";
            lblDate.Text = "21/07/2024";
            lblAmount.Text = "RM66.70";
            lblDelPick.Text = "1";
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
            var o = db.Orders.SingleOrDefault(x => x.OrderID == int.Parse(orderId));
            
            if (o != null)
            {
                lblOrderNo.Text = o.OrderID.ToString();
                lblDate.Text = o.OrderDate.ToString();
                lblAmount.Text = o.TotalAmount.ToString();
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