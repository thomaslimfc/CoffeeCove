<%@ Page Title="" Language="C#" 
    MasterPageFile="~/Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="TwoFactorAuthentication.aspx.cs" 
    Inherits="CoffeeCove.Security.TwoFactorAuthentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br /><br /><br /><br /><br />
<div id="signIn_container" class="sign_container">
    <table>
        <tr>
            <td id="signIn_td">
                <h2>Two-Factor Authentication</h2>
            </td>
        </tr>
        <tr>
            <td>
                <p>Enter the 6 digit code generated in your mailbox.</p>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="otp" CssClass="otp" 
                    runat="server"
                    placeholder="OTP code" title="OTP code" 
                    OnTextChanged="otp_TextChanged" 
                    AutoPostBack="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="textAlignLeft">
                <asp:RequiredFieldValidator 
                    ID="otp_rqdValidator" 
                    runat="server" 
                    ControlToValidate="otp" 
                    ErrorMessage="OTP is required." 
                    Display="Dynamic" 
                    ForeColor="Red" 
                    CssClass="rqdValidator" />

                <asp:RegularExpressionValidator 
                    ID="otp_regexValidator" 
                    runat="server" 
                    ControlToValidate="otp" 
                    ErrorMessage="Must contain exactly 6 digits." 
                    Display="Dynamic" 
                    ForeColor="Red" 
                    CssClass="rqdValidator" 
                    ValidationExpression="^\d{6}$" />
            </td>
        </tr>
        <tr>
            <td id="verifyBtn_td">
                <asp:Button ID="verify_btn" runat="server" Text="Verify" 
                    CssClass="signIn_btn"/>
            </td>
        </tr>
    </table>
</div>
<center>
    <p id="joinUsNow">
        Having Trouble?
        <a href="SignUp.aspx">Try me again</a>
    </p>
</center>
</asp:Content>
