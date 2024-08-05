using System;
using System.Net;
using System.Web.UI;
using Newtonsoft.Json;

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
    }
}
