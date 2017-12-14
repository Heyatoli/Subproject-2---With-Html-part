﻿define(['knockout', 'postman', 'webservice'], function (ko, postman, webservice) {

    return function (params) {

        var search = ko.observable("");

        var words = ko.observableArray([]);
        var weightedWords = ko.observableArray([]);

        var myCloud = ko.observable("");

        var post = ko.observable("");

        var showWeightedList = ko.observable(false);
        var showWordCloud = ko.observable(false);
        var showPost = ko.observable(false);

        var showNext = ko.observable(false);
        var showPrev = ko.observable(false);

        var getNext = function () {
            getLinks(next);
        }

        var getPrev = function () {
            getLinks(prev);
        }

        var getPost = function (url) {

            var cb = function (data) {
                post(data[0].body);
                console.log(post());
                showPost(true);
            };

            webservice.getPostQ(url, cb);


        };

        var getSearch = function () {

            showWeightedList(false);
            var myUrl = "http://localhost:5001/api/posts/words/" + search();

            var cb = function (data) {
                words.removeAll();
                for (i = 0; i < data.length; i++) {
                    words.push(data[i]);
                }
                showWordCloud(true);
                test(words());
            };
            webservice.getPostQ(myUrl, cb);
        }

        var getWeightedSearch = function () {

            showWordCloud(false);
            var myUrl = "http://localhost:5001/api/posts/weights/" + search();

            var cb = function (data) {
                weightedWords.removeAll();
                showWeightedList(true);
                for (i = 0; i < data.data.length; i++) {
                    weightedWords.push(data.data[i]);
                }
                console.log(data);
            };
            webservice.getPostQ(myUrl, cb);
        }



        return {
            words,
            search,
            showWeightedList,
            showPost,
            getNext,
            getPrev,
            getPost,
            post,
            showNext,
            showPrev,
            getWeightedSearch,
            showWordCloud,
            weightedWords,
            getSearch,
            myCloud
            
        };

    }
});