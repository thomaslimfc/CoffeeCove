using System;
using System.Linq;
using System.Web.UI;

namespace CoffeeCove.Security
{
    public partial class PasswordReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is authorized to reset the password (e.g., session validation)
                if (Session["CusID_FP"] == null)
                {
                    Response.Redirect("SignIn.aspx");
                }
            }
        }

        protected void ResetPasswordBtn_PR_Click(object sender, EventArgs e)
        {
            string newPassword = Password_PR.Text;
            string confirmPassword = PasswordConfirm_PR.Text;

            // Ensure passwords match
            if (newPassword != confirmPassword)
            {
                // Show an error message (handled by CompareValidator)
                return;
            }

            string cusID = Session["CusID_FP"].ToString(); // Get the customer ID from session

            // Update the password in the database
            bool isUpdated = UpdatePassword(cusID, newPassword);
            if (isUpdated)
            {
                // Store the username in session
                string username = GetUsernameByCusID(cusID);
                if (!string.IsNullOrEmpty(username))
                {
                    Session["Username"] = username; // Store username in session
                }

                Response.Redirect("PasswordResetSuccess.aspx");
            }
            else
            {
                // Handle error updating password (e.g., show an error message)
                // You might want to use a label to display the error
            }
        }

        private bool UpdatePassword(string cusID, string newPassword)
        {
            try
            {
                // Hash the password and update in the database
                string hashedPassword = HashPassword(newPassword);

                using (var db = new dbCoffeeCoveEntities())
                {
                    // Find the customer by CusID and update their password
                    var customer = db.Customers.SingleOrDefault(c => c.CusID.ToString() == cusID);
                    if (customer != null)
                    {
                        customer.HashedPassword = hashedPassword; // Update password
                        db.SaveChanges(); // Save changes to the database
                        return true; // Return true if update was successful
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                System.Diagnostics.Debug.WriteLine($"Error updating password: {ex.Message}");
            }
            return false; // Return false if there was an error
        }

        private string GetUsernameByCusID(string cusID)
        {
            using (var db = new dbCoffeeCoveEntities())
            {
                // Fetch the customer based on CusID
                var customer = db.Customers.SingleOrDefault(c => c.CusID.ToString() == cusID);
                return customer?.Username; // Return the username or null if not found
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
    }
}
