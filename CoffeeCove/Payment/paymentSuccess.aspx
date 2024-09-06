<%@ Page Title="Payment Success" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="paymentSuccess.aspx.cs" Inherits="CoffeeCove.Payment.paymentSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="https://fonts.googleapis.com/css?family=Nunito+Sans:400,400i,700,900&display=swap" rel="stylesheet" />
    <link href="../CSS/PaymentSuccess.css" rel="stylesheet" />

    <div id="container">
        <div class="card">
            <div style="border-radius:200px; height:200px; width:200px; background: #F8FAF5; margin:0 auto;">
                <i class="checkmark">✓</i>
            </div>
            <h1>Success</h1>
            <p>We received your purchase request,<br /> order will be prepare shortly!</p>
        </div>
    </div>
</asp:Content>
