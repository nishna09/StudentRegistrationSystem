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
            
            if (response.result) {
                toastr.success("Welcome!");
                window.location.href = response.url;
            }
            else {
                toastr.error("Incorrect credentials");
            }
        })
            .catch((error) => {
                toastr.error("Unable to authenticate. Please try again!");
            });
    });

    
});



