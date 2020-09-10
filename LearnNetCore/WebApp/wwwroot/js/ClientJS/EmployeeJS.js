var tableEmployee = {
    create: function () {
        //debugger;
        // jika table tersebut datatable, maka clear and dostroy
        if ($.fn.DataTable.isDataTable('#MydataTable')) {
            // table yg sudah dibentuk menjadi datatable harus d rebuild lagi
            // untuk di instantiasi ulang
            $('#MydataTable').DataTable().clear();
            $('#MydataTable').DataTable().destroy();
        }
        $.ajax({
            url: '/EmployeeWeb/LoadDivision',
            type: 'get',
            contentType: 'application/json',
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    $('#MydataTable').DataTable({
                        data: res,
                        "columnDefs": [
                            { "orderable": false, "targets": 4 }
                        ],
                        columns: [
                            {
                                title: "No", data: null, render: function (data, type, row, meta) {
                                    return meta.row + meta.settings._iDisplayStart + 1;
                                }
                            },
                            { title: "ID", data: "EmployeeId" },
                            { title: "Username", data: "Username" },
                            { title: "Phone", data: "Phone" },
                            {
                                title: "Create Date",
                                data: "CreateDate",
                                render: function (jsonDate) {
                                    var date = moment(jsonDate).format("DD MMMM YYYY");
                                    return date;
                                }
                            },
                            {
                                title: "Update Date",
                                data: "UpdateDate",
                                render: function (jsonDate) {
                                    if (!moment(jsonDate).isBefore('1000-01-01')) {
                                        var date = moment(jsonDate).format("DD MMMM YYYY");
                                        return date;
                                    }
                                    return "Not Updated Yet";

                                }
                            },
                            { title: "Deleted Date", data: "DeleteDate" },
                            //{ title: "isDelete", data: "isDelete" },
                            {
                                title: "Action", data: null, "paging": false,
                                "ordering": false,
                                "info": false,
                                render: function (data, type, row) {
                                    console.log(data);
                                    return "<button class='btn btn-outline-danger' title='Delete' onclick=formDivision.setDeleteData('" + data.Id + "')><i class='fa fa-lg fa-trash'></i></button>"
                                }
                            }
                        ]
                    });
                } else {
                }
            },
            erorrr: function (err) {
                console.log(err);
            }
        });
    }
};
tableEmployee.create();

var formDivision = {
    setDeleteData: function (id) {
        debugger;
        Swal.fire({
            title: 'Confirmation',
            text: "Do you want to delete this data?",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, I do!',
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: '/EmployeeWeb/Delete/' + id,
                    type: 'post'
                }).then((result) => {
                    if (result.statusCode == 200 || result.statusCode == 201) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Delete Successfully',
                            showConfirmButton: false,
                            timer: 1500,
                        });
                        tableEmployee.create();
                    } else {
                        Swal.fire('Error', 'Failed', 'error');
                    }

                })
            };
        });
    }
};
$(document).ready(function () {
    //selopDepartment.getAllDepartment();
    //selopDepartmentEdit.getAllDepartment();
    //$(".select2").select2();
});

