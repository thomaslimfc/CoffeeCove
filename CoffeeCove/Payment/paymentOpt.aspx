<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="paymentOpt.aspx.cs" Inherits="CoffeeCove.Payment.paymentOpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" />
    <link href="../CSS/paymentBootstrap.css" rel="stylesheet" />
    <link href="../CSS/paymentOpt.css" rel="stylesheet" />

    <!-- Banner -->
    <div id="poster">
        <img src="../img/coffeeBag.jpg" id="posterImg" />
        <div id="posterText">Payment</div>
    </div>
    <br />

    <!-- Element -->
    <div class="container py-5">
        <!-- For demo purpose -->
        <div class="row mb-4">
            <div class="col-lg-8 mx-auto text-center">
                <h1 class="display-6">Choose Payment Method</h1>
            </div>
        </div> <!-- End -->

        <div class="row">
            <!-- Order Summary Card -->
            <div class="col-lg-4">
                <div class="card mb-4">
                    <div class="card-header">
                        <h5>Order Summary</h5>
                    </div>
                    <div class="card-body">
                        <!-- User Information -->
                        <h6>User Information</h6>
                        <p class="mb-1"><strong>Name:</strong> John Doe</p> <!-- Replace with dynamic user name -->
                        <p class="mb-1"><strong>Phone:</strong> +1234567890</p> <!-- Replace with dynamic phone number -->
                        <p class="mb-1"><strong>Address:</strong> 123 Coffee Street, Caffeine City, 56789</p> <!-- Replace with dynamic address -->
                        <hr>

                        <!-- Order Items -->
                        <h6>Order Items</h6>
                        <p class="mb-1">Item 1: $10.00</p> <!-- Replace with dynamic item details -->
                        <p class="mb-1">Item 2: $15.00</p>
                        <p class="mb-1">Item 3: $20.00</p>
                        <hr>
                        <p class="mb-1"><strong>Total: $45.00</strong></p> <!-- Replace with dynamic total -->
                    </div>
                </div>
            </div>

            <!-- Payment Method Card -->
            <div class="col-lg-8">
                <div class="card">
                    <div class="card-header">
                        <div class="bg-white shadow-sm pt-4 pl-2 pr-2 pb-2">
                            <!-- Credit card form tabs -->
                            <ul role="tablist" class="nav bg-light nav-pills rounded nav-fill mb-3">
                                <li class="nav-item">
                                    <a data-toggle="pill" href="#credit-card" class="nav-link active">
                                        <i class="fas fa-credit-card mr-2"></i> Credit Card
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a data-toggle="pill" href="#COD" class="nav-link">
                                        <i class="fas fa-mobile-alt mr-2"></i> Cash on Delivery
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a data-toggle="pill" href="#paypal" class="nav-link">
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
                                    <button type="button" class="subscribe btn btn-primary btn-block shadow-sm" onclick="showOrderConfirmationModal()"> Confirm Payment </button>
                                </div>
                            </form>
                        </div> <!-- End credit card info -->

                        <!-- Cash on Delivery info -->
                        <div id="COD" class="tab-pane fade pt-3">
                            <p>
                                <button id="CODBtn" type="button" class="btn btn-primary">
                                    Confirm Payment
                                </button>
                            </p>
                            <p class="text-muted">Note: After clicking on the button, your order will be placed. You have to pay the transaction by cash when your order is delivered to your address.</p>
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
                                    <button type="button" class="btn btn-primary">
                                        <i class="fas fa-mobile-alt mr-2"></i> Proceed Payment
                                    </button>
                                </p>
                            </div>
                            <p class="text-muted">Note: After clicking on the button, you will be directed to a secure gateway for payment. After completing the payment process, you will be redirected back to the website to view details of your order.</p>
                        </div> <!-- End Net Banking info -->

                    </div> <!-- End tab content -->
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
                        <p class="fw-bold mb-0">Ether Chair (Qty: 1)</p>
                        <p class="text-muted mb-0">$1750.00</p>
                    </div>

                    <div class="d-flex justify-content-between">
                        <p class="small mb-0">Shipping</p>
                        <p class="small mb-0">$175.00</p>
                    </div>

                    <div class="d-flex justify-content-between pb-1">
                        <p class="small">Tax</p>
                        <p class="small">$200.00</p>
                    </div>

                    <div class="d-flex justify-content-between">
                        <p class="fw-bold">Total</p>
                        <p class="fw-bold">$2125.00</p>
                    </div>

                </div>
                <div class="modal-footer d-flex justify-content-center border-top-0 py-4">
                    <button type="button" data-mdb-button-init data-mdb-ripple-init class="btn btn-primary btn-lg mb-1" style="background-color: #35558a;">
                        Track your order
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
