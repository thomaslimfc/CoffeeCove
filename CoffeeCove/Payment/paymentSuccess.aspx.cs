using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Payment
{
    public partial class paymentSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Home/Home.aspx");
        }

        protected void btnOrderHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Order/orderHistory.aspx");
        }
    }
}