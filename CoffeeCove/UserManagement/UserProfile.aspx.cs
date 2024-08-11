using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CoffeeCove.UserManagement
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Username"] = "xylim2002";

                LoadUserProfile();
                SetProfileEditMode(false);
            }
        }

        protected void LoadUserProfile()
        {
            string username = Session["Username"]?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT EmailAddress, Gender, DateOfBirth, ContactNo, ResidenceState FROM [dbo].[Customer] WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();

                            lblUsername.Text = username;
                            lblEmail.Text = reader["EmailAddress"].ToString();
                            lblGender.Text = reader["Gender"].ToString();
                            lblDOB.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                            lblContactNo.Text = reader["ContactNo"].ToString();
                            lblResidenceState.Text = reader["ResidenceState"].ToString();
                        }
                        else
                        {
                            // Handle case where the username is not found
                            lblUsername.Text = "Username not found.";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that may have occurred
                        lblUsername.Text = "An error occurred: " + ex.Message;
                    }
                }
            }
            else
            {
                // Handle case where the username is null or empty
                lblUsername.Text = "Username is not provided.";
            }
        }

        protected void SetProfileEditMode(bool isEditMode)
        {
            if (isEditMode)
            {
                // Populate the text boxes with current values
                txtUsername.Text = lblUsername.Text;
                txtEmail.Text = lblEmail.Text;
                txtGender.Text = lblGender.Text;
                txtDOB.Text = lblDOB.Text;
                txtContactNo.Text = lblContactNo.Text;
                txtResidenceState.Text = lblResidenceState.Text;
            }

            // Toggle visibility between labels and text boxes
            lblUsername.Visible = !isEditMode;
            txtUsername.Visible = isEditMode;

            lblEmail.Visible = !isEditMode;
            txtEmail.Visible = isEditMode;

            lblGender.Visible = !isEditMode;
            txtGender.Visible = isEditMode;

            lblDOB.Visible = !isEditMode;
            txtDOB.Visible = isEditMode;

            lblContactNo.Visible = !isEditMode;
            txtContactNo.Visible = isEditMode;

            lblResidenceState.Visible = !isEditMode;
            txtResidenceState.Visible = isEditMode;

            EditBtn_UP.Visible = !isEditMode;
            SaveBtn_UP.Visible = isEditMode;
        }


        protected void EditBtn_UP_Click(object sender, EventArgs e)
        {
            SetProfileEditMode(true);
        }

        protected void SaveBtn_UP_Click(object sender, EventArgs e)
        {
            // Manually trigger validation
            Page.Validate("SaveProfile");

            if (Page.IsValid)
            {
                UpdateUserProfile(); // Call the method to save the updated profile

                // Reload the profile after saving to refresh the labels
                LoadUserProfile();

                // Redirect back to profile view mode after saving
                SetProfileEditMode(false);
            }
            else
            {
                // Keep in edit mode if validation fails
                SetProfileEditMode(true);
            }
        }

        protected void UpdateUserProfile()
        {
            string username = Session["Username"]?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE [dbo].[Customer] SET EmailAddress = @EmailAddress, Gender = @Gender, " +
                                   "DateOfBirth = @DateOfBirth, ContactNo = @ContactNo, ResidenceState = @ResidenceState " +
                                   "WHERE Username = @Username";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDOB.Text));
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@ResidenceState", txtResidenceState.Text);
                    cmd.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery(); // Execute the update query
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that may have occurred
                        lblUsername.Text = "An error occurred: " + ex.Message;
                    }
                }
            }
        }

        protected void UploadPictureBtn_UP_Click(object sender, EventArgs e)
        {
            if (fuProfilePicture.HasFile)
            {
                string filename = System.IO.Path.GetFileName(fuProfilePicture.PostedFile.FileName);
                fuProfilePicture.SaveAs(Server.MapPath("~/UserManagement/UserProfilePictures/") + filename);
                imgProfilePicture.ImageUrl = "/UserProfilePictures/" + filename;

                // Show success message
                lblUploadMessage.Text = "Picture has been successfully uploaded.";
                lblUploadMessage.Visible = true;
            }
            else
            {
                lblUploadMessage.Text = "Please select a picture to upload.";
                lblUploadMessage.CssClass = "text-danger";
                lblUploadMessage.Visible = true;
            }
        }

    }
}
