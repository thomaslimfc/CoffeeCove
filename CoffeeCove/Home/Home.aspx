<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CoffeeCove.Home.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/Home.css" rel="stylesheet" />
    <!-- Slideshow -->
    <div class="sliderContainer">
        <asp:Image ID="slideImg" runat="server" />
        <asp:Button ID="PrevButton" runat="server" Text="&#10094;" OnClick="PrevButton_Click" CssClass="navButton prev" />
        <asp:Button ID="NextButton" runat="server" Text="&#10095;" OnClick="NextButton_Click" CssClass="navButton next" />
        <div id="slideText">
            <asp:Literal ID="SlideText" runat="server"></asp:Literal>
            <asp:Button ID="SlideBtn" runat="server" Text="View More" CssClass="slideBtn" OnClick="SlideBtn_Click" Font-Size="17px" />
        </div>
    </div>

    <!-- About Us -->
    <div id="about">
        <div class="leftContent">
            <img src="/img/about.jpg" />
        </div>
        <div class="rightContent">
            <h2>About Us</h2>
            <h1>We Leave A Delicious Memory For You</h1>
            <p>
                Welcome to CoffeeCove, where every cup and every bite are crafted with passion. 
            Nestled in the heart of each city, we offer more than just coffee and desserts; 
            we provide a cozy retreat from the hustle and bustle. Our special coffee 
            made with reserve coffee beans, and our handcrafted desserts ensure every visit 
            leaves a lasting impression. Whether you're here for a quiet moment, catching up 
            with friends, or a casual meeting, our welcoming atmosphere and friendly staff 
            are here to make your experience unforgettable. Join us and create your own 
            delicious memories.
            </p>
        </div>
    </div>

    <!-- Category Section -->
    <div style="background-image: url('../img/bg_1.jpg');">
    <h1 class="categoryTitle">Our Categories</h1>
    <div id="categoryItem">
        <asp:Repeater ID="rptCategory" runat="server" OnItemCommand="rptCategory_ItemCommand">
            <ItemTemplate>
                <div class="categoryContainer">
                    <img class="categoryImage" src='<%# Eval("CategoryImageUrl") %>' alt='<%# Eval("CategoryName") %>' />
                    <div class="categoryContent" style="align-content: center;">
                        <h3><%# Eval("CategoryName") %></h3>
                        <asp:LinkButton ID="lnkOrderNow" runat="server" CommandArgument='<%# Eval("CategoryId") %>' CommandName="OrderNow" CssClass="orderNowButton">Order Now</asp:LinkButton>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Services -->
    <div class="ourService">
    <h1 class="serviceTitle">Our Services</h1>
        <table style="width: 80%; margin-left: 10%;" class="serviceContainer">
            <tr>
                <td class="box" style="width: 45px">
                    <img src="../img/chef.gif" style="width: 150px" /><br />
                    <h2>Master Chefs</h2>
                </td>
                <td width="20px"></td>
                <td class="box" style="width: 45px">
                    <img src="../img/coffee.gif" style="width: 150px" /><br />
                    <h2>Quality Meals</h2>
                </td>
                <td width="20px"></td>
                <td class="box" style="width: 45px">
                    <img src="../img/delivery-truck.gif" style="width: 150px" /><br />
                    <h2>Food Delivery</h2>
                </td>
                <td width="20px"></td>
                <td class="box" style="width: 45px">
                    <img src="../img/food-pickup.gif" style="width: 150px" /><br />
                    <h2>Pick Up</h2>
                </td>
            </tr>
        </table>
        <div class="buttonContainer">
            <asp:Button ID="btnStart" runat="server" Text="Start Order Now" PostBackUrl="../Order/OrderOption.aspx" CssClass="btnStartOrder" />
        </div>
    </div>

    <!-- Feedback -->
    <div class="ourFeedback">
    <h1 class="feedbackTitle">Our Clients Say</h1>
        <table style="width: 90%;" class="feedbackContainer">
            <tr>
                <td class="box1" style="width: 60px;">
                    <div class="feedbackItem">
                        <img src="../img/user1.jpg" class="userImg" />
                        <div class="feedbackContent">
                            <h3>xylim2002</h3>
                            <small>8/23/2024 6:51:30 PM</small>
                            <h4>This website is perfect! I like it!</h4>
                        </div>
                    </div>
                </td>
                <td width="20px"></td>
                <td class="box1" style="width: 60px;">
                    <div class="feedbackItem">
                        <img src="../img/user2.jpg" class="userImg" />
                        <div class="feedbackContent">
                            <h3>thomas1232</h3>
                            <small>8/24/2024 10:31:30 AM</small>
                            <h4>The food and drinks are great!</h4>
                        </div>
                    </div>
                </td>
                <td width="20px"></td>
                <td class="box1" style="width: 60px;">
                    <div class="feedbackItem">
                        <img src="../img/user3.jpg" class="userImg" />
                        <div class="feedbackContent">
                            <h3>qilunblublu</h3>
                            <small>8/26/2024 8:41:30 PM</small>
                            <h4>I really enjoyed the pastries and coffee!</h4>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div class="buttonContainer">
            <asp:Button ID="btnFeedback" runat="server" Text="View More" PostBackUrl="../RatingReview/ratingReview.aspx" CssClass="btnView" />
        </div>
    </div>
        </div>
</asp:Content>
