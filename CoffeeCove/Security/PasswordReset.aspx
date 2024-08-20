<%@ Page Title="Reset Password" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" 
    Inherits="CoffeeCove.Security.PasswordReset" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
<script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init=""></script>
<br /><br /><br />
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
        var passwordField = document.getElementById("<%= Password_PR.ClientID %>");
        var toggleIcon = document.getElementById("PasswordToggle");

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
        var passwordField = document.getElementById("<%= PasswordConfirm_PR.ClientID %>");
        var toggleIcon = document.getElementById("PasswordToggle");

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


<div class="max-w-md mx-auto p-6 bg-card rounded-lg shadow-md">
    <h2 class="text-2xl font-bold text-foreground mb-4">Reset Password</h2>

    <!-- Password -->
    <label for="first-name" class="block text-sm text-muted-foreground">Password</label>
    <div class="relative mb-4">
         <asp:TextBox ID="Password_PR" 
            CssClass="w-full p-2 border border-border rounded-md focus:outline-none focus:ring focus:ring-ring"
            runat="server" 
            placeholder="**********" 
            title="Password" 
            AutoPostBack="false">
        </asp:TextBox>
        <span id="PasswordToggle_PR" class="absolute right-2 top-2 cursor-pointer">👁️</span>
        <asp:RequiredFieldValidator 
            ID="Password_PR_rqdValidator" 
            runat="server" 
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
    </div>
    
    <!-- Re-enter Password -->
    <label for="first-name" class="block text-sm text-muted-foreground">Re-enter Password</label>
    <div class="relative mb-4">
        <asp:TextBox ID="PasswordConfirm_PR" 
            CssClass="w-full p-2 border border-border rounded-md focus:outline-none focus:ring focus:ring-ring"
            runat="server" 
            placeholder="**********" 
            title="Re-enter Password" 
            AutoPostBack="false">
        </asp:TextBox>
        <span id="PasswordToggle_PR2" class="absolute right-2 top-2 cursor-pointer">👁️</span>
        <asp:RequiredFieldValidator 
            ID="PasswordConfirm_PR_rqdValidator" 
            runat="server" 
            ControlToValidate="PasswordConfirm_PR" 
            ErrorMessage="Password Re-enter is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator" />
        <asp:CompareValidator 
            ID="PasswordConfirm_PR_compValidator" 
            runat="server" 
            ControlToValidate="PasswordConfirm_PR" 
            ControlToCompare="Password_PR" 
            ErrorMessage="Passwords do not match." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" />
    </div>

    <!-- Terms & Conditions -->
    <div class="text-center mt-4">
        <span class="text-muted-foreground">By signing up, you agree to the Staffee</span>
        
        <a href="UserAgreement.aspx">User Agreement</a>, and
        <a href="PrivacyPolicy.aspx">Privacy Policy</a>.
        <div class="trMarginBottom20"></div>
    </div>

    <div class="relative mb-4">
        <asp:Button ID="ResetPasswordBtn_PR" 
            runat="server" 
            class="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
            style="cursor: pointer;"
            Text="Next"
            CausesValidation="true" 
            OnClick="ResetPasswordBtn_PR_Click"/>
    </div>
    
    
</div>
<br />
<br />
</asp:Content>