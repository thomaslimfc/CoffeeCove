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
            <tr id="cartTitle" style="border-bottom: solid 3px #433533">
                <td class="cartLeft" colspan="3">Item</td>
                <td class="cartRight">Quantity</td>
            </tr>

            <!--Repeater-->
            <asp:Repeater ID="rptOrdered" runat="server" OnItemCommand="rptOrdered_ItemCommand">
                <ItemTemplate>
                    <tr style="border-bottom: solid 2px #433533">
                        <td>
                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("ProductId") %>' Font-Bold="True" />
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ProductName") %>' />
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Size:&nbsp&nbsp<asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Flavour:&nbsp&nbsp<asp:Label ID="lblFlavour" runat="server" Text='<%# Eval("Flavour") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ice Level:&nbsp&nbsp<asp:Label ID="lblIce" runat="server" Text='<%# Eval("IceLevel") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Add-Ons:&nbsp&nbsp<asp:Label ID="lblAddOn" runat="server" Text='<%# Eval("AddOn") %>' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align:center">
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
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
