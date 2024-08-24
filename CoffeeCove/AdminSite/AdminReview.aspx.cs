using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class AdminReview : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string reviewId = Request.QueryString["ReviewID"];
                if (!string.IsNullOrEmpty(reviewId))
                {
                    LoadReviewDetails(reviewId);
                }
            }
        }

        private void LoadReviewDetails(string reviewId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Review WHERE RatingReviewID = @ReviewID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate fields with review data
                        // Example: txtReviewContent.Text = reader["ReviewContent"].ToString();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Review.aspx");
        }
    }
}