<%@ Page Title="Sign Up" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" 
    Inherits="CoffeeCove.Security.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/Security.css" rel="stylesheet" />
    <br /><br /><br /><br /><br /><br /><br /><br />
    <div id="signIn_container" class="sign_container">
        <table>
            <tr>
                <td id="signUp_td">
                    <h2>Sign Up</h2>
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
                                    <asp:TextBox ID="username" CssClass="username" 
                                        runat="server" placeholder="Username" 
                                        title="Username" 
                                        OnTextChanged="Username_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <!-- <asp:RequiredFieldValidator 
                                        ID="username_rqdValidator" runat="server" 
                                        ControlToValidate="username" 
                                        ErrorMessage="Username is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="username_rqdValidator" /> -->
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="emailAdd_td">
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="mailIcon" src="../img/mail_icon.png" 
                                        alt="Enter your email" />
                                </td>
                                <td>
                                    <asp:TextBox ID="emailAdd" CssClass="emailAdd" 
                                        runat="server" placeholder="Email Address" 
                                        title="Email Address" 
                                        OnTextChanged="EmailAdd_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <!-- <asp:RequiredFieldValidator 
                                        ID="emailAdd_rqdValidator" runat="server" 
                                        ControlToValidate="emailAdd" 
                                        ErrorMessage="Email Address is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="emailAdd_rqdValidator" /> -->
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
                                    <img class="passwordIcon" src="../img/lock_icon.png" 
                                        alt="Enter your password" />
                                </td>
                                <td>
                                    <asp:TextBox ID="password" CssClass="password" 
                                        runat="server" placeholder="Password" 
                                        title="Password" 
                                        OnTextChanged="Password_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <!-- <asp:RequiredFieldValidator 
                                        ID="password_rqdValidator" runat="server" 
                                        ControlToValidate="password" 
                                        ErrorMessage="Password is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="password_rqdValidator" /> -->
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
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
                                    <asp:TextBox ID="passReenter" CssClass="passReenter" 
                                        runat="server" placeholder="Re-enter Password" 
                                        title="Re-enter Password" 
                                        OnTextChanged="PassReenter_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <!-- <asp:RequiredFieldValidator 
                                        ID="passReenter_rqdValidator" runat="server" 
                                        ControlToValidate="passReenter" 
                                        ErrorMessage="Password Re-enter is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="passReenter_rqdValidator" /> -->
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="birthdayDate_td">
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="cakeIcon" src="../img/cake_icon.png" 
                                        alt="Enter your birthday date" />
                                </td>
                                <td>
                                    <input type="date" id="birthdayDate_cal" 
                                        name="date" required>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
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
                                    <asp:RadioButtonList ID="gender" runat="server"
                                                RepeatDirection="Horizontal"
                                                CssClass="gender_rbl">
                                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <!-- <asp:RequiredFieldValidator 
                                        ID="gender_rqdValidator" 
                                        runat="server" 
                                        ControlToValidate="gender" 
                                        InitialValue="" 
                                        ErrorMessage="Please select your gender." 
                                        Display="Dynamic"
                                        ForeColor="Red" /> -->
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="location_td">
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="locationIcon" src="../img/location_icon.png" 
                                        alt="Enter your location" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="location" runat="server" 
                                        CssClass="location_ddl">
                                        <asp:ListItem Text="~ Select a location ~" Value="" />
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
                                <td>
                                    <!-- <asp:RequiredFieldValidator 
                                        ID="location_rqdValidator" 
                                        runat="server" 
                                        ControlToValidate="location" 
                                        InitialValue="" 
                                        ErrorMessage="Please select a location." 
                                        Display="Dynamic"
                                        ForeColor="Red" /> -->
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>

            <tr>
                <td id="agreement_td">
                    By signing up, you agree to the Staffee
                    <a href="#">User Agreement</a>, and
                    <a href="#">Privacy Policy</a>.
                </td>
            </tr>
            <tr>
                <td id="signUpBtn_td">
                    <asp:Button ID="signUp_btn" runat="server" Text="Agree & Join"
                                CssClass="signUp_btn"/>
                </td>
            </tr>
        </table>        
    </div>

    <center>
        <p id="alreadySignedUp">
            Already on Staffee?
            <a href="SignIn.aspx">Sign in</a>
        </p>
    </center>
</asp:Content>
