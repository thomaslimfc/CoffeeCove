<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="orderOption.aspx.cs" Inherits="CoffeeCove.orderOption.orderOption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <!-- Select Order Option -->
    <div id="cartPoster">
        <img src="/img/menuPoster.jpg" />
        <div id="cartPosterText">Pick Up or Delivery</div>
    </div>
    <br />
    <div id="orderOption">
        <table id="orderOptionTable">
            <tr>
                <td class="orderOptionTD">
                    <asp:LinkButton ID="lbDelivery" runat="server" CssClass="orderOptionLB" OnClick="lbDelivery_Click">
                        <asp:Image ImageUrl="../img/delivery.png" runat="server" class="img"/>
                    </asp:LinkButton>
                </td>
                <td></td>
                <td class="orderOptionTD">
                    <asp:LinkButton ID="lbPickUp" runat="server" CssClass="orderOptionLB" OnClick="lbPickUp_Click">
                        <asp:Image ImageUrl="../img/pickup.png" runat="server" class="img"/>
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>Delivery</td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>PickUp</td>
            </tr>
        </table>
    </div>

    <div>
        <asp:Literal ID="litTest" runat="server"></asp:Literal>
    </div>
</asp:Content>
