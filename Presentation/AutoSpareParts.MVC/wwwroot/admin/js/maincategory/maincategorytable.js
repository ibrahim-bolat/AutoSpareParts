var $=jQuery.noConflict();
$(document).ready(function ($) {
    var tables = $("#mainCategoryTable").DataTable({
        "pageLength": 10,
        "ordering": true,
        "order": [[2, "asc"]],
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
            "url": "/admin/maincategory/getmaincategorylist",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        },
        {
            "targets": [3],
            render: DataTable.render.datetime('DD/MM/YYYY HH:mm:ss')
        },
        {
            "targets": [4],
            "searchable": false,
            "orderable": false
         }],
        "columns": [
            {"data": "Id", "name": "Id", "autoWidth": true},
            {"data": "Name", "name": "Ana Kategori Adı", "autoWidth": true},
            {"data": "MainCategoryOrder", "name": "Ana Kategori Sırası", "autoWidth": true},
            { "data": "ModifiedTime", "name": "Güncellenme Zamanı", "autoWidth": true},
            {
                "data": "Id","className": "text-center","width": "50px", "render": function (data, type, row, meta) {
                    if(row.Status){
                            return '<a class="btn btn-primary mr-2" href="category/getcategorylist/' + data + '"><i class="fa fa-tasks">Kategoriler</i></a>' +
                                '<a class="btn btn-secondary mr-2" onclick="getByIdforUpdate(' + data + ')"><i class="fa fa-pencil-square-o">Ana Kategori Güncelle</i></a>' +
                                '<a class="btn btn-danger" onclick="setPassiveMainCategory(' + data + ')"><i class="fa fa-solid fa-times">Pasif Yap</i></a>';
                    }
                    else{
                        return '<a class="btn btn-primary mr-2" href="category/getcategorylist/' + data + '"><i class="fa fa-tasks">Kategoriler</i></a>' +
                            '<a class="btn btn-secondary mr-2" onclick="getByIdforUpdate(' + data + ')"><i class="fa fa-pencil-square-o">Ana Kategori Güncelle</i></a>' +
                        '<a class="btn btn-success" onclick="setActiveMainCategory(' + data + ')"><i class="fa fa-solid fa-check">Aktif Yap</i></a>';
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
                filename: 'Ana Kategori Listesi',
                title: 'Ana Kategori Listesi',
                exportOptions: {
                    columns: [1, 2, 3]
                },
                className: "btn-export-excel"
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"> Pdf</i>',
                filename: 'Ana Kategori Listesi',
                title: 'Ana Kategori Listesi',
                pageSize: 'A4',
                exportOptions: {
                    columns: [1, 2, 3]
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
                title: 'Ana Kategori Listesi',
                exportOptions: {
                    columns: [1, 2, 3]
                },
                className: "btn-export-print"
            }

        ]
    });

    $(tables.table().body())
        .addClass('tbody');

    //Modal Form Create
    $('#createMainCategoryModalForm').on('submit', '#createModalForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/admin/maincategory/createmaincategory",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#createMainCategoryModal').modal('hide');
                    $(".modal-fade").modal("hide");
                    $(".modal-backdrop").remove();
                    clearCreateModalTextBox();
                    toastMessage(3000,"success","Tebrikler.", "Ana Kategori Başarıyla Oluşturuldu.");
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#createFormModalBody').html(mytag.find(".modal-body").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000,"error","Hata.", "Ana Kategori Oluşturulamadı");
            }
        });
        return false;
    });

    //Modal Form Update
    $('#updateMainCategoryModalForm').on('submit', '#updateModalForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/admin/maincategory/updatemaincategory",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#updateMainCategoryModal').modal('hide');
                    $(".modal-fade").modal("hide");
                    $(".modal-backdrop").remove();
                    clearUpdateModalTextBox();
                    toastMessage(3000, "success", "Tebrikler.", "Ana Kategori Başarıyla Güncellendi.");
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#updateFormModalBody').html(mytag.find(".modal-body").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Hata.", "Ana Kategori Güncellenemedi");
            }
        });
        return false;
    });
    
    //When Close Create Modal Reset ModelSate Errors and Form inputs
    
    $("#createMainCategoryModalForm").on("hidden.bs.modal", function () {
        var createModalForm = $(this).find("#createMainCategoryModalForm");
        resetValidation(createModalForm);
        clearCreateModalTextBox();
    });
    
    //When Close Update Modal Reset ModelSate Errors and Form inputs
    $("#updateMainCategoryModalForm").on("hidden.bs.modal", function () {
        var updateModalForm = $(this).find("#updateMainCategoryModalForm");
        resetValidation(updateModalForm);
        clearUpdateModalTextBox();
    });
});

