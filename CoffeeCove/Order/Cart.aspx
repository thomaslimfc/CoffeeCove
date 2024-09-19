<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="CoffeeCove.Order.orderCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/orderCart.css" rel="stylesheet" />

    <div id="container">
    <h1 class="serviceTitle">My Cart</h1>
    <!-- Cart -->
    <div id="cartContainer">
        <div id="scrollableCart">
            <table id="cartItemTable">
                <tr id="cartTitle" style="border-bottom: solid 3px #433533">
                    <th class="cartLeft" colspan="4">Item</th>
                    <th class="cartRight">Price</th>
                    <th class="cartRight">Quantity</th>
                    <th class="cartRight">Total</th>
                </tr>
            
                <!--Repeater-->
                <asp:Repeater ID="rptOrdered" runat="server" OnItemDataBound="rptOrdered_ItemDataBound" OnItemCommand="rptOrdered_ItemCommand">
                    <ItemTemplate>
                        <tr style="border-bottom: solid 2px #433533">
                            <td class="tableItem" style="text-align:center">
                                <asp:LinkButton ID="lbDelete" runat="server" CommandName="btnDelete" CommandArgument='<%# Eval("OrderedItemID") %>' CssClass="btnDelete" Font-Underline="false"><img src="../img/trash-bin.png" alt="Delete" class="imgDelete"/></asp:LinkButton>
                            </td>
                            <td class="tableItem" style="text-align:center">
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("ProductId") %>' Font-Bold="True" />
                            </td>
                            <td class="tableItem" style="border-right:none">
                                <asp:Label ID="lblName" runat="server" Font-Bold="true" Text='<%# Eval("ProductName") %>' CssClass="itemName" />
                            </td>
                            <td class="tableItem" style="border-left:none">
                                    <table ID="excludeTable">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="panelSize" runat="server">
                                                    Size:&nbsp&nbsp<asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>' />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                <asp:Panel ID="panelFlavour" runat="server">
                                                    Flavour:&nbsp&nbsp<asp:Label ID="lblFlavour" runat="server" Text='<%# Eval("Flavour") %>' />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="panelIce" runat="server">
                                                    Ice Level:&nbsp&nbsp<asp:Label ID="lblIce" runat="server" Text='<%# Eval("IceLevel") %>' />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                <asp:Panel ID="panelAddon" runat="server">
                                                    Add-Ons:&nbsp&nbsp<asp:Label ID="lblAddOn" runat="server" Text='<%# Eval("AddOn") %>' />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>

                            </td>
                            <td style="text-align:center" class="tableItem">
                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>' />
                            </td>
                            <td style="text-align:center" class="tableItem">
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                            </td>
                            <td style="text-align:center" class="tableItem">
                                <asp:Label ID="lblLineTotal" runat="server" Text="" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                    




                
            </table>
        </div>
        <table id="cartTotalTable">
            <tr class="amtTable">
                <td class="cartLeft">Subtotal [RM]</td>
                <td class="cartRight">
                <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="amtTable">
                <td class="cartLeft">Tax [6%]</td>
                <td class="cartRight">+
                <asp:Label ID="lblTax" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="amtTable">
                <td class="cartLeft">Total [RM]</td>
                <td class="cartRight">
                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table style="width:100%">
            <tr>
                <td>
                    <asp:LinkButton ID="btnProceed" runat="server" OnClick="btnProceed_Click" CSSClass="btnProceed" Font-Underline="false">Proceed</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    </div>
</asp:Content>
