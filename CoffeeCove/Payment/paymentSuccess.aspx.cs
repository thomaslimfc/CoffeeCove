using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Web.UI;

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
                        ShowErrorMessage("Payment could not be completed. Please try again.");
                        Response.Redirect("~/Payment/paymentOpt.aspx");
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors by showing a client-side error message prompt
                    ShowErrorMessage("An error occurred while processing your payment: " + ex.Message);
                }
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

        // Method to show client-side error message using ScriptManager
        private void ShowErrorMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showError", $"showError('{message}');", true);
        }
    }
}
