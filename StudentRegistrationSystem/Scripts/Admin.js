$(function () {
    postGetData(null, "/Admin/GetSortedStudents", "GET").then((response) => {
        AppendResults(response.Students)
    }).catch((error) => {
        console.log(error);
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