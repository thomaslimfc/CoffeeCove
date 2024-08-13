using System;
using System.Net;
using System.Web.UI;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;

namespace CoffeeCove.Security
{
    public partial class SignUp : Page
    {
        private const string ReCaptchaSecretKey = "6LdzASAqAAAAABzxRO667snXXntcj6L0-QDrxH_u"; // Replace with your Secret Key

        protected void Page_Load(object sender, EventArgs e)
        {
            // Your logic for Page_Load
        }

        protected void SignUpBtn_SU_Click(object sender, EventArgs e)
        {
            string recaptchaResponse = Request.Form["g-recaptcha-response"];
            bool isValidCaptcha = ValidateReCaptcha(recaptchaResponse);

            if (isValidCaptcha)
            {
                // Proceed with form submission
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
                dynamic jsonData = JsonConvert.DeserializeObject(jsonResult);
                return jsonData.success == "true";
            }
        }
        private bool IsUsernameTaken(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        protected void SignUpUsernameBtn_SU_Click(object sender, EventArgs e)
        {

        }

        protected void SignUpEmailBtn_SU_Click(object sender, EventArgs e)
        {

        }

        // custID
    }
}
