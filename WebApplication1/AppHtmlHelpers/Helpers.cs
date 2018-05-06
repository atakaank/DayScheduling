using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Html;
using System.Security.Policy;

namespace WebApplication1.AppHtmlHelpers
{
    public static class Helpers
    {    //,string PlaceCategory
        public static MvcHtmlString ItinenaryItem(string ActivityID,int PlaceID,string PlaceName, int PlaceRating, string PlacePrice, TimeSpan StartTime, TimeSpan FinishTime, string PlaceDescp,string PlaceType)
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
            TagBuilder spanTag = new TagBuilder("span");
            TagBuilder divDesc = new TagBuilder("div");
            TagBuilder blockquoteTrimDesc = new TagBuilder("blockquote");
            TagBuilder divTours = new TagBuilder("div");
            TagBuilder aTourLinkTextLink = new TagBuilder("a");
            TagBuilder aAttLinkHidden = new TagBuilder("a");
            TagBuilder divClear = new TagBuilder("div");
            TagBuilder btnUpdate = new TagBuilder("button");

            tb.AddCssClass("itinerary-row visit-row data-holder");
            //tb.GenerateId("itinerary-item-22");
            //tb.Attributes.Add("data-visit-id", "3");
            tb.Attributes.Add("data-id", PlaceID.ToString());
            //tb.Attributes.Add("data-stay-id", "1");
            //tb.Attributes.Add("data-name", "HTMLHELPERS DENEME");
            //tb.Attributes.Add("data-duration", "90");
            //tb.Attributes.Add("data-attraction-id", "420888639");
            //tb.Attributes.Add("data-destination-id", "311325597");
            //tb.Attributes.Add("data-details-url", "/trip/a2f6de2f7-eef8-4914-a130-4bc3380476d4/turkey/izmir/konak-square-a420888639");
            //tb.Attributes.Add("data-event-src", "visit-row");
            //tb.Attributes.Add("data-has-notes", "false");


            btnUpdate.Attributes.Add("type", "button");
            btnUpdate.AddCssClass("cta-button large Update");
            btnUpdate.Attributes.Add("id", "btnUpdate" + ActivityID);
            btnUpdate.Attributes.Add("data-id", ActivityID);
            btnUpdate.Attributes.Add("data-toggle","modal");
            btnUpdate.Attributes.Add("data-target", "#UpdateModal");
            btnUpdate.InnerHtml = "Change Activity";

            divLeftCol.AddCssClass("left-col ");

            divClearfix.AddCssClass("visit-contents clearfix");

            divVisitTime.AddCssClass("visit-time");

            divStartTime.AddCssClass("start-time time");

            divEndTime.AddCssClass("end-time time");
            divEndTime.InnerHtml = FinishTime.ToString(@"hh\:mm"); //Activity Modelin FinishTime'ı gelecek ++++

            spanTime.InnerHtml = StartTime.ToString(@"hh\:mm"); // Activity Modelin StartTime'ı gelecek ++++

            divRightCol.AddCssClass("right-col");

            divVisitRowMedium.AddCssClass("visit-row-medium");

            divPhotoClickable.AddCssClass("photo clickable-image attLink");
            divPhotoClickable.Attributes.Add("style", "background-image: url('../../photos/"+ PlaceID +".jpg')");
            //divPhotoClickable.Attributes.Add("data-link", "'/turkey/izmir/konak-square-a420888639'");

            //spanFancyBox.AddCssClass("copyright-fancy-box-div copyright");
            //spanFancyBox.Attributes.Add("data-type", "attraction");
            //spanFancyBox.Attributes.Add("data-id", "420888639");
            //spanFancyBox.Attributes.Add("data-img-path", "konak-square--622289667.html");
            //spanFancyBox.InnerHtml = "&copy;";

            divDetail.AddCssClass("detail");

            divNameAttLinkClickableText.AddCssClass("name attLink clickable-text");
            divNameAttLinkClickableText.InnerHtml = PlaceName; //Activity Modelin Name'i gelecek. ++++

            divReviewTagContainer.AddCssClass("review-tag-container");

            spanReview.AddCssClass("review");

