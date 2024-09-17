<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="orderHistory.aspx.cs" Inherits="CoffeeCove.Order.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/OrderHistory.css" rel="stylesheet" />
    <div class="Content">
        <div class="pagetitle">
            <br />
            <h1 class="serviceTitle">Order History</h1>
        </div>

        <div class="repeaterClass">
            <asp:Repeater ID="OrderHistoryList" runat="server" OnItemDataBound="OrderHistoryList_ItemDataBound">
                <ItemTemplate>
                    <!-- Order Information -->
                    <div class="orderItem">
                        <div class="orderHeader">
                            <h4>Order ID: <%# Eval("OrderID") %></h4>
                            <p>Order Date: <%# Eval("OrderDateTime", "{0:dd-MM-yyyy}") %></p>
                        </div>

                        <!-- Product Information -->
                        <div class="productDetails">
                            <asp:Repeater ID="ProductListRepeater" runat="server">
                                <HeaderTemplate>
                                    <div class="productListContainer">
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <div class="productInfo">
                                        <div class="productImageWrapper">
                                            <img src='<%# Eval("ImageUrl") %>' alt="Product Image" class="productImage" />
                                        </div>
                                        <div class="productDetailsWrapper">
                                            <p><strong>Product Name:</strong> <%# Eval("ProductName") %></p>
                                            <p><strong>Product Price:</strong> RM <%# Eval("Price") %></p>
                                            <p><strong>Quantity:</strong> <%# Eval("Quantity") %></p>
                                        </div>
                                    </div>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>

                        <!-- Track Order Button -->
                        <div class="trackOrderButton">
                            <asp:Button ID="TrackOrderButton" runat="server" Text="Track Order" CommandArgument='<%# Eval("OrderID") %>' OnClick="TrackOrderButton_Click" CssClass="btnCont" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
