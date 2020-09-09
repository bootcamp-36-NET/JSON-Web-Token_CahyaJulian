var tableDivision = {
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
            url: '/DivisionWeb/LoadDivision',
            type: 'get',
            contentType: 'application/json',
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    $('#MydataTable').DataTable({
                        data: res,
                        //"paging": false,
                        //"ordering": false,
                        //"info": false,
                        "columnDefs": [
                            { "orderable": false, "targets": 4 }
                        ],
                        columns: [
                            {
                                title: "No", data: null, render: function (data, type, row, meta) {
                                    return meta.row + meta.settings._iDisplayStart + 1;
                                }
                            },
                            { title: "ID", data: "Id" },
                            { title: "Division Name", data: "Name" },
                            { title: "Department Name", data: "Department.Name" },
                            {
                                data: "CreateDate",
                                render: function (jsonDate) {
                                    var date = moment(jsonDate).format("DD MMMM YYYY");
                                    return date;
                                }
                            },
                            {
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
                            { title: "isDelete", data: "isDelete" },
                            {
                                title: "Action", data: null, "paging": false,
                                "ordering": false,
                                "info": false,
                                render: function (data, type, row) {
                                    console.log(data);
                                    return "<button class='btn btn-outline-warning' title='delete' onclick=formDivision.setEditData('" + data.Id + "')><i class='fa fa-lg fa-edit'></i></button>" +
                                        "<button class='btn btn-outline-danger' title='Delete' onclick=formDivision.setDeleteData('" + data.Id + "')><i class='fa fa-lg fa-trash'></i></button>"
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
    formDivision.editSaveDivision(editDiv);
});
tableDivision.create();

var formDivision = {
    //Create
    saveForm: function () {
        debugger;
        var b = document.getElementById("divName");
        var id1 = b.options[b.selectedIndex].id;
        var myData = {
            Id: id1
        };
        //var div = new Object();
        //div.DepartmentId = myData;
        //div.Name = $('#name').val();

        var divisionVMs = {
            Name: document.getElementById("name").value,
            DepartmentId: myData
        }
        console.log(divisionVMs)
        $.ajax({
            url: '/DivisionWeb/InsertorupdateDivision',
            type: 'post',
            //contentType: 'application/json',
            dataType: 'json',
            data: divisionVMs,
            //data: JSON.stringify(dataDivision),
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    tableDivision.create();
                    //tableDivision.ajax.reload(null, false);
                    toastr.success('Data has been saved');
                    $('#exampleModalCenter').modal('hide');
                } else {
                    toastr.warning(status.msg);
                }
            },
            erorr: function (err) {
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
            if (result.value) {
                $.ajax({
                    url: '/DivisionWeb/Delete/' + id,
                    method: 'post'
                }).then((result) => {
                    if (result.StatusCode == 200) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Delete Successfully',
                            showConfirmButton: false,
                            timer: 1500,
                        });
                        tableDivision.create();
                    } else {
                        Swal.fire('Error', 'Failed', 'error');
                    }

                })
            };
        });
    }, editSaveDivision: function (editD) {
        editDiv = editD;
        var myData = {
            Name: document.getElementById("name2").value,
            DepartmentName : document.getElementById("divName2").value
        }
        //console.log(myData);
        $.ajax({
            url: '/DivisionWeb/InsertorupdateDivision/' + editD,
            type: 'post',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(myData),
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    tableDivision.create();
                    $('#exampleModalCenter').modal('hide');
                    toastr.success('Data has been modified.');
                } else {
                }
            },
            erorr: function (err) {
                console.log(err);
            }
        });
    }, setEditData: function (editD) {
        editDiv = editD; // new, supaya id bisa dibawa ke fungsi editForm
        //console.log(editD);
        $.ajax({
            url: '/DivisionWeb/GetById/' + editD,
            type: 'get',
            contentType: 'application/json',
            dataType: 'json',
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    $('#name2').val(res.Name);
                    $('#divName2').val(res.DepartmentName);
                    $('#exampleModalCenterEdit').modal('show');
                } else {

                }
            },
            erorr: function (err) {
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
                    formDivision.saveForm();
                } else {
                    resolve("Please check confirmation button")
                }
            })
        }
    }).then(function (result) {
        $('#form-division').submit();
    });
});

//dropdown data dept
var selopDepartment = {
    getAllDepartment: function (idAja) {

        $.ajax({
            url: '/DepartmentWeb/LoadDepartments',
            method: 'get',
            contentType: 'application/json',
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    $("#divName").select2();
                    var dynamicSelect = document.getElementById("divName");
                    Array.from(res).forEach(element => {
                        var newOption = document.createElement("option")
                        newOption.setAttribute("id", element.Id);
                        newOption.setAttribute("value", element.Name);
                        newOption.setAttribute("name", "Name");
                        newOption.text = element.Name;
                        dynamicSelect.add(newOption);
                    });
                    //console.log(res);
                    if (idAja != 0) {
                        $("#divName option[id='" + idAja + "']").attr("selected", "selected");
                    }
                } else {
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
};
//dropdown buat edit
var selopDepartmentEdit = {
    getAllDepartment: function (idAja) {

        $.ajax({
            url: '/DepartmentWeb/LoadDepartment',
            method: 'get',
            contentType: 'application/json',
            success: function (res, status, xhr) {
                if (xhr.status == 200 || xhr.status == 201) {
                    $("#divName2").select2();
                    var dynamicSelect = document.getElementById("divName2");
                    $('#divName2').find('option').remove();
                    Array.from(res).forEach(element => {
                        var newOption = document.createElement("option")
                        newOption.setAttribute("id", element.Id);
                        newOption.setAttribute("value", element.Name);
                        newOption.setAttribute("name", "Name");
                        newOption.text = element.Name;
                        dynamicSelect.add(newOption);
                    });
                    console.log(res);
                    if (idAja != 0) {
                        $("#divName2 option[id='" + idAja + "']").attr("selected", "selected");
                    }
                } else {
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
};

