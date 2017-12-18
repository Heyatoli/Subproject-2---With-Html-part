define(['knockout', 'webservice'], function (ko, webservice) {

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
            getWeightedSearch(next);
        }

        var getPrev = function () {
            getWeightedSearch(prev);
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
                wordCloud(words());
            };
            webservice.getPostQ(myUrl, cb);
        }

        var getWeightedSearch = function (url) {

            var myUrl = url;
            showWordCloud(false);
            if (url == null) {
                myUrl = "http://localhost:5001/api/posts/weights/" + search();
            }

            var cb = function (data) {
                weightedWords.removeAll();
                showWeightedList(true);
                for (i = 0; i < data.data.length; i++) {
                    weightedWords.push(data.data[i]);
                }
                next = data.next;
                prev = data.prev;
                displayNextPrev(data.next, data.prev);
            };
            webservice.getPostQ(myUrl, cb);
        }

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