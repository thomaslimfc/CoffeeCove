<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="OrderTracking.aspx.cs" Inherits="CoffeeCove.Order.OrderTracking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/trackingBootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../CSS/OrderTracking.css" rel="stylesheet" />

    <div class="Content">
        <div class="container px-1 px-md-4 py-5 mx-auto">
            <div class="card">
                <div class="row d-flex justify-content-between px-3 top">
                    <div class="d-flex">
                        <h5>ORDER #<span class="text-primary font-weight-bold"><asp:Literal ID="OrderIdLiteral" runat="server"></asp:Literal></span></h5>
                    </div>
                    <div class="d-flex flex-column text-sm-right">
                        <p class="mb-0">Expected Arrival <span>01/12/19</span></p>
                        <p>USPS <span class="font-weight-bold">234094567242423422898</span></p>
                    </div>
                </div>

                <!-- Add class 'active' to progress -->
                <div class="row d-flex justify-content-center">
                    <div class="col-12">
                        <ul id="progressbar" class="text-center">
                            <li id="progressbarStep1" runat="server" class="step0"></li>
                            <li id="progressbarStep2" runat="server" class="step0"></li>
                            <li id="progressbarStep3" runat="server" class="step0"></li>
                            <li id="progressbarStep4" runat="server" class="step0"></li>
                        </ul>
                    </div>
                </div>
                <div class="row justify-content-between top">
                    <div class="row d-flex icon-content">
                        <img class="icon" src="https://i.imgur.com/9nnc9Et.png">
                        <div class="d-flex flex-column">
                            <p class="font-weight-bold">Order<br>Received</p>
                        </div>
                    </div>
                    <div class="row d-flex icon-content">
                        <img class="icon" src="https://i.imgur.com/u1AzR7w.png">
                        <div class="d-flex flex-column">
                            <p class="font-weight-bold">Preparing<br>Your Meal</p>
                        </div>
                    </div>
                    <div class="row d-flex icon-content">
                        <img class="icon" src="https://i.imgur.com/TkPm63y.png">
                        <div class="d-flex flex-column">
                            <p class="font-weight-bold">Your Order is<br>Out for Delivery</p>
                        </div>
                    </div>
                    <div class="row d-flex icon-content">
                        <img class="icon" src="https://i.imgur.com/HdsziHP.png">
                        <div class="d-flex flex-column">
                            <p class="font-weight-bold">Order<br>Delivered</p>
                        </div>
                    </div>
                </div>
                <!-- Add your button below -->
                <div class="row d-flex justify-content-center mt-4">
                    <div class="btn-container">
                        <asp:Button ID="BackButton" runat="server" Text="Back to Order History" CssClass="btnCont custom-back-button" OnClick="BackButton_Click" />
                        <asp:Button ID="InvoiceButton" runat="server" Text="View Invoice" CssClass="btnCont custom-invoice-button" OnClick="InvoiceButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
</asp:Content>
