<%@ Page Title="Sign Up" Language="C#" 
    MasterPageFile="../Master/Customer2.Master" 
    AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" 
    Inherits="CoffeeCove.Security.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
<script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init=""></script>
<br /><br /><br /><br />

<div class="max-w-md mx-auto p-6 bg-card rounded-lg shadow-md">
    <h2 class="text-2xl font-bold text-foreground mb-4">Sign Up</h2>

    <!-- First Name -->
    <div class="mb-4">
        <label for="first-name" class="block text-sm text-muted-foreground">First Name</label>
        <asp:TextBox ID="FirstName_SU"
            CssClass="mt-1 p-2 border border-border rounded w-full"
            runat="server" 
            placeholder="Desmund" 
            title="First Name"
            ValidationGroup="SignUp">
        </asp:TextBox>
        <asp:RequiredFieldValidator 
            ID="FirstName_SU_rqdValidator" 
            runat="server" 
            ControlToValidate="FirstName_SU" 
            ErrorMessage="First Name is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:RegularExpressionValidator 
            ID="FirstName_SU_regexValidator" 
            runat="server" 
            ControlToValidate="FirstName_SU" 
            ErrorMessage="Must contain letters only and no space." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^[a-zA-Z]{1,30}$" />
        <asp:Label ID="NameErrorMessage" 
            runat="server" 
            ForeColor="Red" 
            Visible="False">
        </asp:Label>
    </div>

    <!-- Last Name -->    
    <div class="mb-4">
        <label for="last-name" class="block text-sm text-muted-foreground">Last Name</label>
        <asp:TextBox ID="LastName_SU" 
            CssClass="mt-1 p-2 border border-border rounded w-full"
            runat="server" 
            placeholder="Chau" 
            title="Name"
            ValidationGroup="SignUp">
        </asp:TextBox>
        <asp:RequiredFieldValidator 
            ID="LastName_SU_rqdValidator" runat="server" 
            ControlToValidate="LastName_SU" 
            ErrorMessage="Last Name is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:RegularExpressionValidator 
            ID="LastName_SU_regexValidator" 
            runat="server" 
            ControlToValidate="LastName_SU" 
            ErrorMessage="Must contain letters only and no space." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^[a-zA-Z]{1,30}$" />
        <asp:Label ID="Label1" 
            runat="server" 
            ForeColor="Red" 
            Visible="False">
        </asp:Label>
    </div>
    
    <!-- Username -->
    <div class="mb-4">
        <label for="username" class="block text-sm text-muted-foreground">Username</label>
        <asp:TextBox ID="Username_SU" 
            CssClass="mt-1 p-2 border border-border rounded w-full"
            runat="server" 
            placeholder="desmundchau7668" 
            title="Username"
            ValidationGroup="SignUp"
            onblur="validateUsername()">
        </asp:TextBox>
        <asp:RequiredFieldValidator 
            ID="Username_SU_rqdValidator" runat="server" 
            ControlToValidate="Username_SU" 
            ErrorMessage="Username is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:RegularExpressionValidator 
            ID="Username_SU_regexValidator" 
            runat="server" 
            ControlToValidate="Username_SU" 
            ErrorMessage="Must contain >8 letters and numbers only." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^[a-zA-Z0-9]{8,}$" />
        <asp:CustomValidator 
            ID="Username_SU_customValidator" 
            runat="server" 
            ControlToValidate="Username_SU"
            OnServerValidate="Username_SU_ServerValidate"
            CssClass="error" 
            Display="Dynamic" 
            ErrorMessage="Your username has been used."
            ValidationGroup="SignUp">
        </asp:CustomValidator>
        <asp:Label ID="UsernameErrorMessage" 
            runat="server" ForeColor="Red" Visible="False">
        </asp:Label>
        <script type="text/javascript">
            function validateUsername() {
                // Triggers a postback when the user clicks outside the Username_SU textbox
                __doPostBack('<%= Username_SU.ClientID %>', '');
            }
        </script>

    </div>
    
    <!-- Email Address -->
    <div class="mb-4">
        <label for="email" class="block text-sm text-muted-foreground">Email Address</label>
        <asp:TextBox ID="EmailAdd_SU" 
            CssClass="mt-1 p-2 border border-border rounded w-full"
            runat="server" 
            placeholder="deschau7668@gmail.com" 
            title="Email Address"
            ValidationGroup="SignUp">
        </asp:TextBox>
        <asp:RequiredFieldValidator 
            ID="EmailAdd_SU_rqdValidator" runat="server" 
            ControlToValidate="EmailAdd_SU" 
            ErrorMessage="Email Address is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:RegularExpressionValidator 
            ID="EmailAdd_SU_regexValidator" 
            runat="server" 
            ControlToValidate="EmailAdd_SU" 
            ErrorMessage="Invalid email format." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
        <asp:Label ID="EmailAddErrorMessage" 
            runat="server" ForeColor="Red" Visible="False">
        </asp:Label>
        <script type="text/javascript">
            function validateEmail() {
                // Triggers a postback when the user clicks outside the Username_SU textbox
                __doPostBack('<%= EmailAdd_SU.ClientID %>', '');
            }
        </script>
    </div>

    <!-- Contact Number -->
    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">Contact Number</label>
    <div class="relative mb-4">
        <asp:TextBox ID="ContactNo_SU" runat="server" 
            CssClass="w-full p-2 border border-border rounded-md focus:outline-none"
            placeholder="012-3456789"
            ValidationGroup="SignUp"/>
        <asp:RequiredFieldValidator 
            ID="ContactNo_SU_rqdValidator" runat="server" 
            ControlToValidate="ContactNo_SU" 
            ErrorMessage="Contact Number is required." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:RegularExpressionValidator 
            ID="ContactNo_SU_regexValidator" 
            runat="server" 
            ControlToValidate="ContactNo_SU" 
            ErrorMessage="Must start with 0 in front with '-'." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^01[0-9]-[0-9]{7,9}$" />
    </div>
    
    <!-- Password -->
    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">Password</label>
    <div class="relative mb-4">
        <asp:TextBox ID="Password_SU" 
            CssClass="w-full p-2 border border-border rounded-md focus:outline-none"
            runat="server" 
            placeholder="**********" 
            title="Password" 
            AutoPostBack="false"
            ValidationGroup="SignUp">
        </asp:TextBox>
        <span id="PasswordToggle_SU" class="absolute right-2 top-2 cursor-pointer">👁️</span>
        <asp:RequiredFieldValidator 
            ID="Password_SU_rqdValidator" 
            runat="server" 
            ControlToValidate="Password_SU" 
            ErrorMessage="Password is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:RegularExpressionValidator 
            ID="Password_SU_regexValidator" 
            runat="server" 
            ControlToValidate="Password_SU" 
            ErrorMessage="Must contain >10 letters, numbers, and symbols." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{10,}$" />
    </div>
    
    <!-- Re-enter Password -->
    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">Re-enter Password</label>
    <div class="relative mb-4">
        <asp:TextBox ID="PasswordReenter_SU" 
            CssClass="w-full p-2 border border-border rounded-md focus:outline-none"
            runat="server" 
            placeholder="**********" 
            title="Re-enter Password" 
            AutoPostBack="false"
            ValidationGroup="SignUp">
        </asp:TextBox>
        <span id="PasswordToggle_SU2" class="absolute right-2 top-2 cursor-pointer">👁️</span>
        <asp:RequiredFieldValidator 
            ID="PasswordReenter_SU_rqdValidator" 
            runat="server" 
            ControlToValidate="PasswordReenter_SU" 
            ErrorMessage="Password Re-enter is required." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:CompareValidator 
            ID="PasswordReenter_SU_compareValidator" 
            runat="server" 
            ControlToValidate="PasswordReenter_SU" 
            ControlToCompare="Password_SU" 
            ErrorMessage="Passwords do not match." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" />
    </div>

    <!-- Date of Birth -->
    <label for="DateOfBirth_PR" class="block text-sm text-muted-foreground">Date of Birth</label>
    <div class="mb-4">
        <asp:TextBox ID="DateOfBirth_PR" 
            CssClass="mt-1 p-2 border border-border rounded w-full" 
            runat="server" 
            TextMode="Date"
            placeholder="MM/DD/YYYY"
            ValidationGroup="SignUp"/>
        <asp:RequiredFieldValidator 
            ID="DateOfBirth_PR_rqdValidator" 
            runat="server" 
            ControlToValidate="DateOfBirth_PR" 
            ErrorMessage="Date of Birth is required." 
            Display="Dynamic" 
            ForeColor="Red"
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
    </div>
    
    <!-- Gender -->
    <div class="mb-4">
        <label for="gender" class="block text-sm text-muted-foreground">Gender</label>
        <asp:DropDownList ID="Gender_SU" 
            runat="server" 
            CssClass="mt-1 p-2 border border-border rounded w-full"
            ValidationGroup="SignUp">
            <asp:ListItem Text="~ Select your gender ~" Value="" />
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Female</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator 
            ID="Gender_SU_rqdValidator" 
            runat="server" 
            ControlToValidate="Gender_SU" 
            InitialValue="" 
            ErrorMessage="Please select your gender." 
            Display="Dynamic"
            ForeColor="Red"
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
    </div>
    
    <!-- Residence State -->
    <div class="mb-4">
        <label for="location" class="block text-sm text-muted-foreground">Residence State</label>
        <asp:DropDownList ID="location" 
            runat="server" 
            CssClass="mt-1 p-2 border border-border rounded w-full"
            ValidationGroup="SignUp">
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
        <asp:RequiredFieldValidator 
            ID="location_rqdValidator" 
            runat="server" 
            ControlToValidate="location" 
            InitialValue="" 
            ErrorMessage="Please select a location." 
            Display="Dynamic"
            ForeColor="Red"
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
    </div>

    <!-- Terms & Conditions -->
    <div class="text-center mt-4">
        <span class="text-muted-foreground">By signing up, you agree to the Staffee</span>
        
        <a href="UserAgreement.aspx">User Agreement</a>, and
        <a href="PrivacyPolicy.aspx">Privacy Policy</a>.
        <div class="trMarginBottom20"></div>
    </div>

    <!-- Recaptcha -->
    <div class="mb-4">
        <center>
            <div class="mb-4" 
                style="width: 198px; 
                cursor: pointer;
                height: 40px; 
                transform: scale(0.65); 
                transform-origin: 0 0">
                <div class="g-recaptcha" 
                    data-sitekey="6LdzASAqAAAAAHhQdlfCCZOOzfx17iEXeR-140zQ">
                </div>
            </div>
            <asp:Label ID="lblCaptchaError" 
                runat="server" 
                CssClass="rqdValidator"
                Forecolor="Red"
                visible="false">
            </asp:Label>
        </center>
        <div class="trMarginBottom20"></div>
    </div>
    
    <!-- Sign Up button -->
    <div class="mb-4">
        <asp:Button ID="SignUpBtn_SU" 
        runat="server" 
        CssClass="bg-primary text-primary-foreground p-2 rounded w-full" 
        Text="Agree & Join"
        style="cursor: pointer;"
        OnClick="SignUpBtn_SU_Click"
        ValidationGroup="SignUp" />
        <div class="trMarginBottom20"></div>
        <div class="trMarginBottom20"></div>
    </div>
    
    <!-- Already Joined? -->
    <div class="text-center mt-4">
        <span class="text-muted-foreground">Already on Staffee? </span>
    </div>

    <!-- Sign In -->
    <div class="flex justify-center space-x-4">
        <asp:Button ID="SignInUsernameBtn_SU" 
            runat="server" 
            CssClass="bg-secondary text-secondary-foreground p-2 rounded w-full" 
            Text="Sign In with Username"
            style="cursor: pointer;"
            OnClick="SignUpUsernameBtn_SU_Click"
            CausesValidation="false" />

        <asp:Button ID="SignUpEmailBtn_SU" 
            runat="server" 
            CssClass="bg-secondary text-secondary-foreground p-2 rounded w-full" 
            Text="Sign In with Email"
            style="cursor: pointer;"
            OnClick="SignUpEmailBtn_SU_Click"
            CausesValidation="false" />
    </div>
</div>
<br />
<br />

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

<!-- Password Toggle Tool -->
<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        var passwordField = document.getElementById("<%= Password_SU.ClientID %>");
        var toggleIcon = document.getElementById("PasswordToggle_SU");

        toggleIcon.addEventListener("click", function () {
            if (passwordField.type === "password") {
                passwordField.type = "text";
                toggleIcon.textContent = "🙈"; // Change to closed eye icon when showing password
            } else {
                passwordField.type = "password";
                toggleIcon.textContent = "👁️"; // Change to open eye icon when hiding password
            }
        });
    });
</script>
<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        var passwordField = document.getElementById("<%= PasswordReenter_SU.ClientID %>");
        var toggleIcon = document.getElementById("PasswordToggle_SU2");

        toggleIcon.addEventListener("click", function () {
            if (passwordField.type === "password") {
                passwordField.type = "text";
                toggleIcon.textContent = "🙈"; // Change to closed eye icon when showing password
            } else {
                passwordField.type = "password";
                toggleIcon.textContent = "👁️"; // Change to open eye icon when hiding password
            }
        });
    });
</script>

<!--- reCAPTCHA -->
<script src="https://www.google.com/recaptcha/api.js" async defer></script>
</asp:Content>
