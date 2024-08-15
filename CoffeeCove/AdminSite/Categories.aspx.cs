using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

        private void BindCategory(string searchTerm = "", string sortExpression = "", string sortDirection = "ASC")
        {
            int pageIndex = GridViewCategory.PageIndex;
            int pageSize = GridViewCategory.PageSize;

            string sql = "SELECT CategoryId, CategoryName, CategoryImageUrl, IsActive, CreatedDate FROM Category";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sql += " WHERE CategoryName LIKE @SearchTerm + '%'";
            }

            if (!string.IsNullOrEmpty(sortExpression))
            {
                sql += $" ORDER BY {sortExpression} {sortDirection}";
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    }

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Use a DataTable for paging
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    GridViewCategory.DataSource = dt;
                    GridViewCategory.DataBind();
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string categoryName = args.Value;
            int categoryId = Convert.ToInt32(hdnId.Value);
            string sql = "SELECT COUNT(*) FROM Category WHERE CategoryName = @CategoryName";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    args.IsValid = count == 0;
                }
            }
        }

        private int GetNextCategoryId()
        {
            int nextId = 1;
            string sql = "SELECT ISNULL(MAX(CategoryId), 0) + 1 FROM Category";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    nextId = (int)cmd.ExecuteScalar();
                }
            }
            return nextId;
        }

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

        private void LoadCategoryForEdit(int categoryId)
        {
            string sql = "SELECT CategoryId, CategoryName, CategoryImageUrl, IsActive FROM Category WHERE CategoryId = @CategoryId";
            using (SqlConnection con = new SqlConnection(cs))
            {
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

        private void UpdateCategory(string categoryName, string categoryImageUrl, bool isActive)
        {
            int categoryId = Convert.ToInt32(hdnId.Value);
            string sql = "UPDATE Category SET CategoryName = @CategoryName, CategoryImageUrl = @CategoryImageUrl, IsActive = @IsActive, CreatedDate = @CreatedDate WHERE CategoryId = @CategoryId";
            using (SqlConnection con = new SqlConnection(cs))
            {
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
            ShowSuccessMessage("Category updated successfully.");

            BindCategory();
            ClearForm();
        }

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

        private void DeleteCategory(int categoryId)
        {
            string sqlDelete = "DELETE FROM Category WHERE CategoryId = @CategoryId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sqlDelete, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // reorder the remaining categories
                string sqlReorder = "UPDATE Category SET CategoryId = CategoryId - 1 WHERE CategoryId > @CategoryId";
                using (SqlCommand cmd = new SqlCommand(sqlReorder, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.ExecuteNonQuery();
                }

                BindCategory();
                ClearForm();
            }
            ShowSuccessMessage("Category deleted successfully.");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
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
                    ClearForm();
                }
                else
                {
                    int newCategoryId = GetNextCategoryId();
                    categoryImageUrl = UploadImage();

                    string sql = "INSERT INTO Category (CategoryId, CategoryName, CategoryImageUrl, IsActive, CreatedDate) VALUES (@CategoryId, @CategoryName, @CategoryImageUrl, @IsActive, @CreatedDate)";
                    using (SqlConnection con = new SqlConnection(cs))
                    {
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
                    ShowSuccessMessage("Category added successfully.");
                }

                BindCategory();
                ClearForm();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.Transfer("Categories.aspx");
        }

        private void ClearForm()
        {
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            imgCategory.Attributes["src"] = string.Empty;
            hdnId.Value = "0";
            btnAdd.Text = "Add";
        }

        private void ShowSuccessMessage(string message)
        {
            lblMsg.Text = message;
            lblMsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "hideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 3000);", true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            BindCategory(searchTerm);
        }

        protected void GridViewCategory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);

            BindCategory(txtSearch.Text.Trim(), sortExpression, sortDirection);
        }

        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                if (sortExpression == column)
                {
                    // Reverse the sort direction.
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void GridViewCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCategory.PageIndex = e.NewPageIndex;
            BindCategory(txtSearch.Text.Trim());  // Re-bind the data with current search term
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemList(string prefixText)
        {
            List<string> getitem = new List<string>();

            string sql = "SELECT CategoryName FROM Category WHERE CategoryName LIKE @Text + '%'";
            using (SqlConnection con = new SqlConnection(Global.CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Text", prefixText);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        getitem.Add(dr["CategoryName"].ToString());
                    }
                }
            }

            return getitem;
        }

    }
}