<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="orderCart.aspx.cs" Inherits="CoffeeCove.Order.orderCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <!-- Banner -->
    <div id="cartPoster">
        <img src="/img/menuPoster.jpg" />
        <div id="cartPosterText">My Cart</div>
    </div>
    <br />
    <!-- Cart -->
    <div id="cartContainer">
        <table id="cartItemTable">
            <tr>
                <td class="cartItem">Item</td>
                <td class="cartQuantity">Quantity</td>
            </tr>
            <tr>
                <td class="cartItem">&nbsp;</td>
                <td class="cartQuantity">&nbsp;</td>
            </tr>
            <tr>
                <td class="cartItem">&nbsp;</td>
                <td class="cartQuantity">&nbsp;</td>
            </tr>
            <tr>
                <td class="cartItem">&nbsp;</td>
                <td class="cartQuantity">&nbsp;</td>
            </tr>
            <tr>
                <td class="cartItem">&nbsp;</td>
                <td class="cartQuantity">&nbsp;</td>
            </tr>
        </table>

        <table id="cartTotalTable">
            <tr>
                <td>hihi</td>
            </tr>
        </table>

    </div>

</asp:Content>
