var dataTable;
$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": { url: '/admin/company/getall' },
        "columns": [
            { data: 'id', 'width': "10%" },

            { data: 'name', 'width': "15%" },
            { data: 'stressAddress', 'width': "15%" },
            { data: 'city', 'width': "15%" },
            { data: 'state', 'width': "15%" },
            { data: 'postalCode', 'width': "15%" },
            { data: 'phoneNumber', 'width': "15%" },
            /* {
                 data: 'id',
                 "render": function (data) {
                     return `
                     <div class="">
                     
                     </div>
                     `
                 }
             }*/
        ]
    });
}