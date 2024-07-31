<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="deliveryOpt.aspx.cs" Inherits="CoffeeCove.Order.deliveryOpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/deliveryOpt.css" rel="stylesheet" />
    <script src="getLocation.js"></script>
    
    <div id="poster">
        <img src="../img/coffeeBag.jpg" />
        <div id="container">
            <div class="element">
                <table>
                    <tr>
                        <td colspan="2">
                            <button onclick="getLocation()" type="button">📍Get Your Current Position</button>
                            <p id="coord"></p>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                            OR
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Address Line 1:
                            <asp:TextBox ID="tbAddress1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Address Line 2:
                            <asp:TextBox ID="tbAddress2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

        </div>
    </div>

</asp:Content>
