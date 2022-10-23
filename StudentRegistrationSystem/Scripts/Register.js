//prevents page from reloading when button is clicked in form
$(function () {
    let form = document.querySelector('form');
    const phoneInputField = document.querySelector("#phoneNumber");
    const phoneInput = window.intlTelInput(phoneInputField, {
        initialCountry: "mu",
        utilsScript:
            "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });


    $("span#btnRegister").click(function () {
        var validValues = true;
        var phone = phoneInput.getNumber();
        var today = new Date();
        //verify if number entered is valid
        if (!phoneInput.isValidNumber()) {
            validValues = false;
            toastr.error("Invalid phone number!");
        }
        var dob = Date.parse($("#dob").val()); //get the date as a string. Thus need to parse it as date
        if (dob >= today) {
            validValues = false;
            toastr.error("Invalid date of birth!");
        }

        if (!passwordEquality()) {
            validValues = false;
            toastr.error("Passwords do not match!");
        }

        if (validValues) {

        }

    //end of $("button#btnRegister").click function
    });

    $("#confirmPassword").keyup(function () {
        var check = passwordEquality();
        
        if (!check) {
            $("span.passwordErr").html("Passwords do not match!");
            $("span.passwordErr").css("color", "#D2691E");
        }
        else {
            $("span.passwordErr").html("Passwords match!");
            $("span.passwordErr").css("color", "#FFD700");
        }
    });

    $("#username").keyup(function () {
        usernameCheck().then((response) => {
            if (response.result) {
                $("span.usernameErr").html("Username available!");
                $("span.usernameErr").css("color", "#FFD700");
            }
            else {
                $("span.usernameErr").html("Username not available!");
                $("span.usernameErr").css("color", "#D2691E");
            }
        }).catch((error) => {
            console.log(error);
        });
      
    });

});

function passwordEquality() {
    var match = true;
    var password = $("#password").val();
    var confirmPassword = $("#confirmPassword").val();
    if (password != confirmPassword) {
        match = false;
    }
    return match;
}

function usernameCheck() {
    var userName = $("#username").val();
    if (userName.trim() != '') {
        return postData({ userName: userName }, "UserNameAvailability");
    
    }
}

