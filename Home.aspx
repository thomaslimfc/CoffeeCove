<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplicationAssignment.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
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
</asp:Content>
