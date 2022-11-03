$(function () {
	var data = null;
	postGetData(null, "/Admin/GetStudentSummary", "GET").then((response) => {
		if (response) {
			data = response;
			console.log(response);
			AppendTable(response);
		}
	}).catch((error) => {
		console.log(error);
	});
	$("#btnExport").click(function () {
		ExportData(data);
	});

});

function AppendTable(studentSummary) {
	var table = $("table#studentsInfo");
	var tbody = "";
	var thead = "";
	if (studentSummary) {
		var count = 0;
		thead += '<thead><tr>'
		for (var index = 0; index < studentSummary.length; index++) {
			thead += `<td>${studentSummary[index].Status}</td>`;
			for (var indexStud = count; indexStud < studentSummary[index].Students.length; indexStud++) {
				tbody += "<tr>"
				for (var indexSts = 0; indexSts < studentSummary.length; indexSts++) {
					tbody += `<td>`;
					if (indexStud <= studentSummary[indexSts].Students.length - 1) {
						tbody += `${studentSummary[indexSts].Students[indexStud]}`;
					}
					tbody += `</td>`;
				}
				tbody += "</tr>";
				count = indexStud;
			}
		}
		thead += '</tr></thead>'
		table.append(thead);
		table.append(tbody);
	}
}

function ExportData(studentSummary) {
	var headers = '';
	var data = '';
	var count = 0;
	for (var index = 0; index < studentSummary.length; index++) {
		headers += studentSummary[index].Status;
		headers += ',';
		for (var indexStud = count; indexStud < studentSummary[index].Students.length; indexStud++) {
			for (var indexSts = 0; indexSts < studentSummary.length; indexSts++) {
				if (indexStud <= studentSummary[indexSts].Students.length - 1) {
					data += studentSummary[indexSts].Students[indexStud];
				}
				data += ',';
			}
			data += '\n';
			count = indexStud;
		}
	}
	headers.slice(0, -1);
	headers += '\n';

	var csv2 = headers;
	csv2 += data;
	var hiddenElement = document.createElement('a');
	hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv2);
	hiddenElement.target = '_blank';
	hiddenElement.download = 'StudentSummary.csv';
	hiddenElement.click();
}