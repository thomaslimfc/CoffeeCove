<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="ratingReview.aspx.cs" Inherits="CoffeeCove.RatingReview.ratingReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" />
    <link href="../CSS/ratingReview.css" rel="stylesheet" />

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
                                    <div style="width: 56%" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar bg-primary">
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
                                    <div style="width: 23%" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar bg-primary">
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
                                    <div style="width: 11%" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar bg-primary">
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
                                    <div style="width: 2%" aria-valuemax="5" aria-valuemin="0" aria-valuenow="5" role="progressbar" class="progress-bar bg-primary">
                                        <span class="sr-only">80% Complete (danger)</span>
                                    </div>
                                </div>
                            </div>
                            <div class="rating-list-right text-black">02%</div>
                        </div>
                    </div>
                    <div class="graph-star-rating-footer text-center mt-3 mb-3">
                        <button type="button" class="btn btn-outline-primary btn-sm">Rate and Review</button>
                    </div>
                </div>
                <div id="ratingReviewContainer" class="bg-white rounded shadow-sm p-4 mb-4 restaurant-detailed-ratings-and-reviews">
                    <h5 class="mb-1">All Ratings and Reviews</h5>
                    <!-- Rating Container -->
                    <asp:Repeater ID="rptUserRatingReview" runat="server">
                        <ItemTemplate>
                            <div id="usersRatingContainer" class="reviews-members pt-4 pb-4">
                                <div class="media">
                                    <a href="#"><img alt="Generic placeholder image" src="http://bootdey.com/img/Content/avatar/avatar1.png" class="mr-3 rounded-pill"></a>
                                    <div class="media-body">
                                        <div class="reviews-members-header">
                                            <span class="star-rating float-right">
                                                  <a href="#"><i class="icofont-ui-rating active"></i></a>
                                                  <a href="#"><i class="icofont-ui-rating active"></i></a>
                                                  <a href="#"><i class="icofont-ui-rating active"></i></a>
                                                  <a href="#"><i class="icofont-ui-rating active"></i></a>
                                                  <a href="#"><i class="icofont-ui-rating"></i></a>
                                                  </span>
                                            <h4 class="mb-1"><a class="text-black" href="#">Singh Osahan</a></h4>
                                            <p class="text-gray">Tue, 20 Mar 2020</p>
                                        </div>
                                        <div class="reviews-members-body">
                                            <p>Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections </p>
                                        </div>
                                        <div class="reviews-members-footer">
                                            <a class="total-like" href="#"><i class="icofont-thumbs-up"></i> 856M</a> <a class="total-like" href="#"><i class="icofont-thumbs-down"></i> 158K</a>
                                            <span class="total-like-user-main ml-2" dir="rtl">
                                                  <a data-toggle="tooltip" data-placement="top" title="" href="#" data-original-title="Gurdeep Osahan"><img alt="Generic placeholder image" src="http://bootdey.com/img/Content/avatar/avatar5.png" class="total-like-user rounded-pill"></a>
                                                  <a data-toggle="tooltip" data-placement="top" title="" href="#" data-original-title="Gurdeep Singh"><img alt="Generic placeholder image" src="http://bootdey.com/img/Content/avatar/avatar2.png" class="total-like-user rounded-pill"></a>
                                                  <a data-toggle="tooltip" data-placement="top" title="" href="#" data-original-title="Askbootstrap"><img alt="Generic placeholder image" src="http://bootdey.com/img/Content/avatar/avatar3.png" class="total-like-user rounded-pill"></a>
                                                  <a data-toggle="tooltip" data-placement="top" title="" href="#" data-original-title="Osahan"><img alt="Generic placeholder image" src="http://bootdey.com/img/Content/avatar/avatar4.png" class="total-like-user rounded-pill"></a>
                                                  </span>
                                        </div>
                                    </div>
                                </div>
                                <hr>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    
                    <hr>
                    <a class="text-center w-100 d-block mt-4 font-weight-bold" href="#">See All Reviews</a>
                </div>
                <div id="rateContainer" class="bg-white rounded shadow-sm p-4 mb-5 rating-review-select-page">
                    <h5 class="mb-4">Leave Comment</h5>
                    <p class="mb-2">Rate the Place</p>
                    <div class="mb-4">
                        <span class="star-rating">
                                 <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                                 <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                                 <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                                 <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                                 <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                                 </span>
                    </div>
                    <form>
                        <div class="form-group">
                            <label>Your Comment</label>
                            <textarea class="form-control"></textarea>
                        </div>
                        <div class="form-group">
                            <button class="btn btn-primary btn-sm" type="button"> Submit Comment </button>
                        </div>
                    </form>
                </div>
            </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>