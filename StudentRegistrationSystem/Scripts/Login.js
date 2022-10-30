//prevents page from reloading when button is clicked in form
$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });


    $("button#signupBtn").click(function () {
        var emailAddress = $("#emailAddress").val();
        var password = $("#password").val();
        var obj = { EmailAddress: emailAddress, Password: password };

        postGetData(obj, "/Login/AuthenticateUser","POST").then((response) => {
            
            if (response.Flag) {
                toastr.success("Welcome!");
                window.location.href = response.Url;
            }
            else {
                toastr.error(response.Message);
                $("span#loginEr").html(response.Message);
            }
        })
            .catch((error) => {
                toastr.error("Unable to authenticate. Please try again!");
                $("span#loginEr").html("Unable to authenticate. Please try again!");
            });
    });

    
});



