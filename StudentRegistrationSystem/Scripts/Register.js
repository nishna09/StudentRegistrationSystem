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

    var passwordMatch = false;
    var emailAddressAvailable = false;


    $("button#btnRegister").click(function () {
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
        if (!passwordMatch) {
            toastr.error("Passwords do not match!");
        }

        if (!emailAddressAvailable) {
            toastr.error("This email address is already registered!");
        }

        if ($("#password").val().length < 6) {
            validValues = false;
            toastr.error("Password must be at least 6 characters long!");
        }

        

        if (validValues && passwordMatch && emailAddressAvailable) {
            var student = {
                FirstName: $("#firstname").val(),
                LastName: $("#lastname").val(),
                NationalID: $("#NID").val(),
                DateOfBirth: $("#dob").val(),
                ContactNumber: phone,
            };
            var user={
                EmailAddress: $("#emailAddress").val(),
                Password: $("#password").val(),
                Stud: student
            }

            postData(user, "/Users/RegisterStudent").then((response) => {
                if (response.Success) {
                    toastr.success("Successful registration!")
                    setTimeout(redirect, 3000);
                    
                }
                else {
                    toastr.error(response.Message);
                }
            }).catch((error) => {
                console.log(error);
            })

        }
        

    //end of $("button#btnRegister").click function
    });

    $("#confirmPassword").change(function () {

        if ($("#confirmPassword").val() != '') {
            var check = passwordEquality();

            if (!check) {
                $("span.passwordErr").html("Passwords do not match!");
                passwordMatch = false;
            }
            else {
                $("span.passwordErr").html("");
                passwordMatch = true;
            }
        }
        else {
            $("span.passwordErr").html("");
        }
    });

    $("#password").change(function () {

        if ($("#confirmPassword").val() != '') {
            var check = passwordEquality();

            if (!check) {
                $("span.passwordErr").html("Passwords do not match!");
                passwordMatch = false;
            }
            else {
                $("span.passwordErr").html("");
                passwordMatch = true;
            }
        }
        else {
            $("span.passwordErr").html("");
        }
    });

    $("#emailAddress").change(function () {
        if ($("#emailAddress").val() != '') {
            emailCheck().then((response) => {
                if (response.result) {
                    $("span.emailErr").html("");
                    emailAddressAvailable = true;
                }
                else {
                    $("span.emailErr").html("This email address is already registered!");
                    emailAddressAvailable = false;
                }
            }).catch((error) => {
                console.log(error);
            });
        }
        else {
            $("span.emailErr").html("");
        }
      
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

function emailCheck() {
    var email = $("#emailAddress").val();
    if (email.trim() != '') {
        return postData({ emailAddress: email }, "/Users/EmailAvailability");
    
    }
}

function redirect() {
    window.location.href = "/Login/Index";
}

