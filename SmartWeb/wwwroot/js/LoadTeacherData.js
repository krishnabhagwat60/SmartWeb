var table;

$(document).ready(function () {
    var minutes = true; // change to false if you'd rather use seconds
    var interval = minutes ? 60000 : 1000;
    var IDLE_TIMEOUT = 10; // 3 minutes in this example
    var idleCounter = 0;

    document.onmousemove = document.onkeypress = function () {
        idleCounter = 0;
    };

    window.setInterval(function () {
        if (++idleCounter >= IDLE_TIMEOUT) {
            $('#logoutForm').trigger('submit');
        }
    }, interval);
});

function LoadData() {
    $("#teacher").hide();
    $.ajax({
        type: 'Get',
        url: "/Teacher/Home/GetData",
        success: function (res) {
            if (res.isValid) {
                $("#firstname").text(res.firstname);
                $("#image").attr("src", res.imageurl);
                $("#imagefull").attr("src", res.imageurl);
                $("#fullname").text(res.fullname);
                $("#jobtitle").text(res.jobtitle);
                if (res.category == "teacher") {
                    $("#teacher").show();
                }
            }
            else {
                if (res.code === '401') {
                    window.location.replace(res.path);
                }  
            }
        },
        error: function (err) {
            
        }
    })
}

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        KTApp.blockPage({
            overlayColor: '#000000',
            state: 'danger',
            message: 'إنتظر قليلا ...'
        });
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    table.destroy();
                    loadChoice();
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    KTApp.unblockPage();
                }
                else {
                    $('#form-modal .modal-body').html(res.html);
                    KTApp.unblockPage();
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        KTApp.unblockPage();
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

function loadChoice() {
    var id = $("#QuestionID").val();
    table = $('#kt_datatable1').DataTable({
        responsive: true,
        processing: true,
        "ajax": {
            "url": "/Teacher/Question/GetChoice?QuestionID=" + id
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
            { targets: [0, 3], orderable: false, searchable: false },
            { targets: [2], className: "h6" },
            { targets: [0, 1, 3], className: "text-center h6" }
        ],
        "columns": [
            { "data": null },
            {
                "data": "questionChoiceRight",
                "render": function (data) {
                    if (data == 'true') {
                        return `
                                <i class="label label-xl font-weight-boldest label-inline label-success mr-2">إجابة صحيحة</i>
                           `;
                    }
                    else {
                        return `
                                <i class="label label-xl font-weight-boldest label-inline label-danger mr-2">إجابة خاطئة</i>
                           `;
                    }
                }
            },
            { "data": "questionChoiceName" },
            {
                "data": "questionChoiceID",
                "render": function (data) {
                    return `
                                <a onclick=ChoiceDelete("/Teacher/Question/ChoiceDelete/${data}") style="cursor:pointer">
                                    <i class="icon-2x text-danger flaticon-delete"></i> 
                                </a>
                                <a onclick=showInPopup("/Teacher/Question/ChoiceEdit/${data}","تعديل") class="mr-5" style="cursor:pointer">
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


