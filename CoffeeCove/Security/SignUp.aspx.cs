using System;
using System.Linq;
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
        }

        private bool IsUsernameAvailable(string username)
        {
            return !(db.Customers.Any(u => u.Username == username) ||
                     db.Admins.Any(u => u.Username == username));
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


        private bool IsContactNoAvailable(string contactNo)
        {
            return !db.Customers.Any(u => u.ContactNo == contactNo);
        }


        protected void ContactNo_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = IsContactNoAvailable(args.Value);
            if (!args.IsValid)
            {
                ContactNo_SU_customValidator.ErrorMessage = "Your contact number has been used.";
            }
        }


        protected void SignUpBtn_SU_Click(object sender, EventArgs e)
        {
            string recaptchaResponse = Request.Form["g-recaptcha-response"];
            bool isValidCaptcha = ValidateReCaptcha(recaptchaResponse);

            if (isValidCaptcha)
            {
                bool isUsernameValid = IsUsernameAvailable(Username_SU.Text);
                bool isEmailValid = IsEmailAvailable(EmailAdd_SU.Text);
                bool isContactNoValid = IsContactNoAvailable(ContactNo_SU.Text);

                if (isUsernameValid && isEmailValid)
                {
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
                    };

                    db.Customers.Add(newCust);
                    db.SaveChanges();

                    Response.Redirect("AccountRegistrationSuccess.aspx");
                }
                else
                {
                    if (!isUsernameValid)
                    {
                        Username_SU_customValidator.ErrorMessage = "Your username has been used.";
                        Username_SU_customValidator.IsValid = false;
                    }

                    if (!isEmailValid)
                    {
                        EmailAdd_SU_customValidator.ErrorMessage = "Your email has been used.";
                        EmailAdd_SU_customValidator.IsValid = false;
                    }

                    if (!isContactNoValid)
                    {
                        ContactNo_SU_customValidator.ErrorMessage = "This phone number has already been registered.";
                        ContactNo_SU_customValidator.IsValid = false;
                    }
                }
            }
            else
            {
                lblCaptchaError.Text = "Please complete by ticking the CAPTCHA.";
                lblCaptchaError.Visible = true;
            }
        }


        private bool ValidateReCaptcha(string recaptchaResponse)
        {
            string apiUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={ReCaptchaSecretKey}&response={recaptchaResponse}";
            using (var client = new System.Net.WebClient())
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
