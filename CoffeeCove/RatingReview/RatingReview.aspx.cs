﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.RatingReview
{
    public partial class ratingReview : System.Web.UI.Page
    {
        string cs = Global.CS;

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
                    SELECT R1.RatingScore, R1.ReviewContent, R1.RatingReviewDateTime, R1.CusID, C.Username, 
                           R2.ReviewContent AS AdminReplyContent, R2.RatingReviewDateTime AS AdminReplyDateTime, A.Username AS AdminUsername
                    FROM Review R1
                    LEFT JOIN Review R2 ON R1.RatingReviewID = R2.ReplyTo
                    LEFT JOIN Customer C ON R1.CusID = C.CusID
                    LEFT JOIN Admin A ON R2.UsernameAdmin = A.Username
                    WHERE R1.ReplyTo IS NULL
                    ORDER BY R1.RatingReviewDateTime DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
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

        protected void commentButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("comment.aspx");
        }

        protected void btnCurrentUserRating_Click(object sender, EventArgs e)
        {
            Response.Redirect("OwnComment.aspx");
        }
    }
}