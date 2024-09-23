using System;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web.UI;

namespace CoffeeCove.Security
{
    public partial class ForgotPassword2Factor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if OTP session exists; redirect if not
                if (Session["OTP_FP"] == null)
                {
                    Response.Redirect("ForgotPassword.aspx");
                }
            }
        }

        protected void VerifyButton_FPTF_Click(object sender, EventArgs e)
        {
            string userInputOtp = otp_FPTF.Text; // Get the user input OTP

            if (userInputOtp == OTP_FP)
            {
                // OTP is correct, proceed to reset password
                Response.Redirect("PasswordReset.aspx");
            }
            else
            {
                // Display an error message for incorrect OTP
                lblWrongOtp.Text = "The OTP entered is incorrect.";
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
