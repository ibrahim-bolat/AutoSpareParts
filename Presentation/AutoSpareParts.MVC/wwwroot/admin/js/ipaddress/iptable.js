var $=jQuery.noConflict();
$(document).ready(function ($) {
    var tables = $("#IPTable").DataTable({
        "pageLength": 10,
        "ordering": true,
        "order": [[0, "asc"]],
        "info": true,
        "paging": true,
        "searching": true,
        "responsive": true,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Tümü"]],
        "pagingType": "full_numbers",
        "processing": true,
        "serverSide": true,
        "filter": true,
        "language": {
            'url': '/lib/datatables/turkceDil.json'
        },
        "ajax": {
            "url": "/admin/ipaddress/getipaddresslist",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        },
            {
                "targets": [4,5],
                "searchable": false,
                "orderable": false
            }],
        "columns": [
            {"data": "Id", "name": "Id", "autoWidth": true},
            {"data": "RangeStart", "name": "IP Aralık Başlangıcı", "autoWidth": true},
            {"data": "RangeEnd", "name": "IP Aralık Sonu", "autoWidth": true},
            {"data": "IPListType", "name": "Liste Türü", "autoWidth": true},
            {"data": "Status", "name": "Durumu", "className": "text-center","autoWidth": true, "render": function (data, type, row, meta) {
                    return row.Status ? '<span class="fa fa-solid fa-check" style="color: green">Aktif</span>' : 
                        '<span class="fa fa-solid fa-times" style="color: red">Pasif</span>';
                }
            },
            {
                "data": "Id","className": "text-center","width": "50px", "render": function (data, type, row, meta) {
                    if(row.Status){
                            return'<a class="btn btn-primary mr-2" href="endpoint/endpoints"><i class="fa fa-tasks">Endpoint Yetkilendirme</i></a>' +
                                '<a class="btn btn-secondary mr-2" onclick="getByIdforUpdate(' + data + ')"><i class="fa fa-pencil-square-o">Ip Güncelle</i></a>' +
                                '<a class="btn btn-danger" onclick="setPassiveIPAddress(' + data + ')"><i class="fa fa-solid fa-times">Pasif Yap</i></a>';
                    }
                    else{
                        return '<a class="btn btn-primary mr-2" href="endpoint/endpoints"><i class="fa fa-tasks">Endpoint Yetkilendirme</i></a>' +
                        '<a class="btn btn-secondary mr-2" onclick="getByIdforUpdate(' + data + ')"><i class="fa fa-pencil-square-o">Ip Güncelle</i></a>' +
                        '<a class="btn btn-success" onclick="setActiveIPAddress(' + data + ')"><i class="fa fa-solid fa-check">Aktif Yap</i></a>';
                    }
                }
            }
        ],
        dom: '<"dt-header"Bf>rt<"dt-footer"ip>',
        buttons: [
            "pageLength",
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"> Excel</i>',
                filename: 'IP Listesi',
                title: 'IP Listesi',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: "btn-export-excel"
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"> Pdf</i>',
                filename: 'IP Listesi',
                title: 'IP Listesi',
                pageSize: 'A4',
                exportOptions: {
                    columns: [1, 2, 3,4]
                },
                className: "btn-export-pdf",
                customize: function (doc) {
                    doc.styles.tableHeader.alignment = 'left';
                    doc.content[1].table.widths = [80, 80, 80, '*'];
                    var objLayout = {};
                    objLayout['hLineWidth'] = function (i) {
                        return .8;
                    };
                    objLayout['vLineWidth'] = function (i) {
                        return .5;
                    };
                    objLayout['paddingLeft'] = function (i) {
                        return 8;
                    };
                    objLayout['paddingRight'] = function (i) {
                        return 8;
                    };
                    doc.content[1].layout = objLayout;
                },
            },
            {
                extend: 'print',
                text: '<i class="fa fa-file-o"> Yazdır</i>',
                title: 'IP Listesi',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: "btn-export-print"
            }

        ]
    });

    $(tables.table().body())
        .addClass('tbody');

    //Modal Form Create
    $('#createIPModalForm').on('submit', '#createModalForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/admin/ipaddress/createipaddress",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#createIPModal').modal('hide');
                    $(".modal-fade").modal("hide");
                    $(".modal-backdrop").remove();
                    clearCreateModalTextBox();
                    toastMessage(3000,"success","Tebrikler.", "IP Başarıyla Oluşturuldu.");
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#createModalFormModalBody').html(mytag.find(".modal-body").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000,"error","Hata.", "IP Oluşturulamadı");
            }
        });
        return false;
    });

    //Modal Form Update
    $('#updateIPModalForm').on('submit', '#updateModalForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/admin/ipoperation/updateipaddress",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#updateIPModal').modal('hide');
                    $(".modal-fade").modal("hide");
                    $(".modal-backdrop").remove();
                    clearUpdateModalTextBox();
                    toastMessage(3000,"success","Tebrikler.", "IP Başarıyla Güncellendi.");
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#updateModalFormModalBody').html(mytag.find(".modal-body").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000,"error","Hata.", "IP Güncellenemedi");
            }
        });
        return false;
    });
    
    //When Close Create Modal Reset ModelSate Errors and Form inputs
    
    $("#createIPModal").on("hidden.bs.modal", function () {
        var createModalForm = $(this).find("#createModalForm");
        resetValidation(createModalForm);
        clearCreateModalTextBox();
    });
    
    //When Close Update Modal Reset ModelSate Errors and Form inputs
    $("#updateIPModal").on("hidden.bs.modal", function () {
        var updateModalForm = $(this).find("#updateModalForm");
        resetValidation(updateModalForm);
        clearUpdateModalTextBox();
    });

    //IP Input Mask
    var createRangeStart = $('#createRangeStart');
    var createRangeEnd = $('#createRangeEnd');
    var updateRangeStart = $('#updateRangeStart');
    var updateRangeEnd = $('#updateRangeEnd');
    createRangeStart.inputmask({
        alias: "ip",
        greedy: false
    });
    createRangeEnd.inputmask({
        alias: "ip",
        greedy: false
    });
    updateRangeStart.inputmask({
        alias: "ip",
        greedy: false
    });
    updateRangeEnd.inputmask({
        alias: "ip",
        greedy: false
    });

});

