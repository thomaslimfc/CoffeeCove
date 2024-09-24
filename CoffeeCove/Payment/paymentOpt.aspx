<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="paymentOpt.aspx.cs" Inherits="CoffeeCove.Payment.paymentOpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" />
    <link href="../CSS/paymentBootstrap.css" rel="stylesheet" />
    <link href="../CSS/paymentOpt.css" rel="stylesheet" />

    <div id="container">
        <h1 class="serviceTitle">Payment</h1>
        <!-- Element -->
        <div class="container py-5">
        <asp:Button ID="btnBackToCart" runat="server" Text="&larr; Back to Cart" CssClass="btnCont" OnClick="btnBackToCart_Click" />

            <div class="row">
                <!-- Payment Method Card -->
                <div class="col-lg-12">
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
                                <div class="form-group">
                                    <label for="username">
                                        <h6>Card Owner</h6>
                                    </label>
                                    <asp:TextBox ID="txtCardOwner" runat="server" placeholder="Card Owner Name" CssClass="form-control" ValidationGroup="CreditCardPayment" oninput="restrictCardOwnerInput(this)" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Card owner's name is required." 
                                        ControlToValidate="txtCardOwner" ForeColor="Red" ValidationGroup="CreditCardPayment" InitialValue=""/>
                                    <asp:RegularExpressionValidator ID="RegexValidatorCardOwner" runat="server" ControlToValidate="txtCardOwner" 
                                        ErrorMessage="Please enter a valid name." ValidationExpression="^[a-zA-Z\s]+$" ForeColor="Red" ValidationGroup="CreditCardPayment" />
                                </div>

                                <div class="form-group">
                                    <label for="cardNumber">
                                        <h6>Card number</h6>
                                    </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtCardNumber" runat="server" placeholder="Valid card number" CssClass="form-control" ValidationGroup="CreditCardPayment" 
                                            oninput="formatCardNumber(this)" MaxLength="19"/>
                                        <div class="input-group-append">
                                            <span class="input-group-text text-muted">
                                                <i class="fab fa-cc-visa mx-1"></i>
                                                <i class="fab fa-cc-mastercard mx-1"></i>
                                                <i class="fab fa-cc-amex mx-1"></i>
                                            </span>
                                        </div>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Card Number is required." ControlToValidate="txtCardNumber" ForeColor="Red" ValidationGroup="CreditCardPayment"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegexValidatorCardNumber" runat="server" ControlToValidate="txtCardNumber" 
                                        ErrorMessage="Please enter a valid card number." ValidationExpression="^\d{4} \d{4} \d{4} \d{4}$" ForeColor="Red" ValidationGroup="CreditCardPayment" />
                                </div>

                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <label>
                                                <h6>Expiration Date</h6>
                                            </label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtExpirationMonth" runat="server" placeholder="MM" CssClass="form-control" ValidationGroup="CreditCardPayment" 
                                                    oninput="restrictNumericInput(this, 2)" />
                                                <asp:TextBox ID="txtExpirationYear" runat="server" placeholder="YY" CssClass="form-control" ValidationGroup="CreditCardPayment" 
                                                    oninput="restrictNumericInput(this, 2)" />
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonth" runat="server" ErrorMessage="Expiration month is required." ControlToValidate="txtExpirationMonth" ForeColor="Red" ValidationGroup="CreditCardPayment"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" runat="server" ErrorMessage="Expiration year is required." ControlToValidate="txtExpirationYear" ForeColor="Red" ValidationGroup="CreditCardPayment"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:RegularExpressionValidator ID="RegexValidatorExpirationMonth" runat="server" ControlToValidate="txtExpirationMonth" 
                                                ErrorMessage="Please enter a valid month (MM)." ValidationExpression="^\d{2}$" ForeColor="Red" ValidationGroup="CreditCardPayment" />
                                            <asp:RegularExpressionValidator ID="RegexValidatorExpirationYear" runat="server" ControlToValidate="txtExpirationYear" 
                                                ErrorMessage="Please enter a valid year (YY)." ValidationExpression="^\d{2}$" ForeColor="Red" ValidationGroup="CreditCardPayment" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group mb-4">
                                            <label data-toggle="tooltip" title="Three digit CV code on the back of your card">
                                                <h6>CVV <i class="fa fa-question-circle d-inline"></i></h6>
                                            </label>
                                            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" ValidationGroup="CreditCardPayment" 
                                                oninput="restrictNumericInput(this, 3)" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="CVV is required." ForeColor="Red" ControlToValidate="txtCVV" ValidationGroup="CreditCardPayment"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegexValidatorCVV" runat="server" ControlToValidate="txtCVV" 
                                                ErrorMessage="Please enter a valid CVV." ValidationExpression="^\d{3}$" ForeColor="Red" ValidationGroup="CreditCardPayment" />
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <asp:Button ID="btnCreditCardPayment" runat="server" Text=" Confirm Payment " 
                                        CssClass="subscribe btn btn-dark btn-block shadow-sm" OnClick="btnCreditCardPayment_Click"
                                        ValidationGroup="CreditCardPayment"/>
                                </div>
                            </div> <!-- End credit card info -->

                            <!-- Cash on Delivery info -->
                            <div id="COD" class="tab-pane fade pt-3">
                                <p>
                                    <asp:Button ID="btnCOD" runat="server" Text=" Confirm Payment " CssClass="btn btn-dark" 
                                        OnClientClick="showOrderConfirmationModal();" OnClick="btnCOD_Click" ValidationGroup="CODPayment"/>
                                </p>
                                <p>Note: After clicking on the button, your order will be placed. You have to pay the transaction by cash when your order is delivered to your address.</p>
                            </div> <!-- End Cash on Delivery info -->

                            <!-- Paypal info -->
                            <div id="paypal" class="tab-pane fade pt-3">
                                <div class="form-group">
                                    <p>
                                        <asp:Button ID="btnPaypal" runat="server" Text=" Proceed Payment " CssClass="btn btn-dark" 
                                            OnClientClick="showOrderConfirmationModal();" OnClick="btnPaypal_Click" ValidationGroup="PayPalPayment"/>
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

    <script type="text/javascript">
        function restrictCardOwnerInput(textbox) {
            var input = textbox.value;
            var filtered = input.replace(/[^a-zA-Z\s]/g, ''); // Remove non-letters and non-spaces
            if (input !== filtered) {
                textbox.value = filtered;
            }
        }
    </script>

    <script type="text/javascript">
        function restrictNumericInput(textbox, maxLength) {
            var input = textbox.value;
            var filtered = input.replace(/[^0-9\s]/g, ''); // Remove non-digits and non-spaces
            if (input !== filtered) {
                textbox.value = filtered;
            }
            if (textbox.value.length > maxLength) {
                textbox.value = textbox.value.substring(0, maxLength);
            }
        }
    </script>

    <script type="text/javascript">
        function formatCardNumber(textbox) {
            // Remove non-digit characters
            var input = textbox.value.replace(/\D/g, '');

            // Limit to 16 digits
            if (input.length > 16) {
                input = input.slice(0, 16);
            }

            // Format with spaces
            var formatted = input.replace(/(.{4})/g, '$1 ').trim();

            if (textbox.value !== formatted) {
                textbox.value = formatted;
            }
        }
    </script>
</asp:Content>
