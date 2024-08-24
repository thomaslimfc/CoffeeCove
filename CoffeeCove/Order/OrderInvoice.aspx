<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="OrderInvoice.aspx.cs" Inherits="CoffeeCove.Order.OrderInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/OrderInvoice.css" rel="stylesheet" />
    <div class="outside-container">
        <div class="invoice-container">
            <div class="invoice-header">
                <h1>INVOICE</h1>
                <div class="invoice-meta">
                    <p>Invoice Number: 1</p>
                    <p>Invoice Date: 08/24/2024</p>
                </div>
            </div>
            <div class="note">
                <i class="note-icon"></i>
                <span>Click the print button at the bottom to print.</span>
            </div>
            <div class="invoice-details">
                <div class="invoice-bill-to">
                    <h2>Bill To</h2>
                    <p>Username: Lim Ler Shean</p>
                    <p>Email: Limlershean@gmail.com</p>
                    <p>Address: Tanjong Bungah</p>
                </div>
                <div class="invoice-from">
                    <h2>From</h2>
                    <p>CoffeeCove Gurney Plaza</p>
                    <p>adminCoffeeCove@gmail.com</p>
                    <p>170-G-23,24 Gurney Plaza, Pulau Tikus, 10250 George Town, Penang</p>
                </div>
            </div>
            <table class="invoice-table">
                <thead>
                    <tr>
                        <th>Qty</th>
                        <th>Product</th>
                        <th>Price</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>10</td>
                        <td>Eggs In Purgatory</td>
                        <td>RM12.50</td>
                        <td>RM125</td>
                    </tr>
                    <tr>
                        <td>10</td>
                        <td>Prosciutto & Fontina On Cornetto</td>
                        <td>RM10</td>
                        <td>RM100</td>
                    </tr>
                </tbody>
            </table>
            <div class="invoice-footer">
                <div class="amount-details">
                    <p>Subtotal: <span>RM225</span></p>
                    <p>Discount: <span>RM0</span></p>
                    <p>Tax: <span>RM0</span></p>
                    <p>Shipment: <span>RM0</span></p>
                    <p>Total: <span>RM225</span></p>
                </div>
            </div>
            <div class="print-button">
                <asp:Button ID="BackButton" runat="server" Text="Back" CssClass="btnBack" OnClick="BackButton_Click" />
                <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="btnCont" />
            </div>
        </div>
    </div>
</asp:Content>
