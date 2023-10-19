var dataTable;
var url = window.location.search
$(document).ready(function () {
    var url = window.location.search;
    var urlfull = window.location.href;

    if (url.includes("pending")) {
        loadDataTable("pending");
    } else if (url.includes("paymentpending")) {
        loadDataTable("paymentpending");
    } else if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    } else if (url.includes("completed")) {
        loadDataTable("completed");
    } else if (url.includes("approved")) {
        loadDataTable("approved");
    } else {
        loadDataTable("all");
    }
});
//$(document).ready(function () {
//    var url = window.location.search
//    console.log(url)
//    var urlfull = window.location.href
//    if (url.includes("pending")) {
//        loadDataTable("pending");
//    } else {
//        if (url.includes("paymentpending")) {
//            loadDataTable("paymentpending");
//        }
//    }
//    else {
//        if (url.includes("inprocess")) {
//            loadDataTable("inprocess");
//        }
//    } else {
//        if (url.includes("completed")) {
//            loadDataTable("completed");
//        }
//    } else {
//        if (url.includes("approved")) {
//            loadDataTable("approved");
//        }
//    }
//    else {
//        loadDataTable("all");
//    }
//});


/*
    switch (status)
    {
        case "pending":
            pending = style;
            break;
        case "paymentpending":
            paymentpending = style;
            break;
        case "inprocess":
            inprocess = style;
            break;
        case "completed":
            completed = style;
            break;
        case "approved":
            approved = style;
            break;
        default:
            all = style;
            break;
    }
*/
function loadDataTable(status) {
    dataTable = $('#adminOrderTable').DataTable({
        "ajax": { url: '/admin/order/getAllAPI?status=' + status },
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




/*
var dataTable;

$(document).ready(function () {
var url = window.location.search
var urlfull = window.location.href
if (url.includes("pending"))
loadDataTable("pending");
else if (url.includes("paymentpending")
loadDataTable("paymentpending");
else if (url.includes("inprocess")
loadDataTable("inprocess");
else if (url.includes("completed")
loadDataTable("completed");
else if (url.includes("approved")
loadDataTable("approved");
else loadDataTable("all");

});
function loadDataTable(status) {
dataTable = $('#adminOrderTable').DataTable({
"ajax": { url: '/admin/order/getAllAPI?status=' + status },
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


}*/