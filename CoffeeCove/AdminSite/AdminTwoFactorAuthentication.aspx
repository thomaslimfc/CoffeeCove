<%@ Page Title="2-Factor Verification (Admin)" Language="C#" 
    MasterPageFile="~/Master/Admin.Master" 
    AutoEventWireup="true" CodeBehind="AdminTwoFactorAuthentication.aspx.cs" 
    Inherits="CoffeeCove.AdminSite.AdminTwoFactorAuthentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br />
<div id="signIn_container" class="sign_container">
    <table>
        <tr>
            <td id="signIn_td">
                <h2 style="font-size: 24px; font-weight: bold">
                    Two-Factor Authentication</h2>
            </td>
        </tr>
        <tr>
            <td>
                <p>Enter the 6 digit code generated 
                    <br />in your mailbox.</p>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="adminOtp" CssClass="otp" 
                    runat="server"
                    placeholder="OTP code" title="OTP code" 
                    OnTextChanged="adminOtp_TextChanged" 
                    AutoPostBack="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="textAlignLeft">
                <asp:RequiredFieldValidator 
                    ID="adminOtp_rqdValidator" 
                    runat="server" 
                    ControlToValidate="adminOtp" 
                    ErrorMessage="OTP is required." 
                    Display="Dynamic" 
                    ForeColor="Red" 
                    CssClass="rqdValidator" />

                <asp:RegularExpressionValidator 
                    ID="adminOtp_regexValidator" 
                    runat="server" 
                    ControlToValidate="adminOtp" 
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
        <a href="AdminForgotPassword.aspx"
            style="font-size: medium; text-decoration: underline; color: #551a8b">
            Try me again</a>
    </p>
</center>
</asp:Content>
