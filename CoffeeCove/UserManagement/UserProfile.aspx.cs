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
            // Retrive who is the logged-in user?
            string username = "xylim2002";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;
            string query = "SELECT Username, Gender, EmailAddress, DateOfBirth, ResidenceState FROM Customer WHERE Username = @Username";

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
                        lblDOB.Text = reader["DateOfBirth"].ToString();
                        lblResidenceState.Text = reader["ResidenceState"].ToString();
                    }
                    con.Close();
                }
            }
        }
    }
}