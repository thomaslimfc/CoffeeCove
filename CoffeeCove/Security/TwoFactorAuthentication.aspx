<%@ Page Title="2-Factor Authentication" Language="C#" 
    MasterPageFile="~/Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="TwoFactorAuthentication.aspx.cs" 
    Inherits="CoffeeCove.Security.TwoFactorAuthentication" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
<script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init=""></script>
<br /><br /><br /><br /><br /><br /><br /><br />
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
        var passwordField = document.getElementById("<%= otp.ClientID %>");
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
    <h2 class="text-2xl font-bold text-foreground mb-4">2-Factor Authentication</h2>

    <!-- Password -->
    <label for="first-name" class="block text-sm text-muted-foreground">OTP Code</label>
    <div class="relative mb-4">
        <asp:TextBox ID="otp" 
            CssClass="w-full p-2 border border-border rounded-md focus:outline-none focus:ring focus:ring-ring"
            runat="server" 
            placeholder="******" 
            title="One-time Password (OTP) Code" 
            AutoPostBack="false">
        </asp:TextBox>
        <span id="PasswordToggle" class="absolute right-2 top-2 cursor-pointer">👁️</span>
        <asp:RequiredFieldValidator 
            ID="otp_rqdValidator" 
            runat="server" 
            ControlToValidate="otp" 
            ErrorMessage="OTP is required." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" />

        <asp:RegularExpressionValidator 
            ID="otp_regexValidator" 
            runat="server" 
            ControlToValidate="otp" 
            ErrorMessage="Must contain exactly 6 digits." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^\d{6}$" />
    </div>
    <div class="mb-4">
        <center>
            <p>Enter the 6 digit code generated in your mailbox.
        </center>
    </div>
    <div class="trMarginBottom20"></div>
    <!-- VERIFY Button -->
    <asp:Button ID="VerifyButton_TFA" 
        runat="server" 
        Text="Verify Now" 
        CssClass="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80" />
    <div class="trMarginBottom20"></div>
    <!-- Join Now -->
    <div class="text-center mt-4">
        <span class="text-muted-foreground">Having Trouble? </span>
        <a href="ForgotPassword.aspx" class="text-primary">Try me again</a>
    </div>
</div>
</asp:Content>
