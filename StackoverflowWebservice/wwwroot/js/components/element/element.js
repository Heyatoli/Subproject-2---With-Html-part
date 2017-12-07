define(['knockout', 'postman'], function (ko, postman) {
    return function (params) {
        var title = ko.observable(params.name + " from element");
        //console.log(params);

        var back = function() {
            postman.publish(postman.events.changeView, "index");
        };


        return {
            title,
            back
        };

    }
});