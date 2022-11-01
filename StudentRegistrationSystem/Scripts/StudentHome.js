$(function () {
    postGetData(null, "/Student/CheckIfResultExist", "GET").then((response) => {
        if (response.Flag) {
            $("a#updateProfileLink").hide();
        }
        else {
            $("a#updateProfileLink").show();
        }
    }).catch((error) => {
        console.log(error);
    });
});