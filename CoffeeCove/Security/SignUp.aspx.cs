using System;
using System.Net;
using System.Web.UI;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;
using System.Linq;
using CoffeeCove.Securities;

namespace CoffeeCove.Security
{
    public partial class SignUp : Page
    {
        dbCoffeeCoveEntities db = new dbCoffeeCoveEntities();

        private const string ReCaptchaSecretKey = "6LdzASAqAAAAABzxRO667snXXntcj6L0-QDrxH_u"; // Replace with your Secret Key

        protected void Page_Load(object sender, EventArgs e)
        {
            // Your logic for Page_Load
        }

        protected void Username_SU_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //string username = args.Value;

            //// Check if the username exists in the database
            //if (db.Customers.Any(u => u.Username == username))
            //{
            //    args.IsValid = true; // Username found, validation succeeds
            //    UsernameErrorMessage2.Visible = false;
            //}
            //else
            //{
            //    args.IsValid = false; // Username not found, validation fails
            //    UsernameErrorMessage2.Text = "Username is not found in the database.";
            //    UsernameErrorMessage2.Visible = true;
            //}
        }

        protected void SignUpBtn_SU_Click(object sender, EventArgs e)
        {
            string recaptchaResponse = Request.Form["g-recaptcha-response"];
            bool isValidCaptcha = ValidateReCaptcha(recaptchaResponse);

            if (isValidCaptcha)
            {
                Response.Redirect("SignIn.aspx");
            }
            else
            {
                // Show an error message
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

        //private bool IsUsernameTaken(string username)
        //{
        //    string cs = Global.CS;

        //    using (SqlConnection connection = new SqlConnection(cs))
        //    {
        //        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Username", username);
        //            connection.Open();
        //            int count = (int)command.ExecuteScalar();
        //            return count > 0;
        //        }
        //    }
        //}

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
