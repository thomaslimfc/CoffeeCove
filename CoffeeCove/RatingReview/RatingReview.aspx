<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="ratingReview.aspx.cs" Inherits="CoffeeCove.RatingReview.ratingReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/ratingReview.css" rel="stylesheet" />
    <link href="../CSS/ratingBootstrap.css" rel="stylesheet" />
    <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
                <div id="ratingReviewSummaryContainer" class="bg-white rounded shadow-sm p-4 mb-4 clearfix graph-star-rating">
                    <h5 class="mb-0 mb-4">Ratings and Reviews</h5>
                    <div class="graph-star-rating-header">
                        <div class="star-rating">
                            <a href="#"><i class="icofont-ui-rating active"></i></a>
                            <a href="#"><i class="icofont-ui-rating active"></i></a>
                            <a href="#"><i class="icofont-ui-rating active"></i></a>
                            <a href="#"><i class="icofont-ui-rating active"></i></a>
                            <a href="#"><i class="icofont-ui-rating"></i></a> <b class="text-black ml-2">334</b>
                        </div>
                        <p class="text-black mb-4 mt-2">Rated 3.5 out of 5</p>
                    </div>
                    <div class="graph-star-rating-body">
                        <div class="rating-list">
                            <div class="rating-list-left text-black">
                                5 Star
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
                                4 Star
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
                                3 Star
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
                                2 Star
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
                    <h5 class="mb-1">All Ratings and Reviews</h5>
                    <!-- Rating Container -->
                    <asp:Repeater ID="rptUserRatingReview" runat="server" OnItemDataBound="rptUserRatingReview_ItemDataBound">
                        <ItemTemplate>
                            <div id="usersRatingContainer" class="reviews-members pt-4 pb-4">
                                <div class="media">
                                    <a href="#"><img alt="Generic placeholder image" src="http://bootdey.com/img/Content/avatar/avatar1.png" class="mr-3 rounded-pill"></a>
                                    <div class="media-body">
                                        <div class="reviews-members-header">
                                            <h4 class="mb-1"><a class="text-black" href="#"><strong><%# Eval("CustomerName") %></strong></a></h4>
                                            <p class="text-gray"><%# Eval("RatingReviewDateTime") %></p>
                                        </div>
                                        <div class="reviews-members-body">
                                            <p><%# Eval("ReviewContent") %></p>
                                            <!-- Star Rating Placeholder -->
                                            <asp:PlaceHolder ID="phStars" runat="server"></asp:PlaceHolder>
                                        </div>
                                    </div>
                                </div>
                                <hr>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>