//Get MainCategory By Id For Update
function getByIdforUpdate(Id) {
    clearUpdateModalTextBox();
    $.ajax({
        url: '/admin/maincategory/getmaincategorybyid/' + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                $('#updateID').val(result.mainCategory.Id);
                $('#updateName').val(result.mainCategory.Name);
                $('#updateMainCategoryOrder').val(result.mainCategory.MainCategoryOrder);
                $('#updateStatus').val(result.mainCategory.Status);
                $('#updateMainCategoryModal').modal('show');
            } else {
                toastMessage(3000,"error","Hata.", "Ana Kategori Getirilemedi");
            }

        },
        error: function (errormessage) {
            toastMessage(3000,"error","Hata.", "Ana Kategori Getirilemedi");
        }
    });
    return false;
}


//Set MainCategory Active
function setActiveMainCategory(Id) {
    Swal.fire({
        title: 'Ana Kategoriyi aktif etmek İstediğinizden Emin misiniz?',
        text: 'Ana Kategoriyi  aktif etmeye onay verdiğiniz zaman Ana kategoriye kategori atanabilecektir.!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aktif Et!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/admin/maincategory/setactivemaincategory/' + Id,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Aktif Oldu?',
                            text: "Ana Kategori Başarıyla Aktif Edildi",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "Ana Kategori Aktif Edilemedi",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "Ana Kategori Aktif Edilemedi",
                        icon: 'error',
                    });
                }
            });
        }
    });
}

//Set MainCategory Passive
function setPassiveMainCategory(Id) {
    Swal.fire({
        title: 'Ana Kategoriyi pasif etmek İstediğinizden Emin misiniz?',
        text: "Ana Kategoriyi pasif etmeye onay verdiğiniz zaman bu Ana Kategorinin atanmış olduğu kategorilerde pasif olacaktır.!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Pasif Et!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/admin/maincategory/setpassivemaincategory/' + Id,
                type: "POST",
                contentType: "application/json;charset=UTF-8",                                  
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Pasif Oldu?',
                            text: "Ana Kategori Başarıyla Pasif Edildi",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "Ana Kategori Pasif Edilemedi",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "Ana Kategori Pasif Edilemedi",
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
    $('#createName').val("");
    $('#createName-error').val("");
    $('#btnAdd').show();
    $('#createName').css('border-color', 'lightgrey');
}

//Clear Update Modal Form  Entire Features
function clearUpdateModalTextBox() {
    $('#updateID').val("");
    $('#updateID-error').val("");
    $('#updateName').val("");
    $('#updateName-error').val("");
    $('#updateMainCategoryOrder').val("");
    $('#updateMainCategoryOrder-error').val("");
    $('#updateStatus').val("");
    $('#updateStatus-error').val("");
    $('#btnUpdate').show();
    $('#updateID').css('border-color', 'lightgrey');
    $('#updateName').css('border-color', 'lightgrey');
    $('#updateMainCategoryOrder').css('border-color', 'lightgrey');
    $('#updateStatus').css('border-color', 'lightgrey');
}

//Disable Create Modal Form  Entire TextBox
function disabledCreateModalTextBox(value = true) {
    $('#createName').attr("disabled", value);
}

//Disable Update Modal Form  Entire TextBox
function disabledUpdateModalTextBox(value = true) {
    $('#updateName').attr("disabled", value);
    $('#updateMainCategoryOrder').attr("disabled", value);
}

//Reload DataTable
function ReloadTable() {
    // $('#example').DataTable().clear();                                                        
    $('#mainCategoryTable').DataTable().ajax.reload(null,false);
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