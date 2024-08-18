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
            <asp:Button ID="SlideBtn" runat="server" Text="Order Now" CssClass="slideBtn" OnClick="SlideBtn_Click" Font-Size="17px" />
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
            <p>Welcome to CoffeeCove, where every cup and every bite are crafted with passion. 
            Nestled in the heart of each city, we offer more than just coffee and desserts; 
            we provide a cozy retreat from the hustle and bustle. Our special coffee 
            made with reserve coffee beans, and our handcrafted desserts ensure every visit 
            leaves a lasting impression. Whether you're here for a quiet moment, catching up 
            with friends, or a casual meeting, our welcoming atmosphere and friendly staff 
            are here to make your experience unforgettable. Join us and create your own 
            delicious memories.</p>
        </div>
    </div>

    <!-- Category Section -->
    <h1 class="categoryTitle">Our Categories</h1>
    <div id="categoryItem">
     <asp:Repeater ID="rptCategory" runat="server" OnItemCommand="rptCategory_ItemCommand">
         <ItemTemplate>
             <div class="categoryContainer">
                 <img class="categoryImage" src='<%# Eval("CategoryImageUrl") %>' alt='<%# Eval("CategoryName") %>' />
                 <div class="categoryContent" style="align-content:center;">
                     <h3><%# Eval("CategoryName") %></h3>
                     <asp:LinkButton ID="lnkOrderNow" runat="server" CommandArgument='<%# Eval("CategoryId") %>' CommandName="OrderNow" CssClass="orderNowButton">Order Now</asp:LinkButton>
                 </div>
             </div>
         </ItemTemplate>
     </asp:Repeater>
 </div>
</asp:Content>
