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
using iTextSharp.text.pdf;
using iTextSharp.text;
using CoffeeCove.AdminSite;
using System.Globalization;

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
            }
            lblMsg.Visible = false;
        }

        private void BindCategory(string searchTerm = "")
        {
            string sql = "SELECT * FROM Category WHERE CategoryId != 0";
            string filter = ddlFilter.SelectedValue;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sql += " AND CategoryId LIKE @SearchTerm OR CategoryName LIKE @SearchTerm";
            }

            if (filter != "All")
            {
                sql += " AND IsActive = @IsActive";
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

                    if (filter != "All")
                    {
                        cmd.Parameters.AddWithValue("@IsActive", filter == "True");
                    }

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    gvCategory.DataSource = dt;
                    gvCategory.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        gvCategory.DataSource = null;
                        gvCategory.DataBind();
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
            get { return ViewState["SortExpression"] as string ?? "CategoryId"; }
            set { ViewState["SortExpression"] = value; }
        }

        protected void gvCategory_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

            SortExpression = e.SortExpression;

            BindCategory();
        }

        protected void lnkCategory_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton linkButton)
            {
                SortExpression = linkButton.CommandArgument;

                SortDirection = (SortDirection == "ASC" && SortExpression == linkButton.CommandArgument) ? "DESC" : "ASC";

                BindCategory();
            }
        }

        private void UpdateSortIcons()
        {
            Literal litSortIconId = gvCategory.HeaderRow.FindControl("litSortIconId") as Literal;
            Literal litSortIconName = gvCategory.HeaderRow.FindControl("litSortIconName") as Literal;
            Literal litSortIconDate = gvCategory.HeaderRow.FindControl("litSortIconDate") as Literal;

            string defaultIcon = "<i class='bi bi-caret-up-fill'></i>";
            string ascendingIcon = "<i class='bi bi-caret-up-fill'></i>";
            string descendingIcon = "<i class='bi bi-caret-down-fill'></i>";

            litSortIconId.Text = defaultIcon;
            litSortIconName.Text = defaultIcon;
            litSortIconDate.Text = defaultIcon;

            if (SortExpression == "CategoryId")
            {
                litSortIconId.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
            else if (SortExpression == "CategoryName")
            {
                litSortIconName.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
            else if (SortExpression == "CreatedDate")
            {
                litSortIconDate.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
        }

        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategory.PageIndex = e.NewPageIndex;

            BindCategory();
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCategory.PageSize = int.Parse(ddlPageSize.SelectedValue);

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
                        imgCategory.Attributes["src"] = imageUrl;
                        cbIsActive.Checked = !Convert.IsDBNull(dr["IsActive"]) && (bool)dr["IsActive"];
                        hdnId.Value = categoryId.ToString();

                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            RequiredFieldValidator2.Enabled = false;
                        }
                        else
                        {
                            RequiredFieldValidator2.Enabled = true;
                        }

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
            BindCategory();
            ClearForm();
        }

        private string UploadImage()
        {
            if (fuCategoryImage.HasFile)
            {
                string fileName = Guid.NewGuid().ToString("N") + ".jpg";
                string filePath = Server.MapPath("~/img/Category/") + fileName;

                // Load the uploaded image into a Bitmap object
                using (System.Drawing.Image originalImage = System.Drawing.Image.FromStream(fuCategoryImage.PostedFile.InputStream))
                {
                    using (var jpgImage = new System.Drawing.Bitmap(originalImage))
                    {
                        // Resize image 
                        using (var resizedImage = new System.Drawing.Bitmap(jpgImage, new System.Drawing.Size(150, 150)))
                        {
                            // Save as a jpg file
                            resizedImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                }

                // return relative path store in the database
                return "/img/Category/" + fileName;
            }

            // if no file is uploaded
            return null;
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
            lblMsg.Visible = false;
            string searchTerm = txtSearch.Text.Trim();
            BindCategory(searchTerm);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;

            SortExpression = "CategoryId";
            SortDirection = "ASC";
            ddlFilter.SelectedIndex = 0;
            gvCategory.PageIndex = 0;

            BindCategory();

            lblMsg.Visible = false;

            UpdateSortIcons();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemList(string Text)
        {
            List<string> getitem = new List<string>();

            string sql = "SELECT CategoryId, CategoryName FROM Category WHERE CategoryId LIKE @Text OR CategoryName LIKE @Text";
            using (SqlConnection con = new SqlConnection(Global.CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Text", "%" + Text + "%");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        getitem.Add(dr["CategoryId"].ToString());
                        getitem.Add(dr["CategoryName"].ToString());
                    }
                }
            }

            return getitem;
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // Set up PDF response properties
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CategoryReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            string sql = @"SELECT 
                     c.CategoryId, 
                     c.CategoryName, 
                     c.IsActive,
                     COUNT(p.ProductId) AS TotalProduct, 
                     COALESCE(SUM(oi.Quantity * oi.Price), 0) AS TotalSales,
                     COALESCE(SUM(oi.Quantity), 0) AS ProductSold
                     FROM Category c 
                     LEFT JOIN Product p ON c.CategoryId = p.CategoryId
                     LEFT JOIN OrderedItem oi ON p.ProductId = oi.ProductID
                     WHERE c.CategoryId != 0
                     GROUP BY c.CategoryId, c.CategoryName, c.IsActive";


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

            Paragraph title = new Paragraph("Category Summary Report", FontFactory.GetFont("Arial", 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(title);
            pdfdoc.Add(new Paragraph(" "));

            int totalCategories = dt.Rows.Count;

            Paragraph exportInfo = new Paragraph($"ExportDate: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\nTotal Category: {totalCategories}", FontFactory.GetFont("Arial", 12, Font.NORMAL));
            exportInfo.Alignment = Element.ALIGN_LEFT;
            pdfdoc.Add(exportInfo);

            pdfdoc.Add(new Paragraph(" "));

            PdfPTable pdfTable = new PdfPTable(6);
            pdfTable.WidthPercentage = 100;
            pdfTable.SetWidths(new float[] { 1f, 2f, 1f, 1f, 1f, 1f });
            BaseColor lightGrey = new BaseColor(211, 211, 211);

            // table headers
            pdfTable.AddCell(new PdfPCell(new Phrase("Category ID")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Category Name")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Is Active")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Total Product")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Total Sales(RM)")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Product Sold")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            int totalProductSum = 0;
            decimal totalSalesSum = 0;
            int totalSoldSum = 0;

            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(new PdfPCell(new Phrase(row["CategoryId"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["CategoryName"].ToString())) { HorizontalAlignment = Element.ALIGN_LEFT });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["IsActive"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["TotalProduct"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                // Check for Null and handle properly  
                decimal rowTotalSales = row["TotalSales"] != DBNull.Value ? Convert.ToDecimal(row["TotalSales"]) : 0;
                int rowProductSold = row["ProductSold"] != DBNull.Value ? Convert.ToInt32(row["ProductSold"]) : 0;

                pdfTable.AddCell(new PdfPCell(new Phrase(rowTotalSales.ToString("0.00"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(rowProductSold.ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });

                totalProductSum += Convert.ToInt32(row["TotalProduct"]);
                totalSalesSum += rowTotalSales;
                totalSoldSum += rowProductSold;
            }

            pdfTable.AddCell(new PdfPCell(new Phrase("Total")) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(totalProductSum.ToString())) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(totalSalesSum.ToString("0.00"))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(totalSoldSum.ToString())) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            pdfdoc.Add(pdfTable);

            pdfdoc.Close();
            Response.Write(pdfdoc);
            Response.End();
        }

    }
}