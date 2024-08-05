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
                    <h2 style="font-size: 24px; font-weight: bold">
                        Reset Password</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="passwordIcon" src="../img/lock_icon.png" 
                                        alt="Enter your new password" />
                                </td>
                                <td>
                                    <asp:TextBox ID="Password_PR" CssClass="securityInput" 
                                        runat="server" placeholder="New Password" 
                                        title="New Password" TextMode="Password" 
                                        AutoPostBack="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textAlignLeft" colspan="2">
                                    <asp:RequiredFieldValidator 
                                        ID="Password_PR_rqdValidator" runat="server" 
                                        ControlToValidate="Password_PR" 
                                        ErrorMessage="Password is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="rqdValidator" />
                                    <asp:RegularExpressionValidator 
                                        ID="Password_PR_regexValidator" 
                                        runat="server" 
                                        ControlToValidate="Password_PR" 
                                        ErrorMessage="Must contain >10 letters, numbers, and symbols." 
                                        Display="Dynamic" 
                                        ForeColor="Red" 
                                        CssClass="rqdValidator" 
                                        ValidationExpression="^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{10,}$" />
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
                                    <asp:TextBox ID="PasswordConfirm_PR" CssClass="securityInput" 
                                        runat="server" placeholder="Confirm New Password" 
                                        title="Confirm New Password" TextMode="Password"
                                        AutoPostBack="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textAlignLeft" colspan="2">
                                    <asp:RequiredFieldValidator 
                                        ID="PasswordConfirm_PR_rqdValidator" runat="server" 
                                        ControlToValidate="PasswordConfirm_PR" 
                                        ErrorMessage="Password Confirmation is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="rqdValidator" />
                                    <asp:CompareValidator 
                                        ID="PasswordConfirm_PR_compareValidator" 
                                        runat="server" 
                                        ControlToValidate="PasswordConfirm_PR" 
                                        ControlToCompare="Password_PR" 
                                        ErrorMessage="Passwords do not match." 
                                        Display="Dynamic" 
                                        ForeColor="Red" 
                                        CssClass="rqdValidator" />
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
                    <asp:Button ID="ResetPasswordBtn_PR" runat="server" Text="Reset Password"
                        CssClass="securityPrimaryBtn"
                        CausesValidation="true" OnClick="ResetPasswordBtn_PR_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="errorMessageLabel" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
