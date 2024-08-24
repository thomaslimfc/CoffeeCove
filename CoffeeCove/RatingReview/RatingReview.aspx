<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="ratingReview.aspx.cs" Inherits="CoffeeCove.RatingReview.ratingReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="../CSS/ratingBootstrap.css" rel="stylesheet" />
    <link href="../CSS/ratingReview.css" rel="stylesheet" />
    <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
                <div id="ratingReviewSummaryContainer" class="bg-white rounded shadow-sm p-4 mb-4 clearfix graph-star-rating">
                    <h5 class="mb-0 mb-4">Ratings and Reviews</h5>
                    <div class="graph-star-rating-header">
                        <div class="star-rating">
                            <b class="text-black ml-2">Total rate: 334</b>
                        </div>
                    </div>
                    <div class="graph-star-rating-body">
                        <div class="rating-list">
                            <div class="rating-list-left text-black">
                                ⭐ 5 Stars
                            </div>
                            <div class="rating-list-center">
                                <div class="progress">
                                    <div style="width: 56%; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar">
                                        <span class="sr-only">80% Complete (danger)</span>
                                    </div>
                                </div>
                            </div>
                            <div class="rating-list-right text-black">56%</div>
                        </div>
                        <div class="rating-list">
                            <div class="rating-list-left text-black">
                                ⭐ 4 Stars
                            </div>
                            <div class="rating-list-center">
                                <div class="progress">
                                    <div style="width: 23%; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar">
                                        <span class="sr-only">80% Complete (danger)</span>
                                    </div>
                                </div>
                            </div>
                            <div class="rating-list-right text-black">23%</div>
                        </div>
                        <div class="rating-list">
                            <div class="rating-list-left text-black">
                                ⭐ 3 Stars
                            </div>
                            <div class="rating-list-center">
                                <div class="progress">
                                    <div style="width: 11%; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar">
                                        <span class="sr-only">80% Complete (danger)</span>
                                    </div>
                                </div>
                            </div>
                            <div class="rating-list-right text-black">11%</div>
                        </div>
                        <div class="rating-list">
                            <div class="rating-list-left text-black">
                                ⭐ 2 Stars
                            </div>
                            <div class="rating-list-center">
                                <div class="progress">
                                    <div style="width: 2%; background-color: #433533f0;" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar">
                                        <span class="sr-only">80% Complete (danger)</span>
                                    </div>
                                </div>
                            </div>
                            <div class="rating-list-right text-black">02%</div>
                        </div>
                    </div>
                    <div class="graph-star-rating-footer text-center mt-3 mb-3">
                        <asp:Button ID="commentButton" CssClass="btnCont" runat="server" Text="Leave a Rating For Us!" OnClick="commentButton_Click" />
                        
                    </div>
                </div>
                <div id="ratingReviewContainer" class="bg-white rounded shadow-sm p-4 mb-4 restaurant-detailed-ratings-and-reviews">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h5 class="mb-1">All Ratings and Reviews</h5>
                        <asp:Button ID="btnCurrentUserRating" CssClass="btnCont" runat="server" Text="Your Rating" OnClick="btnCurrentUserRating_Click" />
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