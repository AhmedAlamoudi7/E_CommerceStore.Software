var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "displayOrder", "width": "15%" },



        ]
    });
}
