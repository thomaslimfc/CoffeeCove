<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CoffeeCove.Menu.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div id="menuContainer">
        <!-- Category -->
        <div id="categoryContainer">
            <asp:Repeater ID="rptCategory" runat="server" OnItemCommand="rptCategory_itemCommand" >
                <ItemTemplate>
                    <asp:LinkButton ID="linkCategory" runat="server" CssClass="categoryLink" CommandArgument='<%# Eval("CategoryId") %>' CommandName="Select" 
                        Style="display: inline-block; padding: 10px; margin: 5px; color: #433533; 
                        text-decoration: none; font-size: 16px; font-weight: bold; cursor: pointer; 
                        letter-spacing: 0.5px ;">
                        <%# Eval("CategoryName") %>
                    </asp:LinkButton>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Product -->
        <div id="productContainer">
            <div id="productItem">
                <asp:Repeater ID="rptProduct" runat="server" OnItemCommand="rptProducts_ItemCommand">
                    <ItemTemplate>
                        <div style="width: 20%; height: 59%;margin: 1%; box-sizing: border-box;" id="productContainer">
                            <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>' style="width: 100%; height: auto" />
                            <div style="height:45px"><strong><%# Eval("ProductId") %>&nbsp;&nbsp;<%# Eval("ProductName") %></strong></div>
                            <div style="padding-top:5px;font-size:17px"><%# Eval("UnitPrice", "RM {0:N2}") %></div>
                            <asp:LinkButton ID="lnkSelectProduct" runat="server" CssClass="SelectBtn" CommandArgument='<%# Eval("ProductID") %>' CommandName="SelectProduct">Select</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Order Form -->
            <asp:Panel ID="pnlOrderForm" runat="server" CssClass="orderform" Visible="false">
                <table>
                    <tr>
                        <td style="width: 100px"><asp:Button ID="btnClose" runat="server" Text="X" OnClick="btnClose_Click" Font-Size="20px" Height="30px" Width="30px" BackColor="White" ForeColor="#CC0000"/></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="width: 100px"><asp:Image ID="imgProduct" runat="server" Width="100px" /></td>
                        <td>
                            <asp:Label ID="lblProductID" runat="server" Font-Size="20px" />
                            <asp:Label ID="lblProductName" runat="server" Font-Size="20px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Label ID="lblProductDescription" runat="server" Font-Size="15px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lbSize" runat="server" Text="Size"></asp:Label>
                        </td>
                        <td>   
                            <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdatePrice">
                                <asp:ListItem Value="Regular">Regular</asp:ListItem>
                                <asp:ListItem Value="Large">Large</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lbFlavour" runat="server" Text="Flavour"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFlavour" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdatePrice">
                                <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                <asp:ListItem Value="Cold">Cold</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lbIceLevel" runat="server" Text="Ice Level"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIceLevel" runat="server">
                                <asp:ListItem Value="NoIce">No Ice</asp:ListItem>
                                <asp:ListItem Value="HalfIce">Half Ice</asp:ListItem>
                                <asp:ListItem Value="NormalIce">Regular Ice</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lbAddOn" runat="server" Text="Add-Ons"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddOn" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdatePrice">
                                <asp:ListItem Value="None">None</asp:ListItem>
                                <asp:ListItem Value="1EspressoShot">1 Espresso Shot (+RM2.50)</asp:ListItem>
                                <asp:ListItem Value="2EspressoShots">2 Espresso Shots (+RM5.00)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lbSpecialInstruction" runat="server" Text="Special Instructions"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSpecialInstructions" runat="server" TextMode="MultiLine" Rows="5" Columns="30" MaxLength="500" Placeholder="e.g. no mayo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblPrice" runat="server" Text="Price: RM 0.00"/>
                        </td>
                        <td>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" Font-Size="17px" ForeColor="Black" Height="30px" Width="100px"/>
                        </td>
                        <td>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" Font-Size="17px" ForeColor="Black" Height="30px" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
