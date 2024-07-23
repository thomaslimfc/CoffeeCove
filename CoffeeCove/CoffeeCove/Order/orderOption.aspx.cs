using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.orderOption
{
    public partial class orderOption : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbDelivery_Click(object sender, EventArgs e)
        {
            litTest.Text = "Delivery";
        }

        protected void lbPickUp_Click(object sender, EventArgs e)
        {
            litTest.Text = "Pick Up";
        }
    }
}