/**
 *  Copyright (c) 2012-2013, Inspirock Corporation.
 *  All rights reserved.
 */

require(["modules/analytics","modules/header","modules/utils","modules/ajax","jquery"],function(h,m,g,n){function d(a){13===a.keyCode&&(a.stopPropagation(),e.trigger("click"))}var f={},e,i,b,c,j,p=function(){0<b.val().trim().length?g.validateEmailField(b):b.qtip("destroy")},l=function(){if(""===c.val().trim()){var a=c,b=a.qtip("api"),k;b&&(k=b.options.content.text);!b||"Please enter your Feedback."!==k?a.qtip({content:"Please enter your Feedback.",position:{my:"top left",at:"bottom left",adjust:{x:40}},
style:{classes:"error",tip:{mimic:"top center",offset:20,width:20,height:10}},hide:!1}).qtip("show"):a.qtip("show");return!1}c.qtip("destroy");return!0},q=function(){""!==c.val().trim()?l():c.qtip("destroy")},r=function(){var a=$("#feedbackForm"),b=a.find(".name"),c=a.find(".email"),d=a.find(".feedback-area"),e=a.find("input[name=willRecommend]");if(g.validateEmailField(c)&&l()){a={};a.personName=b.val().trim();a.emailAddress=c.val().trim();a.feedback=d.val().trim();var f=e.filter(":checked");a.willRecommend=
0<f.length?f.val():"";n.makeAJAXcall({type:"POST",data:a,url:window.contextPath+"/feedback",success:function(){b.val("");c.val("");d.val("");e.filter(":checked").prop("checked",!1);g.showOkDialog("Feedback","Thank you for your feedback.","OK")}})}};f.initialize=function(){e=$("#submit-button");var a=$("#feedbackForm");i=a.find(".name");b=a.find(".email");c=a.find(".feedback-area");j=a.find("input[name=willRecommend]");h.setPageType("Feedback Page");h.trackPageView();e.click(r);b.blur(p);c.blur(q);
i.keypress(d);b.keypress(d);j.keypress(d);m.initialize()};f.initialize();return f});define("feedback",function(){});