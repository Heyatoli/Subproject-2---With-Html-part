﻿define(['knockout', 'postman', 'webservice'], function (ko, postman, webservice) {

    return function (params) {

        var title = ko.observable("Titel!");

        var tag = ko.observable("");

        var displayingTagPosts = false;

        var specificPostTitle = ko.observable("");
        var specificPostBody = ko.observable("");

        var links = ko.observableArray([]);    // Initially an empty array

        var bodyTextDiv = ko.observable(false);

        var showComments = ko.observable(false);
        var currentPostComments = ko.observableArray([]);
        var currentPostLink = ko.observable("");

        var next = ko.observable();
        var prev = ko.observable();

        var showNext = ko.observable(false);
        var showPrev = ko.observable(false);

        var showBody = function () {
            console.log(bodyTextDiv())
            if (bodyTextDiv() == false) {
                bodyTextDiv(true);
            }
            else {
                bodyTextDiv(false);
            }
        }

        var getNext = function () {
            getLinks(next);
        }

        var getPrev = function () {
            getLinks(prev);
        }

        var getPostWithTag = function () {
            console.log(tag());

            var myUrl = "http://localhost:5001/api/posts/tag/" + tag();
            links.removeAll();

            $.ajax({
                url: myUrl,
                type: "GET",
                crossDomain: true,
                dataType: 'json',
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    console.log(data);

                    for (i = 0; i < data.data.length; i++) {
                        links.push(data.data[i]);
                    }
                    console.log(data);
                    next = data.next;
                    prev = data.prev;
                    displayNextPrev(data.next, data.prev);

                }
            });

        }

        var getPost = function (myUrl) {

               //Calling function from Webservice

            var cb = function (data) {
                console.log(data);
                specificPostTitle(data[0].title);
                specificPostBody(data[0].body);
                currentPostLink(data[0].link);
            };

            webservice.getPost(myUrl, cb);



            showComments(false);
            
        }

        var getComments = function () {
            currentPostComments.removeAll();
            $.ajax({
                url: currentPostLink(),
                type: "GET",
                crossDomain: true,
                dataType: 'json',
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    for (i = 0; i < data.comments.length; i++) {
                        currentPostComments.push(data.comments[i]);
                    }
                    showComments(true);
                    console.log(currentPostComments());
                }
            });
        }

        var getLinks = function (url) {

            links.removeAll();

            var myUrl = url;

            if (url == null) {
                myUrl = "http://localhost:5001/api/posts";
            }

            $.ajax({
                url: myUrl,
                type: "GET",
                crossDomain: true,
                dataType: 'json',
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    console.log(data.data[0].link);
                    for (i = 0; i < data.data.length; i++) {
                        links.push(data.data[i]);
                    }
                    console.log(data);
                    next = data.next;
                    prev = data.prev;
                    displayNextPrev(data.next, data.prev);
                }
            });
        };

        var displayNextPrev = function (next, prev) {

            if (next != null) {
                showNext(true);
            }
            else {
                showNext(false);
            }

            if (prev != null) {
                showPrev(true);
            }
            else {
                showPrev(false);
            }

        }

        return {
            links,
            getPostWithTag,
            tag,
            specificPostTitle,
            bodyTextDiv,
            getComments,
            specificPostBody,
            showComments,
            currentPostComments,
            showBody,
            getPost,
            getNext,
            getPrev,
            title,
            next,
            prev,
            showNext,
            showPrev,
            getLinks,
            displayNextPrev

        };

    }
});