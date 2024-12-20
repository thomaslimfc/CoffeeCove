﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="comment.aspx.cs" Inherits="CoffeeCove.RatingReview.comment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/ratingBootstrap.css" rel="stylesheet" />
    <link href="../CSS/LeaveAComment.css" rel="stylesheet" />

    <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
        
        <asp:Button ID="backBtn" runat="server" Text="Back" CssClass="backBtn" OnClick="backBtn_Click"/>

        <div id="rateContainer" class="bg-white rounded shadow-sm p-4 mb-5 rating-review-select-page">
            <h3 class="mb-4">Leave Comment</h3>
            <p class="mb-2">Rate the Order</p>
            <div class="mb-4">
                <!-- Rating Score Selection -->
                <asp:RadioButtonList ID="rblRating" runat="server" CssClass="rating" RepeatDirection="Horizontal" Width="800px">
                    <asp:ListItem Text="" Value="1">
                        <img src="../img/yellowStar.png" alt="1 Star" class="rating-star" />
                        1 Star
                    </asp:ListItem>
                    <asp:ListItem Text="" Value="2">
                        <img src="../img/yellowStar.png" alt="2 Stars" class="rating-star" />
                        2 Stars
                    </asp:ListItem>
                    <asp:ListItem Text="" Value="3">
                        <img src="../img/yellowStar.png" alt="3 Stars" class="rating-star" />
                        3 Stars
                    </asp:ListItem>
                    <asp:ListItem Text="" Value="4">
                        <img src="../img/yellowStar.png" alt="4 Stars" class="rating-star" />
                        4 Stars
                    </asp:ListItem>
                    <asp:ListItem Text="" Value="5">
                        <img src="../img/yellowStar.png" alt="5 Stars" class="rating-star" />
                        5 Stars
                    </asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvRating" runat="server" ControlToValidate="rblRating" 
                    ErrorMessage="Please select a rating." Display="Dynamic" ForeColor="Red" 
                    InitialValue="" CssClass="error" ValidationGroup="CommentValidation" />
            </div>
            <form>
                <div class="form-group">
                    <label>Your Comment</label>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="form-control fixed-height" MaxLength="150"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btnCont" Text="Submit Comment" OnClick="btnSubmit_Click" ValidationGroup="CommentValidation" />
                </div>
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
