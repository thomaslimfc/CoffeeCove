<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CoffeeCove.Menu.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
        <link href="../CSS/Menu.css" rel="stylesheet" />
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
                            <asp:LinkButton ID="lnkSelectProduct" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="SelectProduct">Select</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Order Form -->
            <asp:Panel ID="pnlOrderForm" runat="server" CssClass="orderform" Visible="false">
                <table>

                    <!-- Close button -->
                    <tr>
                        <td style="width: 100px">
                            <asp:Button ID="btnClose" runat="server" Text="X" OnClick="btnClose_Click" CssClass="btnClose" />
                        </td>
                    </tr>

                    <!-- Product details -->
                    <tr>
                        <td rowspan="2" style="width: 150px">
                            <asp:Image ID="imgProduct" runat="server" Width="150px" CssClass="productImg" />

                        </td>
                        <td colspan="3" style="height: 20px">
                            <asp:Label ID="lblProductID" runat="server" Font-Size="20px" />
                            <asp:HiddenField ID="hfProductId" runat="server" />
                            <asp:Label ID="lblProductName" runat="server" Font-Size="20px" />
                            <asp:HiddenField ID="hfProductName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 21px">
                            <asp:Label ID="lblProductDescription" runat="server" Font-Size="15px" />
                        </td>
                    </tr>

                    <!-- Order product  -->
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lblSize" runat="server" Text="Size" CssClass="label" ></asp:Label>
                            <asp:HiddenField ID="hfSize" runat="server" />
                        </td>
                        <td>   
                            <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdatePrice" CssClass="ddlOrder">
                                <asp:ListItem Value="Regular">Regular</asp:ListItem>
                                <asp:ListItem Value="Large">Large (+RM1.50)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lblFlavour" runat="server" Text="Flavour" CssClass="label"></asp:Label>
                            <asp:HiddenField ID="hfFlavour" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFlavour" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdatePrice" CssClass="ddlOrder">
                                <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                <asp:ListItem Value="Cold">Cold (+RM1.50)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lblIceLevel" runat="server" Text="Ice Level" CssClass="label"></asp:Label>
                            <asp:HiddenField ID="hfIceLevel" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIceLevel" runat="server" CssClass="ddlOrder">
                                <asp:ListItem Value="NoIce">No Ice</asp:ListItem>
                                <asp:ListItem Value="HalfIce">Half Ice</asp:ListItem>
                                <asp:ListItem Value="NormalIce">Regular Ice</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lblAddOn" runat="server" Text="Add-Ons" CssClass="label"></asp:Label>
                            <asp:HiddenField ID="hfAddOn" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddOn" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdatePrice" CssClass="ddlOrder">
                                <asp:ListItem Value="None">None</asp:ListItem>
                                <asp:ListItem Value="1EspressoShot">1 Espresso Shot (+RM2.50)</asp:ListItem>
                                <asp:ListItem Value="2EspressoShots">2 Espresso Shots (+RM5.00)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lblSpecialInstruction" runat="server" Text="Special Instructions" CssClass="label"></asp:Label>
                            <asp:HiddenField ID="hfSpecialInstructions" runat="server" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtSpecialInstructions" runat="server" TextMode="MultiLine" Rows="5" Columns="40" MaxLength="500" Placeholder="e.g. no mayo" Font-Size="15px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity" CssClass="label"></asp:Label>
                            <asp:HiddenField ID="hfQuantity" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnDecrease" runat="server" Text="-" OnClick="btnDecrease_Click" Font-Size="20px" Width="30px" />
                            <asp:TextBox ID="txtQuantity" runat="server" Text="1" Width="50px" CssClass="quantity-box" ReadOnly="true" Font-Size="15px" Height="23px"/>
                            <asp:Button ID="btnIncrease" runat="server" Text="+" OnClick="btnIncrease_Click" Font-Size="20px" Width="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblPrice" runat="server" Text="Price: RM 0.00" Font-Size="17px" CssClass="label"/>
                            <asp:HiddenField ID="hfUpdatedPrice" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btnReset"/>
                        </td>
                        <td>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btnAdd" OnClick="btnAddToCart_Click"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
