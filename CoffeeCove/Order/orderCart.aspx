<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="orderCart.aspx.cs" Inherits="CoffeeCove.Order.orderCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/orderCart.css" rel="stylesheet" />
    <link href="../CSS/orderOption.css" rel="stylesheet" />
    <!-- Banner -->
    <div id="cartPoster">
        <img src="/img/poster.jpg" />
        <div id="cartPosterText">My Cart</div>
    </div>
    <br />
    <!-- Cart -->
    <div id="cartContainer">
        <table id="cartItemTable">
            <tr id="cartTitle">
                <td class="cartLeft" colspan="2">Item</td>
                <td class="cartRight">Quantity</td>
            </tr>
            <tr>
                <td>
                    <asp:Image runat="server" ID="imgCart" CssClass="imgCart" ImageUrl="~/imgProductItems/coffee8.jpg"></asp:Image>
                </td>
                <td class="cartLeft">coffeee</td>
                <td class="cartRight">50</td>
            </tr>
        </table>

        <table id="cartTotalTable">
            <tr>
                <td class="cartLeft">Subtotal [RM]</td>
                <td class="cartRight">RM
                <asp:Literal ID="litSubtotal" runat="server">lit</asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="cartLeft">Tax [6%]</td>
                <td class="cartRight">RM
                <asp:Literal ID="litTax" runat="server">lit</asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="cartLeft">Total [RM]</td>
                <td class="cartRight">RM
                <asp:Literal ID="litTotal" runat="server">lit</asp:Literal>
                </td>
            </tr>
        </table>

        
        <asp:Button runat="server" Text="Proceed" CSSClass="btnProceed" ID="btnProceed"/>
    </div>
</asp:Content>
