<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CoffeeCove.Security.WebForm1" %>

<asp:Content ID="ForgotPasswordHolder" ContentPlaceHolderID="ForgotPasswordHolder" runat="server">
    <div id="signIn_container" class="sign_container">
        <table>
            <tr>
                <td id="signIn_td">
                    <h2>Forgot Password</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="usernameIcon" src="../images/username_icon.png" alt="Enter your username or email" />
                                </td>
                                <td>
                                    <asp:TextBox ID="usernameEmail" CssClass="usernameEmail" runat="server"
                                        placeholder="Username / Email" title="usernameEmail" OnTextChanged="UsernameEmail_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator 
                                        ID="usernameEmail_rqdValidator" runat="server" 
                                        ControlToValidate="usernameEmail" 
                                        ErrorMessage="Username or Email is required." 
                                        Display="Dynamic" ForeColor="Red" CssClass="usernameEmail_rqdValidator" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="codeVerification_td">
                    <br />
                    We’ll send a verification code to this 
                    <br />email or phone number if it matches 
                    <br /> an existing Staffee account.
                </td>
            </tr>
            <tr>
                <td id="nextBtn_td">
                    <asp:Button ID="next_btn" runat="server" Text="Next"
                        OnClick="NextBtn_Click" CssClass="next_btn"
                        CausesValidation="true"/>
                </td>
            </tr>
            <tr>
                <td id="backBtn_td">
                    <asp:Button ID="back_btn" runat="server" Text="Back"
                        OnClick="BackBtn_Click" CssClass="back_btn"
                        CausesValidation="false"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
