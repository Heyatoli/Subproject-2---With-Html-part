define(['knockout', 'postman', 'webservice'], function (ko, postman, webservice) {

    return function (params) {

        var title = ko.observable("Titel på noget andet!");

        var username = ko.observable("");
        
        var links = ko.observableArray([]);    // Initially an empty array

        var bodyTextDiv = ko.observable(false);

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

        var getUsername = function () {
            console.log(username());

            var myUrl = "http://localhost:5001/api/users/" + username();
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
                myUrl = "http://localhost:5001/api/users";
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

        return {
            links,
            getUsername,
            //postTitle,
            //tag,
            //specificPostTitle,
            bodyTextDiv,
            //getComments,
            //getAnswers,
            //specificPostBody,
            //showComments,
            //showAnswers,
            //currentPostAnswers,
            //currentPostComments,
            showBody,
            //getPostQ,
            getNext,
            getPrev,
            //getNextAnswers,
            //getPrevAnswers,
            //getNextComments,
            //getPrevComments,
            title,
            showNext,
            showPrev,
            //showNextAnswers,
            //showPrevAnswers,
            //showNextComments,
            //showPrevComments,
            getLinks,
            displayNextPrev

        };

    }
});