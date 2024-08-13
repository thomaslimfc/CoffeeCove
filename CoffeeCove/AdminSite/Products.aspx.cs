﻿using System;
using System.Collections.Generic;
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
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId, CategoryName FROM Category WHERE CategoryId != 1";
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

        private void BindProduct()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT p.ProductId, p.ProductName, p.Description, p.UnitPrice, p.ImageUrl, p.IsActive, 
                        p.CategoryId, c.CategoryName, p.CreatedDate 
                        FROM Product p
                        INNER JOIN Category c ON p.CategoryId = c.CategoryId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    GridViewProduct.DataSource = dr;
                    GridViewProduct.DataBind();
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
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT ISNULL(MAX(CAST(SUBSTRING(ProductId, 3, 2) AS INT)), 0) FROM Product WHERE CategoryId = @CategoryId";
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            string description = txtDesc.Text.Trim();
            decimal unitPrice = Convert.ToDecimal(txtPrice.Text.Trim());
            bool isActive = cbIsActive.Checked;
            int categoryId;
            bool isCategorySelected = int.TryParse(ddlCategory.SelectedValue, out categoryId);

            //if product exist
            if (IsProductExists(productName))
            {
                lblMsg.Text = "Product already exist.";
                lblMsg.Visible = true;
                HideMessageAfterDelay();
                return;
            }

            //if didnt select category
            if (!isCategorySelected || categoryId == 0)
            {
                lblMsg2.Text = "Please select a category.";
                lblMsg2.Visible = true;
                HideMessageAfterDelay();
                return;
            }

            //upload image
            string imageUrl = null;

            if (hdnId.Value != "0")
            {
                if (fuProductImage.HasFile)
                {
                    imageUrl = UploadImage();
                }
                else
                {
                    imageUrl = imgProduct.Attributes["src"];
                }

                UpdateProduct(productName, description, imageUrl, unitPrice, isActive);
            }
            else
            {
                //add new product
                string categoryName = ddlCategory.SelectedItem.Text;
                string productId = GenerateProductId(categoryId);

                using (SqlConnection con = new SqlConnection(cs))
                {
                    string sql = @"INSERT INTO Product (ProductId, ProductName, Description, UnitPrice, CategoryId, ImageUrl, IsActive, CreatedDate)
                            VALUES (@ProductId, @ProductName, @Description, @UnitPrice, @CategoryId, @ImageUrl, @IsActive, @CreatedDate)";
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
                lblMsg.Text = "Product added successfully!";
                lblMsg.Visible = true;
                HideMessageAfterDelay();
                BindProduct();
                ClearForm();
            }
        }

        private void UpdateProduct(string productName, string description, string imageUrl, decimal unitPrice, bool isActive)
        {
            string productId = hdnId.Value;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "UPDATE Product SET ProductName = @ProductName, Description = @Description, ImageUrl = @ImageUrl, UnitPrice = @UnitPrice, IsActive = @IsActive, CreatedDate = @CreatedDate WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMsg.Text = "Product updated successfully!";
            lblMsg.Visible = true;
            HideMessageAfterDelay();
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

        private bool IsProductExists(string productName)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT COUNT(*) FROM Product WHERE ProductName = @ProductName";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    con.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
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

        protected void GridViewProduct_RowCommand(object sender, GridViewCommandEventArgs e)
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
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT ProductId, ProductName, Description, UnitPrice, ImageUrl, IsActive, CategoryId FROM Product WHERE ProductId = @ProductId";
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
                        // Set the selected category by ID
                        ddlCategory.SelectedValue = dr["CategoryId"].ToString();
                        cbIsActive.Checked = !Convert.IsDBNull(dr["IsActive"]) && (bool)dr["IsActive"];
                        hdnId.Value = productId.ToString();
                    }
                }
            }

            btnAdd.Text = "Update";
        }

        private void DeleteProduct(string productId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    // Step 1: Get the category ID of the product to be deleted.
                    string sqlCategory = "SELECT CategoryId FROM Product WHERE ProductId = @ProductId";
                    int categoryId;
                    using (SqlCommand cmd = new SqlCommand(sqlCategory, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        categoryId = (int)cmd.ExecuteScalar();
                    }

                    // Step 2: Delete the product.
                    string sqlDelete = "DELETE FROM Product WHERE ProductId = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(sqlDelete, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.ExecuteNonQuery();
                    }

                    // Step 3: Reorder the remaining products in the category.
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

                    lblMsg.Text = "Product deleted successfully.";
                    lblMsg.Visible = true;
                    HideMessageAfterDelay();
                    BindProduct();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error deleting product: " + ex.Message;
                lblMsg.Visible = true;
                HideMessageAfterDelay();
            }
        }

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