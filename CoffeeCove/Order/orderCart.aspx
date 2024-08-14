<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="orderCart.aspx.cs" Inherits="CoffeeCove.Order.orderCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/orderCart.css" rel="stylesheet" />
    <!-- Banner -->
    <div id="cartPoster">
        <img src="../img/coffeeBag.jpg" id="cartPosterImg" />
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

            <!--Repeater-->
            <asp:Repeater ID="rptOrdered" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("ProductId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ProductName") %>' />
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSize" runat="server" Text='<%# Eval("ProductId") %>' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
                    




                
        </table>

        <table id="cartTotalTable">
            <tr>
                <td class="cartLeft">Subtotal [RM]</td>
                <td class="cartRight">RM
                <asp:Label ID="lblSubtotal" runat="server">x</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="cartLeft">Tax [6%]</td>
                <td class="cartRight">RM
                <asp:Label ID="lblTax" runat="server">x</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="cartLeft">Total [RM]</td>
                <td class="cartRight">RM
                <asp:Label ID="lblTotal" runat="server">x</asp:Label>
                </td>
            </tr>
        </table>

        
        <asp:Button runat="server" Text="Proceed" CSSClass="btnProceed" ID="btnProceed"/>
    </div>
</asp:Content>
