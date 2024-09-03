using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Payment
{
    public partial class paymentOpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnCreditCardPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Order/orderHistory.aspx");
        }

        protected void btnCOD_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Order/orderHistory.aspx");
        }

        protected void btnPaypal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Order/orderHistory.aspx");
        }
    }
}