"use strict";

// Class definition
var KTUserEdit = function () {
    var validationUserData = function () {
        var validation;
        var form = KTUtil.getById('kt_Upsert');
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            form,
            {
                fields: {
                    StageID: {
                        validators: {
                            notEmpty: {
                                message: 'تأكد من إختيار الصف الدراسى'
                            }
                        }
                    },
                    AlertName: {
                        validators: {
                            notEmpty: {
                                message: 'تأكد من تسجيل إسم التنبيه'
                            }
                        }
                    },
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );

        $('#kt_btnsave_submit').on('click', function (e) {
            e.preventDefault();

            validation.validate().then(function (status) {
                if (status == 'Valid') {
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
                                Swal.fire({
                                    icon: "success",
                                    title: res.title,
                                    text: res.message,
                                    showConfirmButton: false,
                                    timer: 1500
                                });
                                $('#AlertName').val("");
                                KTApp.unblockPage();
                            } else {
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
                } else {
                    KTUtil.scrollTop();
                }
            });
        });

        $('#kt_btnedit_submit').on('click', function (e) {
            e.preventDefault();

            validation.validate().then(function (status) {
                if (status == 'Valid') {
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
                                Swal.fire({
                                    icon: "success",
                                    title: res.title,
                                    text: res.message,
                                    showConfirmButton: false,
                                    timer: 1500
                                });
                                KTApp.unblockPage();
                            } else {
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
                } else {
                    KTUtil.scrollTop();
                }
            });
        });
    }

    return {
        // public functions
        init: function () {
            validationUserData();
        }
    };
}();

var KTDropzoneDemo = function () {
    // Private functions
    var demo1 = function () {

        // multiple file upload
        $('#kt_dropzone_2').dropzone({
            url: "https://keenthemes.com/scripts/void.php", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 10,
            maxFilesize: 10, // MB
            addRemoveLinks: true,
            accept: function (file, done) {
                if (file.name == "justinbieber.jpg") {
                    done("Naha, you don't.");
                } else {
                    done();
                }
            }
        });
    }

    return {
        // public functions
        init: function () {
            demo1();
        }
    };
}();

jQuery(document).ready(function () {
    KTUserEdit.init();
    KTDropzoneDemo.init();
});