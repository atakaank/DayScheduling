/* 
 *  Copyright (c) 2012-2015, Inspirock Corporation.
 *  All rights reserved.
 */

/* 
 *  Copyright (c) 2012-2017, Inspirock Corporation.
 *  All rights reserved.
 */

/**
 *  Copyright (c) 2012-2013, Inspirock Corporation.
 *  All rights reserved.
 */

define("modules/lazyload",["jquery"],function(){function g(a){var e=new window.Image;e.onload=function(){a.is("img")?a.attr("src",a.data("url")):a.css("background-image","url('"+a.data("url")+"')")};e.src=a.data("url");h=h.add(a)}function i(){if(b.length){var a=f.scrollTop(),e=a+f.height();b=$($.grep(b,function(c){var c=$(c),b=c.offset().top;return b+c.height()>=a-d&&b<=e+d?(g(c),!1):!0}))}}function j(a){a.onscroll?(b=$(".lazyload").not(h),i(),f.off("scroll.ll resize.ll").on("scroll.ll resize.ll",
i)):$(".lazyload").not(h).not(b).each(function(){g($(this))})}var k={},h=$(),b=$(),f=$(window),d=50;k.initialize=function(a){a=a||{};if("complete"===document.readyState)j(a);else $(window).on("load",function(){j(a)})};return k});
require(["jquery","jqueryui"],function(){$.widget("ui.dialog",$.ui.dialog,{_create:function(){this.options.closeText="";setTimeout(function(){$("body").addClass("dummy").removeClass("dummy")},1);return this._super()},_title:function(g){this.options.title?g.html(this.options.title):g.html("&#160;")}})});define("jqueryuioverrides",function(){});
require("modules/analytics modules/utils modules/header modules/lazyload modules/events jquery jqueryui jqueryuioverrides jQueryExtensions/planform qtip".split(" "),function(g,i,j,k,h){function b(a,b){var c=a.qtip("api"),d;c&&(d=c.options.content.text);!c||d!==b?a.qtip({content:b,overwrite:!0,position:{my:"top left",at:"bottom left",adjust:{x:0},container:$("#planning-form")},style:{classes:"error",tip:{mimic:"top center",offset:20,width:20,height:10}},hide:!1}).qtip("show"):a.qtip("show")}var f=
{},d;f.initialize=function(){g.setPageType("New Plan Page");g.trackPageView();h.triggerEvent("home","mmt_track_page",{channel:"inspirock",page:"NewPlan:Inspirock"});d=$(".planning-form");d.preparePlanForm("newplan.form",$("#progress-indicator"),$("#shade"));var a=i.getParameterByName("error-domains"),e=i.getParameterByName("error-startDate"),c=i.getParameterByName("error-endDate");if(a){var f=d.find(".destinations").find(".domain").first();b(f,a)}e&&(a=d.find(".startDate-alt"),b(a,e));c&&(e=d.find(".endDate-alt"),
b(e,c));var l=d.find(".destinations");l.find(".domain").focus(function(){var a=l.find(".domain").first();a.removeClass("error");a.qtip("destroy")});k.initialize();j.initialize()};f.initialize();return f});define("newplan",function(){});