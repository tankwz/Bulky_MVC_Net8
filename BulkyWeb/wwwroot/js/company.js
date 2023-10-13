var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/company/getall' },
        "columnDefs": [
            { "width": "15%", "targets": [1] }, // stressaddress
            { "width": "10%", "targets": [2] }, // city
            { "width": "10%", "targets": [3] }, // state
            { "width": "10%", "targets": [4] }, // postalcode
            { "width": "15%", "targets": [5] }, // phonenumber
            { "width": "30%", "targets": [6] }  // 2button
        ],
        "columns": [
            { "data": 'name' },
            { "data": 'stressAddress' },
            { "data": 'city' },
            { "data": 'state' },
            { "data": 'postalCode' },
            { "data": 'phoneNumber' },
            {
                "data": 'id',
                "render": function (data) {
                    return `
                    <div class="w-100 btn-group" role="group">
                        <a href="/admin/company/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil"></i> &nbsp Edit
                        </a>
                           <a onClick=Deletef('/admin/company/delete/${data}') class=" btn btn-danger mx-2">
                               <i class="bi bi-trash3"></i> &nbsp  Delete
                           </a>
                    </div>
                    `
                }
            }
        ]
    });
} 







function Deletef(url) {

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {

                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}
