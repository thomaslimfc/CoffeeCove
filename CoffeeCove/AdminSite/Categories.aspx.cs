using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Collections.Concurrent;

namespace CoffeeCove
{
    public partial class Categories : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategory();
                PositionGlyph(gvCategory, SortExpression, SortDirection);

            }
            lblMsg.Visible = false;
        }

        private void BindCategory(string searchTerm = "")
        {
            string sql = "SELECT * FROM Category";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sql += " WHERE CategoryName LIKE @SearchTerm + '%'";
            }

            if (!string.IsNullOrEmpty(SortExpression))
            {
                sql += $" ORDER BY {SortExpression} {SortDirection}";
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

                    // Use a DataTable for binding
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    gvCategory.DataSource = dt;
                    gvCategory.DataBind();
                }
            }
            PositionGlyph(gvCategory, SortExpression, SortDirection);
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "CategoryId"; }
            set { ViewState["SortExpression"] = value; }
        }

        protected void gvCategory_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Always toggle the sort direction
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

            // Update the sort expression to the new column or keep the same column
            SortExpression = e.SortExpression;

            // Rebind the GridView with the new sorting applied
            BindCategory();
            PositionGlyph(gvCategory, SortExpression, SortDirection);
        }


        private void PositionGlyph(GridView gridView, string currentSortColumn, string currentSortDirection)
        {
            if (gridView.HeaderRow == null)
                return;

            // Remove existing glyphs
            foreach (TableCell cell in gridView.HeaderRow.Cells)
            {
                foreach (Control ctrl in cell.Controls)
                {
                    if (ctrl is Image img && img.ID == "sortGlyph")
                        cell.Controls.Remove(ctrl);
                }
            }

            // Create new glyphs for each sortable column
            foreach (TableCell cell in gridView.HeaderRow.Cells)
            {
                if (cell.Controls.OfType<LinkButton>().Any())
                {
                    LinkButton linkButton = cell.Controls.OfType<LinkButton>().First();
                    Image glyph = new Image
                    {
                        ID = "sortGlyph",
                        EnableTheming = false,
                        Width = Unit.Pixel(10),
                        Height = Unit.Pixel(10)
                    };

                    if (string.Compare(currentSortColumn, linkButton.CommandArgument, true) == 0)
                    {
                        glyph.ImageUrl = currentSortDirection == "ASC" ? "~/img/up.png" : "~/img/down.png";
                        glyph.AlternateText = currentSortDirection == "ASC" ? "Ascending" : "Descending";
                    }
                    else
                    {
                        glyph.ImageUrl = "~/img/up.png";
                        glyph.AlternateText = "Ascending";
                    }

                    cell.Controls.Add(glyph);
                }
            }
        }

        protected void lnkCategory_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton linkButton)
            {
                // Always toggle the sort direction
                SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

                // Update the sort expression to the column that was clicked
                SortExpression = linkButton.CommandArgument;

                // Rebind the GridView with the new sorting applied
                BindCategory();
                PositionGlyph(gvCategory, SortExpression, SortDirection);
            }
        }

        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvCategory.PageIndex = e.NewPageIndex;

            BindCategory();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (hdnId.Value != "0")
            {
                args.IsValid = true;
                return;
            }

            string categoryName = args.Value;
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

        protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        imgCategory.Attributes["src"] = !string.IsNullOrEmpty(imageUrl) ? imageUrl : "/path/to/default/image.jpg";
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
            if (string.IsNullOrEmpty(categoryImageUrl))
            {
                string sqlGetImage = "SELECT CategoryImageUrl FROM Category WHERE CategoryId = @CategoryId";
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlGetImage, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        con.Open();
                        categoryImageUrl = cmd.ExecuteScalar()?.ToString();
                    }
                }
            }

            string sql = "UPDATE Category SET CategoryName = @CategoryName, CategoryImageUrl = @CategoryImageUrl, IsActive = @IsActive, CreatedDate = @CreatedDate WHERE CategoryId = @CategoryId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@CategoryImageUrl", string.IsNullOrEmpty(categoryImageUrl) ? (object)DBNull.Value : categoryImageUrl);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            ShowSuccessMessage("Category updated successfully.");
            BindCategory(); // Rebind data to GridView
            ClearForm(); // Clear the form after update
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

            // Hide the message after a delay using a client-side script
            ScriptManager.RegisterStartupScript(this, GetType(), "hideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 3000);", true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            string searchTerm = txtSearch.Text.Trim();
            BindCategory(searchTerm);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;

            // Reset the sorting settings
            SortExpression = "CategoryId"; // Default sorting column
            SortDirection = "ASC"; // Default sorting direction

            // Reset page index
            gvCategory.PageIndex = 0;

            BindCategory();

            lblMsg.Visible = false;
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