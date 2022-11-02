function postGetData(dataObj, urlGetPost, callType) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: callType,
            url: urlGetPost,
            data: dataObj,
            dataType: "json",
            success: function (result) {
                resolve(result)
            },
            error: function (error) {
                reject(error)
            }
        })
    });
}