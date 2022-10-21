function postData(dataObj,urlPost) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
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