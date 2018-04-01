/* 
 *  Copyright (c) 2012-2014, Inspirock Corporation.
 *  All rights reserved.
 */

/* 
 *  Copyright (c) 2012-2015, Inspirock Corporation.
 *  All rights reserved.
 */

/* 
 *  Copyright (c) 2012-2017, Inspirock Corporation.
 *  All rights reserved.
 */

/* 
 *  Copyright (c) 2012-2016, Inspirock Corporation.
 *  All rights reserved.
 */

define("modules/dock",["modules/events","jquery"],function(i){var g={},c={},f=[],b=$("#planHeader").height();g.dockUndockViews=function(){var a,l=$(window).height(),e=$(window).scrollTop()-$("#pageWrapper").offset().top;$("#planHeader");var m=$("footer").is(":visible"),l=$(window).height(),g=$(window).scrollTop(),i=$("footer").offset().top;for(a in c)if(c.hasOwnProperty(a)){var h=c[a][0];if(-1===f.indexOf(a)&&h.is(":visible")){var d=c[a][1],j=d.dockTopAt&&d.dockTopAt.constructor&&d.dockTopAt.call&&
d.dockTopAt.apply?d.dockTopAt():d.dockTopAt,t=d.dockBottomAt&&d.dockBottomAt.constructor&&d.dockBottomAt.call&&d.dockBottomAt.apply?d.dockBottomAt():d.dockBottomAt,k=d.shouldDockBottom,r=d.canScroll,n=h.prop("scrollHeight"),q=d.contentHeight,s=d.scrollHeight,p=m?g+l-i:-1;0<p&&(q-=p);var q=n>q,u=e+b+n+23;d.subNav&&(u+=d.subNav.height());if(q&&!r)h.removeClass("dock-top"),h.removeClass("dock-bottom"),s||h.removeClass("scrollable");else if(e<j&&(0>p||!h.hasClass("scrollable")))s||h.removeClass("scrollable"),
h.removeClass("dock-top"),h.removeClass("dock-bottom");else if(k&&u>t)h.hasClass("dock-bottom")||(h.addClass("dock-bottom"),h.removeClass("dock-top"));else if(e>j||0<p)k=h.hasClass("dock-top"),k||(h.addClass("dock-top"),h.removeClass("dock-bottom")),e<j&&k&&h.removeClass("dock-top"),j=Math.min(l,t-e+23),j-=b,j-=23,j-=23,d.subNav&&(j-=d.subNav.height()),r&&(n>=j||q)?(0<p&&(j-=p),h.css({"max-height":j+"px"}),h.addClass("scrollable")):r&&h.hasClass("scrollable")&&(h.css({"max-height":""}),h.removeClass("scrollable"))}}};
g.dockView=function(a,b,e){e.dockBottomAt=void 0!==e.dockBottomAt?e.dockBottomAt:Number.MAX_VALUE;e.contentHeight=void 0!==e.contentHeight?e.contentHeight:Number.MAX_VALUE;e.shouldDockBottom=e.shouldDockBottom||!1;e.canScroll=e.canScroll||!1;c[a]=[b,e]};g.undockView=function(a){c[a]&&(c[a][0].removeClass("dock-top").removeClass("dock-bottom").removeClass("scrollable").css({"max-height":""}),delete c[a])};g.disableDock=function(a){-1===f.indexOf(a)&&f.push(a);c[a]&&c[a][0].removeClass("dock-top").removeClass("dock-bottom").removeClass("scrollable").css({"max-height":""})};
g.enableDock=function(a){a=f.indexOf(a);-1<a&&f.splice(a,1)};g.changeDockAttr=function(a,b){if(c[a])for(var e in b){var m=e,f=b[e];if(c[a]){var i=c[a][1];i.hasOwnProperty(m)&&(i[m]=f)}}g.dockUndockViews()};g.initialize=function(){i.addObserver("scroll","dock",g.dockUndockViews)};return g});
define("modules/view",["modules/events","modules/utils","modules/dock","jquery"],function(i,g){var c={},f;c.scrollCalendarElementIntoViewInModal=function(b,a,c){b=b instanceof jQuery?b:$(document.getElementById(b));b.length&&(a=c?c.parents(".dlg-show-overflow-calendar"):void 0,a=parseInt(a.css("padding-top"),10)+parseInt(a.css("top"),10),a=c.offset().top+a+c.find("#calendarBox .fc-head .fc-row thead").height()+c.find("#calendarStayBox").height(),b=b.offset().top-a,c.scrollTop(b))};c.scrollCalendarElementIntoView=
function(b,a){var c=b instanceof jQuery?b:$(document.getElementById(b));if(c.length){void 0===a&&(a=0);var e=$("#itinerarySubnavPane").outerHeight()+$("#planHeader").outerHeight()+$("#calendarBox .fc-head .fc-row thead").outerHeight()+$("#calendarStayBox").outerHeight(),c=c.offset().top-e+a,e=$("footer").offset().top-$(window).height(),c=Math.min(e,c);localStorage&&!localStorage.getItem("firstTimeLoadOfCalendarPage")?($("html").animate({scrollTop:c},"slow"),localStorage.setItem("firstTimeLoadOfCalendarPage",
!0)):window.scrollTo(0,c)}};c.scrollDayByDayElementIntoView=function(b,a){var c=b instanceof jQuery?b:$(document.getElementById(b));c.length&&(c=c.offset().top-$("#planHeader").height()-parseInt($("#itineraryMainPane").css("margin-top"))-parseInt($("#daybyday").css("margin-top"))+10,f=!0,$("html, body").animate({scrollTop:c},a?500:0,function(){f=!1}))};c.scrollElementIntoView=function(b,a,c,e){b=b instanceof jQuery?b:$(document.getElementById(b));if(b.length){void 0===a&&(a=0);var m=b.offset().left,
g=b.offset().top,i=b.offset().left+b.outerWidth(),h=b.offset().top+b.outerHeight(),d=window.pageXOffset,j=window.pageYOffset,t=window.pageXOffset+$(window).width(),k=window.pageYOffset+$(window).height();if(m>=d&&i<=t&&g>=j&&h<=k)e&&e();else if(m=0<$(".modal-box").length,b=b.offset().top,m&&(b+=$(".modal-box .ui-dialog-content").scrollTop()),a=b-10+a,f=!0,c)window.scrollTo(0,a),f=!1,e&&e();else{var r=!1;(m?$(".modal-box .ui-dialog-content"):$("html, body")).animate({scrollTop:a},500,function(){f=
!1;!r&&e&&(e(),r=!0)})}}};c.scrollAndUpdate=function(){i.triggerEvent("view","scroll",{manualScrollEvent:f})};c.initialize=function(){var b;if(b=window.addEventListener){var a=!1;try{var f=Object.defineProperty({},"passive",{get:function(){a=!0}});window.addEventListener("test",null,f)}catch(e){}b=a}b?window.addEventListener("scroll",function(){c.scrollAndUpdate()},{passive:!0}):$(window).scroll(function(){c.scrollAndUpdate()});g.enableMouseWheel();i.addObserver("scroll-first-visit","view",function(a,
b){b.$dialog?c.scrollCalendarElementIntoViewInModal(b.element,b.additionalYOffset,b.$dialog):c.scrollCalendarElementIntoView(b.element,b.additionalYOffset)})};return c});
define("modules/lazyload",["jquery"],function(){function i(a){var c=new window.Image;c.onload=function(){a.is("img")?a.attr("src",a.data("url")):a.css("background-image","url('"+a.data("url")+"')")};c.src=a.data("url");b=b.add(a)}function g(){if(a.length){var b=l.scrollTop(),c=b+l.height();a=$($.grep(a,function(a){var a=$(a),h=a.offset().top;return h+a.height()>=b-e&&h<=c+e?(i(a),!1):!0}))}}function c(c){c.onscroll?(a=$(".lazyload").not(b),g(),l.off("scroll.ll resize.ll").on("scroll.ll resize.ll",
g)):$(".lazyload").not(b).not(a).each(function(){i($(this))})}var f={},b=$(),a=$(),l=$(window),e=50;f.initialize=function(a){a=a||{};if("complete"===document.readyState)c(a);else $(window).on("load",function(){c(a)})};return f});
define("modules/planTileEventRegister","modules/analytics modules/analyticsEventParams modules/events modules/ajax modules/utils seo/landing modules/view modules/lazyload jquery jqueryui".split(" "),function(i,g,c,f,b,a,l,e){function m(a){$(this).parents(".block").addClass("deleting");a.stopPropagation();return!1}function v(a){var b=$(this).parents(".block");$(this).parents(".duplicate").qtip("destroy");var e=a.data.from,h=a.data.analyticsCategory;if(!d){d=!0;var n=b.parents(".blocks"),q=b.data("planId"),
s=n.find(".block.plan");n.data("maxBlocks")&&s.length===parseInt(n.data("maxBlocks"))&&s.last().remove();var p=$('<div class="block loading"><div class="box"><span class="text in-box"><img class="progress-indicator" src="'+window.ajaxLoaderPath+'" alt="loading"><span class="primarytext">Duplicating...</span></span></div></div>');n.prepend(p);l.scrollElementIntoView(p,-120);f.makeAJAXcall({url:window.contextPath+"/duplicatePlan/"+q,type:"POST",success:function(a){var f;f=a.split("/");for(var g,d=0;d<
f.length;d++)if("plan"===f[d]){g=f[d+1];break}if(g){f=g.split("-");g=[];for(d=f.length-5;d<f.length;d++)g.push(f[d]);f=g.join("-")}else f=void 0;p.remove();d=b.clone(!0,!0);d.attr("href",a).css({opacity:0});d.attr("data-plan-id",f).data("planId",f);d.find(".updated span").text("LAST UPDATED: Less than a minute ago");window.isMobile?(a=$('<div class="col-12"></div>'),a.append(d),n.prepend(a)):n.prepend(d);d.animate({opacity:1},500);d.find(".delete svg").click(m);d.find(".duplicate svg").click({from:e,
analyticsCategory:h},v);window.isMobile||w(n.parents(".myplans,.myplans-main-pane"));a=n.parents(".myplans,.myplans-main-pane");d=a.data("numPlans");d+=1;a.attr("data-num-plans",d).data("numPlans",d);a.find(".myplans-title a, .plan-section .count, .page-title-area .count").text("("+d+")");c.triggerEvent(e,"plan-add",{numPlans:d})},complete:function(){d=!1}});i.trackEvent(h,g.ACT_DUP_PLAN)}a.stopPropagation();b.blur();return!1}function w(a){a.find(".duplicate").qtip({overwrite:!0,content:"DUPLICATE",
position:{my:"center right",at:"center left"},show:{event:"mouseover"},hide:{event:"mouseout",delay:50},style:{classes:"noShadow noBorder icon-tip"}})}var h={},d=!1;h.preparePlanTiles=function(d,l,k){k.off("click.newplan").on("click.newplan",".new-plan",function(){a.showPlanFormDialog({analyticsCategoryPrefix:l})}).off("click.block").on("click.block",".block",function(){if($(this).hasClass("deleting"))return!1}).off("click.cancel").on("click.cancel",".block .delete-conf .cancel",function(a){$(this).parents(".block").removeClass("deleting");
a.stopPropagation();return!1}).off("click.delete").on("click.delete",".block .delete",function(a){$(this).parents(".block").addClass("deleting");a.stopPropagation();return!1}).off("click.confdelete").on("click.confdelete",".block .confirm",function(a){$(this).attr("disabled","disabled").text("Deleting...");var b=$(this).parents(".block"),e=b.data("planId"),m=(b=b.data("owner"))?g.ACT_DEL_PLAN:g.ACT_DEL_SHARED_PLAN;f.makeAJAXcall({url:b?window.contextPath+"/trip/"+e+"/delete":window.contextPath+"/user/sharedplans/"+
e+"/delete",type:"POST",data:{view:d},success:function(a){a=$(a||"<div></div>");k.html(a.html());a=a.data("numPlans");k.attr("data-num-plans",a).data("numPlans",a);c.triggerEvent(d,"plan-delete",{numPlans:a});i.trackEvent(l,m,"",function(){"myPlans"===d&&0===k.find(".block").length?window.location=window.contextPath+"/":0===k.find(".block").length?window.location.reload():h.preparePlanTiles(d,l,k)})}});a.stopPropagation();return!1}).off("click.plan").on("click.plan",".block",function(a){var b=$(this).attr("href");
if(b)return i.trackEvent(l,"plan-tile-click",null,function(){window.location=b}),a.stopPropagation(),!1});k.find(".delete svg").click(m);k.find(".duplicate svg").click({from:d,analyticsCategory:l},v);window.isMobile||w(k);b.ellipsizePlanNames($(".block.plan"));navigator.userAgent.match(/(iPhone|iPod|iPad|Android|BlackBerry)/)&&k.find(".plan.block").addClass("tablet");e.initialize()};return h});
define("modules/myplans","modules/ajax modules/planTileEventRegister jquery jqueryui jqueryuioverrides qtip".split(" "),function(i,g){function c(){var a={sendPlanReminderEmails:$("#sendReminders").is(":checked")};i.makeAJAXcall({url:window.contextPath+"/user/preferences",type:"POST",data:a})}var f={},b;f.setupActions=function(a,f){var e={view:a,analyticsCategory:f};b=$("#pageContent .myplans-content");b.off("click.sendReminderEmails").on("click.sendReminderEmails",".send-reminder-emails",e,c);b.length&&
"myplans"===a&&i.makeAJAXcall({url:window.contextPath+"/myPlans",type:"GET",success:function(a){$("#progress").addClass("hidden");$(".myplans-content").html(a).removeClass("hidden");0===$(".myplans-content").find(".block").length?window.location=window.contextPath+"/":g.preparePlanTiles("myPlans","myplans",$(".myplans-main-pane"))}})};return f});
require("modules/analytics modules/header modules/myplans jquery jqueryui jqueryuioverrides qtip".split(" "),function(i,g,c){var f={initialize:function(){c.setupActions("myplans","myplans");i.setPageType("My Plans Page");i.trackPageView();g.initialize()}};f.initialize();return f});define("myplans",function(){});