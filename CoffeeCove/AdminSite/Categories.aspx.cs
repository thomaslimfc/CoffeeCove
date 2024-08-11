using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class Categories : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategory();
            }
        }

        private void BindCategory()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId, CategoryName, CategoryImageUrl, IsActive, CreatedDate FROM Category";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    GridViewCategory.DataSource = dr;
                    GridViewCategory.DataBind();
                }
            }
        }

        private int GetNextCategoryId()
        {
            int nextId = 1;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT ISNULL(MAX(CategoryId), 0) + 1 FROM Category";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    nextId = (int)cmd.ExecuteScalar();
                }
            }
            return nextId;
        }

        // button add category
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();
            bool isActive = cbIsActive.Checked;
            string categoryImageUrl = null;

            if (hdnId.Value != "0")
            {
                // Edit existing category
                if (fuCategoryImage.HasFile)
                {
                    categoryImageUrl = UploadImage();
                }
                else
                {
                    categoryImageUrl = imgCategory.Attributes["src"];
                }
                UpdateCategory(categoryName, categoryImageUrl, isActive);
            }
            else
            {
                // Add new category
                int newCategoryId = GetNextCategoryId();
                categoryImageUrl = UploadImage();

                if (IsCategoryExists(categoryName))
                {
                    lblMsg.Text = "Category name already exists.";
                    lblMsg.Visible = true;
                    HideMessageAfterDelay();
                    return;
                }

                using (SqlConnection con = new SqlConnection(cs))
                {
                    string sql = "INSERT INTO Category (CategoryId, CategoryName, CategoryImageUrl, IsActive, CreatedDate) VALUES (@CategoryId, @CategoryName, @CategoryImageUrl, @IsActive, @CreatedDate)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", newCategoryId);
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@CategoryImageUrl", string.IsNullOrEmpty(categoryImageUrl) ? (object)DBNull.Value : categoryImageUrl);
                        cmd.Parameters.AddWithValue("@IsActive", isActive);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMsg.Text = "Category added successfully.";
                lblMsg.Visible = true;
                HideMessageAfterDelay();
                BindCategory();
                ClearForm();
            }
        }

        // update category database
        private void UpdateCategory(string categoryName, string categoryImageUrl, bool isActive)
        {
            int categoryId = Convert.ToInt32(hdnId.Value);

            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "UPDATE Category SET CategoryName = @CategoryName, CategoryImageUrl = @CategoryImageUrl, IsActive = @IsActive, CreatedDate = @CreatedDate WHERE CategoryId = @CategoryId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@CategoryImageUrl", categoryImageUrl);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMsg.Text = "Category updated successfully.";
            lblMsg.Visible = true;
            HideMessageAfterDelay();
            BindCategory();
            ClearForm();
        }


        // upload image 
        private string UploadImage()
        {
            if (fuCategoryImage.HasFile)
            {
                string fileName = Path.GetFileName(fuCategoryImage.PostedFile.FileName);
                string filePath = Server.MapPath("/img/Category/") + fileName;

                // Save the uploaded image
                fuCategoryImage.SaveAs(filePath);
                return "/img/Category/" + fileName;
            }
            return null; // Return null if no image uploaded
        }

        // check if the category name already exist
        private bool IsCategoryExists(string categoryName)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT COUNT(*) FROM Category WHERE CategoryName = @CategoryName";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    con.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        // button reset
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // clear the form
        private void ClearForm()
        {
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            imgCategory.Attributes["src"] = string.Empty;
            hdnId.Value = "0";
            btnAdd.Text = "Add";
        }

        // action edit and delete 
        protected void GridViewCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCategory")
            {
                int categoryId = Convert.ToInt32(e.CommandArgument);
                LoadCategoryForEdit(categoryId);
            }
            else if (e.CommandName == "DeleteCategory")
            {
                int categoryId = Convert.ToInt32(e.CommandArgument);
                DeleteCategory(categoryId);
            }
        }

        // display the current data in the form when click on edit button
        private void LoadCategoryForEdit(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId, CategoryName, CategoryImageUrl, IsActive FROM Category WHERE CategoryId = @CategoryId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtCategoryName.Text = dr["CategoryName"].ToString();
                        string imageUrl = dr["CategoryImageUrl"].ToString();
                        imgCategory.Attributes["src"] = dr["CategoryImageUrl"].ToString();
                        cbIsActive.Checked = !Convert.IsDBNull(dr["IsActive"]) && (bool)dr["IsActive"];
                        hdnId.Value = categoryId.ToString();
                    }
                }
            }

            btnAdd.Text = "Update";
        }

        // delete the current category
        private void DeleteCategory(int categoryId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    // Step 1: Delete the category
                    string sqlDelete = "DELETE FROM Category WHERE CategoryId = @CategoryId";
                    using (SqlCommand cmd = new SqlCommand(sqlDelete, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    // Step 2: Reorder the remaining categories
                    string sqlReorder = "UPDATE Category SET CategoryId = CategoryId - 1 WHERE CategoryId > @CategoryId";
                    using (SqlCommand cmd = new SqlCommand(sqlReorder, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        cmd.ExecuteNonQuery();
                    }

                    lblMsg.Text = "Category deleted successfully.";
                    lblMsg.Visible = true;
                    HideMessageAfterDelay();
                    BindCategory();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error deleting category: " + ex.Message;
                lblMsg.Visible = true;
                HideMessageAfterDelay();
            }
        }

        // hide the lblmsg after a delay
        private void HideMessageAfterDelay()
        {
            string script = @"setTimeout(function() {
                var lblMsg = document.getElementById('" + lblMsg.ClientID + @"');
                if (lblMsg) {
                    lblMsg.style.display = 'none';
                }
            }, 3000);";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", script, true);
        }

    }
}