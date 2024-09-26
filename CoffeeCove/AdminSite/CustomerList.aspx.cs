using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class CustomerList : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected string adminHashedPassword;
        protected string superuserHP;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // show customer list
                BindCustomerList();

                // retrieve real-time branch name from db
                BindBranchLocation();
            }
        }

        protected void RegisterAcc_CL_Click(object sender, EventArgs e)
        {
            lblUsernameRegister_CL.Text = "";
            lblSuperuserPassword_CL.Text = "";

            // Retrieve user inputs
            string username = UsernameRegister_CL.Text.Trim();
            string gender = Gender_CL.SelectedValue;
            string password = HashPassword(Password_CL.Text);
            string branchLocation = BranchLocation_CL.SelectedValue;
            string contactNo = ContactNo_CL.Text.Trim();
            string superuserPassword = HashPassword(SuperuserPassword_CL.Text.Trim());

            string superuserHP = RetrieveAdminPassword("superuser");

            // Verify superuser password
            if (!VerifyPassword(superuserPassword, superuserHP))
            {
                lblSuperuserPassword_CL.Text = "Invalid superuser password.";
                return;
            }

            using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
            {
                try
                {
                    // Check if the username already exists
                    var admin = db.Admins.SingleOrDefault(a => a.Username == username);
                    if (admin != null)
                    {
                        lblUsernameRegister_CL.Text = "Username has already been taken.";
                        return;
                    }

                    // Create a new admin if everything is valid
                    var newAdmin = new Admin
                    {
                        Username = username,
                        HashedPassword = password,
                        Gender = gender,
                        Branch = branchLocation,
                        ContactNo = contactNo
                    };

                    // Save into the database
                    db.Admins.Add(newAdmin);
                    db.SaveChanges();

                    lblUsernameRegister_CL.Text = "The account has been created.";
                }
                catch (DbEntityValidationException ex)
                {
                    // Capture validation errors and display them
                    StringBuilder sb = new StringBuilder("Validation errors:<br/>");
                    foreach (var validationError in ex.EntityValidationErrors)
                    {
                        foreach (var error in validationError.ValidationErrors)
                        {
                            sb.AppendLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}<br/>");
                        }
                    }
                    lblUsernameRegister_CL.Text = sb.ToString();
                }
                catch (Exception ex)
                {
                    lblUsernameRegister_CL.Text = "Database error: " + ex.Message;
                }
            }
        }


        protected void SaveChangesBtn_CL2_Click(object sender, EventArgs e)
        {
            lblUsernameEdit_CL2.Text = "";
            lblSuperuserPassword_CL2.Text = "";

            string username = UsernameEdit_CL2.Text.Trim();
            string superuserPassword = HashPassword(SuperuserPassword_CL2.Text);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(superuserPassword))
            {
                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    try
                    {
                        // Retrieve the admin profile by username
                        var admin = db.Admins.SingleOrDefault(a => a.Username == username);

                        if (admin != null)
                        {
                            string superuserHP = RetrieveAdminPassword("superuser");
                            if (VerifyPassword(superuserPassword, superuserHP))
                            {
                                // Update fields
                                admin.Gender = GenderEdit_CL2.SelectedValue;
                                admin.Branch = BranchLocationEdit_CL2.SelectedValue;

                                db.SaveChanges();

                                lblUsernameEdit_CL2.Text = "Profile has been updated successfully.";
                            }
                            else
                            {
                                lblSuperuserPassword_CL2.Text = "Invalid superuser password.";
                            }
                        }
                        else
                        {
                            lblUsernameEdit_CL2.Text = "Admin username is not found.";
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Capture detailed validation errors
                        foreach (var validationError in ex.EntityValidationErrors)
                        {
                            foreach (var error in validationError.ValidationErrors)
                            {
                                lblUsernameEdit_CL2.Text += $"Property: {error.PropertyName}, Error: {error.ErrorMessage} <br/>";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblUsernameEdit_CL2.Text = "Error: " + ex.Message;
                    }
                }
            }
            else
            {
                lblUsernameEdit_CL2.Text = "Please enter both the username and the superuser password.";
            }
        }


        protected void DeleteAccBtn_CL3_Click(object sender, EventArgs e)
        {
            lblUsernameDeletion_CL3.Text = "";
            lblSuperuserPassword_CL3.Text = "";

            string usernameToDelete = UsernameDeletion_CL3.Text.Trim();
            string superuserHP = RetrieveAdminPassword("superuser");

            using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
            {
                try
                {
                    // Find the admin by username
                    var admin = db.Admins.SingleOrDefault(c => c.Username == usernameToDelete);

                    if (admin == null)
                    {
                        lblUsernameDeletion_CL3.Text = "Admin username is not found.";
                        return;
                    }

                    string password = HashPassword(SuperuserPassword_CL3.Text);

                    if (password == superuserHP)
                    {
                        // Delete the customer
                        db.Admins.Remove(admin);
                        db.SaveChanges();

                        // Display a success message
                        lblUsernameDeletion_CL3.Text = "Admin account has been deleted.";
                    }
                    else
                    {
                        lblSuperuserPassword_CL3.Text = "Invalid superuser password.";
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors
                    lblUsernameDeletion_CL3.Text = "Error deleting the account: " + ex.Message;
                }
            }
        }


        protected void LoadAdminProfile_CL2_Click(object sender, EventArgs e)
        {
            string username = UsernameEdit_CL2.Text.Trim();

            if (!string.IsNullOrEmpty(username))
            {
                using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
                {
                    var admin = db.Admins.SingleOrDefault(a => a.Username == username);
                    if (admin != null)
                    {
                        UsernameEdit_CL2.Text = admin.Username;
                        GenderEdit_CL2.SelectedValue = admin.Gender;
                        BranchLocationEdit_CL2.SelectedValue = admin.Branch;
                        SuperuserPassword_CL2.Text = admin.HashedPassword;
                    }
                    else
                    {
                        lblUsernameEdit_CL2.Text = "Admin username not found.";
                    }
                }
            }
        }


        private string RetrieveAdminPassword(string username)
        {
            using (dbCoffeeCoveEntities db = new dbCoffeeCoveEntities())
            {
                var admin = db.Admins.SingleOrDefault(a => a.Username == username);
                return admin?.HashedPassword;
            }
        }


        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string enteredHashedPassword = enteredPassword;
            return enteredHashedPassword == storedHashedPassword;
        }


        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private void BindBranchLocation()
        {
            using (SqlConnection conn = new SqlConnection(Global.CS))
            {
                conn.Open();
                string query = "SELECT StoreName FROM Store";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                BranchLocation_CL.DataSource = dt;
                BranchLocation_CL.DataTextField = "StoreName";
                BranchLocation_CL.DataValueField = "StoreName";
                BranchLocation_CL.DataBind();
                BranchLocation_CL.Items.Insert(0, new ListItem("Select a branch", "0"));

                BranchLocationEdit_CL2.DataSource = dt;
                BranchLocationEdit_CL2.DataTextField = "StoreName";
                BranchLocationEdit_CL2.DataValueField = "StoreName";
                BranchLocationEdit_CL2.DataBind();
                BranchLocationEdit_CL2.Items.Insert(0, new ListItem("Select a branch", "0"));
            }
        }

        private void BindCustomerList()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT CusID, Username, FirstName, LastName, EmailAddress, ContactNo, Gender, DateOfBirth, ResidenceState FROM [dbo].[Customer]";
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
