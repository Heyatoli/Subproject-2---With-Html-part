

require.config({

    baseUrl: "js",

    paths: {
        jquery: '../lib/jQuery/dist/jquery.min',
        knockout: '../lib/knockout/dist/knockout',
        text: '../lib/text/text',
        postman: 'services/postman',
        webservice: 'services/WebserviceCalls'
    }
});

require(['knockout'], function (ko) {

    ko.components.register("mylist", {
        viewModel: { require: "components/mylist/mylist" },
        template: { require: "text!components/mylist/mylist_view.html"}
    });

    ko.components.register("my-element", {
        viewModel: { require: "components/element/element" },
        template: { require: "text!components/element/element_view.html" }
    });

    ko.components.register("index", {
        viewModel: { require: "components/index/indexVm" },
        template: { require: "text!components/index/index.html" }
    });

});

require(['knockout', 'postman'], function(ko, postman) {
    var vm = (function() {
        var currentView = ko.observable('index');
        var currentParams = ko.observable("!!!");
        var switchComponent = function() {
            if (currentView() === "mylist") {
                currentView("my-element");
            } else {
                currentView("mylist");
            }

        }

        postman.subscribe(postman.events.changeView,
            viewName => {
                currentParams({ name: "Yahallo!"});
                currentView(viewName);
            });

        return {
            currentView,
            currentParams,
            switchComponent
        }
    })();

    ko.applyBindings(vm);
});