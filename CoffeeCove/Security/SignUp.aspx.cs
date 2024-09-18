using System;
using System.Net;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CoffeeCove.Securities;
using System.Net.Mail;
using System.Linq;
using System.Web.UI.WebControls;

namespace CoffeeCove.Security
{
    public partial class SignUp : System.Web.UI.Page
    {
        dbCoffeeCoveEntities db = new dbCoffeeCoveEntities();

        private const string ReCaptchaSecretKey = "6LdzASAqAAAAABzxRO667snXXntcj6L0-QDrxH_u";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool IsUsernameAvailable(string username)
        {
            return !(db.Customers.Any(u => u.Username == username) || db.Admins.Any(u => u.Username == username));
        }

        protected void Username_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string username = args.Value;

            // Check if the username exists in the database
            if (db.Customers.Any(u => u.Username == username) || db.Admins.Any(u => u.Username == username))
            {
                args.IsValid = false; // Username already exists
                UsernameErrorMessage2.Text = "Your username has been used.";
                UsernameErrorMessage2.Visible = true;
            }
            else
            {
                args.IsValid = true; // Username is available
                UsernameErrorMessage2.Visible = false;
            }
        }

        private bool IsEmailAvailable(string email)
        {
            return !db.Customers.Any(u => u.EmailAddress == email);
        }

        protected void EmailAdd_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value;

            // Check if the email address exists in the database
            if (db.Customers.Any(u => u.EmailAddress == email))
            {
                args.IsValid = false; // Email already exists
                UsernameErrorMessage2.Text = "Your email has been used.";
                UsernameErrorMessage2.Visible = true;
            }
            else
            {
                args.IsValid = true; // Email is available to register
                UsernameErrorMessage2.Visible = false;
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
            // Placeholder: implement a real password hashing mechanism
            // This is just an example using SHA256. You should ideally use a library like BCrypt.Net for stronger security.
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // check duplicate gmail


        protected void SignUpUsernameBtn_SU_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignIn.aspx");
        }

        protected void SignUpEmailBtn_SU_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignInWithEmail.aspx");
        }

        // custID
    }
}
