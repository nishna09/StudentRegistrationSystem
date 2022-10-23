//prevents page from reloading when button is clicked in form
$(function () {
    let form = document.querySelector('form');
    const phoneInputField = document.querySelector("#phoneNumber");
    
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });


    $("button#signupBtn").click(function () {
        var username = $("#username").val();
        var password = $("#password").val();
        var obj = { Username: username, Password: password };

        postData(obj, "Login/AuthenticateUser").then((response) => {
            
            if (response.result) {
                toastr.success("Welcome!");
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



