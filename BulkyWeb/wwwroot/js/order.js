var orderDataTable;

$(document).ready(function () {
    let url = window.location.search;
    let estring = ["inprocess", "completed", "pending", "approved", "all"];
    for (let status of estring) { 
        if (url.includes(status)) {
            loadDataTable(status);
            break;
        }
    }
});


function loadDataTable(status) {
    orderDataTable = $('#orderTable').DataTable({
        "ajax": {
            url: '/Admin/Order/GetAll?status=' + status,
            type: 'GET',
            dataType: 'json'
        },
        "columns": [
            { data: 'id', width: "10%" },
            { data: 'name', width: "15%" },
            { data: 'phoneNumber', width: "15%" },
            { data: 'applicationUser.email', width: "15%" },
            { data: 'orderStatus', width: "15%" },
            { data: 'orderTotal', width: "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group"> 
                    <a href="/Admin/Order/Details?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-eye"></i> </a> 
                    </div>`
                },
                width: "15%"
            }
        ]
    });
}