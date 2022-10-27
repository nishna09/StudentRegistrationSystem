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

        postData(obj, "/Login/AuthenticateUser").then((response) => {
            
            if (response.Flag) {
                toastr.success("Welcome!");
                window.location.href = response.Url;
            }
            else {
                toastr.error(response.Message);
            }
        })
            .catch((error) => {
                toastr.error("Unable to authenticate. Please try again!");
            });
    });

    
});



