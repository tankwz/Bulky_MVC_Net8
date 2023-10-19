var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#adminOrderTable').DataTable({
        "ajax": { url: '/admin/order/getAllAPI' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "15%" },
            { data: 'appUser.email', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'paymentStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return ` 
                        <div class="w-75 ">
                            <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-layout-text-window-reverse"></i> Details</a>
                        </div> 
                    `
                },
                "width": "20%"
            }
        ]
    })


}