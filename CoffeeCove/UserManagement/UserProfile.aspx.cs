using System;
using System.Data.SqlClient;

namespace CoffeeCove.UserManagement
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserProfile();
            }
        }

        private void LoadUserProfile()
        {
            // Retrieve the logged-in user's username
            string username = "xylim2002";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;
            string query = "SELECT Username, Gender, EmailAddress, DateOfBirth, ContactNo, ResidenceState FROM Customer WHERE Username = @Username";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblUsername.Text = reader["Username"].ToString();
                        lblGender.Text = reader["Gender"].ToString();
                        lblEmail.Text = reader["EmailAddress"].ToString();

                        DateTime dob = Convert.ToDateTime(reader["DateOfBirth"]);
                        lblDOB.Text = dob.ToString("d");

                        lblContactNo.Text = reader["ContactNo"].ToString();
                        lblResidenceState.Text = reader["ResidenceState"].ToString();

                        // Set TextBox values
                        txtUsername.Text = lblUsername.Text;
                        txtGender.Text = lblGender.Text;
                        txtEmail.Text = lblEmail.Text;
                        txtDOB.Text = lblDOB.Text;
                        txtContactNo.Text = lblContactNo.Text;
                        txtResidenceState.Text = lblResidenceState.Text;
                    }
                    con.Close();
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ToggleControls(false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string username = "xylim2002";  // The logged-in user's username, replace with the actual logged-in user logic
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

            string query = "UPDATE Customer SET Gender = @Gender, EmailAddress = @EmailAddress, DateOfBirth = @DateOfBirth, ContactNo = @ContactNo, ResidenceState = @ResidenceState WHERE Username = @Username";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDOB.Text));
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@ResidenceState", txtResidenceState.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // After saving, revert to label display
            ToggleControls(true);

            // Reload the profile to reflect updated information
            LoadUserProfile();
        }


        private void ToggleControls(bool isLabelVisible)
        {
            lblUsername.Visible = isLabelVisible;
            lblGender.Visible = isLabelVisible;
            lblEmail.Visible = isLabelVisible;
            lblDOB.Visible = isLabelVisible;
            lblContactNo.Visible = isLabelVisible;
            lblResidenceState.Visible = isLabelVisible;

            txtUsername.Visible = !isLabelVisible;
            txtGender.Visible = !isLabelVisible;
            txtEmail.Visible = !isLabelVisible;
            txtDOB.Visible = !isLabelVisible;
            txtContactNo.Visible = !isLabelVisible;
            txtResidenceState.Visible = !isLabelVisible;

            btnEdit.Visible = isLabelVisible;
            btnSave.Visible = !isLabelVisible;
        }
    }
}
