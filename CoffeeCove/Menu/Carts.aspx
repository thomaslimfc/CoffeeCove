<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Carts.aspx.cs" Inherits="CoffeeCove.Menu.Carts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <style>
        .Image {
            width:200px;
            height:200px;
        }

        #cartContainer {
            padding-top: 80px;
        }
    </style>
    <div id="cartContainer">
        <asp:Repeater ID="rptCart" runat="server">
            <HeaderTemplate>
                <table>
                    <thead>
                        <tr>
                            <th colspan="2">Product</th>
                            <th>Quantity</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td rowspan="6"><img class="Image" src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>'/></td>
                </tr>
                <tr>
                    <td><%# Eval("ProductID") %></td>
                    <td><%# Eval("ProductName") %></td>
                    <td><%# Eval("Quantity") %></td>
                    <td><%# Eval("Price") %></td>
                </tr>
                <tr>
                    <td><%# Eval("Size") %></td>
                </tr>
                <tr>
                    <td><%# Eval("Flavour") %></td>
                </tr>
                <tr>
                    <td><%# Eval("IceLevel") %></td>
                </tr>
                <tr>
                    <td><%# Eval("AddOn") %></td>
                </tr>
                <tr>
                    <td><%# Eval("Instruction") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
