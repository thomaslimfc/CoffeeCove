﻿<%@ Page Title="Forgot Password" Language="C#" 
    MasterPageFile="../Master/Customer2.Master" 
    AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" 
    Inherits="CoffeeCove.Security.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
<script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init=""></script>
<br /><br /><br /><br /><br /><br /><br /><br />

<div class="max-w-md mx-auto p-6 bg-card rounded-lg shadow-md" style="height: 420px;">
    <h2 class="text-2xl font-bold text-foreground mb-4">Forgot Password</h2>
    <div class="trMarginBottom20"></div>    

        <!-- Contact Number -->
    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">Contact Number</label>
    <div class="relative mb-4">
        <asp:TextBox ID="ContactNo_FP" runat="server" 
            CssClass="w-full p-2 border border-border rounded-md focus:outline-none"
            placeholder="010-2897296"
            ValidationGroup="SignUp"/>
        <asp:RequiredFieldValidator 
            ID="ContactNo_FP_rqdValidator" runat="server" 
            ControlToValidate="ContactNo_FP" 
            ErrorMessage="Contact Number is required." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator"
            ValidationGroup="SignUp"/>
        <asp:RegularExpressionValidator 
            ID="ContactNo_FP_regexValidator" 
            runat="server" 
            ControlToValidate="ContactNo_FP" 
            ErrorMessage="Must start with 0 in front with '-'." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^01[0-9]-[0-9]{7,9}$" />
            <asp:Label 
                ID="lblMessage" 
                runat="server" 
                Display="Dynamic" 
                ForeColor="Red" 
                CssClass="rqdValidator" 
                Text="">
            </asp:Label>
            <asp:Label         
                ID="lblWrongOtp" 
                runat="server" 
                Display="Dynamic" 
                ForeColor="Red" 
                CssClass="rqdValidator"
                Text="">
            </asp:Label>
    </div>

    <!-- Description before NEXT -->
    <div class="relative mb-4">
        <center>
            <p>
                We’ll send a verification code to this WhatsApp address if it matches an existing CoffeeCove account.
            </p>
        </center>
    </div>
    <div class="trMarginBottom20"></div>
    <div class="trMarginBottom20"></div>
    <!-- NEXT & BACK -->
    <div class="relative mb-4">
        <asp:Button ID="NextBtn_FP" 
            runat="server" 
            class="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
            style="cursor: pointer;"
            Text="Next"
            CausesValidation="true" 
            OnClick="NextBtn_FP_Click"/>
        <asp:Button ID="BackBtn_FP" 
            runat="server" 
            CssClass="securitySecondaryBtn"
            Text="Back"
            CausesValidation="false" 
            OnClick="BackBtn_FP_Click"/>
    </div>
</div>

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
</asp:Content>
