define(['knockout', 'postman', 'webservice', 'jquery', 'jqcloud2'], function (ko, postman, webservice, $, jq) {

    return function (params) {

        var search = ko.observable("");

        var words = ko.observableArray([]);

        var myCloud = ko.observable("");

        var getSearch = function () {

            var myUrl = "http://localhost:5001/api/posts/words/" + search();

            var cb = function (data) {
                words.removeAll();
                for (i = 0; i < 10; i++) {
                    words.push(data[i]);
                }
                test(words());
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