<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="orderOption.aspx.cs" Inherits="CoffeeCove.Order.orderOption" %>
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
                                    <td colspan="2" class="fullElement">
                                        Address Line 1:
                                        <asp:TextBox runat="server" ID="tbAddress1" Width="75%" CssClass="textAdd" PlaceHolder="xxxxx">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="fullElement">
                                        Address Line 2:
                                        <asp:TextBox runat="server" ID="tbAddress2" Width="75%" CssClass="textAdd" PlaceHolder="xxxxx">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftElement">
                                        PostCode:
                                        <asp:TextBox runat="server" ID="tbPostCode" CssClass="textAdd" PlaceHolder="00000">
                                        </asp:TextBox>
                                    </td>
                                    <td class="leftElement">
                                        Unit/Level:
                                        <asp:TextBox runat="server" ID="tbUnit" CssClass="textAdd" PlaceHolder="Unit 8/Level 10">
                                        </asp:TextBox>
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
                            <asp:Label ID="lblStoreAdd" runat="server" EnableViewState="False"></asp:Label>
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
                        <button id="btnCont" type="submit">Proceed</button>
                    </td>
                </tr>
            </table>

        <!--Overlay for Delivery Map-->
        <div id="overlay" class="overlay"></div>
        <div id="popupDialog">
            <div id="map" style="height: 85%;border-radius:8px;border: 1px solid #ddd;box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1)"></div>
            <br />
            <button onclick="closeMap()" type="button" class="btnClose">Close</button>
        </div>

        <!--Overlay for Pick Up-->
        <div id="overlay2" class="overlay"></div>
        <div id="popupDialog2">
            <table style="width:100%;border-collapse:separate;border-spacing:10px">
                <!--Repeater-->
                <asp:Repeater ID="rptStoreList" runat="server" OnItemCommand="rptStoreList_ItemCommand">
                    <ItemTemplate>
                        <%# (Container.ItemIndex + 2) % 2 == 0 ? "<tr>" : string.Empty %>
                        <td style="width:30%" class='<%# (Container.ItemIndex + 2) % 2 == 0 ? "oddClass" : "evenClass" %>'>
                            <asp:LinkButton ID="lbStoreList" runat="server" Font-Underline="false" CommandArgument='<%# Eval("StoreId") %>'>
                                <div class="storeName"><h3><%# DataBinder.Eval(Container.DataItem, "StoreName") %></h3></div>
                                <div><%# DataBinder.Eval(Container.DataItem, "StoreAddress") %></div>
                            </asp:LinkButton>
                        </td>
                        <%# (Container.ItemIndex + 2) % 2 == 1 ? "</tr>" : string.Empty %>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <br />
            <button onclick="closeStoreList()" type="button" class="btnClose">Close</button>
        </div>
    </div>
</div>
</asp:Content>
