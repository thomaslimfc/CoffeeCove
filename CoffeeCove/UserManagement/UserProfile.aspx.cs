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
                    string query = "SELECT EmailAddress, Gender, DateOfBirth, ContactNo, ResidenceState, ProfilePicturePath FROM [dbo].[Customer] WHERE Username = @Username";
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

                            // Set dropdown selected value
                            txtGender.SelectedValue = reader["Gender"].ToString();
                            txtResidenceState.SelectedValue = reader["ResidenceState"].ToString();

                            // Load profile picture
                            string profilePicturePath = reader["ProfilePicturePath"].ToString();
                            if (!string.IsNullOrEmpty(profilePicturePath))
                            {
                                imgProfilePicture.ImageUrl = "/UserManagement/UserProfilePictures/" + profilePicturePath;
                            }
                            else
                            {
                                imgProfilePicture.ImageUrl = "~/img/username_icon.png"; // Use a default picture if none is uploaded
                            }
                        }
                        else
                        {
                            lblUsername.Text = "Username not found.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblUsername.Text = "An error occurred: " + ex.Message;
                    }
                }
            }
            else
            {
                lblUsername.Text = "Username is not provided.";
            }
        }


        protected void SetProfileEditMode(bool isEditMode)
        {
            if (isEditMode)
            {
                txtUsername.Text = lblUsername.Text;
                txtEmail.Text = lblEmail.Text;
                txtGender.SelectedValue = lblGender.Text;
                txtDOB.Text = lblDOB.Text;
                txtContactNo.Text = lblContactNo.Text;
                txtResidenceState.SelectedValue = lblResidenceState.Text;
            }

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

            fuProfilePicture.Visible = isEditMode;
            UploadPictureBtn_UP.Visible = isEditMode;
        }


        protected void EditBtn_UP_Click(object sender, EventArgs e)
        {
            SetProfileEditMode(true);
        }

        protected void SaveBtn_UP_Click(object sender, EventArgs e)
        {
            Page.Validate("SaveProfile");

            if (Page.IsValid)
            {
                UpdateUserProfile();
                LoadUserProfile();
                SetProfileEditMode(false);
            }
            else
            {
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

                    // Ensure all parameters are added correctly
                    cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", txtGender.Text.Trim());

                    DateTime dob;
                    if (DateTime.TryParse(txtDOB.Text.Trim(), out dob))
                    {
                        cmd.Parameters.AddWithValue("@DateOfBirth", dob);
                    }
                    else
                    {
                        lblUsername.Text = "Invalid date format.";
                        return; // Exit method if the date is invalid
                    }

                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@ResidenceState", txtResidenceState.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            lblUsername.Text = "No rows were updated. Please check the provided data.";
                        }
                        else
                        {
                            lblUsername.Text = "Profile updated successfully.";
                        }
                    }
                    catch (Exception ex)
                    {
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
                string savePath = Server.MapPath("~/UserManagement/UserProfilePictures/") + filename;
                fuProfilePicture.SaveAs(savePath);
                imgProfilePicture.ImageUrl = "/UserManagement/UserProfilePictures/" + filename;

                // Save the path to the database
                string username = Session["Username"]?.ToString();
                if (!string.IsNullOrEmpty(username))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE [dbo].[Customer] SET ProfilePicturePath = @ProfilePicturePath WHERE Username = @Username";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProfilePicturePath", filename);
                        cmd.Parameters.AddWithValue("@Username", username);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            lblUploadMessage.Text = "Picture has been successfully uploaded.";
                            lblUploadMessage.Visible = true;
                            lblUploadMessage.CssClass = "text-success";
                        }
                        catch (Exception ex)
                        {
                            lblUploadMessage.Text = "An error occurred: " + ex.Message;
                            lblUploadMessage.Visible = true;
                            lblUploadMessage.CssClass = "text-danger";
                        }
                    }
                }
            }
            else
            {
                lblUploadMessage.Text = "Please select a picture to upload.";
                lblUploadMessage.Visible = true;
                lblUploadMessage.CssClass = "text-danger";
            }
        }
    }
}
