<%@ Page Title="Sign Up" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" 
    Inherits="CoffeeCove.Security.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<!--- reCAPTCHA -->
<script src="https://www.google.com/recaptcha/api.js" async defer></script>
<br /><br /><br /><br /><br /><br /><br /><br />
<div id="signIn_container" class="signUp_container">
    <table>
        <tr colspan="2">
            <td id="signUp_td">
                <h2 style="font-size: 24px; font-weight: bold">
                    Sign Up</h2>
            </td>
        </tr>
        <tr>
            <!--  First Name  -->
            <td>
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="usernameIcon" src="../img/username_icon.png" 
                                    alt="Enter your first name" />
                            </td>
                            <td>
                                <asp:TextBox ID="FirstName_SU" CssClass="securityInput" 
                                    runat="server" placeholder="First Name" 
                                    title="First Name"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="FirstName_SU_rqdValidator" runat="server" 
                                    ControlToValidate="FirstName_SU" 
                                    ErrorMessage="Name is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="FirstName_SU_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="FirstName_SU" 
                                    ErrorMessage="Must contain letters only." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^[a-zA-Z]{1,30}$" />
                                <asp:Label ID="NameErrorMessage" 
                                    runat="server" ForeColor="Red" Visible="False">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <!--  Last Name  -->
            <td>
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="usernameIcon" src="../img/username_icon.png" 
                                    alt="Enter your first name" />
                            </td>
                            <td>
                                <asp:TextBox ID="LastName_SU" CssClass="securityInput" 
                                    runat="server" placeholder="Last Name" 
                                    title="Name"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="LastName_SU_rqdValidator" runat="server" 
                                    ControlToValidate="LastName_SU" 
                                    ErrorMessage="Last Name is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="LastName_SU_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="LastName_SU" 
                                    ErrorMessage="Must contain letters only." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^[a-zA-Z]{1,30}$" />
                                <asp:Label ID="Label1" 
                                    runat="server" ForeColor="Red" Visible="False">
                                </asp:Label>
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
            <!--  Username  -->
            <td>
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="usernameIcon" src="../img/username_icon.png" 
                                    alt="Enter your username" />
                            </td>
                            <td>
                                <asp:TextBox ID="Username_SU" CssClass="securityInput" 
                                    runat="server" placeholder="Username" 
                                    title="Username"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="Username_SU_rqdValidator" runat="server" 
                                    ControlToValidate="Username_SU" 
                                    ErrorMessage="Username is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="Username_SU_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="Username_SU" 
                                    ErrorMessage="Must contain >10 letters and numbers only." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^[a-zA-Z0-9]{10,}$" />
                                <asp:Label ID="UsernameErrorMessage" 
                                    runat="server" ForeColor="Red" Visible="False">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <!--  Email Address  -->
            <td id="emailAdd_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="mailIcon" src="../img/mail_icon.png" 
                                    alt="Enter your email" />
                            </td>
                            <td>
                                <asp:TextBox ID="EmailAdd_SU" CssClass="securityInput" 
                                    runat="server" placeholder="Email Address" 
                                    title="Email Address"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="EmailAdd_SU_rqdValidator" runat="server" 
                                    ControlToValidate="EmailAdd_SU" 
                                    ErrorMessage="Email Address is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="EmailAdd_SU_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="EmailAdd_SU" 
                                    ErrorMessage="Invalid email format." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
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
            <!--  Password  -->
            <td id="password_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="passwordIcon" src="../img/lock_icon.png" 
                                    alt="Enter your password" />
                            </td>
                            <td>
                                <asp:TextBox ID="Password_SU" CssClass="securityInput" 
                                    runat="server" placeholder="Password" 
                                    title="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="Password_SU_rqdValidator" runat="server" 
                                    ControlToValidate="Password_SU" 
                                    ErrorMessage="Password is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:RegularExpressionValidator 
                                    ID="Password_SU_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="Password_SU" 
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
            <!--  Password Re-enter  -->
            <td id="passReenter_td">
                <div class="input_div">
                    <table>
                        <tr>
                            <td>
                                <img class="passwordIcon" src="../img/lock_icon.png" 
                                    alt="Re-enter your password" />
                            </td>
                            <td>
                                <asp:TextBox ID="PasswordReenter_SU" CssClass="securityInput" 
                                    runat="server" placeholder="Re-enter Password" 
                                    title="Re-enter Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textAlignLeft" colspan="2">
                                <asp:RequiredFieldValidator 
                                    ID="PasswordReenter_SU_rqdValidator" runat="server" 
                                    ControlToValidate="PasswordReenter_SU" 
                                    ErrorMessage="Password Re-enter is required." 
                                    Display="Dynamic" ForeColor="Red" 
                                    CssClass="rqdValidator" />
                                <asp:CompareValidator 
                                    ID="PasswordReenter_SU_compareValidator" 
                                    runat="server" 
                                    ControlToValidate="PasswordReenter_SU" 
                                    ControlToCompare="Password_SU" 
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
            <!--  Date of Birth  -->
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
            <!--  Gender  -->
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
                                    ControlToValidate="gender" 
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
        <tr colspan="2">
            <!--  Location  -->
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
        <tr colspan="2">
            <td class="trMarginBottom20"></td>
        </tr>
        <tr colspan="2">
            <td id="agreement_td">
                By signing up, you agree to the Staffee
                <a href="UserAgreement.aspx">User Agreement</a>, and
                <a href="PrivacyPolicy.aspx">Privacy Policy</a>.
            </td>
        </tr>
        <tr>
            <td class="trMarginBottom20"></td>
        </tr>
        <tr colspan="4">
            <td>
                <center>
                    <div style="width: 198px; height: 40px; 
                        transform: scale(0.65); transform-origin: 0 0">
                        <div class="g-recaptcha" 
                            data-sitekey="6LdzASAqAAAAAHhQdlfCCZOOzfx17iEXeR-140zQ">
                        </div>
                    </div>
                </center>
            </td>
        </tr>
        <tr>
            <td class="trMarginBottom20"></td>
        </tr>
        <tr colspan="2">
            <td id="signUpBtn_td">
                <asp:Button ID="SignUpBtn_SU" runat="server" Text="Agree & Join"
                            CssClass="securityPrimaryBtn" OnClick="SignUpBtn_SU_Click"/>
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
