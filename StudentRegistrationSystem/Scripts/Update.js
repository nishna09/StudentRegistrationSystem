$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    getData(null, '/Result/GetAllSubjects').then((response) => {
        if (response) {
            for (var i = 0; i < response.length; i++){
                $('.subjects').append(`<option value="${response[i].SubjectId}">
                                       ${response[i].SubjectName}
                                  </option>`);
            }
        }
    }).catch((error) => {
        console.log(error);
    });


});