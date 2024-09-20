using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRatingReviews();
            }
        }

        protected void BindRatingReviews()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                // SQL query to count ratings for each star (1-5 stars)
                string query = @"
            SELECT 
                COUNT(CASE WHEN RatingScore = 5 THEN 1 END) AS FiveStarCount,
                COUNT(CASE WHEN RatingScore = 4 THEN 1 END) AS FourStarCount,
                COUNT(CASE WHEN RatingScore = 3 THEN 1 END) AS ThreeStarCount,
                COUNT(CASE WHEN RatingScore = 2 THEN 1 END) AS TwoStarCount,
                COUNT(CASE WHEN RatingScore = 1 THEN 1 END) AS OneStarCount,
                COUNT(*) AS TotalCount
            FROM Review";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int fiveStarCount = Convert.ToInt32(reader["FiveStarCount"]);
                        int fourStarCount = Convert.ToInt32(reader["FourStarCount"]);
                        int threeStarCount = Convert.ToInt32(reader["ThreeStarCount"]);
                        int twoStarCount = Convert.ToInt32(reader["TwoStarCount"]);
                        int oneStarCount = Convert.ToInt32(reader["OneStarCount"]);
                        int totalCount = Convert.ToInt32(reader["TotalCount"]);

                        if (totalCount > 0)
                        {
                            // Calculate percentages
                            decimal fiveStarPercentage = (decimal)fiveStarCount / totalCount * 100;
                            decimal fourStarPercentage = (decimal)fourStarCount / totalCount * 100;
                            decimal threeStarPercentage = (decimal)threeStarCount / totalCount * 100;
                            decimal twoStarPercentage = (decimal)twoStarCount / totalCount * 100;
                            decimal oneStarPercentage = (decimal)oneStarCount / totalCount * 100;

                            // Set the widths of the progress bars
                            progressBar5.Style.Add("width", $"{fiveStarPercentage}%");
                            progressBar4.Style.Add("width", $"{fourStarPercentage}%");
                            progressBar3.Style.Add("width", $"{threeStarPercentage}%");
                            progressBar2.Style.Add("width", $"{twoStarPercentage}%");
                            progressBar1.Style.Add("width", $"{oneStarPercentage}%");

                            // Set the labels for percentages
                            lblFiveStarPercentage.Text = $"{Math.Round(fiveStarPercentage)}%";
                            lblFourStarPercentage.Text = $"{Math.Round(fourStarPercentage)}%";
                            lblThreeStarPercentage.Text = $"{Math.Round(threeStarPercentage)}%";
                            lblTwoStarPercentage.Text = $"{Math.Round(twoStarPercentage)}%";
                            lblOneStarPercentage.Text = $"{Math.Round(oneStarPercentage)}%";
                        }
                    }
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