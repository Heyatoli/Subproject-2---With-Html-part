define([], function () {


    var getPostQ = function (myUrl, callback) {
        //
        
        console.log("!!!");
        $.ajax({
            url: myUrl,
            type: "GET",
            crossDomain: true,
            dataType: 'json',
            async: true,
            processData: false,
            cache: false,
            success: callback(data)
        });
    };


    return {
        getPostQ
    };

});