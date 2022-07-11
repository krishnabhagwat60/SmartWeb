var table;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    //var id = $("#StageID").val();
    table = $('#kt_datatable1').DataTable({
        responsive: true,
        processing: true,
        "ajax": {
            "url": "/Teacher/Alert/GetAlert?StageID=2"
        },
        "language": {
            "search": "البحث ",
            "emptyTable": "لا توجد بيانات",
            "loadingRecords": "جارى التحميل ...",
            "processing": "جارى التحميل ...",
            "lengthMenu": "عرض _MENU_",
            "paginate": {
                "first": "الأول",
                "last": "الأخير",
                "next": "التالى",
                "previous": "السابق"
            },
            "info": "عرض _START_ الى _END_ من _TOTAL_ المدخلات",
            "infoFiltered": "(البحث من _MAX_ إجمالى المدخلات)",
            "infoEmpty": "لا توجد مدخلات للعرض",
            "zeroRecords": "لا توجد مدخلات مطابقة للبحث"
        },
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "pageLength": 100,
        fixedHeader: {
            header: true
        },
        columnDefs: [
            { targets: [0, 1], className: "text-center h6" }
        ],
        "columns": [
            { "data": null },
            { "data": "alertName" }
        ]
    });

    $('.dataTables_filter input[type="search"]').
        attr('placeholder', 'اكتب هنا للبحث .....').
        css({ 'display': 'inline-block' });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
            table.cell(cell).invalidate('dom');
        });
    }).draw();
}

jQueryAjaxSearch = form => {
    table.destroy();
    loadDataTable();
    //prevent default form submit event
    return false;
}

function Delete(url) {
    Swal.fire({
        title: "هل انت متأكد ؟",
        text: "سيتم حذف التنبيه",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        confirmButtonText: "نعم , أريد الحذف",
        cancelButtonText: "لا , إلغاء الحذف",
        showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        }
    }).then((result) => {
        if (result.isConfirmed) {
            KTApp.blockPage({
                overlayColor: '#000000',
                state: 'danger',
                message: 'إنتظر قليلا ...'
            });
            $.ajax({
                type: 'POST',
                url: url,
                success: function (res) {
                    if (res.isValid) {
                        table.ajax.reload();
                        Swal.fire({
                            icon: "success",
                            title: res.title,
                            text: res.message,
                            showConfirmButton: false,
                            timer: 1500
                        });
                        KTApp.unblockPage();
                    }
                    else {
                        Swal.fire({
                            icon: "error",
                            title: res.title,
                            text: res.message,
                            confirmButtonText: "حسنا",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                        KTApp.unblockPage();
                    }
                },
                error: function (err) {
                    Swal.fire({
                        icon: "error",
                        title: "التنبيهات",
                        text: "من فضلك تأكد من تسجيل البيانات بشكل صحيح",
                        confirmButtonText: "حسنا",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    });
                    KTApp.unblockPage();
                }
            })
            KTApp.unblockPage();
        }
    });
}


