require.config({

    baseUrl: "js",

    paths: {
        jquery: '../lib/jQuery/dist/jquery.min',
        knockout: '../lib/knockout/dist/knockout',
        text: '../lib/text/text',
        webservice: 'services/WebserviceCalls',
    }
});

require(['knockout'], function (ko) {
    ko.components.register("post", {
        viewModel: { require: "components/post/postVm" },
        template: { require: "text!components/post/post.html" }
    });

    ko.components.register("user", {
        viewModel: { require: "components/user/userVm" },
        template: { require: "text!components/user/user.html" }
    });

    ko.components.register("word", {
        viewModel: { require: "components/words/wordVm" },
        template: { require: "text!components/words/word.html" }
    });

});

require(['knockout'], function(ko) {
    var vm = (function() {
        var currentView = ko.observable('user');
        var switchComponent = function (view) {
            currentView(view);
        }
    
        return {
            currentView,
            switchComponent
        }
    })();

    ko.applyBindings(vm);
});