var dataTable;
$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": { url: '/admin/company/getall' },
        "columns": [
            { "data": 'name', 'width': "15%" },
            { "data": 'stressAddress', 'width': "20%" },
            { "data": 'city', 'width': "10%" },
            { "data": 'state', 'width': "10%" },
            { "data": 'postalCode', 'width': "10%" },
            { "data": 'phoneNumber', 'width': "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
                    <div class="w-100 btn-group" role="group">
                        <a href = "admin/company/upsert?id=${data}" class="btn btn-primary mx-2">
                             <i class="bi bi-pencil"></i> &nbsp   Edit
                        <a onClick=Deletef('/admin/company/delete/${data}') class=" btn btn-danger mx-2">
                               <i class="bi bi-trash3"></i> &nbsp  Delete
                           </a>
                    </div>
                    `
                },
                'width': "25%"
            }
        ]

    });
}