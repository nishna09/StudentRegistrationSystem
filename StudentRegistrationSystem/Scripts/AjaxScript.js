function postGetData(dataObj, urlPost, callType) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: callType,
            url: urlPost,
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