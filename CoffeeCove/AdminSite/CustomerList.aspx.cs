using CoffeeCove.Master;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class CustomerList : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected string adminHashedPassword;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                adminHashedPassword = RetrieveAdminPassword("admin");
                BindCustomerList();
            }
        }
        protected void RegisterAcc_CL_Click(object sender, EventArgs e)
        {
            //lbl
            if (Page.IsValid)
            {
                string username = UsernameRegister_CL.Text.Trim();
                string gender = Gender_CL.SelectedValue;
                string password = Password_CL.Text;
                string superuserPassword = lblSuperuserPassword_CL.Text;
                string branchLocation = BranchLocation_CL.SelectedValue;

                // Check if the admin username already exists
                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    var existingAdmin = db.Admins.SingleOrDefault(a => a.Username == username);
                    if (existingAdmin != null)
                    {
                        // Admin with this username already exists
                        lblUsernameRegister_CL.Text = "Username has already taken.";
                        return;
                    }

                    // Compare superuser password if needed
                    if (!VerifyPassword(superuserPassword, adminHashedPassword))
                    {
                        lblSuperuserPassword_CL.Text = "Invalid superuser password.";
                        return;
                    }

                    // Create a new admin record
                    var newAdmin = new Admin
                    {
                        Username = username,
                        Gender = gender,
                        HashedPassword = password
                    };

                    // Add and save the new admin to the database
                    db.Admins.Add(newAdmin);
                    db.SaveChanges();

                    // Redirect or show a success message
                    //lblBranchLocation_CL.Text = "Admin registered successfully!";
                    Response.Redirect("AccountRegistrationSuccess.aspx");
                }
            }
        }

        private string RetrieveAdminPassword(string username)
        {
            using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
            {
                // Find the admin with the given username
                var admin = db.Admins.SingleOrDefault(a => a.Username == username);
                return admin?.HashedPassword; // Return the hashed password or null if not found
            }
        }
        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string enteredHashedPassword = enteredPassword;
            return enteredHashedPassword == storedHashedPassword;
        }

        private void BindCustomerList()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT cusID, Username, FirstName, LastName, EmailAddress, ContactNo, Gender, DateOfBirth, ResidenceState FROM [dbo].[Customer]";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                rptCustomerList.DataSource = dt;
                rptCustomerList.DataBind();
            }
        }
    }

}
