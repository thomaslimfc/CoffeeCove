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

        private void BindCategory(string searchTerm = ""/*, string sortExpression = "", string sortDirection = "ASC"*/)
        {
            int pageIndex = GridViewCategory.PageIndex;
            int pageSize = GridViewCategory.PageSize;

            string sql = "SELECT CategoryId, CategoryName, CategoryImageUrl, IsActive, CreatedDate FROM Category";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sql += " WHERE CategoryName LIKE @SearchTerm + '%'";
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

        
    }
}