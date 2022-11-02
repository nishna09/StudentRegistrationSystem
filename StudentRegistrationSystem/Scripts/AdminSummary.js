$(function () {
    var data = null;
    postGetData(null, "/Admin/GetStudentSummary", "GET").then((response) => {
        if (response) {
            data = response;
            console.log(response);
        }
    }).catch((error) => {
        console.log(error);
    });
    
});
