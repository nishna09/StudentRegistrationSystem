$(function () {
    var statusSet = false;
    var formattedList = null;
    postGetData(null, "/Admin/GetSortedStudents", "GET").then((response) => {
        if (response.IsSetStatus) {
            $("button#btnStatus").hide();
        }
        else {
            formattedList = response;
        }
        AppendResults(response.Students)
    }).catch((error) => {
        console.log(error);
    });
    $("button#btnStatus").click(function () {
        if (formattedList) {
            postGetData(formattedList, "/Admin/BatchUpdateStudentsStatus", "POST").then((response) => {
                if (response.Flag) {
                    toastr.success("Status updated successfully!")
                    setTimeout(redirect, 3000);

                }
                else {
                    toastr.error(response.Message);
                    $("span#updateErr").html(response.Message)
                }
            }).catch((error) => {
                console.log(error);
            });
        }
    });
});
function AppendResults(students) {
    var table = $("table#studentsInfo");
    var tbody = "";
    if (students) {
        for (var indexStudent = 0; indexStudent < students.length; indexStudent++) {
            let numResults = students[indexStudent].Results.length;
            for (var index = 0; index < numResults; index++) {
                let result = students[indexStudent].Results[index];
                if (index == 0) {
                    tbody += `<tr>
                            <td rowspan="${numResults}">${students[indexStudent].FirstName}</td>
                            <td rowspan="${numResults}">${students[indexStudent].LastName}</td>
                            <td> ${ result.SubjectName }</td >
                            <td>${result.Grade}</td>
                            <td rowspan="${numResults}">${students[indexStudent].TotalPoints}</td>
                            <td rowspan="${numResults}">${students[indexStudent].StudentStatus}</td>
                            </tr>`;
                }
                else {
                    tbody += `<tr>
                              <td>${result.SubjectName}</td>
                              <td>${result.Grade}</td>
                              </tr>`;
                }
            }
        }
    }
    else {
        tbody = "<tr>No students!</tr>";
    }
    table.append(tbody);
}
function redirect() {
    window.location.href = "/Home/HomeAdmin";
}