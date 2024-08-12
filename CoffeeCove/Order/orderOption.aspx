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
                            <h2 style="padding-top:20px">Delivery</h2>
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
                            <h2 style="padding-top:20px">Pick Up</h2>
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
            <div id="map" style="height: 100%"></div>
            <button onclick="closeMap()" type="button" class="btnClose">Close</button>
        </div>

        <!--Overlay for Pick Up-->
        <div id="overlay2" class="overlay"></div>
        <div id="popupDialog2">
            <table style="width:100%">
                <tr>
                    <td>
                        <img src="../img/location_icon.png" style="height:50px;width:50px" />
                    </td>
                    <td class="storeListTxt" style="width:35%">
                        <asp:LinkButton runat="server" ID="lbGurney" Font-Underline="False" OnClientClick="closeStoreList();" OnClick="lbGurney_Click">
                            <h2>CoffeeCove Gurney Plaza</h2>
                            170-G-23,24 Gurney Plaza, Pulau Tikus, 10250 George Town, Penang
                        </asp:LinkButton>
                    </td>
                    <td style="width:15%">&nbsp</td>
                    <td>
                        <img src="../img/location_icon.png" style="height:50px;width:50px" />
                    </td>
                    <td class="storeListTxt" style="width:35%">
                        <asp:LinkButton runat="server" ID="lbKarpalSingh" Font-Underline="False" OnClientClick="closeStoreList();" OnClick="lbKarpalSingh_Click">
                            <h2>CoffeeCove Karpal Singh</h2>
                            No. 29C, Lot L1-1, L1-2, Maritime, 5, Lebuh Sungai Pinang, 11600 Jelutong, Penang
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../img/location_icon.png" style="height:50px;width:50px" />
                    </td>
                    <td class="storeListTxt">
                        <asp:LinkButton runat="server" ID="lbQueensBay" Font-Underline="False" OnClientClick="closeStoreList();" OnClick="lbQueensBay_Click">
                            <h2>CoffeeCove QueensBay</h2>
                            1-G-01, Jalan Bayan Indah, Queens Waterfront Q1 Commercial, 11900 George Town, Pulau Pinang
                        </asp:LinkButton>
                    </td>
                    <td colspan="3">&nbsp</td>
                </tr>
            </table>
            <br />
            <button onclick="closeStoreList()" type="button" class="btnClose">Close</button>
            <br />
        </div>
    </div>
</div>
</asp:Content>
