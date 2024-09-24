using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CoffeeCove.RatingReview
{
    public partial class comment : System.Web.UI.Page
    {
        string cs = Global.CS;

        int customerID = 11;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the PaymentID from the query string
                string paymentIDQuery = Request.QueryString["PaymentID"];

                if (!string.IsNullOrEmpty(paymentIDQuery) && int.TryParse(paymentIDQuery, out int paymentID))
                {
                    // Save the PaymentID in a ViewState or field for later use
                    ViewState["PaymentID"] = paymentID;

                    // Load previous review if it exists
                    LoadPreviousReview(paymentID);
                }
                else
                {
                    // Handle case when PaymentID is invalid or missing
                    Response.Redirect("~/Order/OrderHistory.aspx");
                }
            }
        }

        private void LoadPreviousReview(int paymentID)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"SELECT RatingScore, ReviewContent 
                         FROM Review 
                         WHERE PaymentID = @PaymentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentID", paymentID);

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
            Response.Redirect("~/Order/orderHistory.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int paymentID = Convert.ToInt32(ViewState["PaymentID"]);

            // Retrieve form data
            int ratingScore = Convert.ToInt32(rblRating.SelectedValue);
            string reviewContent = txtComment.Text.Trim();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                // Check if the review already exists
                string checkQuery = @"SELECT COUNT(*) 
                      FROM Review 
                      WHERE PaymentID = @PaymentID";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@PaymentID", paymentID);
                    checkCmd.Parameters.AddWithValue("@CusID", customerID);

                    conn.Open();
                    int reviewExists = (int)checkCmd.ExecuteScalar();

                    // If review exists, update it. Otherwise, insert a new review.
                    if (reviewExists > 0)
                    {
                        // Update the existing review
                        string updateQuery = @"UPDATE Review 
                       SET RatingScore = @RatingScore, ReviewContent = @ReviewContent, RatingReviewDateTime = @RatingReviewDateTime
                       WHERE PaymentID = @PaymentID";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@RatingScore", ratingScore);
                            updateCmd.Parameters.AddWithValue("@ReviewContent", reviewContent);
                            updateCmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@PaymentID", paymentID);
                            updateCmd.Parameters.AddWithValue("@CusID", customerID);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert a new review
                        string insertQuery = @"INSERT INTO Review (RatingScore, ReviewContent, RatingReviewDateTime, PaymentID)
                       VALUES (@RatingScore, @ReviewContent, @RatingReviewDateTime, @PaymentID)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@RatingScore", ratingScore);
                            insertCmd.Parameters.AddWithValue("@ReviewContent", reviewContent);
                            insertCmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                            insertCmd.Parameters.AddWithValue("@PaymentID", paymentID);
                            insertCmd.Parameters.AddWithValue("@CusID", customerID);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            Response.Redirect("~/Order/orderHistory.aspx");
        }
    }
}