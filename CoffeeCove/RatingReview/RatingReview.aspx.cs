using System;
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
                    SELECT R.RatingScore, R.ReviewContent, R.RatingReviewDateTime, R.CusID, C.Username 
                    FROM Review R
                    JOIN Customer C ON R.CusID = C.CusID";
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
            }
        }

        protected void commentButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("comment.aspx");
        }
    }
}