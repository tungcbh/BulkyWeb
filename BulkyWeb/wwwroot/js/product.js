var productDataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    productDataTable = $('#productTable').DataTable({
        "ajax": {
            url: '/Admin/Product/GetAll/',
            type: 'GET',
            dataType: 'json'
        },
        "columns": [
            { data: 'title', width: "25%" },
            { data: 'isbn', width: "20%" },
            { data: 'author', width: "20%" },
            { data: 'price', width: "10%" },
            { data: 'category.name', width: "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group"> 
                    <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a> 
                    <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-secondary mx-2">  <i class="bi bi-trash"></i> </a> 
                    </div>`
                },
                width: "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    productDataTable.ajax.reload();
                    console.log(data);
                    toastr.success(data.message);
                }
            })
        }
    });
}