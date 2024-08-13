<%@ Page Title="Sign In" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" 
    Inherits="CoffeeCove.Security.SignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br /><br /><br /><br /><br />
<div id="signIn_container" class="sign_container">
    <table>
        <tr>
            <td id="signIn_td">
                <h2 style="font-size: 24px; font-weight: bold">
                    Sign In</h2>
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
                                <asp:TextBox ID="Username_SI" CssClass="securityInput" 
                                    runat="server"
                                    placeholder="Username / Email Address" title="Username / Email Address"
                                    AutoPostBack="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft"  colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="Username_SI_rqdValidator" 
                                    runat="server" 
                                    ControlToValidate="Username_SI" 
                                    ErrorMessage="Username or Email is required." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                
                                <asp:CustomValidator 
                                    ID="Username_SI_CustomValidator" 
                                    runat="server" 
                                    ControlToValidate="Username_SI" 
                                    ErrorMessage="Invalid Username or Email." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator"
                                    OnServerValidate="UsernameOrEmailValidator_ServerValidate" />
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
                                <asp:TextBox ID="Password_SI" CssClass="securityInput" 
                                    runat="server" placeholder="Password" 
                                    title="Password" TextMode="Password"
                                    AutoPostBack="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="Password_SI_rqdValidator" runat="server" 
                                    ControlToValidate="Password_SI" 
                                    ErrorMessage="Password is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="Password_SI_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="Password_SI" 
                                    ErrorMessage="Must >10 lower + uppercases, numbers & symbols." 
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
            <td id="forgotPassword_td">
                <asp:HyperLink ID="forgotPasswordLink" runat="server" 
                    NavigateUrl="ForgotPassword.aspx">
                    Forgot password?
                </asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td id="signInBtn_td">
                <asp:Button ID="SignInButton_SI" runat="server" Text="Sign In" 
                    CssClass="securityPrimaryBtn"/>
            </td>
        </tr>
    </table>
</div>
<center>
    <p id="joinUsNow">
        New to CoffeeCove?
        <a href="SignUp.aspx">Join now</a>
    </p>
</center>
</asp:Content>
