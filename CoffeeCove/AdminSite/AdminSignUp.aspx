<%@ Page Title="Sign Up (Admin)" Language="C#" 
    MasterPageFile="../Master/Admin.Master" 
    AutoEventWireup="true" CodeBehind="AdminSignUp.aspx.cs" 
    Inherits="CoffeeCove.AdminSite.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<br /><br /><br /><br />
<div id="signIn_container" class="sign_container">
    <table>
        <tr>
            <td id="signUp_td">
                <h2 style="font-size: 24px; font-weight: bold">
                    Admin Registration</h2>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="usernameIcon" src="../img/username_icon.png" 
                                    alt="Enter your username" />
                            </td>
                            <td>
                                <asp:TextBox ID="adminUsername" CssClass="adminUsername" 
                                    runat="server" placeholder="Username" 
                                    title="Username" 
                                    OnTextChanged="AdminUsername_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="adminUsername_rqdValidator" runat="server" 
                                    ControlToValidate="adminUsername" 
                                    ErrorMessage="Username is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="adminUsername_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="adminUsername" 
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
                                <asp:TextBox ID="adminPassword" CssClass="adminPassword" 
                                    runat="server" placeholder="Password" 
                                    title="Password" 
                                    OnTextChanged="AdminPassword_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator
                                    ID="adminPassword_rqdValidator" runat="server" 
                                    ControlToValidate="adminPassword" 
                                    ErrorMessage="Password is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="adminPassword_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="adminPassword" 
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
            <td id="passReenter_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="passwordIcon" src="../img/lock_icon.png" 
                                    alt="Re-enter your password" />
                            </td>
                            <td>
                                <asp:TextBox ID="adminPassReenter" CssClass="adminPassReenter" 
                                    runat="server" placeholder="Re-enter Password" 
                                    title="Re-enter Password" 
                                    OnTextChanged="AdminPassReenter_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="adminPassReenter_rqdValidator" runat="server" 
                                    ControlToValidate="adminPassReenter" 
                                    ErrorMessage="Password Re-enter is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:CompareValidator 
                                    ID="reenterAdminPassword_compareValidator" 
                                    runat="server" 
                                    ControlToValidate="adminPassReenter" 
                                    ControlToCompare="adminPassword" 
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
            <td id="gender_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="genderIcon" src="../img/gender_icon.png" 
                                    alt="Enter your gender" />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="adminGender" runat="server"
                                            RepeatDirection="Horizontal"
                                            CssClass="gender_rbl">
                                    <asp:ListItem Text="Male" Value="Male" style="padding-left: 5px"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female" style="padding-left: 15px"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="gender_rqdValidator" 
                                    runat="server" 
                                    ControlToValidate="adminGender" 
                                    InitialValue="" 
                                    ErrorMessage="Please select your gender." 
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
            <td id="location_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="locationIcon" src="../img/location_icon.png" 
                                    alt="Enter your branch" />
                            </td>
                            <td>
                                <asp:DropDownList ID="location" runat="server" 
                                    CssClass="location_ddl">
                                    <asp:ListItem Text="~ Select a branch ~" Value="" />
                                    <asp:ListItem>Selangor</asp:ListItem>
                                    <asp:ListItem>Penang</asp:ListItem>
                                    <asp:ListItem>Johor</asp:ListItem>
                                    <asp:ListItem>Malacca</asp:ListItem>
                                    <asp:ListItem>Negeri Sembilan</asp:ListItem>
                                    <asp:ListItem>Pahang</asp:ListItem>
                                    <asp:ListItem>Perak</asp:ListItem>
                                    <asp:ListItem>Kedah</asp:ListItem>
                                    <asp:ListItem>Kelantan</asp:ListItem>
                                    <asp:ListItem>Terengganu</asp:ListItem>
                                    <asp:ListItem>Perlis</asp:ListItem>
                                    <asp:ListItem>Sarawak</asp:ListItem>
                                    <asp:ListItem>Sabah</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="location_rqdValidator" 
                                    runat="server" 
                                    ControlToValidate="location" 
                                    InitialValue="" 
                                    ErrorMessage="Please select a location." 
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
            <td id="agreement_td" style="text-align: justify">
                By registering, you agree to the CoffeeCove's Code of Conduct for administrators and all relevant laws and regulation governing the use of this platform.
            </td>
        </tr>
        <tr>
            <td id="signUpBtn_td">
                <asp:Button ID="signUp_btn" runat="server" Text="Register Now"
                            CssClass="signUp_btn" OnClick="SignUp_btn_Click"/>
            </td>
        </tr>
    </table>        
</div>
<center>
    <p id="alreadySignedUp">
        Already on CoffeeCove?
        <a id="alreadyRegistered" class="aReset" href="AdminSignIn.aspx"
            style="font-size: medium; text-decoration: underline; color: #551a8b">
            Login</a>
    </p>
</center>
</asp:Content>
