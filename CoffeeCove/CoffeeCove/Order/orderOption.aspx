<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="orderOption.aspx.cs" Inherits="CoffeeCove.orderOption.orderOption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

<<<<<<< HEAD
    <div id="option-title">
        <h1>Delivery or Pick Up</h1>
    </div>
    
    <div class="option-container">
        <asp:LinkButton ID="lbDelivery" runat="server" CssClass="option-child" OnClick="lbDelivery_Click" Font-Underline="false">
            <h2>Delivery</h2>
            <br />
            <asp:Image ImageUrl="delivery.png" runat="server" class="img"/>
        </asp:LinkButton>
        <asp:LinkButton ID="lbPickUp" runat="server" CssClass="option-child" OnClick="lbPickUp_Click" Font-Underline="false">
            <h2>Pick Up</h2>
            <br />
            <asp:Image ImageUrl="pickup.png" runat="server" class="img"/>
        </asp:LinkButton>   
    </div>
=======
    <!-- Banner -->
    <div id="cartPoster">
        <img src="/img/menuPoster.jpg" />
        <div id="cartPosterText">Pick Up or Delivery</div>
    </div>
    <br />
    <!-- Select Order Option -->
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

>>>>>>> 8a5e199df1135dcba652b8424c14d045d5dc4047
    <div>
        <asp:Literal ID="litTest" runat="server"></asp:Literal>
    </div>
</asp:Content>
