using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoffeeCove.Security
{
    public partial class SignUp : System.Web.UI.Page
    {
        dbCoffeeCoveEntities db = new dbCoffeeCoveEntities();
        private const string ReCaptchaSecretKey = "6LdzASAqAAAAABzxRO667snXXntcj6L0-QDrxH_u";

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (IsPostBack)
            //    {
            //        string target = Request["__EVENTTARGET"];
            //        if (target == Username_SU.ClientID)
            //        {
            //            ValidateUsernameServerSide();
            //            ValidateEmailAddServerSide();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log the error or handle it appropriately
            //    // For example: LogError(ex);
            //    throw;
            //}
        }


        private bool IsUsernameAvailable(string username)
        {
            return !(db.Customers.Any(u => u.Username == username) || db.Admins.Any(u => u.Username == username));
        }

        protected void Username_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = IsUsernameAvailable(args.Value);
            if (!args.IsValid)
            {
                Username_SU_customValidator.ErrorMessage = "Your username has been used.";
            }
        }

        private bool IsEmailAvailable(string email)
        {
            return !db.Customers.Any(u => u.EmailAddress == email);
        }

        protected void EmailAdd_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = IsEmailAvailable(args.Value);
            if (!args.IsValid)
            {
                EmailAdd_SU_customValidator.ErrorMessage = "Your email has been used.";
            }
        }

        //protected void ValidateUsernameServerSide()
        //{
        //    // Ensure Username_SU and UsernameErrorMessage are not null
        //    if (Username_SU != null && UsernameErrorMessage != null)
        //    {
        //        string username = Username_SU.Text;
        //        if (!IsUsernameAvailable(username))
        //        {
        //            UsernameErrorMessage.Text = "Your username has been used.";
        //            UsernameErrorMessage.Visible = true;
        //        }
        //        else
        //        {
        //            UsernameErrorMessage.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        // Handle the case where Username_SU or UsernameErrorMessage is null
        //        // Log an error or provide a fallback mechanism
        //    }
        //}


        //private void ValidateEmailAddServerSide()
        //{
        //    string email = EmailAdd_SU.Text;
        //    if (!IsEmailAvailable(email))
        //    {
        //        EmailAddErrorMessage.Text = "Your email has been used.";
        //        EmailAddErrorMessage.Visible = true;
        //    }
        //    else
        //    {
        //        EmailAddErrorMessage.Visible = false;
        //    }
        //}

        protected void SignUpBtn_SU_Click(object sender, EventArgs e)
        {
            string recaptchaResponse = Request.Form["g-recaptcha-response"];
            bool isValidCaptcha = ValidateReCaptcha(recaptchaResponse);

            if (isValidCaptcha)
            {
                if (IsUsernameAvailable(Username_SU.Text) && IsEmailAvailable(EmailAdd_SU.Text))
                {
                    // Generate verification token
                    string verificationToken = GenerateVerificationToken();

                    // Create a new user record (unverified)
                    var newCust = new Customer
                    {
                        Username = Username_SU.Text,
                        FirstName = FirstName_SU.Text,
                        LastName = LastName_SU.Text,
                        EmailAddress = EmailAdd_SU.Text,
                        HashedPassword = HashPassword(Password_SU.Text),
                        DateOfBirth = DateTime.Parse(DateOfBirth_PR.Text),
                        ContactNo = ContactNo_SU.Text,
                        Gender = Gender_SU.SelectedValue,
                        ResidenceState = location.SelectedValue,
                        // VerificationToken = verificationToken,
                        // VerificationStatus = false
                    };

                    db.Customers.Add(newCust);
                    db.SaveChanges();

                    // Send verification email
                    SendVerificationEmail(EmailAdd_SU.Text, verificationToken);

                    // Show confirmation message
                    lblEmailVerification.Text = "A verification email has been sent. Please verify your email.";
                    lblEmailVerification.Visible = true;
                }
                else
                {
                    //if (!IsUsernameAvailable(Username_SU.Text))
                    //{
                    //    UsernameErrorMessage.Text = "Your username has been used.";
                    //    UsernameErrorMessage.Visible = true;
                    //}

                    //if (!IsEmailAvailable(EmailAdd_SU.Text))
                    //{
                    //    lblCaptchaError.Text = "Your email has been used.";
                    //    lblCaptchaError.Visible = true;
                    //}
                }
            }
            else
            {
                lblCaptchaError.Text = "Please complete the CAPTCHA.";
                lblCaptchaError.Visible = true;
            }
        }

        private bool ValidateReCaptcha(string recaptchaResponse)
        {
            string apiUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={ReCaptchaSecretKey}&response={recaptchaResponse}";
            using (WebClient client = new WebClient())
            {
                string jsonResult = client.DownloadString(apiUrl);
                var jsonData = JsonConvert.DeserializeObject<JObject>(jsonResult);
                return jsonData["success"].Value<bool>();
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private string GenerateVerificationToken()
        {
            return Guid.NewGuid().ToString(); // Generate a unique token
        }

        private void SendVerificationEmail(string userEmail, string verificationToken)
        {
            var fromAddress = new MailAddress("tlfc2102@gmail.com", "Coffee Cove");
            var toAddress = new MailAddress(userEmail);
            const string fromPassword = "your-gmail-password"; // Use app password if 2FA is enabled
            const string subject = "Coffee Cove - Confirm your email";
            string verificationLink = "https://yourwebsite.com/VerifyEmail.aspx?token=" + verificationToken;
            string body = $"Dear User,<br/><br/>Please click the link below to verify your email:<br/><a href='{verificationLink}'>Verify Email</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
