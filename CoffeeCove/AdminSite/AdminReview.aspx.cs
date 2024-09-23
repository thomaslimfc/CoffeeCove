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
                string ratingReviewId = Request.QueryString["RatingReviewID"];
                string paymentId = Request.QueryString["PaymentID"];

                if (!string.IsNullOrEmpty(ratingReviewId) && !string.IsNullOrEmpty(paymentId))
                {
                    PopulatePreviousReply(ratingReviewId, paymentId);
                }
            }
        }

        private void PopulatePreviousReply(string ratingReviewId, string paymentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string query = @"
                    SELECT ReviewContent
                    FROM Review
                    WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID AND UsernameAdmin = 'admin'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewId);
                    cmd.Parameters.AddWithValue("@PaymentID", paymentId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        // If a reply exists, populate the comment box with the previous ReviewContent
                        txtComment.Text = result.ToString();
                    }
                    else
                    {
                        // If no reply exists, leave the comment box empty
                        txtComment.Text = string.Empty;
                    }
                }

                con.Close();
            }
        }

        protected void Btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Review.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ratingReviewId = Request.QueryString["RatingReviewID"];
            string paymentId = Request.QueryString["PaymentID"];

            if (!string.IsNullOrEmpty(ratingReviewId) && !string.IsNullOrEmpty(paymentId))
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    // Check if there's already a reply
                    string checkQuery = @"
                SELECT COUNT(*)
                FROM Review
                WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID AND UsernameAdmin = 'admin'";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewId);
                        checkCmd.Parameters.AddWithValue("@PaymentID", paymentId);

                        int count = (int)checkCmd.ExecuteScalar();

                        string query;
                        if (count > 0)
                        {
                            // If a reply exists, update the existing reply
                            query = @"
                        UPDATE Review
                        SET ReviewContent = @ReviewContent, RatingReviewDateTime = @RatingReviewDateTime
                        WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID AND UsernameAdmin = 'admin'";
                        }
                        else
                        {
                            // If no reply exists, insert a new reply
                            query = @"
                        INSERT INTO Review (RatingScore, ReviewContent, RatingReviewDateTime, PaymentID, ReplyTo, UsernameAdmin)
                        VALUES (@RatingScore, @ReviewContent, @RatingReviewDateTime, @PaymentID, @ReplyTo, @UsernameAdmin)";
                        }

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Add parameters for the query
                            if (count > 0) // Update scenario
                            {
                                cmd.Parameters.AddWithValue("@ReviewContent", txtComment.Text);
                                cmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewId); // Add this line
                                cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                                cmd.Parameters.AddWithValue("@UsernameAdmin", "admin");
                            }
                            else // Insert scenario
                            {
                                cmd.Parameters.AddWithValue("@RatingScore", 5); // Assuming a fixed rating score of 5
                                cmd.Parameters.AddWithValue("@ReviewContent", txtComment.Text);
                                cmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                                cmd.Parameters.AddWithValue("@ReplyTo", ratingReviewId);
                                cmd.Parameters.AddWithValue("@UsernameAdmin", "admin");
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                }

                Response.Redirect("Review.aspx");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string ratingReviewId = Request.QueryString["RatingReviewID"];
            string paymentId = Request.QueryString["PaymentID"];

            if (!string.IsNullOrEmpty(ratingReviewId) && !string.IsNullOrEmpty(paymentId))
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM Review WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID AND UsernameAdmin = 'admin'";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewId);
                        cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Review.aspx");
            }
        }
    }
}