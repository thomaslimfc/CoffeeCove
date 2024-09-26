<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="orderHistory.aspx.cs" Inherits="CoffeeCove.Order.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
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
                            <p>Order Status: <strong><%# Eval("OrderStatus") %></strong></p>
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

                        <div class="reviewSection" runat="server" id="ReviewSection" visible="false">
                            <div class="rating-stars mt-2">
                                <p><strong>Rate for order: </strong><asp:PlaceHolder ID="phStars" runat="server"></asp:PlaceHolder></p>
                                
                            </div>
                            <p><strong>Comment:</strong> <asp:Literal ID="ReviewContent" runat="server"></asp:Literal></p>
                        </div>

                        <!-- Track Order Button -->
                        <div class="trackOrderButton">
                            <asp:Button ID="CancelOrderButton" runat="server" Text="Cancel Order" CssClass="btnCont2" Visible="false" OnClick="CancelOrderButton_Click" 
                                CommandArgument='<%# Eval("OrderID") %>' OnClientClick="return confirm('Are you sure you want to cancel this order?');" />
                            <asp:Button ID="RatingButton" runat="server" Text="Rate Order" CssClass="btnCont" Visible="false" CommandArgument='<%# Eval("PaymentID") %>' OnClick="RatingButton_Click" />
                            <asp:Button ID="TrackOrderButton" runat="server" Text="Track Order" CommandArgument='<%# Eval("OrderID") %>' OnClick="TrackOrderButton_Click" CssClass="btnCont" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
