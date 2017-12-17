define(['knockout', 'postman', 'webservice'], function (ko, postman, webservice) {

    return function (params) {

        var tag = ko.observable("");
        var postTitle = ko.observable("");

        var specificPostTitle = ko.observable("");
        var specificPostBody = ko.observable("");

        var links = ko.observableArray([]);    // Initially an empty array

        var bodyTextDiv = ko.observable(false);

        var showQuestion = ko.observable(false);

        var showComments = ko.observable(false);
        var currentPostComments = ko.observableArray([]);

        var showAnswers = ko.observable(false);
        var currentPostAnswers = ko.observableArray([]);

        var currentPost = ko.observable("");

        var next = ko.observable();
        var prev = ko.observable();

        var nextAnswers = ko.observable();
        var prevAnswers = ko.observable();

        var nextComments = ko.observable();
        var prevComments = ko.observable();

        var showNext = ko.observable(false);
        var showPrev = ko.observable(false);

        var showNextAnswers = ko.observable(false);
        var showPrevAnswers = ko.observable(false);

        var showNextComments = ko.observable(false);
        var showPrevComments = ko.observable(false);

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

        var getNextAnswers = function () {
            showAnswers(false);
            getAnswers(nextAnswers);
        }

        var getPrevAnswers = function () {
            showAnswers(false);
            getAnswers(prevAnswers);
        }

        var getNextComments = function () {
            showComments(false);
            getComments(nextComments);
        }

        var getPrevComments = function () {
            showComments(false);
            getComments(prevComments);
        }

        var getPostWithTag = function () {
            console.log(tag());

            var myUrl = "http://localhost:5001/api/posts/tag/" + tag();
            links.removeAll();

            var cb = function (data) {
                for (i = 0; i < data.data.length; i++) {
                    links.push(data.data[i]);
                }
                next = data.next;
                prev = data.prev;
                displayNextPrev(data.next, data.prev);
            };

            webservice.getPostQ(myUrl, cb);
        }

        var getPostWithTitle = function () {
            console.log(postTitle());

            var myUrl = "http://localhost:5001/api/posts/title/" + postTitle();
            links.removeAll();

            var cb = function (data) {
                for (i = 0; i < data.data.length; i++) {
                    links.push(data.data[i]);
                }
                next = data.next;
                prev = data.prev;
                displayNextPrev(data.next, data.prev);
            };

            webservice.getPostQ(myUrl, cb);
        }

        var getPostQ = function (myUrl) {

            var cb = function (data) {
                specificPostTitle(data[0].title);
                specificPostBody(data[0].body);
                console.log(data[0]);
                currentPost(data[0]);
            };

            webservice.getPostQ(myUrl, cb);

            if (showQuestion === true) {
                showQuestion(false);
            }
            else {
                showQuestion(true);
            }

            showComments(false);
            
        }

        var getComments = function (url = null) {

            var myUrl = currentPost().commentsLink;

            if (url !== null) {
                myUrl = url;
            }
            else {
                showAnswers(false);
            }
            
            currentPostComments.removeAll();

            if (showComments() === true) {
                showComments(false);
            }
            else {
                showComments(true);
            }

            var cb = function (data) {
                console.log(data);
                for (i = 0; i < data.data.comments.length; i++) {
                    currentPostComments.push(data.data.comments[i]);
                }
                nextComments = data.next;
                prevComments = data.prev;
                displayNextPrevComments(data.next, data.prev);
            };

            webservice.getPostQ(myUrl, cb);
        }

        var getAnswers = function (url = null) {

            currentPostAnswers.removeAll();
            var myUrl = currentPost().answersLink;

            if (url !== null) {
                myUrl = url;
            }

            showComments(false);
            if (showAnswers() === true) {
                showAnswers(false);
            }
            else {
                showAnswers(true);
            }

            var cb = function (data) {

                for (i = 0; i < data.data.length; i++) {
                    currentPostAnswers.push(data.data[i]);
                }
                nextAnswers = data.next;
                prevAnswers = data.prev;
                displayNextPrevAnswers(data.next, data.prev);
                console.log(currentPostAnswers());
            };

            webservice.getPostQ(myUrl, cb);
        };

        var getLinks = function (url) {

            links.removeAll();

            var cb = function (data) {
                for (i = 0; i < data.data.length; i++) {
                    links.push(data.data[i]);
                }
                console.log(data);
                next = data.next;
                prev = data.prev;
                displayNextPrev(data.next, data.prev);
            };

            var myUrl = url;

            if (url == null) {
                myUrl = "http://localhost:5001/api/posts";
            }

            webservice.getPostQ(myUrl, cb);

            
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

        var displayNextPrevAnswers = function (next, prev) {
            if (next != null) {
                showNextAnswers(true);
            }
            else {
                showNextAnswers(false);
            }
            if (prev != null) {
                showPrevAnswers(true);
            }
            else {
                showPrevAnswers(false);
            }
        }

        var displayNextPrevComments = function (next, prev) {
            if (next != null) {
                showNextComments(true);
            }
            else {
                showNextComments(false);
            }
            if (prev != null) {
                showPrevComments(true);
            }
            else {
                showPrevComments(false);
            }
        }

        return {
            links,
            showQuestion,
            getPostWithTag,
            getPostWithTitle,
            postTitle,
            tag,
            specificPostTitle,
            bodyTextDiv,
            getComments,
            getAnswers,
            specificPostBody,
            showComments,
            showAnswers,
            currentPostAnswers,
            currentPostComments,
            showBody,
            getPostQ,
            getNext,
            getPrev,
            getNextAnswers,
            getPrevAnswers,
            getNextComments,
            getPrevComments,
            showNext,
            showPrev,
            showNextAnswers,
            showPrevAnswers,
            showNextComments,
            showPrevComments,
            getLinks,
            displayNextPrev

        };

    }
});