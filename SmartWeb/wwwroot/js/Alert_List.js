var table;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    var id = $("#StageID").val();
    table = $('#kt_datatable1').DataTable({
        responsive: true,
        processing: true,
        "ajax": {
            "url": "/Teacher/Alert/GetAlert?StageID=" + id
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
            { targets: [0, 2], orderable: false, searchable: false },
            { targets: [1], className: "h6" },
            { targets: [0, 2], className: "text-center h6" }
        ],
        "columns": [
            { "data": null },
            { "data": "alertName" },
            {
                "data": "alertID",
                "render": function (data) {
                    return `
                                <a onclick=Delete("/Teacher/Alert/Delete/${data}") style="cursor:pointer">
                                    <i class="icon-2x text-danger flaticon-delete"></i> 
                                </a>
                                <a href="/Teacher/Alert/Edit/${data}" class="mr-5" style="cursor:pointer">
                                    <i class="icon-2x text-info flaticon-edit-1"></i> 
                                </a>
                           `;
                }
            }
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


