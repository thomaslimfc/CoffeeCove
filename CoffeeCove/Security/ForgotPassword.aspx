<%@ Page Title="Forgot Password" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" 
    Inherits="CoffeeCove.Security.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br /><br /><br /><br /><br />

<div id="forgotPassword_container" class="sign_container">
    <table>
        <tr>
            <td id="forgotPassword_td" class="specialPadding25">
                <h2>Forgot Password</h2>
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
                                <asp:TextBox ID="usernameEmail" CssClass="usernameEmail" 
                                    runat="server" placeholder="Username / Email" 
                                    title="usernameEmail" 
                                    OnTextChanged="UsernameEmail_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="usernameEmail_rqdValidator" runat="server" 
                                    ControlToValidate="usernameEmail" 
                                    ErrorMessage="Username or Email is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
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
                <asp:Button ID="next_btn" runat="server" Text="Next"
                    OnClick="NextBtn_Click" CssClass="next_btn"
                    CausesValidation="true"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="back_btn" runat="server" Text="Back"
                    OnClick="BackBtn_Click" CssClass="back_btn"
                    CausesValidation="false"/>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
