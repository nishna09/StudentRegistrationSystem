//prevents page from reloading when button is clicked in form
$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});

function signIn() {
    toastr.info('Sign in!!');
}