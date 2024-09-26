using System;

namespace CoffeeCove.Security
{
    public partial class ForgotPassword2Factor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the OTP session variable is set
                if (Session["OTP_FP"] == null)
                {
                    lblWrongOtp.Text = "Session expired, please request a new OTP.";
                    lblWrongOtp.Visible = true;
                    Response.Redirect("ForgotPassword.aspx");
                }
            }
        }


        protected void VerifyButton_FPTF_Click(object sender, EventArgs e)
        {
            string userInputOtp = otp_FPTF.Text.Trim();

            // Check if session OTP is available
            if (Session["OTP_FP"] == null)
            {
                lblWrongOtp.Text = "Session expired, please request a new OTP.";
                lblWrongOtp.Visible = true;
                Response.Redirect("ForgotPassword.aspx");
                return; // Exit to avoid further execution
            }

            // Verify the OTP entered by the user
            if (userInputOtp == OTP_FP)
            {
                // Mark the status of 2FA as true
                Session["statusTFA"] = true;

                // Redirect to the password reset page
                Response.Redirect("PasswordReset.aspx");
            }
            else
            {
                // Show error message if OTP is incorrect
                lblWrongOtp.Text = "The OTP entered is incorrect." + Session["Username_FP"] + "22";
                lblWrongOtp.Visible = true;
            }
        }


        private string OTP_FP
        {
            get { return (string)Session["OTP_FP"]; }
            set { Session["OTP_FP"] = value; }
        }
    }
}
