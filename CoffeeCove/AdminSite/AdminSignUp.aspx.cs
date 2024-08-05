//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data.SqlClient;
//using BCrypt.Net;
//using System.Security.Cryptography;
//using System.Text;
//using System.EnterpriseServices;
//using System.Diagnostics.Eventing.Reader;

//// PACKAGE NEEDED TO BE INSTALLED HERE: BCrypt.Net-Next

//namespace CoffeeCove.AdminSite
//{
//    public partial class SignUp : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            // Handle any actions needed during page load
//        }

//        protected void SignUp_btn_Click(object sender, EventArgs e)
//        {
//            // Retrieve user inputs
//            string username = adminUsername.Text;
//            string password = adminPassword.Text;
//            string gender = adminGender.SelectedValue;
//            string branch = location.SelectedValue;

//            // Hash the password
//            string hashedPassword = HashPassword(password);

//            // Save user data to the database
//            if (SaveToDatabase(username, hashedPassword, gender, branch))
//            {
//                // Store the username in session for OTP verification
//                Session["username"] = username;

//                // Show success message
//                ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage('Registration successful. Redirecting to Two-Factor Authentication...');", true);
//                System.Threading.Thread.Sleep(2000);

//                // Redirect to Two-Factor Authentication page
//                Response.Redirect("AdminTwoFactorAuthentication.aspx?action=signup&username=" + username);
//            }
//            else
//            {
//                // Show error message if registration fails
//                ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage('Registration failed. Please try again.');", true);
//            }
//        }

//        private bool SaveToDatabase(string username, string hashedPassword, string gender, string branch)
//        {
//            try
//            {
//                // Define the connection string (update as needed for your environment)
//                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\# GitHub Local Repository\\CoffeeCove\\CoffeeCove\\App_Data\\dbCoffeeCove.mdf\";Integrated Security=True";

//                // Define the query to insert a new admin record
//                string query = "INSERT INTO Admin (Username, PasswordHash, Gender, Branch) VALUES (@Username, @EmailAddress, @PasswordHash, @Gender, @Branch)";

//                using (SqlConnection connection = new SqlConnection(connectionString))
//                {
//                    using (SqlCommand command = new SqlCommand(query, connection))
//                    {
//                        // Add parameters to the query
//                        command.Parameters.AddWithValue("@Username", username);
//                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
//                        command.Parameters.AddWithValue("@Gender", gender);
//                        command.Parameters.AddWithValue("@Branch", branch);

//                        // Open the connection
//                        connection.Open();

//                        // Execute the query
//                        int result = command.ExecuteNonQuery();

//                        // Check if the insert was successful
//                        return result > 0;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (for example, write to a log file or event viewer)
//                LogException(ex);
//                return false;
//            }
//        }

//        private string HashPassword(string password)
//        {
//            // Hash the password using bcrypt
//            return BCrypt.Net.BCrypt.HashPassword(password);
//        }

//        private void LogException(Exception ex)
//        {
//            // Implement logging logic here (e.g., log to a file, database, or event viewer)
//            // Example: Log to a file
//            string logFilePath = Server.MapPath("~/App_Data/ErrorLog.txt");
//            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(logFilePath, true))
//            {
//                writer.WriteLine("Date: " + DateTime.Now.ToString());
//                writer.WriteLine("Exception Message: " + ex.Message);
//                writer.WriteLine("Stack Trace: " + ex.StackTrace);
//                writer.WriteLine();
//            }
//        } 

//        // Placeholder methods for TextBox change events
//        protected void AdminUsername_TextChanged(object sender, EventArgs e)
//        {
//            // Logic for username text change event
//        }

//        protected void AdminPassword_TextChanged(object sender, EventArgs e)
//        {
//            // Logic for password text change event
//        }

//        protected void AdminPassReenter_TextChanged(object sender, EventArgs e)
//        {
//            // Logic for re-enter password text change event
//        }
//    }
//}