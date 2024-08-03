<%@ Page Title="Forgot Password (Admin)" Language="C#" 
    MasterPageFile="../Master/Admin.Master" 
    AutoEventWireup="true" CodeBehind="AdminForgotPassword.aspx.cs" 
    Inherits="CoffeeCove.AdminSite.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br />

<div id="forgotPassword_container" class="sign_container">
    <table>
        <tr>
            <td id="forgotPassword_td" class="specialPadding25">
                <h2 style="font-size: 24px; font-weight: bold">
                    Forgot Password</h2>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input_div specialMargin25">
                    <table>
                        <tr>
                            <td>
                                <img class="usernameIcon" src="../img/username_icon.png" 
                                    alt="Enter your username or email" />
                            </td>
                            <td>
                                <asp:TextBox ID="adminUsernameEmail" CssClass="adminUsernameEmail" 
                                    runat="server" placeholder="Username / Email" 
                                    title="usernameEmail" 
                                    OnTextChanged="AdminUsernameEmail_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="adminUsernameEmail_rqdValidator" runat="server" 
                                    ControlToValidate="adminUsernameEmail" 
                                    ErrorMessage="Username is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="usernameEmail_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="adminUsernameEmail" 
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
            <td id="codeVerification_td" class="specialPadding25">
                <br />
                We’ll send a verification code to this
                <br /> 
                email or phone number if it matches
                <br /> 
                an existing Staffee account.
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="adminNext_btn" runat="server" Text="Next"
                    OnClick="AdminNextBtn_Click" CssClass="next_btn"
                    CausesValidation="true"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="back_btn" runat="server" Text="Back"
                    OnClick="AdminBackBtn_Click" CssClass="back_btn"
                    CausesValidation="false"/>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
