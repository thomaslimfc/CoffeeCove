using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class Review : System.Web.UI.Page
    {
        string cs = Global.CS;

        public int TotalRatings { get; set; } = 0;
        public int FiveStarCount { get; set; } = 0;
        public int FourStarCount { get; set; } = 0;
        public int ThreeStarCount { get; set; } = 0;
        public int TwoStarCount { get; set; } = 0;
        public int OneStarCount { get; set; } = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRatingReviews();
            }
        }

        private void BindRatingReviews()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"
                    SELECT RatingScore, COUNT(*) AS RatingCount
                    FROM Review
                    WHERE ReplyTo IS NULL
                    GROUP BY RatingScore";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int ratingScore = Convert.ToInt32(reader["RatingScore"]);
                        int ratingCount = Convert.ToInt32(reader["RatingCount"]);

                        TotalRatings += ratingCount;

                        switch (ratingScore)
                        {
                            case 5:
                                FiveStarCount = ratingCount;
                                break;
                            case 4:
                                FourStarCount = ratingCount;
                                break;
                            case 3:
                                ThreeStarCount = ratingCount;
                                break;
                            case 2:
                                TwoStarCount = ratingCount;
                                break;
                            case 1:
                                OneStarCount = ratingCount;
                                break;
                        }
                    }
                }

                // Set the total ratings count to the Literal control
                litTotalRatings.Text = TotalRatings.ToString();

                rptUserRatingReview.DataSource = GetRatingReviews();
                rptUserRatingReview.DataBind();
            }
        }

        private DataTable GetRatingReviews()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"
            SELECT R.RatingReviewID, R.RatingScore, R.ReviewContent, R.RatingReviewDateTime, 
                   C.CusID, C.Username, C.ProfilePicturePath,
                   R2.ReviewContent AS AdminReplyContent, 
                   R2.RatingReviewDateTime AS AdminReplyDateTime, 
                   A.Username AS AdminUsername,
                   PD.PaymentID, O.OrderID
            FROM Review R
            LEFT JOIN Review R2 ON R.RatingReviewID = R2.ReplyTo
            LEFT JOIN PaymentDetail PD ON R.PaymentID = PD.PaymentID
            LEFT JOIN OrderPlaced O ON PD.OrderID = O.OrderID
            LEFT JOIN Customer C ON O.CusID = C.CusID
            LEFT JOIN Admin A ON R2.UsernameAdmin = A.Username
            WHERE R.ReplyTo IS NULL
            ORDER BY R.RatingReviewDateTime DESC";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public string GetRatingPercentage(int ratingCount)
        {
            if (TotalRatings == 0) return "0%";
            return $"{(ratingCount * 100) / TotalRatings}%";
        }

        protected void rptUserRatingReview_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Retrieve the profile picture path
                string profilePicturePath = DataBinder.Eval(e.Item.DataItem, "ProfilePicturePath")?.ToString();
                Image imgProfile = (Image)e.Item.FindControl("imgProfilePicture");

                // Set default image if no profile picture exists
                if (!string.IsNullOrEmpty(profilePicturePath))
                {
                    imgProfile.ImageUrl = "/UserManagement/UserProfilePictures/" + profilePicturePath;
                }
                else
                {
                    imgProfile.ImageUrl = "http://bootdey.com/img/Content/avatar/avatar1.png";
                }

                // Retrieve the rating value from the data item
                int rating = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "RatingScore"));
                PlaceHolder phStars = (PlaceHolder)e.Item.FindControl("phStars");

                // Generate stars based on the rating
                for (int i = 1; i <= 5; i++)
                {
                    if (i <= rating)
                    {
                        phStars.Controls.Add(new LiteralControl("<span class='fa fa-star checked'></span>"));
                    }
                    else
                    {
                        phStars.Controls.Add(new LiteralControl("<span class='fa fa-star'></span>"));
                    }
                }

                // Check if there's an admin reply
                string adminReplyContent = DataBinder.Eval(e.Item.DataItem, "AdminReplyContent")?.ToString();
                string adminReplyDateTime = DataBinder.Eval(e.Item.DataItem, "AdminReplyDateTime")?.ToString();
                string adminUsername = DataBinder.Eval(e.Item.DataItem, "AdminUsername")?.ToString();

                if (!string.IsNullOrEmpty(adminReplyContent))
                {
                    PlaceHolder phAdminReply = (PlaceHolder)e.Item.FindControl("phAdminReply");
                    phAdminReply.Controls.Add(new LiteralControl($@"
                        <div class='media mt-4'>
                            <a href='#'>
                                <img alt='Admin avatar' src='http://bootdey.com/img/Content/avatar/avatar2.png' class='mr-3 rounded-circle' />
                            </a>
                            <div class='media-body'>
                                <h4 class='mt-0 mb-1 text-muted'><strong>{adminUsername}</strong></h5>
                                <p class='text-muted mb-0'>{adminReplyDateTime}</p>
                                <div class='review-content mt-2'>
                                    <p>{adminReplyContent}</p>
                                </div>
                            </div>
                        </div>
                    "));
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button btn = (Button)sender;
            int ratingReviewID = Convert.ToInt32(btn.CommandArgument);

            // Call the method to delete the review
            DeleteReview(ratingReviewID);

            // Rebind the data to refresh the display
            BindRatingReviews();
        }

        private void DeleteReview(int ratingReviewID)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "DELETE FROM Review WHERE RatingReviewID = @RatingReviewID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button btn = (Button)sender;

            // Retrieve both RatingReviewID and PaymentID from the CommandArgument, assuming it's passed as "RatingReviewID,PaymentID"
            string[] args = btn.CommandArgument.Split(',');
            int ratingReviewID = Convert.ToInt32(args[0]);
            int paymentID = Convert.ToInt32(args[1]);

            // Pass both RatingReviewID and PaymentID in the query string
            Response.Redirect($"AdminReview.aspx?RatingReviewID={ratingReviewID}&PaymentID={paymentID}");
        }
    }
}