            divRatingStars.AddCssClass("rating-stars ");

            spanRatingStarsFill.AddCssClass("rating-stars-fill");
            spanRatingStarsFill.Attributes.Add("style", "width:" + PlaceRating*20 + "%;"); //ActivityModeldeki Placein Ratingi. ++++

            spanTagsAttractions.AddCssClass("tags-attractions");

            spanTag.AddCssClass("tag");
            spanTag.Attributes.Add("data-cat-id", "140");
            spanTag.InnerHtml = PlaceType;//PlaceCategory; // ActivityModelin içindeki Placein Kategorisi Gelecek. +++++

            divDesc.AddCssClass("desc");

            blockquoteTrimDesc.AddCssClass("trim-desc");

            divTours.AddCssClass("tours");

            aTourLinkTextLink.AddCssClass("tours-link text-link attLink jumper");
            aTourLinkTextLink.Attributes.Add("href", "mekanbilgilendirmesi");
            aTourLinkTextLink.InnerHtml = "The Estimated Minimum Spend " + PlacePrice +"TL"; //ActivityModelin Fiyatı Gelecek. ++++

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

            spanTagsAttractions = combineTags(spanTagsAttractions,spanTag);
            divReviewTagContainer = combineTags(divReviewTagContainer, spanTagsAttractions);

            divDesc = combineTags(divDesc,blockquoteTrimDesc);

            divTours = combineTags(divTours,aTourLinkTextLink);
            divTours = combineTags(divTours, aAttLinkHidden);

            divDetail = combineTags(divDetail, divNameAttLinkClickableText);
            divDetail = combineTags(divDetail, divReviewTagContainer);
            divDetail = combineTags(divDetail,divDesc);
            divDetail = combineTags(divDetail, divTours);
            divDetail = combineTags(divDetail, btnUpdate);

            divRightCol = combineTags(divRightCol, divDetail);

            divClearfix = combineTags(divClearfix, divRightCol);
            divClearfix = combineTags(divClearfix, divClear);
            tb = combineTags(tb, divClearfix);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ItineraryRow(string SourceLat, string SourceLong, string DestLat, string DestLong ,string duration)
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
                spanTravelTime.InnerHtml = duration +"&#8203;";

                aDirectionsTextLink.AddCssClass("directions text-link");

            //https://www.google.com/maps/dir/'38.451,27.21'/'38.4695426,27.0749951'
            //https://www.google.com/maps/dir/'38.4412,27.143932'/'38.436796,27.143132

            /*https://www.google.com/maps/dir/'38.451,27.21'/'38.4695426,27.0749951'/@38.4681066,27.101598,13z/data=!3m1!4b1!4m10!4m9!1m3!2m2!1d27.21!2d38.451!1m3!2m2!1d27.0749951!2d38.4695426!3e0*/

