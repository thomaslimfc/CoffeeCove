using System;
using System.Data.Entity.Validation;
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
                if (Session["ContactNo"] == null)
                {
                    Response.Redirect("SignIn.aspx");
                }
            }
        }


        protected void ResetPasswordBtn_PR_Click(object sender, EventArgs e)
        {
            string newPassword = HashPassword(NewPassword_PR.Text);
            string contactNo = (string)Session["ContactNo"];

            using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
            {
                try
                {
                    // Retrieve the customer by contact no
                    var customer = db.Customers.SingleOrDefault(c => c.ContactNo == contactNo);

                    if (customer != null)
                    {
                        customer.HashedPassword = newPassword;
                        db.SaveChanges();
                        Response.Redirect("PasswordResetSuccess.aspx");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    // Capture validation errors
                    foreach (var validationError in ex.EntityValidationErrors)
                    {
                        foreach (var error in validationError.ValidationErrors)
                        {
                            lblNewPassword_PR.Text += $"Property: {error.PropertyName}, Error: {error.ErrorMessage} <br/>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle generic errors
                    lblNewPassword_PR.Text = "Error: " + ex.Message;
                }
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
