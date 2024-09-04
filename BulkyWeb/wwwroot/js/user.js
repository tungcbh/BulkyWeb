var companyDataTable;

$(document).ready(function () {
    loadDataTable2();
});

function loadDataTable2() {
    companyDataTable = $('#userTable').DataTable({
        "ajax": {
            url: '/Admin/User/GetAll/',
            type: 'GET',
            dataType: 'json'
        },
        "columns": [
            { data: 'name', width: "15%" },
            { data: 'email', width: "25%" },
            { data: 'phoneNumber', width: "15%" },
            { data: 'company.name', width: "15%" },
            { data: 'city', width: "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group"> 
                    <a href="/Admin/Company/Upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a> 
                    <a onClick=Delete('/Admin/Company/Delete/${data}') class="btn btn-secondary mx-2">  <i class="bi bi-trash"></i> </a> 
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
                    companyDataTable.ajax.reload();
                    console.log(data);
                    toastr.success(data.message);
                }
            })
        }
    });
}