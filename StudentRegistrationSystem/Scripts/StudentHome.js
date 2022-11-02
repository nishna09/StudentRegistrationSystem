$(function () {
    postGetData(null, "/Student/GetStudent", "GET").then((student) => {
        var table = $("table#studentsInfo");
        var tbody = "";
        if (student) {
            console.log(student);
            if (student.Results) {
                $("a#updateProfileLink").hide();
                for (var index = 0; index < student.Results.length; index++) {
                    tbody += `<tr>
                            <td> ${student.Results[index].SubjectName}</td >
                            <td>${student.Results[index].Grade}</td>
                            </tr>`;
                }
                tbody += `<tr>
                      <td class='labelTitle'>Total Points</td>
                      <td>${student.TotalPoints}</td></tr>
                      <tr><td class='labelTitle'>Status</td>
                      <td>${student.StudentStatus}</td></tr>`
            }
            else {
                $("a#updateProfileLink").show();
                tbody += `<tr>
                            <td colspan='2'>Results need to be added. Click on this<a href='/Student/UpdateProfile'> link</a> to add results!</td >`
            }
            table.append(tbody);
        }
    }).catch((error) => {
        console.log(error);
    });
});