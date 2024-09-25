using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.RatingReview
{
    public partial class editComment : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the RatingReviewID from the query string
                string RatingReviewIDQuery = Request.QueryString["RatingReviewID"];

                if (!string.IsNullOrEmpty(RatingReviewIDQuery) && int.TryParse(RatingReviewIDQuery, out int ratingReviewIDQuery))
                {
                    // Save the RatingReviewID in a ViewState or field for later use
                    ViewState["RatingReviewID"] = ratingReviewIDQuery;

                    // Load previous review if it exists
                    LoadPreviousReview(ratingReviewIDQuery);
                }
                else
                {
                    // Handle case when RatingReviewID is invalid or missing
                    Response.Redirect("~/RatingReview/OwnComment.aspx");
                }
            }
        }

        private void LoadPreviousReview(int ratingReviewID)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"SELECT RatingScore, ReviewContent 
                         FROM Review 
                         WHERE RatingReviewID = @RatingReviewID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate the rating and comment fields with the existing review
                        rblRating.SelectedValue = reader["RatingScore"].ToString();
                        txtComment.Text = reader["ReviewContent"].ToString();
                    }
                    else
                    {
                        // No previous review found, keep the fields empty
                        rblRating.ClearSelection();
                        txtComment.Text = string.Empty;
                    }
                }
            }
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RatingReview/OwnComment.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ratingReviewID = Convert.ToInt32(ViewState["RatingReviewID"]);

            // Retrieve form data
            int ratingScore = Convert.ToInt32(rblRating.SelectedValue);
            string reviewContent = txtComment.Text.Trim();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                // Check if the review already exists
                string checkQuery = @"SELECT COUNT(*) 
                      FROM Review 
                      WHERE RatingReviewID = @RatingReviewID";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewID);

                    conn.Open();
                    int reviewExists = (int)checkCmd.ExecuteScalar();

                    // If review exists, update it. Otherwise, insert a new review.
                    if (reviewExists > 0)
                    {
                        // Update the existing review
                        string updateQuery = @"UPDATE Review 
                       SET RatingScore = @RatingScore, ReviewContent = @ReviewContent, RatingReviewDateTime = @RatingReviewDateTime
                       WHERE RatingReviewID = @RatingReviewID";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@RatingScore", ratingScore);
                            updateCmd.Parameters.AddWithValue("@ReviewContent", reviewContent);
                            updateCmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewID);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert a new review
                        string insertQuery = @"INSERT INTO Review (RatingScore, ReviewContent, RatingReviewDateTime, RatingReviewID)
                       VALUES (@RatingScore, @ReviewContent, @RatingReviewDateTime, @RatingReviewID)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@RatingScore", ratingScore);
                            insertCmd.Parameters.AddWithValue("@ReviewContent", reviewContent);
                            insertCmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                            insertCmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewID);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            Response.Redirect("~/RatingReview/OwnComment.aspx");
        }
    }
}