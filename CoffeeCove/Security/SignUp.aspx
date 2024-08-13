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
    </table
</div>
<center>
    <p id="alreadySignedUp">
        Already on Staffee?
        <a href="SignIn.aspx">Sign in</a>
    </p>
</center>


    <html>
  <head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
		<script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init></script>
		<script type="text/javascript">
			window.tailwind.config = {
				darkMode: ['class'],
				theme: {
					extend: {
						colors: {
							border: 'hsl(var(--border))',
							input: 'hsl(var(--input))',
							ring: 'hsl(var(--ring))',
							background: 'hsl(var(--background))',
							foreground: 'hsl(var(--foreground))',
							primary: {
								DEFAULT: 'hsl(var(--primary))',
								foreground: 'hsl(var(--primary-foreground))'
							},
							secondary: {
								DEFAULT: 'hsl(var(--secondary))',
								foreground: 'hsl(var(--secondary-foreground))'
							},
							destructive: {
								DEFAULT: 'hsl(var(--destructive))',
								foreground: 'hsl(var(--destructive-foreground))'
							},
							muted: {
								DEFAULT: 'hsl(var(--muted))',
								foreground: 'hsl(var(--muted-foreground))'
							},
							accent: {
								DEFAULT: 'hsl(var(--accent))',
								foreground: 'hsl(var(--accent-foreground))'
							},
							popover: {
								DEFAULT: 'hsl(var(--popover))',
								foreground: 'hsl(var(--popover-foreground))'
							},
							card: {
								DEFAULT: 'hsl(var(--card))',
								foreground: 'hsl(var(--card-foreground))'
							},
						},
					}
				}
			}
        </script>
		<style type="text/tailwindcss">
			@layer base {
				:root {
					--background: 0 0% 100%;
--foreground: 240 10% 3.9%;
--card: 0 0% 100%;
--card-foreground: 240 10% 3.9%;
--popover: 0 0% 100%;
--popover-foreground: 240 10% 3.9%;
--primary: 240 5.9% 10%;
--primary-foreground: 0 0% 98%;
--secondary: 240 4.8% 95.9%;
--secondary-foreground: 240 5.9% 10%;
--muted: 240 4.8% 95.9%;
--muted-foreground: 240 3.8% 46.1%;
--accent: 240 4.8% 95.9%;
--accent-foreground: 240 5.9% 10%;
--destructive: 0 84.2% 60.2%;
--destructive-foreground: 0 0% 98%;
--border: 240 5.9% 90%;
--input: 240 5.9% 90%;
--ring: 240 5.9% 10%;
--radius: 0.5rem;
				}
				.dark {
					--background: 240 10% 3.9%;
--foreground: 0 0% 98%;
--card: 240 10% 3.9%;
--card-foreground: 0 0% 98%;
--popover: 240 10% 3.9%;
--popover-foreground: 0 0% 98%;
--primary: 0 0% 98%;
--primary-foreground: 240 5.9% 10%;
--secondary: 240 3.7% 15.9%;
--secondary-foreground: 0 0% 98%;
--muted: 240 3.7% 15.9%;
--muted-foreground: 240 5% 64.9%;
--accent: 240 3.7% 15.9%;
--accent-foreground: 0 0% 98%;
--destructive: 0 62.8% 30.6%;
--destructive-foreground: 0 0% 98%;
--border: 240 3.7% 15.9%;
--input: 240 3.7% 15.9%;
--ring: 240 4.9% 83.9%;
				}
			}
		</style>
  </head>
  <body>
    

<div class="max-w-md mx-auto p-6 bg-card rounded-lg shadow-md">
  <h2 class="text-2xl font-bold text-foreground mb-4">Log In</h2>
  <div class="mb-4">
    <input type="text" placeholder="01/27" class="w-full p-2 border border-border rounded-md focus:outline-none focus:ring focus:ring-ring" />
  </div>
  <div class="mb-4">
    <input type="password" placeholder="•••" class="w-full p-2 border border-border rounded-md focus:outline-none focus:ring focus:ring-ring" />
  </div>
  <button class="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80">LOG IN</button>
  <div class="flex justify-between mt-2">
    <a href="#" class="text-muted-foreground text-sm">Forgot Password</a>
    <a href="#" class="text-muted-foreground text-sm">Log In with Phone Number</a>
  </div>
  <div class="my-4 text-center text-muted-foreground">OR</div>
  <div class="flex justify-between">
    <button class="flex-1 bg-muted text-muted-foreground p-2 rounded-md mr-2 hover:bg-muted/80">
      <img aria-hidden="true" alt="Facebook Logo" src="https://openui.fly.dev/openui/facebook.svg?text=Facebook" />
    </button>
    <button class="flex-1 bg-muted text-muted-foreground p-2 rounded-md ml-2 hover:bg-muted/80">
      <img aria-hidden="true" alt="Google Logo" src="https://openui.fly.dev/openui/google.svg?text=Google" />
    </button>
  </div>
  <div class="text-center mt-4">
    <span class="text-muted-foreground">New to Shopee? </span>
    <a href="#" class="text-primary">Sign Up</a>
  </div>
  <button class="mt-4 bg-secondary text-secondary-foreground p-2 rounded-md hover:bg-secondary/80">Log in with QR</button>
</div>


  </body>
</html>
</asp:Content>
