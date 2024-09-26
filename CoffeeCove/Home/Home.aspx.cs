using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Home
{
    public partial class Home : System.Web.UI.Page
    {
        private static int slideIndex = 1;
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateSlide();
                BindCategory();
                BindFeedback();
            }
            rptFeedback.ItemDataBound += rptFeedback_ItemDataBound;
        }

        // Array to store slide images
        private static readonly string[] slideImages = new string[]
        {
            "/img/slideImg1.jpeg",
            "/img/slideImg2.jpeg",
            "/img/slideImg4.jpg"
        };

        // Array to store slide texts
        private static readonly string[] slideTexts = new string[]
        {
            @"<h1>WELCOME AND ENJOY OUR SPECIAL COFFEE</h1>
            <p>Reserve coffee are waiting for you to explore!</p>",
            @"<h1>FRESHLY BREWED ESPRESSO DRINKS READY TO SERVE</h1>
            <p>Experience the rich, aromatic delight of our freshly brewed espresso drinks. Whether you prefer a classic espresso, a creamy cappuccino, or a velvety latte, each drink is made to order with the utmost care!</p>",
            @"<h1>PICKUP OR DELIVERY, JUST A FEW TAPS AWAY!</h1>
            <p>Three simple steps:</br>
            1. Enter your address</br>
            2. Select your meals</br>
            3. Sit back and relax</p>"
        };

        private void UpdateSlide()
        {
            // Set ImageUrl to the current slide
            slideImg.ImageUrl = slideImages[slideIndex - 1];
            // Set the text to the current slide
            SlideText.Text = slideTexts[slideIndex - 1];
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            slideIndex++;
            if (slideIndex > slideImages.Length)
            {
                slideIndex = 1;
            }
            UpdateSlide();
        }

        protected void PrevButton_Click(object sender, EventArgs e)
        {
            slideIndex--;
            if (slideIndex < 1)
            {
                slideIndex = slideImages.Length;
            }
            UpdateSlide();
        }

        protected void SlideBtn_Click(object sender, EventArgs e)
        {
            string url = string.Empty;

            switch (slideIndex)
            {
                case 1:
                    url = "/Menu/Menu.aspx";
                    break;
                case 2:
                    url = "/Menu/Menu.aspx?CategoryId=2";
                    break;
                case 3:
                    url = "/Order/OrderOption.aspx";
                    break;
            }
            Response.Redirect(url);
        }

        private void BindCategory()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId, CategoryName, CategoryImageUrl FROM Category WHERE IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    rptCategory.DataSource = dr;
                    rptCategory.DataBind();
                }
            }
        }

        protected void rptCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "OrderNow")
            {
                string categoryId = e.CommandArgument.ToString();
                Response.Redirect($"/Menu/Menu.aspx?CategoryId={categoryId}");
            }
        }

        private void BindFeedback()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT TOP 3 r.ReviewContent, r.RatingReviewDateTime, c.Username, c.ProfilePicturePath 
                               FROM Review r 
                               INNER JOIN PaymentDetail p ON r.PaymentID = p.PaymentID 
                               INNER JOIN OrderPlaced o ON p.OrderID = o.OrderID 
                               INNER JOIN Customer c ON o.CusID = c.CusID 
                               WHERE r.RatingScore = 5 AND r.ReviewContent IS NOT NULL AND r.ReviewContent <> '' 
                               ORDER BY r.RatingReviewDateTime DESC";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    rptFeedback.DataSource = dr;
                    rptFeedback.DataBind();
                }
            }
        }

        protected void rptFeedback_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            }
        }
    }
}
