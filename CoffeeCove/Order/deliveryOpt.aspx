<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="deliveryOpt.aspx.cs" Inherits="CoffeeCove.Order.deliveryOpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/deliveryOpt.css" rel="stylesheet" />
    <script src="getLocation.js"></script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC3L6SbWC6TopqExyYVoVLWaPX7p8CQUHI&libraries=places&callback=initMap"
    async defer></script>
    
    <div id="poster">
        <img src="../img/coffeeBag.jpg" />
        <div id="container">
                <table id="tableContainer">
                    <tr>
                        <td style="width:45%" class="tableElement">
                            <div>
                                <h1>Delivery</h1>
                                <hr />
                                <button onclick="getLocation()" type="button" id="btnLocation">
                                📍Get Your Current Position
                                </button>

                                <!--

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

                                -->

                                <asp:Literal ID="litDelivery" runat="server"></asp:Literal>

                            </div>
                            

                        </td>
                        <td>
                                &nbsp
                        </td>
                        <td style="width:45%" class="tableElement">
                            <div>
                                <h1>Pick Up</h1>
                                <hr />
                                <button onclick="getLocation()" type="button" id="btnLocation">
                                Find Our Stores Location
                                </button>
                                
                                <asp:Literal ID="litPickUp" runat="server"></asp:Literal>

                            </div>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:center;height:0">
                            <button id="btnCont" type="submit">Proceed</button>
                        </td>
                    </tr>
                    
                </table>

            <div id="overlay"></div>
            <div id="popupDialog" class="popupDialog">
                <div id="map" style="height: 100%"></div>
                <button onclick="closeFn()" type="button" id="btnClose">Close</button>
            </div>
        </div>
    </div>

</asp:Content>
