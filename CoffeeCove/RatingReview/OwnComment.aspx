﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="OwnComment.aspx.cs" Inherits="CoffeeCove.RatingReview.OwnComment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="../CSS/ratingBootstrap.css" rel="stylesheet" />
    <link href="../CSS/ratingReview.css" rel="stylesheet" />

    <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
                <asp:Button ID="Btnback" runat="server" Text="Back" PostBackUrl="~/RatingReview/ratingReview.aspx" CssClass="backBtn" />
            <div id="ratingReviewContainer" class="bg-white rounded shadow-sm p-4 mb-4 restaurant-detailed-ratings-and-reviews">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h5 class="mb-1">Your Ratings and Reviews</h5>
                </div>

                <!-- Rating Container -->
                <asp:Repeater ID="rptUserRatingReview" runat="server" OnItemDataBound="rptUserRatingReview_ItemDataBound">
                    <ItemTemplate>
                        <div class="review-container mb-4">
                            <div class="media">
                                <a href="#">
                                    <img alt="User avatar" src="http://bootdey.com/img/Content/avatar/avatar1.png" class="mr-3 rounded-circle" />
                                </a>
                                <div class="media-body">
                                    <div class="d-flex justify-content-between">
                                        <h4 class="mt-0 mb-1"><a class="text-dark" href="#"><strong><%# Eval("Username") %></strong></a></h4>
                                        <p class="text-muted mb-0"><%# Eval("RatingReviewDateTime") %></p>
                                    </div>
                                    <div class="rating-stars mt-2">
                                        <asp:PlaceHolder ID="phStars" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="review-content mt-2">
                                        <p><%# Eval("ReviewContent") %></p>
                                    </div>
                                    <div class="d-flex justify-content-end mt-2">
                                        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-dark mr-2" Text="Edit" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" />
                                    </div>
                                    <asp:PlaceHolder ID="phAdminReply" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>