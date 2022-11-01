$(function () {
    postGetData(null, "/Admin/GetSortedStudents", "GET").then((students) => {
        if (students) {
            alert(students[0]);
        }
    }).catch((error) => {
        console.log(error);
    });
});