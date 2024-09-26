using System;
using System.Data.SqlClient;

namespace CoffeeCove.UserManagement
{
    public partial class UserProfile : System.Web.UI.Page
    {
        string cs = Global.CS;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CusID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadUserProfile();
                SetProfileEditMode(false);
            }
        }


        protected void LoadUserProfile()
        {
            string CusID = Session["CusID"]?.ToString();
            if (!string.IsNullOrEmpty(CusID))
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "SELECT Username, EmailAddress, Gender, DateOfBirth, ContactNo, ResidenceState, ProfilePicturePath FROM [dbo].[Customer] WHERE CusID = @CusID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CusID", CusID);

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
            lblUsernameError.Text = "";
            lblContactNoError.Text = "";

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
            lblUsernameError.Text = "";
            lblContactNoError.Text = "";

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
            string CusID = Session["CusID"]?.ToString();
            if (!string.IsNullOrEmpty(CusID))
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    try
                    {
                        con.Open();

                        // Check if the new username already exists for another user
                        string checkUsernameQuery = "SELECT COUNT(*) FROM [dbo].[Customer] WHERE Username = @Username AND CusID != @CusID";
                        SqlCommand checkUsernameCmd = new SqlCommand(checkUsernameQuery, con);
                        checkUsernameCmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        checkUsernameCmd.Parameters.AddWithValue("@CusID", CusID);
                        int usernameCount = (int)checkUsernameCmd.ExecuteScalar();

                        // If the username is already taken
                        if (usernameCount > 0)
                        {
                            lblUsernameError.Text = "This username has already taken.";
                            return;
                        }

                        // Check if duplicate new contact number
                        string checkContactNoQuery = "SELECT COUNT(*) FROM [dbo].[Customer] WHERE ContactNo = @ContactNo AND CusID != @CusID";
                        SqlCommand checkContactNoCmd = new SqlCommand(checkContactNoQuery, con);
                        checkContactNoCmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
                        checkContactNoCmd.Parameters.AddWithValue("@CusID", CusID);
                        int contactNoCount = (int)checkContactNoCmd.ExecuteScalar();

                        // If the contact number is already taken
                        if (contactNoCount > 0)
                        {
                            lblContactNoError.Text = "This contact number has already taken.";
                            return;
                        }

                        // Proceed with the update if both the username and contact number are unique
                        string query = "UPDATE [dbo].[Customer] SET Username = @Username, Gender = @Gender, " +
                                       "DateOfBirth = @DateOfBirth, ContactNo = @ContactNo, ResidenceState = @ResidenceState " +
                                       "WHERE CusID = @CusID";

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
                            lblUsernameError.Text = "Invalid date format.";
                            return;
                        }

                        cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@ResidenceState", txtResidenceState.SelectedValue.Trim());
                        cmd.Parameters.AddWithValue("@CusID", CusID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            lblUsername.Text = "Check entered data.";
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
                    string CusID = Session["CusID"]?.ToString();
                    if (!string.IsNullOrEmpty(CusID))
                    {
                        string filename = CusID + fileExtension;
                        string savePath = Server.MapPath("~/UserManagement/UserProfilePictures/") + filename;

                        try
                        {
                            fuProfilePicture.SaveAs(savePath);

                            imgProfilePicture.ImageUrl = "/UserManagement/UserProfilePictures/" + filename;

                            using (SqlConnection con = new SqlConnection(cs))
                            {
                                string query = "UPDATE [dbo].[Customer] SET ProfilePicturePath = @ProfilePicturePath WHERE CusID = @CusID";
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@ProfilePicturePath", filename);
                                cmd.Parameters.AddWithValue("@CusID", CusID);

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
            // Delete the picture from the folder
            string CusID = Session["CusID"]?.ToString();
            if (!string.IsNullOrEmpty(CusID))
            {
                string filename = CusID + ".jpg";
                string savePath = Server.MapPath("~/UserManagement/UserProfilePictures/") + filename;

                if (System.IO.File.Exists(savePath))
                {
                    try
                    {
                        System.IO.File.Delete(savePath);

                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            string query = "UPDATE [dbo].[Customer] SET ProfilePicturePath = NULL WHERE CusID = @CusID";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@CusID", CusID);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }

                        // Reset the profile picture to the default image
                        imgProfilePicture.ImageUrl = "/img/DefaultProfilePicture.jpg";
                        lblRemoveMessage.Text = "Picture has been successfully removed.";
                        lblRemoveMessage.Visible = true;
                        lblRemoveMessage.CssClass = "text-success";

                        // Hide the Remove Picture button after removing the picture
                        RemovePictureBtn_UP.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        lblRemoveMessage.Text = "An error occurred: " + ex.Message;
                        lblRemoveMessage.Visible = true;
                        lblRemoveMessage.CssClass = "text-danger";
                    }
                }
                else
                {
                    lblRemoveMessage.Text = "Picture not found.";
                    lblRemoveMessage.Visible = true;
                    lblRemoveMessage.CssClass = "text-danger";
                }
            }
        }
    }
}
