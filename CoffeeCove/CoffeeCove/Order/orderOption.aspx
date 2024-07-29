<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="orderOption.aspx.cs" Inherits="CoffeeCove.orderOption.orderOption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/orderOption.css" rel="stylesheet" />

    <div id="option-title">
    <h1>Delivery or Pick Up</h1>
</div>
<div class="option-container">
    <asp:LinkButton ID="lbDelivery" runat="server" CssClass="option-child" OnClick="lbDelivery_Click" Font-Underline="false">
        <h2>Delivery</h2>
        <br />
        <asp:Image ImageUrl="delivery.png" runat="server" class="img"/>
    </asp:LinkButton>
    <asp:LinkButton ID="lbPickUp" runat="server" CssClass="option-child" OnClick="lbPickUp_Click" Font-Underline="false">
        <h2>Pick Up</h2>
        <br />
        <asp:Image ImageUrl="pickup.png" runat="server" class="img"/>
    </asp:LinkButton>
    
</div>
<div>
    <asp:Literal ID="litTest" runat="server"></asp:Literal>
</div>
</asp:Content>