            aDirectionsTextLink.Attributes.Add("href", "https://www.google.com/maps/dir/" + "'" + SourceLat + "," + SourceLong + "'/" + "'" + DestLat + "," + DestLong + "'");
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
        public static MvcHtmlString Radio(string id,string cssClass,string value)
        {
            TagBuilder inputRadio = new TagBuilder("input");
            
            inputRadio.Attributes.Add("type", "radio");
            inputRadio.AddCssClass(cssClass);
            inputRadio.Attributes.Add("id", id);
            inputRadio.Attributes.Add("name", "style");
            inputRadio.Attributes.Add("values",value);
            return MvcHtmlString.Create(inputRadio.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString Label(string labelTitle)
        {
            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("title", labelTitle);
            label.InnerHtml = labelTitle;
            return MvcHtmlString.Create(label.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString BlockPlan(int PlanID,string ProvinceName, string ProvinceID,string PlanCategories,string Popularity)
        {
            TagBuilder blockSmPlan = new TagBuilder("a");
            blockSmPlan.AddCssClass("block sm plan  notranslate");
            TagBuilder spanDestImage = new TagBuilder("span");
            spanDestImage.AddCssClass("dest-image lazyload");
            spanDestImage.Attributes.Add("data-url", "../../photos/Pro" + ProvinceID + ".jpg");
            TagBuilder spanBox = new TagBuilder("span");
            spanBox.AddCssClass("box");
            TagBuilder spanTextinBox = new TagBuilder("span");
            spanTextinBox.AddCssClass("text in-box");
            spanTextinBox.Attributes.Add("data-id", PlanID.ToString());
            TagBuilder spanPrimaryText = new TagBuilder("span");
            spanPrimaryText.AddCssClass("primarytext");
            TagBuilder spanActionText = new TagBuilder("span");
            spanActionText.AddCssClass("actiontext");
            spanActionText.InnerHtml = "VIEW/EDIT";
            TagBuilder spanSubtextBoxInBox = new TagBuilder("span");
            spanSubtextBoxInBox.AddCssClass("subtext-box in-box");
            TagBuilder spanSubtext = new TagBuilder("span");
            spanSubtext.AddCssClass("subtext");
            TagBuilder spanDefault = new TagBuilder("span");
            spanDefault.AddCssClass("default");
            TagBuilder spanHover = new TagBuilder("span");
            spanHover.AddCssClass("hover");
            TagBuilder spanLine1 = new TagBuilder("span");
            spanLine1.AddCssClass("line");
            spanLine1.InnerHtml = "<strong>PREFERENCES:</strong>" + "  "  + PlanCategories;
            TagBuilder spanLine2 = new TagBuilder("span");
            spanLine2.AddCssClass("line");
            spanLine2.InnerHtml = " <strong>ATTRACTION STYLE:</strong>" + Popularity;
            TagBuilder spanDeleteConf = new TagBuilder("span");
            spanDeleteConf.AddCssClass("delete-conf in-box");
            TagBuilder spanQ = new TagBuilder("span");
            spanQ.AddCssClass("q");
            spanQ.InnerHtml = "Delete this plan?";
            TagBuilder btnCancel = new TagBuilder("button");
            btnCancel.AddCssClass("cancel cta-button large");
            btnCancel.InnerHtml = "Cancel";
            TagBuilder btnDelete = new TagBuilder("button");
            btnDelete.AddCssClass("confirm cta-button large");
            btnDelete.Attributes.Add("data-id", PlanID.ToString());
            btnDelete.InnerHtml = "Yes, Delete";
            TagBuilder spanCaution = new TagBuilder("span");
            spanCaution.AddCssClass("caution");
            spanCaution.InnerHtml = "CAUTION: THIS CANNOT BE UNDONE";
            TagBuilder spanDeleteIcon = new TagBuilder("span");
            spanDeleteIcon.AddCssClass("delete");
            spanDeleteIcon.InnerHtml = "<svg><use xlink:href=" + "#icon-trash /></svg>";
            

            spanHover = combineTags(spanHover,spanLine1);
            spanHover = combineTags(spanHover, spanLine2);

            spanSubtext = combineTags(spanSubtext,spanDefault);
            spanSubtext = combineTags(spanSubtext, spanHover);
            spanSubtextBoxInBox = combineTags(spanSubtextBoxInBox, spanSubtext);

            spanPrimaryText.InnerHtml = "Plan in " + ProvinceName;

            spanTextinBox = combineTags(spanTextinBox,spanPrimaryText);
            spanTextinBox = combineTags(spanTextinBox, spanActionText);

            spanDeleteConf = combineTags(spanDeleteConf,spanQ);
            spanDeleteConf = combineTags(spanDeleteConf, btnCancel);
            spanDeleteConf = combineTags(spanDeleteConf, btnDelete);
            spanDeleteConf = combineTags(spanDeleteConf, spanCaution);

            spanBox = combineTags(spanBox, spanTextinBox);
            spanBox = combineTags(spanBox,spanSubtextBoxInBox);
            spanBox = combineTags(spanBox,spanDeleteConf);

            blockSmPlan = combineTags(blockSmPlan,spanDestImage);
            blockSmPlan = combineTags(blockSmPlan,spanBox);
            blockSmPlan = combineTags(blockSmPlan,spanDeleteIcon);
            return MvcHtmlString.Create(blockSmPlan.ToString(TagRenderMode.Normal));
        }

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