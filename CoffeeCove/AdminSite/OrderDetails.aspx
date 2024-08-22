<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/OrderDetails.css" rel="stylesheet" />
    <div id="main" class="main">
        <div class="pagetitle">
            <br />
            <h3>Order Details</h3>
        </div>
        
        <div class="order-details-container">
            <!-- Order Information -->
            <div class="order-info">
                <h4>Order Information</h4>
                <p><strong>Order ID:</strong> <span id="orderId">#123456</span></p>
                <p><strong>Order Date:</strong> <span id="orderDate">12/08/2024</span></p>
                <p><strong>Payment Method:</strong> <span id="paymentMethod">Credit Card</span></p>
            </div>

            <!-- Customer Information -->
            <div class="customer-info">
                <h4>Customer Information</h4>
                <p><strong>Customer Name:</strong> <span id="customerName">John Doe</span></p>
                <p><strong>Email:</strong> <span id="customerEmail">johndoe@example.com</span></p>
                <p><strong>Shipping Address:</strong> <span id="shippingAddress">123 Coffee Street, Brewtown, BT 12345</span></p>
            </div>

            <!-- Product Information -->
            <div class="product-info">
                <h4>Products Ordered</h4>
                <table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Quantity</th>
                            <th>Unit Price</th>
                            <th>Total Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Coffee Beans - Dark Roast</td>
                            <td>2</td>
                            <td>$15.00</td>
                            <td>$30.00</td>
                        </tr>
                        <tr>
                            <td>Espresso Machine</td>
                            <td>1</td>
                            <td>$150.00</td>
                            <td>$150.00</td>
                        </tr>
                        <!-- Additional products can be listed here -->
                    </tbody>
                </table>
            </div>

            <!-- Order Summary -->
            <div class="order-summary">
                <h4>Order Summary</h4>
                <p><strong>Subtotal:</strong> <span id="subtotal">$180.00</span></p>
                <p><strong>Shipping:</strong> <span id="shipping">$10.00</span></p>
                <p><strong>Total:</strong> <span id="total">$190.00</span></p>
            </div>
        </div>
    </div>
</asp:Content>
