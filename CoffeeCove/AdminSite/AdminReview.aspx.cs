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
                string reviewId = Request.QueryString["ratingReviewID"];
                if (!string.IsNullOrEmpty(reviewId))
                {
                    
                }
            }
        }

        protected void Btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Review.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string reviewId = Request.QueryString["ReviewID"];
            string comment = txtComment.Text.Trim(); // Get the comment entered by the admin
            int ratingScore = 5; // Fixed rating score of 5
            DateTime currentDateTime = DateTime.Now; // Current date and time
            string usernameAdmin = "admin"; // Fixed admin username

            if (!string.IsNullOrEmpty(reviewId))
            {
                // Get the PaymentID related to the existing review (the one being replied to)
                int paymentId = 0;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string queryGetPaymentId = "SELECT PaymentID FROM Review WHERE RatingReviewID = @ReviewID";
                    using (SqlCommand cmd = new SqlCommand(queryGetPaymentId, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                        paymentId = (int)cmd.ExecuteScalar(); // Get the PaymentID
                    }
                }

                // Now insert the new review (admin's reply)
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string queryInsert = @"INSERT INTO Review (RatingScore, ReviewContent, RatingReviewDateTime, PaymentID, ReplyTo, UsernameAdmin) 
                                           VALUES (@RatingScore, @ReviewContent, @RatingReviewDateTime, @PaymentID, @ReplyTo, @UsernameAdmin)";

                    using (SqlCommand cmd = new SqlCommand(queryInsert, conn))
                    {
                        // Add the parameters to the command
                        cmd.Parameters.AddWithValue("@RatingScore", ratingScore);
                        cmd.Parameters.AddWithValue("@ReviewContent", comment);
                        cmd.Parameters.AddWithValue("@RatingReviewDateTime", currentDateTime);
                        cmd.Parameters.AddWithValue("@PaymentID", paymentId); // From the existing review
                        cmd.Parameters.AddWithValue("@ReplyTo", reviewId); // ReviewID from query string
                        cmd.Parameters.AddWithValue("@UsernameAdmin", usernameAdmin);

                        // Execute the command to insert the new record
                        cmd.ExecuteNonQuery();
                    }
                }

                // After inserting, redirect to the Review page
                Response.Redirect("Review.aspx");
            }
        }
    }
}