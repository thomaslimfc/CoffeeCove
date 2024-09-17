using PayPal.Api;
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
            var apiContext = GetApiContext();

            if (Request.QueryString["paymentId"] != null && Request.QueryString["PayerID"] != null)
            {
                var paymentId = Request.QueryString["paymentId"];
                var payerId = Request.QueryString["PayerID"];

                try
                {
                    // Execute the payment
                    var paymentExecution = new PaymentExecution() { payer_id = payerId };
                    var payment = new PayPal.Api.Payment() { id = paymentId };
                    var executedPayment = payment.Execute(apiContext, paymentExecution);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        Response.Redirect("~/Payment/paymentOpt.aspx");
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors
                    Response.Write(ex.Message);
                }
            }
            else
            {
                Response.Write("Payment not completed.");
            }
        }

        private APIContext GetApiContext()
        {
            var config = new Dictionary<string, string>
            {
                { "mode", System.Configuration.ConfigurationManager.AppSettings["PayPalMode"] }
            };

            var accessToken = new OAuthTokenCredential(
                System.Configuration.ConfigurationManager.AppSettings["PayPalClientId"],
                System.Configuration.ConfigurationManager.AppSettings["PayPalClientSecret"],
                config).GetAccessToken();

            return new APIContext(accessToken);
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