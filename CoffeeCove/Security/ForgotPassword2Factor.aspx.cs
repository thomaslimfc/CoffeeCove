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
                // if no otp, go forgot password again
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

            if (userInputOtp == OTP_FP)
            {
                Session["statusTFA"] = true;

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
