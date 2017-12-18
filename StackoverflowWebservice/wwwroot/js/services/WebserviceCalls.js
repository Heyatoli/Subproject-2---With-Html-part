define([], function () {
    var getPostQ = function (myUrl, callback) {
        $.ajax({
            url: myUrl,
            type: "GET",
            crossDomain: true,
            dataType: 'json',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                callback(data);
            }
        });
    };

    var deleteFunction = function (myUrl, callback) {
        console.log("!!");
        $.ajax({
            url: myUrl,
            type: "DELETE",
            crossDomain: true,
            dataType: 'json',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                callback(data);
            }
        });
    };

    var postFunction = function (myUrl, callback) {
        $.ajax({
            url: myUrl,
            type: "POST",
            crossDomain: true,
            dataType: 'json',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                callback(data);
            }
        });
    };

    var updateFunction = function (myUrl, callback, data) {
        console.log(data);
        $.ajax({
            url: myUrl,
            type: "PUT",
            crossDomain: true,
            data: JSON.stringify(data),
            dataType: 'json',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                callback(data);
            }
        });
    };

    return {
        getPostQ,
        deleteFunction,
        postFunction,
        updateFunction
    }; 


});