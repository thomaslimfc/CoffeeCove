﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeFile="AdminReview.aspx.cs" Inherits="CoffeeCove.AdminSite.AdminReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/AdminReview.css" rel="stylesheet" />
    <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
    
        <asp:Button ID="Btnback" runat="server" Text="Back" CssClass="backBtn" OnClick="Btnback_Click" />

        <div id="rateContainer" class="bg-white rounded shadow-sm p-4 mb-5 rating-review-select-page">
            <h3 class="mb-4">Reply Customer</h3>
            
            <asp:Panel ID="CommentPanel" runat="server">
                <div class="form-group">
                    <label for="txtComment">Your Comment</label>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="form-control fixed-height" MaxLength="150"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                        ErrorMessage="Please leave a comment." CssClass="text-danger" Display="Dynamic" ValidationGroup="SubmitGroup" />
                    <asp:RegularExpressionValidator ID="revComment" runat="server" ControlToValidate="txtComment"
                        ErrorMessage="Comment must be 150 characters or less." 
                        ValidationExpression="^.{0,250}$" CssClass="text-danger" Display="Dynamic" ValidationGroup="SubmitGroup" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btnCont" Text="Submit Comment" OnClick="btnSubmit_Click" ValidationGroup="SubmitGroup" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="btnCont2" Text="Delete Your Comment" OnClick="btnDelete_Click" 
                        OnClientClick="return confirm('Are you sure you want to delete this comment?');" />
                </div>
            </asp:Panel>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
