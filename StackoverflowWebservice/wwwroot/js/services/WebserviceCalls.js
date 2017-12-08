define([], function () {


    var getPost = function (myUrl, callback) {
        //
        var data = { name: 'hello' };
        callback(data);
        //console.log("!!!");
        //return true;
        //showComments(false);
        //$.ajax({
        //    url: myUrl,
        //    type: "GET",
        //    crossDomain: true,
        //    dataType: 'json',
        //    async: true,
        //    processData: false,
        //    cache: false,
        //    success: fn
        //    //function (data) {
        //    //    fn(data);
        //    //    //console.log(data);
        //    //    //specificPostTitle(data[0].title);
        //    //    //specificPostBody(data[0].body);
        //    //    //currentPostLink(data[0].link);
        //    //}
        //});
    };


    return {
        getPost
    };

});