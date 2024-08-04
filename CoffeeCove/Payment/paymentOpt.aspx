<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="paymentOpt.aspx.cs" Inherits="CoffeeCove.Payment.paymentOpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/paymentOpt.css" rel="stylesheet" />
    <!-- Banner -->
    <div id="poster">
        <img src="../img/coffeeBag.jpg" id="posterImg" />
        <div id="posterText">Payment</div>
    </div>
    <br />
    <!--Element-->
    <div id="container">
        <span>Cash</span>
        <br />
        <hr />
        <span>Card</span>
        <div id="cardContainer">
            <div class="cardDetail">
                <div>
                    <p>Card Holder Name</p>
                    <asp:TextBox ID="txtName" runat="server" Width="90%" CssClass="bankForm" PlaceHolder="David Lee"></asp:TextBox>
                </div>
            </div>
            <div class="cardDetail">
                <div>
                    <p>Card Number</p>
                    <asp:TextBox ID="txtCardNo" runat="server" Width="90%" CssClass="bankForm" PlaceHolder="xxxx xxxx xxxx xxxx"></asp:TextBox>
                </div>
            </div>
            <div class="cardDetail">
                <div style="display:flex">
                    <div>
                        <p>Expiry Date</p>
                        <asp:TextBox ID="txtExpiry" runat="server" Width="80%"  CssClass="bankForm" PlaceHolder="MM/YYYY"></asp:TextBox>
                    </div>
                    <div>
                        <p>CVV/CVC</p>
                        <asp:TextBox ID="txtCVV" runat="server" Width="80%" CssClass="bankForm" PlaceHolder="123"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div>
                <div class="btn">
                    <asp:LinkButton ID="lbCard" runat="server" Font-Underline="False" ForeColor="Black">
                        <br />
                        <span>
                            Pay RM<asp:Literal ID="litAmt" runat="server"></asp:Literal>
                        </span>
                        <span id="proceed">
                            >
                        </span>
                        

                    </asp:LinkButton>
                </div>
            </div>


        </div>
    </div>



</asp:Content>
