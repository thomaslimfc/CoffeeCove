<%@ Page Title="Sign In" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" 
    Inherits="CoffeeCove.Security.SignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div id="signIn_container" class="sign_container">
        <table>
            <tr>
                <td id="signIn_td">
                    <h2>Sign In</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="usernameIcon" src="../images/username_icon.png" alt="Enter your username" />
                                </td>
                                <td>
                                    <asp:TextBox ID="username2" CssClass="username2" runat="server"
                                        placeholder="Username" title="Username" OnTextChanged="Username_TextChanged2" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator 
                                        ID="username_rqdValidator2" runat="server" 
                                        ControlToValidate="username2" 
                                        ErrorMessage="Username is required." 
                                        Display="Dynamic" ForeColor="Red" CssClass="username2_rqdValidator" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="password_td">
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="passwordIcon" src="../images/lock_icon.png" alt="Enter your password" />
                                </td>
                                <td>
                                    <asp:TextBox ID="password2" CssClass="password2" runat="server"
                                        placeholder="Password" title="Password" TextMode="Password" OnTextChanged="Password2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator 
                                        ID="password_rqdValidator2" runat="server" 
                                        ControlToValidate="password2" 
                                        ErrorMessage="Password is required." 
                                        Display="Dynamic" ForeColor="Red" CssClass="password2_rqdValidator" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="forgotPassword_td">
                    <asp:HyperLink ID="forgotPasswordLink" runat="server" NavigateUrl="ForgotPassword.aspx">
                        Forgot password?
                    </asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td id="signInBtn_td">
                    <asp:Button ID="signIn_btn" runat="server" Text="Sign In"
                                CssClass="signIn_btn"/>
                </td>
            </tr>
        </table>
    </div>

    <center>
        <p id="joinUsNow">
            New to Staffee?
            <a href="SignUp.aspx">Join now</a>
        </p>
    </center>
</asp:Content>
