using PayPal.Api;
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
            // Check if all the validators on the page are valid
            if (Page.IsValid)
            {
                // If valid, redirect to payment success page
                Response.Redirect("~/Payment/paymentSuccess.aspx");
            }
        }

        protected void btnCOD_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Payment/paymentSuccess.aspx");
        }

        protected void btnPaypal_Click(object sender, EventArgs e)
        {
            var apiContext = GetApiContext();

            try
            {
                // Create a PayPal payment
                var payment = CreatePayment(apiContext, Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/paymentSuccess.aspx");

                // Redirect the user to PayPal for approval
                var approvalUrl = payment.GetApprovalUrl();
                Response.Redirect(approvalUrl);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Response.Write(ex.Message);
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

        // Create a PayPal payment
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var payer = new Payer() { payment_method = "paypal" };

            var itemList = new ItemList()
            {
                items = new List<Item>()
                {
                    new Item()
                    {
                        name = "Coffee Purchase",
                        currency = "USD",
                        price = "5.00",
                        quantity = "1",
                        sku = "COFFEE001"
                    }
                }
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = "5.00"
            };

            var transaction = new Transaction()
            {
                description = "Coffee purchase from Coffee Cove",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            };

            var payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = new List<Transaction>() { transaction },
                redirect_urls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "?cancel=true",
                    return_url = redirectUrl
                }
            };

            return payment.Create(apiContext);
        }
    }
}