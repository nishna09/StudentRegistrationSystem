//prevents page from reloading when button is clicked in form
$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});

function signIn() {

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
            //toastr.error("Unable to authenticate. Please try again!");
            toastr.error(error);
        })
    //toastr.info('Sign in!!');
   
}

function goRegister() {
    var url = Url.Action("Registration", "LoginRegister")
    window.location = url;
}

