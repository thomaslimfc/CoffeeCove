<!--<%@ Page Title="Sign Up (Admin)" Language="C#" 
    MasterPageFile="~/Master/Admin.Master" 
    AutoEventWireup="true" CodeBehind="AdminSignIn.aspx.cs" 
    Inherits="CoffeeCove.AdminSite.SignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br />
<div id="signIn_container" class="sign_container">
    <table>
        <tr>
            <td id="signIn_td">
                <h2 style="font-size: 24px; font-weight: bold">
                    Admin Login</h2>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="usernameIcon" src="../img/username_icon.png" alt="Enter your username" />
                            </td>
                            <td>
                                <asp:TextBox ID="adminUsername2" CssClass="adminUsername2" 
                                    runat="server"
                                    placeholder="Username" title="Username" 
                                    OnTextChanged="adminUsername2_TextChanged" 
                                    AutoPostBack="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft"  colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="adminUsername2_rqdValidator" runat="server" 
                                    ControlToValidate="adminUsername2" 
                                    ErrorMessage="Username is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="adminUsername2_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="adminUsername2" 
                                    ErrorMessage="Must contain >10 letters and numbers only." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^[a-zA-Z0-9]{10,}$" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="trMarginBottom20"></td>
        </tr>
        <tr>
            <td id="password_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="passwordIcon" src="../img/lock_icon.png" 
                                    alt="Enter your password" />
                            </td>
                            <td>
                                <asp:TextBox ID="adminPassword2" CssClass="adminPassword2" 
                                    runat="server" placeholder="Password" 
                                    title="Password" TextMode="Password" 
                                    OnTextChanged="adminPassword2_TextChanged" 
                                    AutoPostBack="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="adminPassword2_rqdValidator" runat="server" 
                                    ControlToValidate="adminPassword2" 
                                    ErrorMessage="Password is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="adminPassword2_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="adminPassword2" 
                                    ErrorMessage="Must contain >10 letters, numbers, and symbols." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).+$" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="trMarginBottom20"></td>
        </tr>
        <tr>
            <td id="forgotPassword_td">
                <asp:HyperLink ID="forgotPasswordLink" runat="server" 
                    NavigateUrl="AdminForgotPassword.aspx"
                    CssClass="adminForgotPassword" 
                    style="font-size: small; text-decoration: underline">
                    Forgot password?
                </asp:HyperLink>
            </td>
        </tr>

        <tr>
            <td id="signInBtn_td">
                <asp:Button ID="signIn_btn" runat="server" Text="Log now" 
                    CssClass="signIn_btn"/>
            </td>
        </tr>
    </table>
</div>
<center>
    <p id="joinUsNow">
        New to CoffeeCove?
        <a href="AdminSignUp.aspx"
            style="font-size: medium; text-decoration: underline; color: #551a8b">
            Register here</a>
    </p>
</center>
</asp:Content>
-->