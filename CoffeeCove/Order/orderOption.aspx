<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="orderOption.aspx.cs" Inherits="CoffeeCove.Order.orderOption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/orderOpt.css" rel="stylesheet" />
<script src="getLocation.js"></script>

<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC3L6SbWC6TopqExyYVoVLWaPX7p8CQUHI&libraries=places&callback=initMap"
async defer></script>

<div id="poster">
    <img src="../img/coffeeBag.jpg" style="left: 0px; top: -37px" />
    <div id="container">
            <table id="tableContainer">
                <tr>
                    <td style="width:45%" class="tableElement">
                        <div>
                            <h1>Delivery</h1>
                            <hr />
                            <button onclick="getLocation()" type="button" class="btnLocation">
                            📍Get Your Current Position
                            </button>


                            <br />
                            <hr />
                            OR
                            <br />
                            <hr />
                            <table style="width:100%; padding:2%">
                                <tr>
                                    <td colspan="2" class="fullElement">
                                        Address Line 1:
                                        <asp:TextBox runat="server" ID="tbAddress1" Width="75%">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="fullElement">
                                        Address Line 2:
                                        <asp:TextBox runat="server" ID="tbAddress2" Width="75%">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftElement">
                                        State
                                        <asp:TextBox runat="server" ID="tbState">
                                        </asp:TextBox>
                                    </td>
                                    <td class="leftElement">
                                        City
                                        <asp:TextBox runat="server" ID="tbCity">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftElement">
                                        PostCode
                                        <asp:TextBox runat="server" ID="tbPostCode">
                                        </asp:TextBox>

                                    </td>
                                    <td class="leftElement">
                                        Unit/Level
                                        <asp:TextBox runat="server" ID="tbUnit">
                                        </asp:TextBox>
                                    </td>
                                </tr>

                            </table>

                        </div>
                        

                    </td>
                    <td>
                            &nbsp
                    </td>
                    <td style="width:45%" class="tableElement">
                        <div>
                            <h1>Pick Up</h1>
                            <hr />
                            <button onclick="openStoreList()" type="button" class="btnLocation">
                            Find Our Stores Location
                            </button>
                            
                            <asp:Literal ID="litStore" runat="server" Text=""></asp:Literal>

                        </div>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center;height:0">
                        <button id="btnCont" type="submit">Proceed</button>
                    </td>
                </tr>
                
            </table>

        <div id="overlay" class="overlay"></div>
        <div id="popupDialog">
            <div id="map" style="height: 100%"></div>
            <button onclick="closeMap()" type="button" class="btnClose" id="btnClose">Close</button>
        </div>

        <div id="overlay2" class="overlay"></div>
        <div id="popupDialog2">

            <table style="width:100%">
                <tr>
                    <td>
                        <img src="../img/location_icon.png" style="height:50px;width:50px" />
                    </td>
                    <td class="storeListTxt">
                        <asp:LinkButton runat="server" ID="lbGurney" Font-Underline="False">
                            <h2>CoffeeCove Gurney Plaza</h2>
                            170-G-23,24 Gurney Plaza, Pulau Tikus, 10250 George Town, Penang
                        </asp:LinkButton>
                    </td>

                    <td style="width:25%">&nbsp</td>

                    <td>
                        <img src="../img/location_icon.png" style="height:50px;width:50px" />
                    </td>
                    <td class="storeListTxt">
                        <asp:LinkButton runat="server" ID="lbKarpalSingh" Font-Underline="False">
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
                        <asp:LinkButton runat="server" ID="lbQueensBay" Font-Underline="False">
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
