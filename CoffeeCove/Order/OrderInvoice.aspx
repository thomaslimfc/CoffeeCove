<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="OrderInvoice.aspx.cs" Inherits="CoffeeCove.Order.OrderInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/OrderInvoice.css" rel="stylesheet" />
    <div class="outside-container">
        <div class="invoice-container">
            <div class="invoice-header">
                <h1>INVOICE</h1>
                <div class="invoice-meta">
                    <p>Invoice Number: <asp:Literal ID="OrderIdLiteral" runat="server"></asp:Literal></p>
                    <p>Invoice Date: <asp:Literal ID="InvoiceDateLiteral" runat="server"></asp:Literal></p>
                </div>
            </div>
            <div class="note">
                <i class="note-icon"></i>
                <span>Click the print button at the bottom to print.</span>
            </div>
            <div class="invoice-details">
                <div class="invoice-bill-to">
                    <h2>Bill To</h2>
                    <p>Username: <asp:Literal ID="UsernameLiteral" runat="server"></asp:Literal></p>
                    <p>Email: <asp:Literal ID="EmailLiteral" runat="server"></asp:Literal></p>
                    <p>Phone: <asp:Literal ID="PhoneLiteral" runat="server"></asp:Literal></p>
                </div>
                <div class="invoice-from">
                    <h2>From</h2>
                    <p>CoffeeCove Malaysia</p>
                    <p>Website: </p>
                    <p>https://localhost:44324/Home/Home.aspx</p>
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
                    <asp:Repeater ID="ProductTable" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Quantity") %></td>
                                <td><%# Eval("ProductName") %></td>
                                <td>RM<%# Eval("Price") %></td>
                                <td>RM<%# Eval("Subtotal") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            
            <asp:Label ID="NoProductsMessage" runat="server" Text="No products found for this order." Visible="false" CssClass="no-products-msg"></asp:Label>

            <div class="invoice-footer">
                <div class="amount-details">
                    <p>Subtotal: <span><asp:Label ID="InvoiceSubtotal" runat="server" Text="RM0.00"></asp:Label></span></p>
                    <p>Tax(6%): <span><asp:Label ID="InvoiceTax" runat="server" Text="RM0.00"></asp:Label></span></p>
                    <p>Total: <span><asp:Label ID="InvoiceTotal" runat="server" Text="RM0.00"></asp:Label></span></p>
                </div>
            </div>
            <div class="print-button">
                <asp:Button ID="BackButton" runat="server" Text="Back" CssClass="btnBack" OnClick="BackButton_Click" />
                <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="btnCont" OnClick="PrintButton_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
