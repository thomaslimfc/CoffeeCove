<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="paymentOpt.aspx.cs" Inherits="CoffeeCove.Payment.paymentOpt" %>
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
                                    <a data-toggle="pill" href="#credit-card" class="nav-link active" style="color: white;background-color: #413432;"
>
                                        <i class="fas fa-credit-card mr-2"></i> Credit Card
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a data-toggle="pill" href="#COD" class="nav-link" style="color: white;background-color: #413432;"
>
                                        <i class="fas fa-mobile-alt mr-2"></i> Cash on Delivery
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a data-toggle="pill" href="#paypal" class="nav-link" style="color: white;background-color: #413432;"
>
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
                            <form role="form" onsubmit="event.preventDefault()">
                                <div class="form-group">
                                    <label for="username">
                                        <h6>Card Owner</h6>
                                    </label>
                                    <input type="text" name="username" placeholder="Card Owner Name" required class="form-control">
                                </div>

                                <div class="form-group">
                                    <label for="cardNumber">
                                        <h6>Card number</h6>
                                    </label>
                                    <div class="input-group">
                                        <input type="text" name="cardNumber" placeholder="Valid card number" class="form-control" required>
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
                                                <input type="number" placeholder="MM" name="" class="form-control" required>
                                                <input type="number" placeholder="YY" name="" class="form-control" required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group mb-4">
                                            <label data-toggle="tooltip" title="Three digit CV code on the back of your card">
                                                <h6>CVV <i class="fa fa-question-circle d-inline"></i></h6>
                                            </label>
                                            <input type="text" required class="form-control">
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <asp:Button ID="btnCreditCardPayment" runat="server" Text=" Confirm Payment " 
                                        CssClass="subscribe btn btn-dark btn-block shadow-sm" 
                                        OnClientClick="showOrderConfirmationModal();" OnClick="btnCreditCardPayment_Click"/>
                                </div>
                            </form>
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
                                <input type="text" name="email" placeholder="Email or Mobile Number" required class="form-control">
                            </div>
                            <div class="form-group">
                                <label for="Enter password">
                                    <h6>Password</h6>
                                </label>
                                <input type="password" name="password" placeholder="Password" required class="form-control">
                            </div>
                            <div class="form-group">
                                <p>
                                    <asp:Button ID="btnPaypal" runat="server" Text=" Proceed Payment " CssClass="btn btn-dark" 
                                        OnClientClick="showOrderConfirmationModal();" OnClick="btnPaypal_Click" />
                                </p>
                            </div>
                            <p class="text-muted">Note: After clicking on the button, you will be directed to a secure gateway for payment. After completing the payment process, you will be redirected back to the website to view details of your order.</p>
                        </div> <!-- End Net Banking info -->

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

    <br />
    <br />

    <script>
        function showOrderConfirmationModal() {
            $('#orderConfirmationModal').modal('show');
        }

        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>

    <!-- Modal -->
    <div class="modal fade" id="orderConfirmationModal" tabindex="-1" aria-labelledby="orderConfirmationLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body text-start p-4">
                    <h5 class="modal-title text-uppercase mb-5" id="orderConfirmationLabel">John Doe</h5>
                    <h4 class="mb-5">Thanks for your order</h4>
                    <p class="mb-0">Payment summary</p>
                    <hr class="mt-2 mb-4" style="height: 0; background-color: transparent; opacity: .75; border-top: 2px dashed #9e9e9e;">

                    <div class="d-flex justify-content-between">
                        <p class="fw-bold mb-0">Eggs In Purgatory</p>
                        <p class="text-muted mb-0">RM12.50</p>
                    </div>

                    <div class="d-flex justify-content-between">
                        <p class="small mb-0">Shipping</p>
                        <p class="small mb-0">RM0</p>
                    </div>

                    <div class="d-flex justify-content-between">
                        <p class="fw-bold">Total</p>
                        <p class="fw-bold">RM12.50</p>
                    </div>

                </div>
                <div class="modal-footer d-flex justify-content-center border-top-0 py-4">
                    <asp:Button ID="btnOrderHistory" runat="server" Text="Track your order" CssClass="btn btn-dark btn-lg mb-1" OnClick="btnOrderHistory_Click"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
