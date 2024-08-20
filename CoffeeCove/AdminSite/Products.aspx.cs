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
    public partial class Products : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategoryDropDown();
                BindProduct();
                PositionGlyph(gvProduct, SortExpression, SortDirection);
            }
            lblMsg.Visible = false;
        }

        private void BindCategoryDropDown()
        {
            string sql = "SELECT CategoryId, CategoryName FROM Category WHERE CategoryId != 1";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlCategory.DataSource = dt;
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();

                    ddlFilterCategory.DataSource = dt;
                    ddlFilterCategory.DataTextField = "CategoryName";
                    ddlFilterCategory.DataValueField = "CategoryId";
                    ddlFilterCategory.DataBind();
                }
            }
            ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
            ddlFilterCategory.Items.Insert(0, new ListItem("All Categories", ""));
        }

        private void BindProduct(string searchTerm = "")
        {
            string sql = @"SELECT p.ProductId, p.ProductName, p.Description, p.UnitPrice, p.ImageUrl, p.IsActive, 
                        p.CategoryId, c.CategoryName, p.CreatedDate 
                        FROM Product p
                        INNER JOIN Category c ON p.CategoryId = c.CategoryId WHERE p.IsActive=1";
            string selectedCategory = ddlFilterCategory.SelectedValue;
            string filterActive = ddlFilterActive.SelectedValue;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sql += " WHERE ProductId LIKE @SearchTerm OR ProductName LIKE @SearchTerm";
            }

            if (!string.IsNullOrEmpty(selectedCategory))
            {
                sql += " AND p.CategoryId = @CategoryId";
            }

            if (filterActive != "All")
            {
                sql += " AND p.IsActive = @IsActive";
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
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm + '%');
                    }

                    if (!string.IsNullOrEmpty(selectedCategory))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", selectedCategory);
                    }

                    if (filterActive != "All")
                    {
                        cmd.Parameters.AddWithValue("@IsActive", filterActive == "True");
                    }

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Use a DataTable for paging
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        gvProduct.DataSource = null;
                        gvProduct.DataBind();
                    }
                }
            }
            PositionGlyph(gvProduct, SortExpression, SortDirection);
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "ProductId"; }
            set { ViewState["SortExpression"] = value; }
        }

        protected void gvProduct_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Always toggle the sort direction
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

            // Update the sort expression to the new column or keep the same column
            SortExpression = e.SortExpression;

            // Rebind the GridView with the new sorting applied
            BindProduct();
            PositionGlyph(gvProduct, SortExpression, SortDirection);
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

        protected void lnkProduct_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton linkButton)
            {
                // Always toggle the sort direction
                SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

                // Update the sort expression to the column that was clicked
                SortExpression = linkButton.CommandArgument;

                // Rebind the GridView with the new sorting applied
                BindProduct();
                PositionGlyph(gvProduct, SortExpression, SortDirection);
            }
        }

        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvProduct.PageIndex = e.NewPageIndex;

            BindProduct();
        }

        protected void ddlFilterActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProduct();
        }

        protected void ddlFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProduct();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string ProductName = args.Value;
            string ProductId = hdnId.Value;

            string sql = "SELECT COUNT(*) FROM Product WHERE ProductName = @ProductName AND ProductId != @ProductId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@ProductId", ProductId);

                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    args.IsValid = count == 0;
                }
            }
        }

        public string GetCategoryPrefix(int categoryId)
        {
            switch (categoryId)
            {
                case 2: return "CC";
                case 3: return "EC";
                case 4: return "OB";
                case 5: return "BF";
                case 6: return "LC";
                case 7: return "DS";
                default: throw new ArgumentException("Invalid category ID");
            }
        }

        public int GetNextIndex(int categoryId)
        {
            string sql = "SELECT ISNULL(MAX(CAST(SUBSTRING(ProductId, 3, 2) AS INT)), 0) FROM Product WHERE CategoryId = @CategoryId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    con.Open();
                    int highestIndex = (int)cmd.ExecuteScalar();
                    return highestIndex + 1;
                }
            }
        }

        public string GenerateProductId(int categoryId)
        {
            string prefix = GetCategoryPrefix(categoryId);
            int nextIndex = GetNextIndex(categoryId);

            string formattedIndex = nextIndex.ToString("D2");

            return prefix + formattedIndex;
        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditProduct")
            {
                string productId = (string)e.CommandArgument;
                LoadProductForEdit(productId);
            }
            else if (e.CommandName == "DeleteProduct")
            {
                string productId = (string)e.CommandArgument;
                DeleteProduct(productId);
            }
        }

        private void LoadProductForEdit(string productId)
        {
            string sql = "SELECT ProductId, ProductName, Description, UnitPrice, ImageUrl, IsActive, CategoryId FROM Product WHERE ProductId = @ProductId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtProductName.Text = dr["ProductName"].ToString();
                        txtDesc.Text = dr["Description"].ToString();
                        txtPrice.Text = dr["UnitPrice"].ToString();
                        imgProduct.Attributes["src"] = dr["ImageUrl"].ToString();
                        ddlCategory.SelectedValue = dr["CategoryId"].ToString();
                        cbIsActive.Checked = !Convert.IsDBNull(dr["IsActive"]) && (bool)dr["IsActive"];
                        hdnId.Value = productId.ToString();
                    }
                }
            }
            btnAdd.Text = "Update";
        }

        private void UpdateProduct(string productName, string description, string imageUrl, decimal unitPrice, bool isActive)
        {
            string productId = hdnId.Value;
            string sql = "UPDATE Product SET ProductName = @ProductName, Description = @Description, ImageUrl = @ImageUrl, UnitPrice = @UnitPrice, IsActive = @IsActive, CreatedDate = @CreatedDate WHERE ProductId = @ProductId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@ImageUrl", string.IsNullOrEmpty(imageUrl) ? (object)DBNull.Value : imageUrl);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            ShowSuccessMessage("Product updated successfully.");
            BindProduct();
            ClearForm();
        }

        private string UploadImage()
        {
            if (fuProductImage.HasFile)
            {
                string fileName = Path.GetFileName(fuProductImage.PostedFile.FileName);
                string filePath = Server.MapPath("/imgProductItems/") + fileName;

                // Save the uploaded image
                fuProductImage.SaveAs(filePath);
                return "/imgProductItems/" + fileName;
            }
            return null; // Return null if no image uploaded
        }

        private void DeleteProduct(string productId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sqlCategory = "SELECT CategoryId FROM Product WHERE ProductId = @ProductId";
                int categoryId;
                using (SqlCommand cmd = new SqlCommand(sqlCategory, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    categoryId = (int)cmd.ExecuteScalar();
                }

                string sqlDelete = "DELETE FROM Product WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(sqlDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();
                }

                string prefix = GetCategoryPrefix(categoryId);
                string sqlUpdateIds = @";WITH CTE AS (SELECT ProductId,ROW_NUMBER() OVER (ORDER BY CAST(SUBSTRING(ProductId, 3, 2) AS INT)) AS RowNum
                FROM Product WHERE CategoryId = @CategoryId) UPDATE Product SET ProductId = @Prefix + RIGHT('00' + CAST(CTE.RowNum AS VARCHAR(2)), 2)
                FROM Product INNER JOIN CTE ON Product.ProductId = CTE.ProductId";

                using (SqlCommand cmd = new SqlCommand(sqlUpdateIds, con))
                {
                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.ExecuteNonQuery();
                }

                BindProduct();
                ClearForm();
            }
            ShowSuccessMessage("Product deleted successfully.");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string productName = txtProductName.Text.Trim();
                string description = txtDesc.Text.Trim();
                decimal unitPrice = Convert.ToDecimal(txtPrice.Text.Trim());
                bool isActive = cbIsActive.Checked;
                int categoryId;
                bool isCategorySelected = int.TryParse(ddlCategory.SelectedValue, out categoryId);
                string imageUrl = null;

                if (fuProductImage.HasFile)
                {
                    imageUrl = UploadImage();
                }
                else if (hdnId.Value != "0")
                {
                    imageUrl = imgProduct.Attributes["src"];
                }

                if (hdnId.Value != "0")
                {
                    UpdateProduct(productName, description, imageUrl, unitPrice, isActive);
                }
                else
                {
                    string productId = GenerateProductId(categoryId);

                    string sql = @"INSERT INTO Product (ProductId, ProductName, Description, UnitPrice, CategoryId, ImageUrl, IsActive, CreatedDate)
                    VALUES (@ProductId, @ProductName, @Description, @UnitPrice, @CategoryId, @ImageUrl, @IsActive, @CreatedDate)";
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@ProductId", productId);
                            cmd.Parameters.AddWithValue("@ProductName", productName);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                            cmd.Parameters.AddWithValue("@ImageUrl", string.IsNullOrEmpty(imageUrl) ? (object)DBNull.Value : imageUrl);
                            cmd.Parameters.AddWithValue("@IsActive", isActive);
                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    ShowSuccessMessage("Product added successfully.");
                }
                BindProduct();
                ClearForm();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.Transfer("Products.aspx");
        }

        private void ClearForm()
        {
            txtProductName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtPrice.Text = string.Empty;
            hdnId.Value = "0";
            ddlCategory.SelectedIndex = 0;
            imgProduct.Attributes["src"] = string.Empty;
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
            lblMsg.Visible = false;
            string searchTerm = txtSearch.Text.Trim();
            BindProduct(searchTerm);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;

            // Reset the sorting settings
            SortExpression = "CategoryId"; // Default sorting column
            SortDirection = "ASC"; // Default sorting direction

            // Reset page index
            gvProduct.PageIndex = 0;

            BindProduct();

            lblMsg.Visible = false;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemList(string prefixText)
        {
            List<string> getitem = new List<string>();

            string sql = "SELECT ProductId, ProductName FROM Product WHERE ProductId LIKE @Text OR ProductName LIKE @Text";
            using (SqlConnection con = new SqlConnection(Global.CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Text", prefixText + '%');
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        getitem.Add(dr["ProductId"].ToString());
                        getitem.Add(dr["ProductName"].ToString());
                    }
                }
            }

            return getitem;
        }
    }
}
