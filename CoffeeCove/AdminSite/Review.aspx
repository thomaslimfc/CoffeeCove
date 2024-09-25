﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="CoffeeCove.AdminSite.Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="../CSS/ratingBootstrap.css" rel="stylesheet" />
    <link href="../CSS/Review.css" rel="stylesheet" />
    <div id="main" class="main" style="margin-right:1%">
        <div class="pagetitle" style="color:#fff">
            <br />
            <h3>Rating & Review Management</h3>
        </div>
        <section class="section">
            <div class="row" style="margin-top: 2%;">
                <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
                    <div id="ratingReviewSummaryContainer" class="bg-white rounded shadow-sm p-4 mb-4 clearfix graph-star-rating">
                        <h5 class="mb-0 mb-4">Ratings and Reviews</h5>
                        <div class="graph-star-rating-header">
                            <div class="star-rating">
                                <b class="text-black ml-2">Total rate: <asp:Literal ID="litTotalRatings" runat="server" /></b>
                            </div>
                        </div>
                        <div class="graph-star-rating-body">
                            <div class="rating-list">
                                <div class="rating-list-left text-black">
                                    ⭐ 5 Stars
                                </div>
                                <div class="rating-list-center">
                                    <div class="progress">
                                        <div style="width: <%= GetRatingPercentage(FiveStarCount) %>; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar">
                                            <span class="sr-only"><%= GetRatingPercentage(FiveStarCount) %> Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="rating-list-right text-black"><%= GetRatingPercentage(FiveStarCount) %></div>
                            </div>
                            <div class="rating-list">
                                <div class="rating-list-left text-black">
                                    ⭐ 4 Stars
                                </div>
                                <div class="rating-list-center">
                                    <div class="progress">
                                        <div style="width: <%= GetRatingPercentage(FourStarCount) %>; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="4" role="progressbar" class="progress-bar">
                                            <span class="sr-only"><%= GetRatingPercentage(FourStarCount) %> Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="rating-list-right text-black"><%= GetRatingPercentage(FourStarCount) %></div>
                            </div>
                            <div class="rating-list">
                                <div class="rating-list-left text-black">
                                    ⭐ 3 Stars
                                </div>
                                <div class="rating-list-center">
                                    <div class="progress">
                                        <div style="width: <%= GetRatingPercentage(ThreeStarCount) %>; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="3" role="progressbar" class="progress-bar">
                                            <span class="sr-only"><%= GetRatingPercentage(ThreeStarCount) %> Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="rating-list-right text-black"><%= GetRatingPercentage(ThreeStarCount) %></div>
                            </div>
                            <div class="rating-list">
                                <div class="rating-list-left text-black">
                                    ⭐ 2 Stars
                                </div>
                                <div class="rating-list-center">
                                    <div class="progress">
                                        <div style="width: <%= GetRatingPercentage(TwoStarCount) %>; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="2" role="progressbar" class="progress-bar">
                                            <span class="sr-only"><%= GetRatingPercentage(TwoStarCount) %> Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="rating-list-right text-black"><%= GetRatingPercentage(TwoStarCount) %></div>
                            </div>
                            <div class="rating-list">
                                <div class="rating-list-left text-black">
                                    ⭐ 1 Stars
                                </div>
                                <div class="rating-list-center">
                                    <div class="progress">
                                        <div style="width: <%= GetRatingPercentage(OneStarCount) %>; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="1" role="progressbar" class="progress-bar">
                                            <span class="sr-only"><%= GetRatingPercentage(OneStarCount) %> Complete</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="rating-list-right text-black"><%= GetRatingPercentage(OneStarCount) %></div>
                            </div>
                        </div>
                    </div>
                    <div id="ratingReviewContainer" class="bg-white rounded shadow-sm p-4 mb-4 restaurant-detailed-ratings-and-reviews">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h5 class="mb-1">All Ratings and Reviews</h5>
                        </div>

                        <!-- Rating Container -->
                        <asp:Repeater ID="rptUserRatingReview" runat="server" OnItemDataBound="rptUserRatingReview_ItemDataBound">
                            <ItemTemplate>
                                <div class="review-container mb-4">
                                    <div class="media">
                                        <a href="#">
                                            <asp:Image ID="imgProfilePicture" runat="server" CssClass="mr-3 rounded-circle" Width="50" Height="50" />
                                        </a>
                                        <div class="media-body">
                                            <div class="d-flex justify-content-between">
                                                <h6 class="mt-0 mb-1"><a class="text-dark" href="#"><strong><%# Eval("Username") %></strong></a></h6>
                                                <p class="text-muted mb-0"><%# Eval("RatingReviewDateTime") %></p>
                                            </div>
                                            <p class="mb-1"><strong>Order ID: </strong><%# Eval("OrderID") %></p>
                                            <div class="rating-stars mt-2">
                                                <asp:PlaceHolder ID="phStars" runat="server"></asp:PlaceHolder>
                                            </div>
                                            <div class="review-content mt-2">
                                                <p><%# Eval("ReviewContent") %></p>
                                            </div>
                                            <div class="d-flex justify-content-end mt-2">
                                                <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-dark mr-2" Text="Reply/Edit" 
                                                    OnClick="btnEdit_Click" CommandArgument='<%# Eval("RatingReviewID") + "," + Eval("PaymentID") %>' />
                                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" 
                                                    Text="Delete" OnClick="btnDelete_Click" 
                                                    CommandArgument='<%# Eval("RatingReviewID") %>' 
                                                    OnClientClick="return confirm('Are you sure you want to delete this review?');" />
                                            </div>
                                            <asp:PlaceHolder ID="phAdminReply" runat="server"></asp:PlaceHolder>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </section>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
