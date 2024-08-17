using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace CoffeeCove.UserManagement
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["cusID"] = "00001"; // Hardcoded for testing purposes

                LoadUserProfile();
                SetProfileEditMode(false);
            }
        }

        protected void LoadUserProfile()
        {
            string cusID = Session["cusID"]?.ToString();
            if (!string.IsNullOrEmpty(cusID))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT Username, EmailAddress, Gender, DateOfBirth, ContactNo, ResidenceState, ProfilePicturePath FROM [dbo].[Customer] WHERE cusID = @cusID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cusID", cusID);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();

                            lblUsername.Text = reader["Username"].ToString();
                            lblEmail.Text = reader["EmailAddress"].ToString();
                            lblGender.Text = reader["Gender"].ToString();
                            lblDOB.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                            lblContactNo.Text = reader["ContactNo"].ToString();
                            lblResidenceState.Text = reader["ResidenceState"].ToString();

                            txtGender.SelectedValue = reader["Gender"].ToString();
                            txtResidenceState.SelectedValue = reader["ResidenceState"].ToString();

                            string profilePicturePath = reader["ProfilePicturePath"].ToString();
                            if (!string.IsNullOrEmpty(profilePicturePath))
                            {
                                imgProfilePicture.ImageUrl = "/UserManagement/UserProfilePictures/" + profilePicturePath;
                                RemovePictureBtn_UP.Visible = true;
                            }
                            else
                            {
                                imgProfilePicture.ImageUrl = "/img/DefaultProfilePicture.jpg";
                                RemovePictureBtn_UP.Visible = false;
                            }
                        }
                        else
                        {
                            lblUsername.Text = "Username is not found in the database.";
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
                lblUsername.Text = "Username is not in the database.";
            }
        }

        protected void EditBtn_UP_Click(object sender, EventArgs e)
        {
            SetProfileEditMode(true);
        }

        protected void SetProfileEditMode(bool isEditMode)
        {
            if (isEditMode)
            {
                txtUsername.Text = lblUsername.Text;
                txtGender.SelectedValue = lblGender.Text;
                txtDOB.Text = lblDOB.Text;
                txtContactNo.Text = lblContactNo.Text;
                txtResidenceState.SelectedValue = lblResidenceState.Text;
            }

            lblUsername.Visible = !isEditMode;
            txtUsername.Visible = isEditMode;

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

            // Show or hide the picture-related controls based on edit mode and picture editing state
            fuProfilePicture.Visible = isEditMode && IsEditingPicture;
            UploadPictureBtn_UP.Visible = isEditMode && IsEditingPicture;
            UploadBackBtn_UP.Visible = isEditMode && IsEditingPicture;

            // Hide Remove and Edit Picture buttons when in picture editing mode
            RemovePictureBtn_UP.Visible = isEditMode && !IsEditingPicture && imgProfilePicture.ImageUrl != "/img/DefaultProfilePicture.jpg";
            EditPictureBtn_UP.Visible = isEditMode && !IsEditingPicture;
        }

        protected void SaveBtn_UP_Click(object sender, EventArgs e)
        {
            Page.Validate("SaveProfile");

            if (Page.IsValid)
            {
                UpdateUserProfile();
                LoadUserProfile();
                SetProfileEditMode(false);
                IsEditingPicture = false;
            }
            else
            {
                SetProfileEditMode(true);
            }

            lblUploadMessage.Text = "";
            lblRemoveMessage.Text = "";
        }

        protected void UpdateUserProfile()
        {
            string cusID = Session["cusID"]?.ToString();
            if (!string.IsNullOrEmpty(cusID))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE [dbo].[Customer] SET Username = @Username, Gender = @Gender, " +
                                   "DateOfBirth = @DateOfBirth, ContactNo = @ContactNo, ResidenceState = @ResidenceState " +
                                   "WHERE cusID = @cusID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", txtGender.SelectedValue.Trim());

                    DateTime dob;
                    if (DateTime.TryParse(txtDOB.Text.Trim(), out dob))
                    {
                        cmd.Parameters.AddWithValue("@DateOfBirth", dob);
                    }
                    else
                    {
                        lblUsername.Text = "Invalid date format.";
                        return;
                    }

                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@ResidenceState", txtResidenceState.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@cusID", cusID);

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

        private bool IsEditingPicture
        {
            get
            {
                return (bool)(Session["IsEditingPicture"] ?? false);
            }
            set
            {
                Session["IsEditingPicture"] = value;
            }
        }

        protected void EditPictureBtn_UP_Click(object sender, EventArgs e)
        {
            IsEditingPicture = true;
            SetProfileEditMode(true);
        }

        protected void UploadPictureBtn_UP_Click(object sender, EventArgs e)
        {
            lblRemoveMessage.Text = "";

            if (fuProfilePicture.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(fuProfilePicture.PostedFile.FileName).ToLower();

                // Validate the file extension
                if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
                {
                    string cusID = Session["cusID"]?.ToString();
                    if (!string.IsNullOrEmpty(cusID))
                    {
                        string filename = cusID + fileExtension;
                        string savePath = Server.MapPath("~/UserManagement/UserProfilePictures/") + filename;

                        try
                        {
                            fuProfilePicture.SaveAs(savePath);

                            imgProfilePicture.ImageUrl = "/UserManagement/UserProfilePictures/" + filename;

                            string connectionString = ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                string query = "UPDATE [dbo].[Customer] SET ProfilePicturePath = @ProfilePicturePath WHERE cusID = @cusID";
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@ProfilePicturePath", filename);
                                cmd.Parameters.AddWithValue("@cusID", cusID);

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }

                            lblUploadMessage.Text = "Picture has been successfully uploaded.";
                            lblUploadMessage.Visible = true;
                            lblUploadMessage.CssClass = "text-success";

                            // After uploading, switch back to normal edit mode
                            IsEditingPicture = false;
                            SetProfileEditMode(true);
                        }
                        catch (Exception ex)
                        {
                            lblUploadMessage.Text = "An error occurred: " + ex.Message;
                            lblUploadMessage.Visible = true;
                            lblUploadMessage.CssClass = "text-danger";
                        }
                    }
                }
                else
                {
                    // If the file type is not allowed
                    lblUploadMessage.Text = "Only PNG, JPG, and JPEG image files are allowed.";
                    lblUploadMessage.Visible = true;
                    lblUploadMessage.CssClass = "text-danger";
                }
            }
            else
            {
                lblUploadMessage.Text = "Please select a picture to upload.";
                lblUploadMessage.Visible = true;
                lblUploadMessage.CssClass = "text-danger";
            }
        }

        protected void UploadBackBtn_UP_Click(object sender, EventArgs e)
        {
            // Reset the picture editing mode to false
            IsEditingPicture = false;

            // Call SetProfileEditMode to show the profile editing fields
            SetProfileEditMode(true);

            // Clear any upload messages
            lblUploadMessage.Text = "";
            lblRemoveMessage.Text = "";
        }

        protected void RemovePictureBtn_UP_Click(object sender, EventArgs e)
        {
            lblUploadMessage.Text = "";

            string cusID = Session["cusID"]?.ToString();
            if (!string.IsNullOrEmpty(cusID))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CoffeeCoveDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string querySelect = "SELECT ProfilePicturePath FROM [dbo].[Customer] WHERE cusID = @cusID";
                    SqlCommand cmdSelect = new SqlCommand(querySelect, con);
                    cmdSelect.Parameters.AddWithValue("@cusID", cusID);

                    try
                    {
                        con.Open();
                        string profilePicturePath = cmdSelect.ExecuteScalar() as string;

                        if (string.IsNullOrEmpty(profilePicturePath))
                        {
                            lblRemoveMessage.Text = "There is no picture to be removed.";
                            lblRemoveMessage.Visible = true;
                            lblRemoveMessage.CssClass = "text-warning";
                        }
                        else
                        {
                            string queryUpdate = "UPDATE [dbo].[Customer] SET ProfilePicturePath = NULL WHERE cusID = @cusID";
                            SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con);
                            cmdUpdate.Parameters.AddWithValue("@cusID", cusID);

                            cmdUpdate.ExecuteNonQuery();

                            imgProfilePicture.ImageUrl = "/img/DefaultProfilePicture.jpg";
                            lblRemoveMessage.Text = "Picture has been successfully removed.";
                            lblRemoveMessage.Visible = true;
                            lblRemoveMessage.CssClass = "text-success";

                            RemovePictureBtn_UP.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblRemoveMessage.Text = "An error occurred: " + ex.Message;
                        lblRemoveMessage.Visible = true;
                        lblRemoveMessage.CssClass = "text-danger";
                    }
                }
            }
        }

    }
}
