<%@ Page Title="Payment Success" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="paymentSuccess.aspx.cs" Inherits="CoffeeCove.Payment.paymentSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
        function showError(message) {
            alert(message);
        }
    </script>
    <link href="https://fonts.googleapis.com/css?family=Nunito+Sans:400,400i,700,900&display=swap" rel="stylesheet" />
    <link href="../CSS/PaymentSuccess.css" rel="stylesheet" />

    <div id="container">
        <div class="card">
            <div style="border-radius:200px; height:200px; width:200px; background: #F8FAF5; margin:0 auto;">
                <i class="checkmark">✓</i>
            </div>
            <h1>Success</h1>
            <p>We received your purchase request,<br /> your order will be prepared shortly!</p>
            
            <!-- Buttons for Home and Order History -->
            <div class="button-container">
                <asp:Button ID="btnHome" runat="server" Text="Home" CssClass="success-btn" OnClick="btnHome_Click" />
                <asp:Button ID="btnOrderHistory" runat="server" Text="Order History" CssClass="success-btn" OnClick="btnOrderHistory_Click" />
            </div>
        </div>
    </div>
</asp:Content>
