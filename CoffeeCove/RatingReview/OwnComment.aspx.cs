using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.RatingReview
{
    public partial class OwnComment : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if CusID is available in the session
                if (Session["CusID"] != null)
                {
                    BindOwnComment();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void BindOwnComment()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = @"
        SELECT R.RatingReviewID, R.RatingScore, R.ReviewContent, R.RatingReviewDateTime, 
               C.CusID, C.Username, C.ProfilePicturePath, 
               O.OrderID, 
               R2.ReviewContent AS AdminReplyContent, 
               R2.RatingReviewDateTime AS AdminReplyDateTime, 
               A.Username AS AdminUsername
        FROM Review R
        LEFT JOIN Review R2 ON R.RatingReviewID = R2.ReplyTo
        LEFT JOIN PaymentDetail PD ON R.PaymentID = PD.PaymentID
        LEFT JOIN OrderPlaced O ON PD.OrderID = O.OrderID
        LEFT JOIN Customer C ON O.CusID = C.CusID
        LEFT JOIN Admin A ON R2.UsernameAdmin = A.Username
        WHERE R.ReplyTo IS NULL
        AND C.CusID = @CusID
        ORDER BY R.RatingReviewDateTime DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Retrieve CusID from session
                    int cusID = Convert.ToInt32(Session["CusID"]);
                    cmd.Parameters.AddWithValue("@CusID", cusID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptUserRatingReview.DataSource = reader;
                    rptUserRatingReview.DataBind();
                }
            }
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button btn = (Button)sender;
            // Get the RatingReviewID from the CommandArgument
            int ratingReviewID = Convert.ToInt32(btn.CommandArgument);
            // Redirect to editComment.aspx with RatingReviewID as a query string parameter
            Response.Redirect($"~/RatingReview/editComment.aspx?RatingReviewID={ratingReviewID}");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button btn = (Button)sender;
            int ratingReviewID = Convert.ToInt32(btn.CommandArgument);

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Review WHERE RatingReviewID = @RatingReviewID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@RatingReviewID", ratingReviewID);
                    cmd.ExecuteNonQuery();
                }
            }

            BindOwnComment();
        }
    }
}