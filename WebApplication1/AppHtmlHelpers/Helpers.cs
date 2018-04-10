﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Html;

namespace WebApplication1.AppHtmlHelpers
{
    public static class Helpers
    {    
        public static MvcHtmlString ItinenaryItem(string PlaceName,string PlaceCategory, string PlaceRating, string PlacePrice, DateTime StartTime, DateTime FinishTime, string PlaceDescp)
        {
            TagBuilder tb = new TagBuilder("div");
            TagBuilder divLeftCol = new TagBuilder("div");
            TagBuilder divClearfix = new TagBuilder("div");
            TagBuilder divVisitTime = new TagBuilder("div");
            TagBuilder divStartTime = new TagBuilder("div");
            TagBuilder divEndTime = new TagBuilder("div");
            TagBuilder spanTime = new TagBuilder("span");
            TagBuilder divRightCol = new TagBuilder("div");
            TagBuilder divVisitRowMedium = new TagBuilder("div");
            TagBuilder divPhotoClickable = new TagBuilder("div");
            TagBuilder spanFancyBox = new TagBuilder("span");
            TagBuilder divDetail = new TagBuilder("div");
            TagBuilder divNameAttLinkClickableText = new TagBuilder("div");
            TagBuilder divReviewTagContainer = new TagBuilder("div");
            TagBuilder spanReview = new TagBuilder("span");
            TagBuilder divRatingStars = new TagBuilder("div");
            TagBuilder spanRatingStarsFill = new TagBuilder("span");
            TagBuilder spanTagsAttractions = new TagBuilder("span");
            TagBuilder spanTagMustSeeTag = new TagBuilder("span");
            TagBuilder spanTag = new TagBuilder("span");
            TagBuilder divDesc = new TagBuilder("div");
            TagBuilder blockquoteTrimDesc = new TagBuilder("blockquote");
            TagBuilder divTours = new TagBuilder("div");
            TagBuilder aTourLinkTextLink = new TagBuilder("a");
            TagBuilder aAttLinkHidden = new TagBuilder("a");
            TagBuilder divClear = new TagBuilder("div");

            tb.AddCssClass("itinerary-row visit-row data-holder");
            tb.GenerateId("itinerary-item-22");
            tb.Attributes.Add("data-visit-id", "3");
            tb.Attributes.Add("data-id", "3");
            tb.Attributes.Add("data-stay-id", "1");
            tb.Attributes.Add("data-name", "HTMLHELPERS DENEME");
            tb.Attributes.Add("data-duration", "90");
            tb.Attributes.Add("data-attraction-id", "420888639");
            tb.Attributes.Add("data-destination-id", "311325597");
            tb.Attributes.Add("data-details-url", "/trip/a2f6de2f7-eef8-4914-a130-4bc3380476d4/turkey/izmir/konak-square-a420888639");
            tb.Attributes.Add("data-event-src", "visit-row");
            tb.Attributes.Add("data-has-notes", "false");

            divLeftCol.AddCssClass("left-col ");

            divClearfix.AddCssClass("visit-contents clearfix");

            divVisitTime.AddCssClass("visit-time");

            divStartTime.AddCssClass("start-time time");

            divEndTime.AddCssClass("end-time time");
            divEndTime.InnerHtml = FinishTime.ToString(); //Activity Modelin FinishTime'ı gelecek ++++

            spanTime.InnerHtml = StartTime.ToString(); // Activity Modelin StartTime'ı gelecek ++++

            divRightCol.AddCssClass("right-col");

            divVisitRowMedium.AddCssClass("visit-row-medium");

            divPhotoClickable.AddCssClass("photo clickable-image attLink");
            divPhotoClickable.Attributes.Add("style", "background-image: url('../../photos/konak-square--622289667.jpg')");
            divPhotoClickable.Attributes.Add("data-link", "'/turkey/izmir/konak-square-a420888639'");

            spanFancyBox.AddCssClass("copyright-fancy-box-div copyright");
            spanFancyBox.Attributes.Add("data-type", "attraction");
            spanFancyBox.Attributes.Add("data-id", "420888639");
            spanFancyBox.Attributes.Add("data-img-path", "konak-square--622289667.html");
            spanFancyBox.InnerHtml = "&copy;";

            divDetail.AddCssClass("detail");

            divNameAttLinkClickableText.AddCssClass("name attLink clickable-text");
            divNameAttLinkClickableText.InnerHtml = PlaceName; //Activity Modelin Name'i gelecek. ++++

            divReviewTagContainer.AddCssClass("review-tag-container");

            spanReview.AddCssClass("review");

            divRatingStars.AddCssClass("rating-stars ");

            spanRatingStarsFill.AddCssClass("rating-stars-fill");
            spanRatingStarsFill.Attributes.Add("style", PlaceRating); //ActivityModeldeki Placein Ratingi. ++++

            spanTagsAttractions.AddCssClass("tags-attractions");

            spanTagMustSeeTag.AddCssClass("tag must-see-tag"); // Kaldırılabilir. Bakılacak.
            spanTagMustSeeTag.InnerHtml = "#Must See,";

            spanTag.AddCssClass("tag");
            spanTag.Attributes.Add("data-cat-id", "140");
            spanTag.InnerHtml = PlaceCategory; // ActivityModelin içindeki Placein Kategorisi Gelecek. +++++

            divDesc.AddCssClass("desc");

            blockquoteTrimDesc.AddCssClass("trim-desc");
            blockquoteTrimDesc.Attributes.Add("cite", "mekanbilgilendirmesi");//////
            blockquoteTrimDesc.InnerHtml = PlaceDescp;//ActivityModelin Descp gelecek. ++++

            divTours.AddCssClass("tours");

            aTourLinkTextLink.AddCssClass("tours-link text-link attLink jumper");
            aTourLinkTextLink.Attributes.Add("href", "mekanbilgilendirmesi");
            aTourLinkTextLink.InnerHtml = "Tours from" + PlacePrice; //ActivityModelin Fiyatı Gelecek. ++++

            aAttLinkHidden.AddCssClass("attLink hidden full-details-link text-link");
            aAttLinkHidden.Attributes.Add("href", "mekanbilgilendirmesi");
            aAttLinkHidden.InnerHtml = "View full attraction details";

            divClear.AddCssClass("clear");

            divStartTime = combineTags(divStartTime, spanTime);
            divVisitTime = combineTags(divVisitTime, divStartTime);
            divVisitTime = combineTags(divVisitTime, divEndTime);
            divLeftCol = combineTags(divLeftCol, divVisitTime);
            divClearfix = combineTags(divClearfix, divLeftCol);

            divVisitRowMedium = combineTags(divVisitRowMedium,divPhotoClickable);
            divVisitRowMedium = combineTags(divVisitRowMedium, spanFancyBox);
            divRightCol = combineTags(divRightCol,divVisitRowMedium); 
            
            divRatingStars = combineTags(divRatingStars,spanRatingStarsFill);
            spanReview = combineTags(spanReview,divRatingStars);
            divReviewTagContainer = combineTags(divReviewTagContainer,spanReview);

            spanTagsAttractions = combineTags(spanTagsAttractions,spanTagMustSeeTag);
            spanTagsAttractions = combineTags(spanTagsAttractions,spanTag);
            divReviewTagContainer = combineTags(divReviewTagContainer, spanTagsAttractions);

            divDesc = combineTags(divDesc,blockquoteTrimDesc);

            divTours = combineTags(divTours,aTourLinkTextLink);
            divTours = combineTags(divTours, aAttLinkHidden);

            divDetail = combineTags(divDetail, divNameAttLinkClickableText);
            divDetail = combineTags(divDetail, divReviewTagContainer);
            divDetail = combineTags(divDetail,divDesc);
            divDetail = combineTags(divDetail, divTours);

            divRightCol = combineTags(divRightCol, divDetail);

            divClearfix = combineTags(divClearfix, divRightCol);
            divClearfix = combineTags(divClearfix, divClear);
            tb = combineTags(tb, divClearfix);

            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ItineraryRow()
        {
                TagBuilder divItinerarRowHopRow = new TagBuilder("div");
                TagBuilder spanTravelTime = new TagBuilder("span");
                TagBuilder aDirectionsTextLink = new TagBuilder("a");
                TagBuilder divLeftCol = new TagBuilder("div");
                TagBuilder divRightCol = new TagBuilder("div");

                divItinerarRowHopRow.AddCssClass("itinerary-row hop-row    ");
                divItinerarRowHopRow.Attributes.Add("data-stay-id", "1");
                divLeftCol.AddCssClass("left-col ");
                divRightCol.AddCssClass("right-col");
                spanTravelTime.AddCssClass("TravelTime");
                spanTravelTime.InnerHtml = "12&#8203;min";

                aDirectionsTextLink.AddCssClass("directions text-link");
                aDirectionsTextLink.Attributes.Add("href", "http://maps.google.com/maps?saddr=38.4106,27.16165&amp;daddr=38.435547,27.139322");
                aDirectionsTextLink.Attributes.Add("target", "_blank");
                aDirectionsTextLink.InnerHtml = "Get details &raquo;";
            
            divItinerarRowHopRow = combineTags(divItinerarRowHopRow, divLeftCol);
            divRightCol = combineTags(divRightCol, spanTravelTime);
            divRightCol = combineTags(divRightCol,aDirectionsTextLink);
            divItinerarRowHopRow = combineTags(divItinerarRowHopRow, divRightCol);
            return MvcHtmlString.Create(divItinerarRowHopRow.ToString(TagRenderMode.Normal));
        }
        //<div class="itinerary-row hop-row    " data-stay-id="1">
        //                                    <div class="left-col"></div>
        //                                    <div class="right-col">
        //                                        <span class="travelTime">12&#8203;min</span>
        //                                        <a class="directions text-link" rel="nofollow noopener" href="http://maps.google.com/maps?saddr=38.4106,27.16165&amp;daddr=38.435547,27.139322" target="_blank">Get details &raquo;</a>
        //                                    </div>
        //                                </div>

        private static TagBuilder combineTags(TagBuilder t1,TagBuilder t2)
        {
            t1.InnerHtml += t2.ToString(TagRenderMode.Normal);
            return t1;
        }

        /*  *******************************TO-DO*******************************
            Tag builder tag, TagBuilder childLists, recursive.
             1. --> Tag ve childları içinde bulunduracak bir veri yapısı.
             2. --> Tag ve childlar için sadece tag ismi verilip recursive olarak childlar sonrada tagin objeleri oluşturulacak.
             3. --> Childların childı aynı şekilde fonksiyon içerisine gönderilecek recursive olarak burada çalışacak.
             4. --> Aynı seviyedeki childları tespit edebilmek için veri yapısı içerisine level değeri atanacak.
             5. --> Aynı seviyedeki childları saptandıktan sonra InnerHtml ile olması gereken sırayla birbirine bağlanacak.
             6. --> Tagler için ayrı ayrı methodlar yazılabilir.(DivInnerLoad,WriteModal,Link...)
            *******************************TO-DO*******************************   */
    }
}