﻿define(['knockout', 'postman', 'webservice'], function (ko, postman, webservice) {

    return function (params) {

        var search = ko.observable("");

        var words = ko.observableArray([]);

        var myCloud = ko.observable("");

        var getSearch = function () {

            var myUrl = "http://localhost:5001/api/posts/words/" + search();

            var cb = function (data) {
                for (i = 0; i < data.length; i++) {
                    words.push(data[i]);
                }
                myCloud = jQCloud(words);
            };

            webservice.getPostQ(myUrl, cb);

        }


        return {
            words,
            search,
            getSearch,
            myCloud
            
        };

    }
});