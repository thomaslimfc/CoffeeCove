<%@ Page Title="Reset Password" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" 
    Inherits="CoffeeCove.Security.PasswordReset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br /><br /><br /><br /><br />

<div id="signIn_container" class="sign_container">
    <table>
        <tr>
            <td id="passwordReset_td">
                <h2>Reset Password</h2>
            </td>
        </tr>
        <tr>
            <td id="password_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="passwordIcon" src="../img/lock_icon.png" 
                                    alt="Enter your new password" />
                            </td>
                            <td>
                                <asp:TextBox ID="password3" CssClass="password" 
                                    runat="server" placeholder="New Password" 
                                    title="New Password" TextMode="Password" 
                                    OnTextChanged="Password3_TextChanged" 
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <!-- <asp:RequiredFieldValidator 
                                    ID="password_rqdValidator3" runat="server" 
                                    ControlToValidate="password3" 
                                    ErrorMessage="Password is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" /> -->
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
                                    alt="Confirm your new password" />
                            </td>
                            <td>
                                <asp:TextBox ID="passwordConfirm" CssClass="password" 
                                    runat="server" placeholder="Confirm New Password" 
                                    title="Confirm New Password" TextMode="Password" 
                                    OnTextChanged="PasswordConfirm_TextChanged" 
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                     <tr>
                            <td colspan="2">
                                <!-- <asp:RequiredFieldValidator 
                                    ID="passwordConfirm_rqdValidator" runat="server" 
                                    ControlToValidate="passwordConfirm" 
                                    ErrorMessage="Password is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" /> -->
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
            <td id="resetPassword_td">
                <asp:Button ID="resetPassword_btn" runat="server" Text="Reset Password"
                    OnClick="ResetPasswordBtn_Click" CssClass="resetPassword_btn"
                    CausesValidation="true"/>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
