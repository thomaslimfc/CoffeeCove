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

        int paymentID = 1;
        int customerID = 11;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPreviousReview();
            }
        }

        private void LoadPreviousReview()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"SELECT RatingScore, ReviewContent 
                                 FROM Review 
                                 WHERE PaymentID = @PaymentID AND CusID = @CusID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentID", paymentID);
                    cmd.Parameters.AddWithValue("@CusID", customerID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate the rating and comment fields with the existing review
                        rblRating.SelectedValue = reader["RatingScore"].ToString();
                        txtComment.Text = reader["ReviewContent"].ToString();
                    }
                }
            }
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Order/OrderHistory.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve form data
            int ratingScore = Convert.ToInt32(rblRating.SelectedValue);
            string reviewContent = txtComment.Text.Trim();

            // Assuming PaymentID and CusID are passed as query strings or session
            // int paymentID = Convert.ToInt32(Request.QueryString["PaymentID"]);  // or Session["PaymentID"]
            // int customerID = Convert.ToInt32(Session["CusID"]);  // Assume you store customer ID in session

            using (SqlConnection conn = new SqlConnection(cs))
            {
                // Check if the review already exists
                string checkQuery = @"SELECT COUNT(*) 
                                      FROM Review 
                                      WHERE PaymentID = @PaymentID AND CusID = @CusID";

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
                                               WHERE PaymentID = @PaymentID AND CusID = @CusID";

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
                        string insertQuery = @"INSERT INTO Review (RatingScore, ReviewContent, RatingReviewDateTime, PaymentID, CusID)
                                               VALUES (@RatingScore, @ReviewContent, @RatingReviewDateTime, @PaymentID, @CusID)";

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

            // Redirect to the review page or confirmation page after submission
            Response.Redirect("ratingReview.aspx");
        }
    }
}