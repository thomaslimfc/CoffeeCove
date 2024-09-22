using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ListItem = System.Web.UI.WebControls.ListItem;

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
            lblMsg.Visible = false;
        }

        private void BindCategoryDropDown()
        {
            string sql = "SELECT CategoryId, CategoryName FROM Category WHERE CategoryId != 0";
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
                sql += " AND ProductId LIKE @SearchTerm OR ProductName LIKE @SearchTerm";
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
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
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

                    UpdateSortIcons();
                }
            }
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
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

            SortExpression = e.SortExpression;

            BindProduct();
        }

        protected void lnkProduct_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton linkButton)
            {
                SortExpression = linkButton.CommandArgument;

                SortDirection = (SortDirection == "ASC" && SortExpression == linkButton.CommandArgument) ? "DESC" : "ASC";

                BindProduct();
            }
        }

        private void UpdateSortIcons()
        {
            Literal litSortIconId = gvProduct.HeaderRow.FindControl("litSortIconId") as Literal;
            Literal litSortIconName = gvProduct.HeaderRow.FindControl("litSortIconName") as Literal;
            Literal litSortIconPrice = gvProduct.HeaderRow.FindControl("litSortIconPrice") as Literal;
            Literal litSortIconDate = gvProduct.HeaderRow.FindControl("litSortIconDate") as Literal;

            string defaultIcon = "<i class='bi bi-caret-up-fill'></i>";
            string ascendingIcon = "<i class='bi bi-caret-up-fill'></i>";
            string descendingIcon = "<i class='bi bi-caret-down-fill'></i>";

            litSortIconId.Text = defaultIcon;
            litSortIconName.Text = defaultIcon;
            litSortIconPrice.Text = defaultIcon;
            litSortIconDate.Text = defaultIcon;

            if (SortExpression == "ProductId")
            {
                litSortIconId.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
            else if (SortExpression == "ProductName")
            {
                litSortIconName.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
            else if (SortExpression == "UnitPrice")
            {
                litSortIconPrice.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
            else if (SortExpression == "CreatedDate")
            {
                litSortIconDate.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
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

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected page size from the dropdown list
            gvProduct.PageSize = int.Parse(ddlPageSize.SelectedValue);

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
                try
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
                    ShowSuccessMessage("Product deleted successfully.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // SQL Server error code for foreign key violation
                    {
                        ShowErrorMessage("This product cannot be deleted because it is referenced in an order. You can soft delete using IsActive");
                    }
                }
            }


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

        private void ShowErrorMessage(string message)
        {
            lblErrorMsg.Text = message;
            lblErrorMsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "hideMessage", "setTimeout(function() { document.getElementById('" + lblErrorMsg.ClientID + "').style.display = 'none'; }, 3000);", true);
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

            SortExpression = "ProductId"; // Default sorting column
            SortDirection = "ASC"; // Default sorting direction

            // Reset page index
            gvProduct.PageIndex = 0;

            BindProduct();

            lblMsg.Visible = false;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemList(string Text)
        {
            List<string> getitem = new List<string>();

            string sql = "SELECT ProductId, ProductName FROM Product WHERE ProductId LIKE @Text OR ProductName LIKE @Text";
            using (SqlConnection con = new SqlConnection(Global.CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Text", "%" + Text + "%");
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

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // Set up PDF response properties
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ProductReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            string sql = @"SELECT 
                     p.ProductId, 
                     p.ProductName, 
                     p.UnitPrice, 
                     p.CreatedDate,
                     COALESCE(SUM(oi.Quantity * oi.Price), 0) AS TotalSales,
                     COALESCE(SUM(oi.Quantity), 0) AS TotalSold
                     FROM Product p
                     LEFT JOIN OrderedItem oi ON p.ProductId = oi.ProductID
                     GROUP BY p.ProductId, p.ProductName, p.UnitPrice, p.CreatedDate";

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }

            // Set up iTextSharp PDF document
            Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
            pdfdoc.Open();

            Paragraph title = new Paragraph("Product Summary Report", FontFactory.GetFont("Arial", 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(title);
            pdfdoc.Add(new Paragraph(" "));

            int totalProducts = dt.Rows.Count;

            Paragraph exportInfo = new Paragraph($"ExportDate: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\nTotal Products: {totalProducts}", FontFactory.GetFont("Arial", 12, Font.NORMAL));
            exportInfo.Alignment = Element.ALIGN_LEFT;
            pdfdoc.Add(exportInfo);

            pdfdoc.Add(new Paragraph(" "));

            PdfPTable pdfTable = new PdfPTable(6); // 6 columns
            pdfTable.WidthPercentage = 100;
            pdfTable.SetWidths(new float[] { 1f, 2f, 1f, 1f, 1f, 1f });
            BaseColor lightGrey = new BaseColor(211, 211, 211);

            // table headers
            pdfTable.AddCell(new PdfPCell(new Phrase("Product ID")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Product Name")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Unit Price (RM)")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Created Date")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Total Sales (RM)")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Total Sold")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            // Initialize sums
            decimal totalSalesSum = 0;
            int totalSoldSum = 0;

            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(new PdfPCell(new Phrase(row["ProductId"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["ProductName"].ToString())) { HorizontalAlignment = Element.ALIGN_LEFT });
                pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["UnitPrice"]).ToString("N2"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(row["CreatedDate"]).ToString("dd/MM/yyyy"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["TotalSales"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["TotalSold"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });

                totalSalesSum += Convert.ToDecimal(row["TotalSales"]);
                totalSoldSum += Convert.ToInt32(row["TotalSold"]);
            }

            pdfTable.AddCell(new PdfPCell(new Phrase("Total")) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(totalSalesSum.ToString("N2"))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(totalSoldSum.ToString())) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            pdfdoc.Add(pdfTable);

            pdfdoc.Close();
            Response.Write(pdfdoc);
            Response.End();
        }

    }
}
