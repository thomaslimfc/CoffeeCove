﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="paymentOpt.aspx.cs" Inherits="CoffeeCove.Payment.paymentOpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" />
    <link href="../CSS/paymentBootstrap.css" rel="stylesheet" />
    <link href="../CSS/paymentOpt.css" rel="stylesheet" />

    <div id="container">
        <h1 class="serviceTitle">Payment</h1>
        <!-- Element -->
        <div class="container py-5">

            <div class="row">
                <!-- Payment Method Card -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="bg-white shadow-sm pt-4 pl-2 pr-2 pb-2">
                                <!-- Credit card form tabs -->
                                <ul role="tablist" class="nav bg-light nav-pills rounded nav-fill mb-3">
                                    <li class="nav-item">
                                        <a data-toggle="pill" href="#credit-card" class="nav-link active" style="color: white;background-color: #413432;">
                                            <i class="fas fa-credit-card mr-2"></i> Credit Card
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a data-toggle="pill" href="#COD" class="nav-link" style="color: white;background-color: #413432;">
                                            <i class="fas fa-mobile-alt mr-2"></i> Cash on Delivery
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a data-toggle="pill" href="#paypal" class="nav-link" style="color: white;background-color: #413432;">
                                            <i class="fab fa-paypal mr-2"></i> Paypal
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div> <!-- End -->

                        <!-- Credit card form content -->
                        <div class="tab-content">
                            <!-- Credit card info -->
                            <div id="credit-card" class="tab-pane fade show active pt-3">
                                <div class="form-group">
                                    <label for="username">
                                        <h6>Card Owner</h6>
                                    </label>
                                    <asp:TextBox ID="txtCardOwner" runat="server" placeholder="Card Owner Name" CssClass="form-control" />
                                </div>

                                <div class="form-group">
                                    <label for="cardNumber">
                                        <h6>Card number</h6>
                                    </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtCardNumber" runat="server" placeholder="Valid card number" CssClass="form-control" />
                                        <div class="input-group-append">
                                            <span class="input-group-text text-muted">
                                                <i class="fab fa-cc-visa mx-1"></i>
                                                <i class="fab fa-cc-mastercard mx-1"></i>
                                                <i class="fab fa-cc-amex mx-1"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <label>
                                                <h6>Expiration Date</h6>
                                            </label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtExpirationMonth" runat="server" placeholder="MM" CssClass="form-control" />
                                                <asp:TextBox ID="txtExpirationYear" runat="server" placeholder="YY" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group mb-4">
                                            <label data-toggle="tooltip" title="Three digit CV code on the back of your card">
                                                <h6>CVV <i class="fa fa-question-circle d-inline"></i></h6>
                                            </label>
                                            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <asp:Button ID="btnCreditCardPayment" runat="server" Text=" Confirm Payment " 
                                        CssClass="subscribe btn btn-dark btn-block shadow-sm" 
                                        OnClientClick="showOrderConfirmationModal();" OnClick="btnCreditCardPayment_Click"/>
                                </div>
                            </div> <!-- End credit card info -->

                            <!-- Cash on Delivery info -->
                            <div id="COD" class="tab-pane fade pt-3">
                                <p>
                                    <asp:Button ID="btnCOD" runat="server" Text=" Confirm Payment " CssClass="btn btn-dark" 
                                        OnClientClick="showOrderConfirmationModal();" OnClick="btnCOD_Click"/>
                                </p>
                                <p>Note: After clicking on the button, your order will be placed. You have to pay the transaction by cash when your order is delivered to your address.</p>
                            </div> <!-- End Cash on Delivery info -->

                            <!-- Paypal info -->
                            <div id="paypal" class="tab-pane fade pt-3">
                                <div class="form-group">
                                    <label for="Enter email or number">
                                        <h6>Email or Mobile Number</h6>
                                    </label>
                                    <asp:TextBox ID="txtPaypalEmail" runat="server" placeholder="Email or Mobile Number" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="Enter password">
                                        <h6>Password</h6>
                                    </label>
                                    <asp:TextBox ID="txtPaypalPassword" runat="server" placeholder="Password" CssClass="form-control" TextMode="Password" />
                                </div>
                                <div class="form-group">
                                    <p>
                                        <asp:Button ID="btnPaypal" runat="server" Text=" Proceed Payment " CssClass="btn btn-dark" 
                                            OnClientClick="showOrderConfirmationModal();" OnClick="btnPaypal_Click" />
                                    </p>
                                </div>
                                <p class="text-muted">Note: After clicking on the button, you will be directed to a secure gateway for payment. After completing the payment process, you will be redirected back to the website to view details of your order.</p>
                            </div> <!-- End Paypal info -->

                        </div> <!-- End tab content -->
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap and jQuery -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.bundle.min.js"></script>
    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>
