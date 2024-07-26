using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Home
{
    public partial class Home : System.Web.UI.Page
    {
        private static int slideIndex = 1;

        // Array to store slide images
        private static readonly string[] slideImages = new string[]
        {
            "~/img/slide1.jpeg",
            "~/img/slide2.jpeg",
            "~/img/slide3.jpeg"
        };

        // Array to store slide texts
        private static readonly string[] slideTexts = new string[]
        {
            "<h1>WELCOME AND ENJOY OUR SPECIAL COFFEE</h1>" +
            "<p>Special cocktails based on Reserve coffee are waiting for you to explore!</p>",
            "<h1>FRESHLY BREWED ESPRESSO DRINKS READY TO SERVE</h1>" +
            "<p>Experience the rich, aromatic delight of our freshly brewed espresso drinks. Whether you prefer a classic espresso, a creamy cappuccino, or a velvety latte, each drink is made to order with the utmost care!</p>",
            "<h1>FIND YOUR FAVOURITE COFFEE</h1>" +
            "<p>Dive into our menu and find your perfect match!</p>"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateSlide();
            }
        }

        private void UpdateSlide()
        {
            // Set ImageUrl to the current slide
            slideImg.ImageUrl = slideImages[slideIndex - 1];
            // Set the text to the current slide
            SlideText.Text = slideTexts[slideIndex - 1];
        }

        // Event handler for next button
        protected void NextButton_Click(object sender, EventArgs e)
        {
            slideIndex++;
            if (slideIndex > slideImages.Length)
            {
                slideIndex = 1;
            }
            UpdateSlide();
        }

        // Event handler for previous button
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
                    url = "CoffeeMenu.aspx";
                    break;
                case 2:
                    url = "EspressoMenu.aspx";
                    break;
                case 3:
                    url = "DessertMenu.aspx";
                    break;
            }
            Response.Redirect(url);
        }
    }
}
