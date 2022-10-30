$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    postGetData(null, "/Result/GetAllSubjects", "GET").then((subjects) => {
        if (subjects) {
            for (var i = 0; i < subjects.length; i++) {
                $('select.subjects').append(`<option value="${subjects[i].SubjectId}">${subjects[i].SubjectName}</option>`);
            }
        }
    }).catch((error) => {
        console.log(error);
    });

    $("button#updateBtn").click(function(){
        var result = [];
        var proceed = true;
        if ($("select#subject1").val() != -1 && $("select#grade1").val() != -1) {
            result.push({ SubjectId: parseInt($("select#subject1").val()), Grade: $("select#grade1").val() });
        }
        if ($("select#subject2").val() != -1 && $("select#grade2").val() != -1) {
            result.push({ SubjectId: parseInt($("select#subject2").val()), Grade: $("select#grade2").val() });
        }
        if ($("select#subject3").val() != -1 && $("select#grade3").val() != -1) {
            result.push({ SubjectId: parseInt($("select#subject3").val()), Grade: $("select#grade3").val() });
        }
        if (result.length == 0) {
            $("span#resultErr").html("At least one subject and grade must be entered!")
            toastr.error("At least one subject and grade must be entered!");
            proceed = false;
        }
        if (proceed) {
            var dataObj = {
                Results: result,
                Address: {
                    Street: $("#street").val(),
                    City: $("#city").val(),
                    Country: $("#country").val()
                },
                GuardianName: $("#GuardianName").val()
            };
            postGetData(user, "/Users/UpdateStudentDetails", "POST").then((response) => {
                if (response.Flag) {
                    toastr.success("Details updated successfully!")
                    setTimeout(redirect, 3000);
                }
                else {
                    toastr.error(response.Message);
                    $("span#updateEr").html(response.Message)
                }
            }).catch((error) => {
                console.log(error);
                $("span#updateEr").html("Unable to add details. Please try again!")
            })
        }
    });

});

function redirect() {
    window.location.href = "/Home/HomeStudent";
}