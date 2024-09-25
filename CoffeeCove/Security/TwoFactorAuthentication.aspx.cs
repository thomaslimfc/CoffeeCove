using System;
using System.Data.SqlClient;
using System.Net.PeerToPeer;
using System.Web.UI;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CoffeeCove.Security
{
    public partial class TwoFactorAuthentication : System.Web.UI.Page
    {
        // Twilio API credentials
        private const string AccountSid = "ACe289765a4a6cc720ac5cf5eb75123c3c";
        private const string AuthToken = "23c80b950edba3ed68ce646c57a0fc9b";
        private const string WhatsAppFromNumber = "whatsapp:+14155238886";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Send OTP only on the first page load
            {
                if (Session["2FARequired"] != null && (bool)Session["2FARequired"])
                {
                    string contactNo = Session["ContactNo"] as string;
                    string otpCode = GenerateOTP();

                    // Store the OTP in session
                    OTP = otpCode;

                    // Send the OTP via WhatsApp
                    SendOtpWhatsApp(contactNo, otpCode);
                }
                else
                {
                    // Redirect to sign-in if 2FA is not required
                    Response.Redirect("SignIn.aspx");
                }
            }
        }

        protected void VerifyButton_TFA_Click(object sender, EventArgs e)
        {
            // Get user input OTP from a TextBox (assumed to be named OTPInput)
            string userInputOtp = otp.Text;

            // Check if the input OTP matches the stored OTP
            if (userInputOtp == OTP)
            {
                // Mark 2FA as completed
                Session["2FARequired"] = false;

                // Get user role from session
                string username = Session["Username"] as string;
                string userRole = Session["UserRole"] as string;
                Boolean rememberMe = false;
                UserSecurity.LoginUser(username, userRole, rememberMe);
                
            }
            else
            {
                lblWrongOtp.Text = "OTP code entered is incorrect.";
                lblWrongOtp.Visible = true;
            }
        }

        private string OTP
        {
            get { return (string)Session["OTP"]; }
            set { Session["OTP"] = value; }
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
                    body: $"Your Coffee Cove OTP is: {otpCode}",
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
    }
}
