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
                // Ensure the user is logged in and the session contains the admin username
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

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
            WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewId);
                    cmd.Parameters.AddWithValue("@PaymentID", paymentId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtComment.Text = result.ToString();
                    }
                    else
                    {
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
            string adminUsername = Session["Username"].ToString(); // Retrieve the admin username from the session

            if (!string.IsNullOrEmpty(ratingReviewId) && !string.IsNullOrEmpty(paymentId))
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    string checkQuery = @"
                SELECT COUNT(*)
                FROM Review
                WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewId);
                        checkCmd.Parameters.AddWithValue("@PaymentID", paymentId);

                        int count = (int)checkCmd.ExecuteScalar();

                        string query;
                        if (count > 0)
                        {
                            query = @"
                                UPDATE Review
                                SET ReviewContent = @ReviewContent, RatingReviewDateTime = @RatingReviewDateTime, UsernameAdmin = @UsernameAdmin
                                WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID";
                        }
                        else
                        {
                            query = @"
                                INSERT INTO Review (RatingScore, ReviewContent, RatingReviewDateTime, PaymentID, ReplyTo, UsernameAdmin)
                                VALUES (@RatingScore, @ReviewContent, @RatingReviewDateTime, @PaymentID, @ReplyTo, @UsernameAdmin)";
                        }

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            if (count > 0)
                            {
                                cmd.Parameters.AddWithValue("@ReviewContent", txtComment.Text);
                                cmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewId);
                                cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                                cmd.Parameters.AddWithValue("@UsernameAdmin", adminUsername);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RatingScore", 5);
                                cmd.Parameters.AddWithValue("@ReviewContent", txtComment.Text);
                                cmd.Parameters.AddWithValue("@RatingReviewDateTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                                cmd.Parameters.AddWithValue("@ReplyTo", ratingReviewId);
                                cmd.Parameters.AddWithValue("@UsernameAdmin", adminUsername);
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
                    string deleteQuery = "DELETE FROM Review WHERE ReplyTo = @RatingReviewID AND PaymentID = @PaymentID";
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