//Get Ip By Id For Update
function getByIdforUpdate(Id) {
    clearUpdateModalTextBox();
    $.ajax({
        url: '/admin/ipaddress/getipaddressbyid/' + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                $('#updateID').val(result.ip.Id);
                $('#updateStatus').val(result.ip.Status);
                $('#updateRangeStart').val(result.ip.RangeStart);
                $('#updateRangeEnd').val(result.ip.RangeEnd);
                $('#updateIPListType').val(result.ip.IpListType);
                $('#updateIPModal').modal('show');
            } else {
                toastMessage(3000,"error","Hata.", "IP Getirilemedi");
            }

        },
        error: function (errormessage) {
            toastMessage(3000,"error","Hata.", "IP Getirilemedi");
        }
    });
    return false;
}


//Set IP Active
function setActiveIPAddress(Id) {
    Swal.fire({
        title: 'IPyi aktif etmek İstediğinizden Emin misiniz?',
        text: "IPyi aktif etmeye onay verdiğiniz zaman IP aktif Endpointlere atanabilecektir.!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aktif Et!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/admin/ipaddress/setactiveipaddress/' + Id,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Aktif Oldu?',
                            text: "IP Başarıyla Aktif Edildi",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "IP Aktif Edilemedi",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "IP Aktif Edilemedi",
                        icon: 'error',
                    });
                }
            });
        }
    });
}

