using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            }
        }

        private void BindCategoryDropDown()
        {
            string sql = "SELECT CategoryId, CategoryName FROM Category WHERE CategoryId != 1";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlCategory.DataSource = dr;
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
            }
            ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
        }

        private void BindProduct(string searchTerm = "", string sortExpression = "", string sortDirection = "ASC")
        {
            int pageIndex = GridViewProduct.PageIndex;
            int pageSize = GridViewProduct.PageSize;

            string sql = @"SELECT p.ProductId, p.ProductName, p.Description, p.UnitPrice, p.ImageUrl, p.IsActive, 
                        p.CategoryId, c.CategoryName, p.CreatedDate 
                        FROM Product p
                        INNER JOIN Category c ON p.CategoryId = c.CategoryId";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sql += " WHERE ProductId && ProductName LIKE @SearchTerm + '%'";
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

                    GridViewProduct.DataSource = dt;
                    GridViewProduct.DataBind();
                }
            }
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

        protected void GridViewProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditProduct")
            {
                string productId = (string)e.CommandArgument;
                LoadProductForEdit(productId);
                UpdatePanel1.Update();
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
            string searchTerm = txtSearch.Text.Trim();
            BindProduct(searchTerm);
        }

        protected void GridViewProduct_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);

            BindProduct(txtSearch.Text.Trim(), sortExpression, sortDirection);
        }

        private string GetSortDirection(string column)
        {
            // Default - ascending
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

        protected void GridViewProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProduct.PageIndex = e.NewPageIndex;
            BindProduct(txtSearch.Text.Trim());
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemList(string prefixText)
        {
            List<string> getitem = new List<string>();

            string sql = "SELECT ProductId, ProductName FROM Product WHERE ProductName && ProductId LIKE @Text + '%'";
            using (SqlConnection con = new SqlConnection(Global.CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Text", prefixText);
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