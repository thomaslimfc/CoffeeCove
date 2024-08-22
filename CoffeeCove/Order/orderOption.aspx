<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="OrderOption.aspx.cs" Inherits="CoffeeCove.Order.orderOption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/orderOpt.css" rel="stylesheet" />
<script src="getLocation.js"></script>

<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC3L6SbWC6TopqExyYVoVLWaPX7p8CQUHI&libraries=places&callback=initMap"
async defer></script>

<div id="poster">
    <div id="container">
            <table id="tableContainer">
                <tr>
                    <td style="width:45%" class="tableElement">
                        <div>
                            <h2>Delivery</h2>
                            <button onclick="getLocation()" type="button" class="btnLocation" style="font-weight: bold">
                            📍Get Your Current Position
                            </button>
                            <br />
                            <br />
                            OR
                            <table style="width:100%; padding:2%; border-spacing:5px">
                                <tr>
                                    <td class="leftElement">
                                        Address Line 1:
                                    </td>
                                    <td class="rightElement">
                                        <asp:TextBox runat="server" ID="txtAddress1" CssClass="textAdd" PlaceHolder="Enter Address 1" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Address" ControlToValidate="txtAddress1" CssClass="error" Display="Dynamic" ValidationGroup="AddressForm"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="leftElement">
                                        Address Line 2:
                                    </td>
                                    <td class="rightElement">
                                        <asp:TextBox runat="server" ID="txtAddress2" Width="90%" CssClass="textAdd" PlaceHolder="Enter Address 2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Address" ControlToValidate="txtAddress2" CssClass="error" Display="Dynamic" ValidationGroup="AddressForm"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="leftElement">
                                        PostCode:
                                    </td>
                                    <td class="rightElement"><asp:TextBox runat="server" ID="txtPostCode" Width="90%" CssClass="textAdd" PlaceHolder="Enter PostCode"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter PostCode" ControlToValidate="txtPostCode" CssClass="error" Display="Dynamic" ValidationGroup="AddressForm"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid PostCode (5 digits only)." ControlToValidate="txtPostCode" CssClass="error" Display="Dynamic" ValidationGroup="AddressForm" ValidationExpression="^\d{5}$" /></td>
                                </tr>
                                <tr>
                                    <td class="leftElement">
                                    Unit/Level:
                                </td>
                                    <td class="rightElement"><asp:TextBox runat="server" ID="txtUnit" CssClass="textAdd" Width="90%" PlaceHolder="Enter Unit/Level (Optional)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:center" colspan="2">
                                        <asp:LinkButton ID="lbConfirm" runat="server" CssClass="btnCont" Font-Underline="false" ValidationGroup="AddressForm">Confirm</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <!--Fill address end-->
                        </div>
                    </td>
                    <!--Delivery end-->
                    <td>
                            &nbsp
                    </td>
                    <td style="width:45%" class="tableElement">
                        <div>
                            <h2>Pick Up</h2>
                            <button onclick="openStoreList()" type="button" class="btnLocation" style="font-weight: bold">
                            🏪 Find Our Stores
                            </button>
                            <br />
                            <br />
                            <span style="font-weight: bold"><asp:Label ID="lblStoreName" runat="server" EnableViewState="False"></asp:Label></span>
                            <br />
                            <span style="padding-left:20px;padding-right:20px;display:block"><asp:Label ID="lblStoreAdd" runat="server" EnableViewState="False"></asp:Label></span>
                        </div>
                    </td>
                    <!--Pick Up end-->
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center;height:0">
                        <asp:LinkButton ID="lbProceed" runat="server" CssClass="btnCont" Font-Underline="false">Proceed</asp:LinkButton>
                    </td>
                </tr>
            </table>

        <!--Overlay for Delivery Map-->
        <div id="overlay" class="overlay"></div>
        <div id="popupDialog">
            <div id="map" style="height: 85%;border-radius:8px;border: 1px solid #ddd;box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1)"></div>
            <br />
            <table style="width:100%">
                <tr>
                    <td>
                        <button onclick="closeMap()" class="btnCont">Close</button>
                    </td>
                    <td>
                        <button onclick="closeMap()" class="btnCont">Confirm</button>
                    </td>
                </tr>
            </table>
        </div>

        <!--Overlay for Pick Up-->
        <div id="overlay2" class="overlay"></div>
        <div id="popupDialog2">
            <table style="width:100%;border-collapse:separate;border-spacing:10px">
            <asp:Repeater ID="rptStoreList" runat="server" OnItemCommand="rptStoreList_ItemCommand">
                <ItemTemplate>
                    <%# (Container.ItemIndex + 2) % 2 == 0 ? "<tr>" : string.Empty %>
                    <td style="width:30%" class="storeList">
                        <asp:LinkButton ID="lbStoreList" runat="server" Font-Underline="false" CommandName="storeList" CommandArgument='<%# Eval("StoreID") %>' >
                            <div class="storeName"><h3><%# DataBinder.Eval(Container.DataItem, "StoreName") %></h3></div>
                            <div><%# DataBinder.Eval(Container.DataItem, "StoreAddress") %></div>
                        </asp:LinkButton>
                    </td>
                    <%# (Container.ItemIndex + 2) % 2 == 1 ? "</tr>" : string.Empty %>
                </ItemTemplate>
            </asp:Repeater>
            </table>
            <br />
            <button onclick="closeStoreList()" class="btnCont">Close</button>
        </div>
    </div>
</div>
</asp:Content>
