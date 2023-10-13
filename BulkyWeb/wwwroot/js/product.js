var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columnDefs": [
            { "width": "20%", "targets": 0 }, // 25% width for the first column (index 0)
            { "width": "20%", "targets": 1 }, // 15% width for the second column (index 1)
            { "width": "10%", "targets": 2 }, // 10% width for the third column (index 2)
            { "width": "15%", "targets": 3 }, // 15% width for the fourth column (index 3)
            { "width": "10%", "targets": 4 }, // 10% width for the fifth column (index 4)
            { "width": "25%", "targets": 5 }  // 25% width for the sixth column (index 5)
        ],
        "columns": [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'listPrice'  },
            { data: 'author'  },
            { data: 'category.name'  },
            {
                data: 'id',
                "render": function (data) {
                    return `
                       <div class="w-100 btn-group" role="group">
                           <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                               <i class="bi bi-pencil"></i> &nbsp   Edit
                           </a>
                           <a onClick=Deletef('/admin/product/delete/${data}') class=" btn btn-danger mx-2">
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
