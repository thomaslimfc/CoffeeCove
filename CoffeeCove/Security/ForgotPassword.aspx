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
                                <asp:TextBox ID="UsernameEmail_FP" CssClass="securityInput" 
                                    runat="server" placeholder="Username / Email" 
                                    title="usernameEmail"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="UsernameEmail_FP_rqdValidator" runat="server" 
                                    ControlToValidate="UsernameEmail_FP" 
                                    ErrorMessage="Username is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="UsernameEmail_FP_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="UsernameEmail_FP" 
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
                <asp:Button ID="NextBtn_FP" runat="server" Text="Next"
                    CssClass="securityPrimaryBtn"
                    CausesValidation="true" OnClick="NextBtn_FP_Click"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BackBtn_FP" runat="server" Text="Back"
                    CssClass="securitySecondaryBtn"
                    CausesValidation="false" OnClick="BackBtn_FP_Click"/>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
