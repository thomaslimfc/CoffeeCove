using System;
using System.Net;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CoffeeCove.Securities;
using System.Net.Mail;
using System.Linq;
using System.Web.UI.WebControls;
using System.EnterpriseServices;

namespace CoffeeCove.Security
{
    public partial class SignUp : System.Web.UI.Page
    {
        dbCoffeeCoveEntities db = new dbCoffeeCoveEntities();
        private const string ReCaptchaSecretKey = "6LdzASAqAAAAABzxRO667snXXntcj6L0-QDrxH_u";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string target = Request["__EVENTTARGET"];
                if (target == Username_SU.ClientID)
                {
                    // Handle server-side validation for Username_SU
                    ValidateUsernameServerSide();
                    ValidateEmailAddServerSide();
                }
            }
        }

        private bool IsUsernameAvailable(string username)
        {
            return !(db.Customers.Any(u => u.Username == username) || db.Admins.Any(u => u.Username == username));
        }

        private bool IsEmailAvailable(string email)
        {
            return !db.Customers.Any(u => u.EmailAddress == email);
        }

        // must key in password, then only reenteer

        private void ValidateUsernameServerSide()
        {
            string username = Username_SU.Text;
            if (!IsUsernameAvailable(username))
            {
                UsernameErrorMessage.Text = "Your username has been used.";
                UsernameErrorMessage.Visible = true;
            }
            else
            {
                UsernameErrorMessage.Visible = false;
            }
        }

        private void ValidateEmailAddServerSide()
        {
            string email = EmailAdd_SU.Text;
            if (!IsEmailAvailable(email))
            {
                UsernameErrorMessage.Text = "Your email has been used.";
                UsernameErrorMessage.Visible = true;
            }
            else
            {
                UsernameErrorMessage.Visible = false;
            }
        }

        // click into txtbox, then after touch other region auto check aaibalility
        protected void Username_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string username = args.Value;

            // Check if the username exists in the database
            if (!IsUsernameAvailable(username))
            {
                args.IsValid = false; // Username already exists
                UsernameErrorMessage.Text = "Your username has been used.";
                UsernameErrorMessage.Visible = true;
            }
            else
            {
                args.IsValid = true; // Username is available
                UsernameErrorMessage.Visible = false;
            }
        }

        protected void EmailAdd_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value;

            // Check if the email address exists in the database
            if (!IsEmailAvailable(email))
            {
                args.IsValid = false; // Email already exists
                EmailAddErrorMessage.Text = "Your email has been used.";
                EmailAddErrorMessage.Visible = true;
            }
            else
            {
                args.IsValid = true; // Email is available to register
                EmailAddErrorMessage.Visible = false;
            }
        }

        protected void SignUpBtn_SU_Click(object sender, EventArgs e)
        {
            string recaptchaResponse = Request.Form["g-recaptcha-response"];
            bool isValidCaptcha = ValidateReCaptcha(recaptchaResponse);

            if (isValidCaptcha)
            {
                if (IsUsernameAvailable(Username_SU.Text) && 
                    IsEmailAvailable(EmailAdd_SU.Text))
                {
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
                        ResidenceState = location.SelectedValue
                    };

                    // before save , email confirmation first
                    // ConfirmationEmail(newCust.EmailAddress)


                    db.Customers.Add(newCust);
                    db.SaveChanges();

                    Response.Redirect("SignIn.aspx");
                } else
                {
                    // If username or email is not available, handle the error messages
                    if (!IsUsernameAvailable(Username_SU.Text))
                    {
                        UsernameErrorMessage.Text = "Your username has been used.";
                        UsernameErrorMessage.Visible = true;
                    }

                    if (!IsEmailAvailable(EmailAdd_SU.Text))
                    {
                        // Email already registered alert will be handled in IsEmailAvailable
                        lblCaptchaError.Text = "Your email has been used.";
                        lblCaptchaError.Visible = true;
                    }
                }
            }
            else
            {
                // Show an CAPTCHA error message
                lblCaptchaError.Text = "Click at empty box beside I'm not a robot";
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
                bool success = jsonData["success"].Value<bool>(); // Access 'success' as a bool
                return success;
            }
        }

        private string HashPassword(string password)
        {
            // just using SHA256.
            // library like BCrypt.Net for stronger security.

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        protected void SignUpUsernameBtn_SU_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignIn.aspx");
        }

        protected void SignUpEmailBtn_SU_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignInWithEmail.aspx");
        }
    }
}
