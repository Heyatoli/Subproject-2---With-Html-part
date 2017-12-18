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
        $.ajax({
            url: myUrl,
            type: "DELETE",
            crossDomain: true,
            dataType: 'json',
            async: true,
            processData: false,
            cache: false,
            success: function (textStatus) {
                callback(textStatus);
            }
        });
    };

    var postFunction = function (myUrl, callback, data) {
        $.ajax({
            url: myUrl,
            type: "POST",
            crossDomain: true,
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            processData: false,
            cache: false,
            success: function (data, textStatus, jQxhr) {
                callback(data);
            }
        });
    };

    var updateFunction = function (myUrl, callback, data) {
        $.ajax({
            url: myUrl,
            type: "PUT",
            crossDomain: true,
            data: JSON.stringify(data),
            dataType: 'json',
            async: true,
            processData: false,
            cache: false,
            success: function (textStatus) {
                callback(textStatus);
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