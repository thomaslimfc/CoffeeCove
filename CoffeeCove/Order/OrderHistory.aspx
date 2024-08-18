<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="orderHistory.aspx.cs" Inherits="CoffeeCove.Order.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/OrderHistory.css" rel="stylesheet" />
    <div class="pagetitle">
        <br />
        <h2 id="title">Order History List</h2>
    </div>

    <div class="repeaterClass">
        <asp:Repeater ID="OrderHistoryList" runat="server">
            <ItemTemplate>
                <!-- Order Information -->
                <div class="orderItem">
                    <div class="orderHeader">
                        <h4>Order ID: <%# Eval("OrderID") %></h4>
                        <p>Order Date: <%# Eval("OrderDate", "{0:dd-MM-yyyy}") %></p>
                    </div>

                    <!-- Product Information -->
                    <div class="productDetails">
                        <img src="../img/Category/coffee.jpg" alt="Product Image" class="productImage" />
                        <div class="productInfo">
                            <p>Product Name: </p>
                            <p>Product Price: RM</p>
                            <p>Quantity: </p>
                        </div>
                    </div>
                </div>
                <hr />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>