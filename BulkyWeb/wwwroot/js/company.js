var dtTable;
$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    dtTable = $('#tblData').dtTable({
        "ajax": { url: '/admin/company/getall' },
        "column": [



        ]
    })
}