//Set IP Passive
function setPassiveIPAddress(Id) {
    Swal.fire({
        title: 'IPyi pasif etmek İstediğinizden Emin misiniz?',
        text: "IPyi pasif etmeye onay verdiğiniz zaman bu IPnin atanmış olduğu endpointler çıkarılacaktır.!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Pasif Et!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/admin/ipaddress/setpassiveipaddress/' + Id,
                type: "POST",
                contentType: "application/json;charset=UTF-8",                                  
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Pasif Oldu?',
                            text: "IP Başarıyla Pasif Edildi",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "IP Pasif Edilemedi",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "IP Pasif Edilemedi",
                        icon: 'error',
                    });
                }
            });
        }
    });
}
//remove ModelSate Errors and reset Form
function resetValidation(currentForm) {
    currentForm[0].reset();
    currentForm.find("[data-valmsg-summary=true]")
        .removeClass("validation-summary-errors")
        .addClass("validation-summary-valid")
        .find("ul").empty();
        // formun en üstünde toplu (asp-validation-summary="All") error gösterdiğim için
        // gösterilen tüm hatalar silinir(empty seçilen öğenin tüm içini alt öğeler dahil boşaltır).
    
    currentForm.find("[data-valmsg-replace=true]")
        .removeClass("field-validation-error")
        .addClass("field-validation-valid");
        //.empty ==> selectin içindeki optionları sildirmedim.
    
    currentForm.find("[data-val=true]")
        .removeClass("input-validation-error")
        .addClass("input-validation-valid");
      //.empty ==> her inputun altında error değilde(sp-validation-for)
      // formun en üstünde toplu (asp-validation-summary="All") error gösterdiğim için
      // empty yapmaya gerek yok.
}


//Clear Create Modal Form  Entire Features
function clearCreateModalTextBox() {
    $('#createRangeStart').val("");
    $('#createRangeStart-error').val("");
    $('#createRangeEnd').val("");
    $('#createRangeEnd-error').val("");
    $('#createIPListType').val("");
    $('#createIPListType-error').val("");
    $('#btnAdd').show();
    $('#createRangeStart').css('border-color', 'lightgrey');
    $('#createRangeEnd').css('border-color', 'lightgrey');
    $('#createIPListType').css('border-color', 'lightgrey');
}

//Clear Update Modal Form  Entire Features
function clearUpdateModalTextBox() {
    $('#updateID').val("");
    $('#updateID-error').val("");
    $('#updateStatus').val("");
    $('#updateStatus-error').val("");
    $('#updateRangeStart').val("");
    $('#updateRangeStart-error').val("");
    $('#updateRangeEnd').val("");
    $('#updateRangeEnd-error').val("");
    $('#updateIPListType').val("");
    $('#updateIPListType-error').val("");
    $('#btnUpdate').show();
    $('#updateID').css('border-color', 'lightgrey');
    $('#updateStatus').css('border-color', 'lightgrey');
    $('#updateRangeStart').css('border-color', 'lightgrey');
    $('#updateRangeEnd').css('border-color', 'lightgrey');
    $('#updateIPListType').css('border-color', 'lightgrey');
}

//Disable Create Modal Form  Entire TextBox
function disabledCreateModalTextBox(value = true) {
    $('#createRangeStart').attr("disabled", value);
    $('#createRangeEnd').attr("disabled", value);
    $('#createIPListType').attr("disabled", value);
}

//Disable Update Modal Form  Entire TextBox
function disabledUpdateModalTextBox(value = true) {
    $('#updateRangeStart').attr("disabled", value);
    $('#updateRangeEnd').attr("disabled", value);
    $('#updateIPListType').attr("disabled", value);
}

//Reload DataTable
function ReloadTable() {
    // $('#example').DataTable().clear();                                                        
    $('#IPTable').DataTable().ajax.reload(null,false);
}

//Toast Message
function toastMessage(time, icon,title,text) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: time,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('click', Swal.close)
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })
    Toast.fire({
        icon: icon,
        title: title,
        text: text
    })
}                