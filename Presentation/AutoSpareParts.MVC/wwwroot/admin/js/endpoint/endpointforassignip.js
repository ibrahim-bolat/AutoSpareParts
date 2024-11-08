var $ = jQuery.noConflict();
$(document).ready(function ($) {
    var tree = $('#tree').tree({
        primaryKey: 'id',
        uiLibrary: 'bootstrap4',
        dataSource: '/admin/endpoint/getendpointlistforassignipaddress',
        width: 800,
        icons: {
            expand: '<i class="gj-icon chevron-right"></i>',
            collapse: '<i class="gj-icon chevron-down"></i>'
        }
    });

    tree.on('dataBound', function () {
        tree.expandAll();
    });
    $('#expandAll').on('click', function () {
        tree.expandAll();
    });
    $('#collapseAll').on('click', function () {
        tree.collapseAll();
    });

    //IpModal Save
    $("#endpointIPModalPartial").on('click', '.endpointIPModalSaveBtn', function (e) {
        var IPIds = [];
        var IPEndpointId;
        var IPAreaName;
        var IPMenuName;
        //var Id = $('#endpointIPModalForm .modal-body input[type="checkbox"]').attr("data-id");
        $.each($('#endpointIPModalForm .modal-body input[type="checkbox"]:checked'), function () {
            if ($(this).is('[data-areaname]')) {
                var IP_areaName = $(this).attr("data-areaname");
            }
            if ($(this).is('[data-menuname]')) {
                var IP_menuName = $(this).attr("data-menuname");
            }
            if ($(this).is('[data-id]')) {
                var IP_id = $(this).attr("data-id");
            }
            if ($(this).is('[data-endpointId]')) {
                var ip_endpointId = $(this).attr("data-endpointId");
            }
            IPIds.push(IP_id);
            IPEndpointId=ip_endpointId
            IPAreaName=IP_areaName;
            IPMenuName=IP_menuName;
        })
        if(IPIds.length===0) {
            if($('#endpointIPModalForm .modal-body input[type="checkbox"]').is('[data-areaname]')){
                areaName = $('#endpointIPModalForm .modal-body input[type="checkbox"]').attr("data-areaname");
                if (areaName != null && areaName !== "" && areaName!==undefined )
                    IPAreaName = areaName;
            }
            if($('#endpointIPModalForm .modal-body input[type="checkbox"]').is('[data-menuname]')){
                menuName = $('#endpointIPModalForm .modal-body input[type="checkbox"]').attr("data-menuname");
                if (menuName!==null && menuName !== "" && menuName !==undefined)
                    IPMenuName = menuName;
            }
            if($('#endpointIPModalForm .modal-body input[type="checkbox"]').is('[data-endpointId]')){
                endpointId = $('#endpointIPModalForm .modal-body input[type="checkbox"]').attr("data-endpointId");
                if (endpointId !== null && endpointId !== "" && endpointId !== undefined)
                    IPEndpointId = endpointId;
            }
        }
        
        $.ajax({
            url: '/admin/endpoint/assignipaddresslisttoendpoints',
            type: "POST",
            data: { "IPAreaName": IPAreaName ,"IPMenuName":IPMenuName,"IPEndpointId":IPEndpointId, "IPIds":IPIds},
            dataType: "json",
            traditional: true,
            success: function (result) {
                if (result.success) {
                    $("#endpointIPModal").modal("hide");
                    toastMessage(3000, "success", "Tebrikler","IP Atama Başarıyla Gerçekleşti.");
                } else {
                    toastMessage(3000, "error", "Hata", "IP Atama İşlemi Yapılamadı!");
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error","Hata", "IP Atama İşlemi Yapılamadı!");
            }
        });
        return false;
    });
    
    $('#tree').on('click', '.CustomEndpointClass', function () {
        if ($(this).is('[data-areaname]')) {
            var areaName = $(this).attr("data-areaname");
        }
        if ($(this).is('[data-menuname]')) {
            var menuName = $(this).attr("data-menuname");
        }
        if ($(this).is('[data-id]')) {
            var endpointId = $(this).attr("data-id");
        }
        $.ajax({
            url: '/admin/endpoint/getipaddresslistbyendpoint',
            type: 'POST',
            data: { "areaName": areaName ,"menuName":menuName, "endpointId":endpointId},
            dataType: 'html',
            success: function (modal) {
                $("#endpointIPModalPartial").html(modal);
                $("#endpointIPModal").modal("show");
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Hata","IP Adresleri Getirilemedi");
            }
        });
        return false;
    });

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

});


