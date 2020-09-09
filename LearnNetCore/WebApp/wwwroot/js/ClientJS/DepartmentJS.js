var tableDepartment = {
    create: function () {
        // jika table tersebut datatable, maka clear and dostroy
        if ($.fn.DataTable.isDataTable('#MydataTable')) {
            // table yg sudah dibentuk menjadi datatable harus d rebuild lagi
            // untuk di instantiasi ulang
            $('#MydataTable').DataTable().clear();
            $('#MydataTable').DataTable().destroy();
        }
        $.ajax({
            url: '/DepartmentWeb/LoadDepartments',
            method: 'get',
            contentType: 'application/json',
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    $('#MydataTable').DataTable({
                        data: res,
                        "columnDefs": [
                            { "orderable": false, "targets": 3 }
                        ],
                        columns: [
                            {
                                title: "No.", data: "", render: function (data, type, row, meta) {
                                    return meta.row + meta.settings._iDisplayStart + 1;
                                }
                            },
                            { title: "Nama Department", data: "Name" },
                            { title: "ID", data: "Id" },
                            {
                                title: "Created Date", data: "CreateDate",
                                render: function (jsonDate) {
                                    var date = moment(jsonDate).format("DD MMMM YYYY");
                                    return date;
                                }
                            },
                            {
                                title: "Updated Date", data: "UpdateDate", render: function (jsonDate) {
                                    if (!moment(jsonDate).isBefore('1000-01-01')) {
                                        var date = moment(jsonDate).format("DD MMMM YYYY");
                                        return date;
                                    }
                                    return "Not Updated Yet";

                                }
                            },
                            { title: "Deleted Date", data: "DeleteDate" },
                            { title: "isDelete", data: "isDelete" },
                            {
                                title: "Action", data: null, render: function (data, type, row) {
                                    return "<button class='btn btn-outline-warning' title='delete' onclick=formDepartment.setEditData('" + data.Id + "')><i class='fa fa-lg fa-edit'></i></button>" +
                                        "<button class='btn btn-outline-danger' title='Delete' onclick=formDepartment.setDeleteData('" + data.Id + "')><i class='fa fa-lg fa-trash'></i></button>"
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
var editDiv;
$("#btn-edit").click(function () {
    formDepartment.editSaveDepartment(editDiv);
});
tableDepartment.create();

var formDepartment = {
    saveForm: function () {
        var departments = new Object();
        departments.Id = 0;
        departments.Name = $('#name').val();
        console.log(departments);
        $.ajax({
            url: '/DepartmentWeb/InsertorupdateDepartment/',
            type: 'post',
            dataType: 'json',
            data: departments,
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    tableDepartment.create();
                    toastr.success('Data has been saved');
                    $('#exampleModalCenter').modal('hide');
                    //MydataTable.ajax.reload(null, false);
                } else {
                    toastr.warning(status.msg);
                }
            },
            erorrr: function (err) {
                console.log(err);
            }
        });
    }, setDeleteData: function (id) {
        Swal.fire({
            title: 'Confirmation',
            text: "Do you want to delete this data?",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, I do!',
        }).then((result) => {
            debugger;
            if (result.value) {
                $.ajax({
                    url: '/DepartmentWeb/Delete/' + id,
                    //data: { id: id }
                    method: 'delete'
                }).then((result) => {
                    debugger;
                    if (result.statusCode == 200) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Delete Successfully',
                            showConfirmButton: false,
                            timer: 1500,
                        });
                        tableDepartment.create();
                    } else {
                        Swal.fire('Error', 'Failed', 'error');
                    }

                })
            };
        });
    }, editSaveDepartment: function (editD) {
        debugger;
        editDiv = editD;
        var Dept = new Object();
        //Dept.Id = $('#id').val();
        Dept.Name = $('#name2').val();
        //var myData = {
        //    //Name: document.getElementById("name2").value
        //    Id: $("#id").val(),
        //    Name: $("#name2").val()
        //};
        console.log(Dept);
        $.ajax({
            url: '/DepartmentWeb/InsertorupdateDepartment/' + editD,
            type: 'post',
            //contentType: 'application/json',
            dataType: 'json',
            data: { departments: Dept, id: editD },
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    tableDepartment.create();
                    $('#exampleModalCenter').modal('hide');
                    toastr.success('Data has been modified.');
                    location.reload();
                } else {
                }
            },
            erorrr: function (err) {
                console.log(err);
            }
        });
    }, setEditData: function (editD) {
        editDiv = editD; // new, supaya id bisa dibawa ke fungsi editForm
        console.log(editD);
        $.ajax({
            url: '/DepartmentWeb/GetById/' + editD,
            method: 'get',
            contentType: 'application/json',
            dataType: 'json',
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    $('#name2').val(res.Name);
                    $('#exampleModalCenterEdit').modal('show');
                    //console.log(name);

                } else {

                }
            },
            erorrr: function (err) {
                console.log(err);
            }
        });


    }
};

$(document).on('click', '#btn-save', function (e) {
    e.preventDefault();
    swal({
        title: 'Confirmation',
        input: 'checkbox',
        inputValue: 0,
        inputPlaceholder: 'Do you want to save this data?',
        showCancelButton: true,
        cancelButtonText: 'Cancel',
        confirmButtonText: 'Yes, I do',
        inputValidator: function (result) {
            return new Promise(function (resolve) {
                if (result) {
                    resolve();
                    formDepartment.saveForm();
                } else {
                    resolve("Please check confirmation button")
                }
            })
        }
    }).then(function (result) {
        $('#form-department').submit();

    });
});