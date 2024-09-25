using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using CoffeeCove.Master;
using PayPal.Log;
using System;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web.UI;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CoffeeCove.Security
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        // Twilio API credentials
        private const string AccountSid = "ACe289765a4a6cc720ac5cf5eb75123c3c";
        private const string AuthToken = "23c80b950edba3ed68ce646c57a0fc9b";
        private const string WhatsAppFromNumber = "whatsapp:+14155238886";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string contactNo = ContactNo_FP.Text;

                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    // Check for a matching customer
                    var customer = db.Customers.SingleOrDefault(c => c.ContactNo == contactNo);

                    // Check for a matching admin
                    var admin = db.Admins.SingleOrDefault(a => a.ContactNo == contactNo);

                    // If not identity of customer or admin
                    if (customer != null || admin != null)
                    {
                        Session["ContactNo_FP"] = contactNo;

                        if (customer != null)
                        {
                            Session["Username_FP"] = customer.Username;
                            Session["CusID_FP"] = customer.CusID;
                        }
                    }
                    else
                    {
                        // cannot access to 2-factor
                    }
                }
            }
        }

        protected void NextBtn_FP_Click(object sender, EventArgs e)
        {
            string contactNo = ContactNo_FP.Text;

            if (!string.IsNullOrEmpty(contactNo))
            {
                string otpCode = GenerateOTP();

                // Store the OTP in session
                OTP_FP = otpCode;

                // Send the OTP via WhatsApp
                SendOtpWhatsApp(contactNo, otpCode);
                lblMessage.Text = "OTP has been sent to your registered contact number.";

                Response.Redirect("ForgotPassword2Factor.aspx");
            }
            else
            {
                lblMessage.Text = "Contact number is required.";
            }
        }

        private string OTP_FP
        {
            get { return (string)Session["OTP_FP"]; }
            set { Session["OTP_FP"] = value; }
        }

        private string GenerateOTP()
        {
            Random randOtp = new Random();
            return randOtp.Next(100000, 999999).ToString(); // Generate a 6-digit OTP
        }

        // Method to send OTP via WhatsApp using Twilio
        protected void SendOtpWhatsApp(string phoneNumber, string otpCode)
        {
            TwilioClient.Init(AccountSid, AuthToken);

            string cleanPhoneNumber = phoneNumber.Replace("-", "").Trim();
            string formattedPhoneNumber = $"whatsapp:+6{cleanPhoneNumber}";

            // Log the phone number being used
            Console.WriteLine($"Sending OTP to: {formattedPhoneNumber}");

            try
            {
                var message = MessageResource.Create(
                    body: $"Your Coffee Cove OTP for password reset is: {otpCode}",
                    from: new PhoneNumber(WhatsAppFromNumber),
                    to: new PhoneNumber(formattedPhoneNumber)
                );

                Console.WriteLine($"WhatsApp OTP sent successfully. Message SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending OTP: {ex.Message}");
            }
        }
        protected void BackBtn_FP_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignIn.aspx");
        }
    }
}
