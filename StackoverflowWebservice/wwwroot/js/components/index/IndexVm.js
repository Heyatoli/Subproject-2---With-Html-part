define(['knockout', 'postman'], function (ko, postman) {
    return function (params) {

        var title = ko.observable("Titel!");

        var specificPostTitle = ko.observable("");
        var specificPostBody = ko.observable("");

        var links = ko.observableArray([]);    // Initially an empty array

        var next = ko.observable();
        var prev = ko.observable();

        var showNext = ko.observable(false);
        var showPrev = ko.observable(false);

        var getNext = function () {
            getLinks(next);
        }

        var getPrev = function () {
            getLinks(prev);
        }

        var getPost = function (myUrl) {
            console.log(myUrl);
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
                    specificPostTitle(JSON.stringify(data.title));
                    specificPostBody(JSON.stringify(data.text));
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
            specificPostTitle,
            specificPostBody,
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