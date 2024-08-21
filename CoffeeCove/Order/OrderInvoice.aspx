<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="OrderInvoice.aspx.cs" Inherits="CoffeeCove.Order.OrderInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/OrderInvoice.css" rel="stylesheet" />
    <div class="outside-container">
        <div class="invoice-container">
            <div class="invoice-header">
                <h1>INVOICE</h1>
                <div class="invoice-meta">
                    <p>Invoice Number: 2018644AE4INV</p>
                    <p>Invoice Date: 11-Apr-2018</p>
                    <p>Invoice Due: 27-Apr-2018</p>
                </div>
            </div>
            <div class="note">
                <i class="note-icon"></i>
                <span>This page has been enhanced for printing. Click the print button at the bottom to print.</span>
            </div>
            <div class="invoice-details">
                <div class="invoice-bill-to">
                    <h2>Bill To</h2>
                    <p>Mitsubishi Asia Pacific</p>
                    <p>Mitsubishi Shoji Building</p>
                    <p>info@mitsubishi.com</p>
                    <p>attn: Mr. Kenichiro Yamanishi</p>
                </div>
                <div class="invoice-from">
                    <h2>From</h2>
                    <p>Blue Ocean Cloud HQ</p>
                    <p>Moscow State University, Business Building</p>
                    <p>info@blueoceancloud.com</p>
                    <p>attn: Sergey Karpov</p>
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
                        <td>Red Hat Enterprise Linux</td>
                        <td>$1000.00</td>
                        <td>$10000.00</td>
                    </tr>
                    <tr>
                        <td>10</td>
                        <td>VM Windows Server 2018 Enterprise</td>
                        <td>$3000.00</td>
                        <td>$30000.00</td>
                    </tr>
                </tbody>
            </table>
            <div class="invoice-footer">
                <div class="note-to-recipients">
                    <label for="note">Note to recipients:</label>
                    <input type="text" id="note" name="note" />
                </div>
                <div class="amount-details">
                    <p>Subtotal: <span>$40000.00</span></p>
                    <p>Discount: <span>$0.00</span></p>
                    <p>Tax: <span>$0.00</span></p>
                    <p>Shipment: <span>$0.00</span></p>
                    <p>Total: <span>$40000.00</span></p>
                </div>
            </div>
            <div class="print-button">
                <asp:Button ID="PrintButton" runat="server" Text="Print" />
            </div>
        </div>
    </div>
    
</asp:Content>
