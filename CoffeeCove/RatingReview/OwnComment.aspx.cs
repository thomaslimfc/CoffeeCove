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
                BindOwnComment();
            }
        }

        private void BindOwnComment()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                // Added a WHERE clause to filter by CusID = 2
                string query = @"
                    SELECT R.RatingScore, R.ReviewContent, R.RatingReviewDateTime, 
                           C.CusID, C.Username, 
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
                    ORDER BY R.RatingReviewDateTime DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Added parameter to avoid SQL injection
                    cmd.Parameters.AddWithValue("@CusID", 3);

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
    }
}