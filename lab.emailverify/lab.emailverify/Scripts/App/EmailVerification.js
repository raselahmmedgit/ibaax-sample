var EmailVerification = function () {

    var _validateForm = function () {
        // Setup form validation on the #ContactCreateOrEdit element
        if ($().validate) {
            var form = $("#frmEmailVerificationEditor");
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', //help-block help-block-error default input error message class
                focusInvalid: false, // do not focus the last invalid input
                // Specify the validation rules
                rules: {
                    EmailAddress: {
                        required: true
                    }
                },
                errorPlacement: function (error, element) { // render error placement for each input type
                    //if (element.attr("name") == "gender") { // for uniform radio buttons, insert the after the given container
                    //    error.insertAfter("#form_gender_error");
                    //} else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
                    //    error.insertAfter("#form_payment_error");
                    //} else {
                    //    error.insertAfter(element); // for other inputs, just perform default behavior
                    //}
                    var errorContainer = element.parents('div.form-group');
                    errorContainer.append(error);
                    //error.insertAfter(element);
                },
                messages: {
                    // Specify the custom validation error messages
                    EmailAddress: {
                        required: "Email Address is required!"
                    }
                },
                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success.hide();
                    error.show();
                    Metronic.scrollTo(error, -200);
                },
                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').addClass('has-error'); // set error class to the control group
                },
                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },
                success: function (label) {
                    label.closest('.form-group').removeClass('has-error'); // set success class to the control group
                },
                submitHandler: function (form) {
                    if ($('#btnSaveEmailVerification').length > 0) {
                        var url = $(form).attr('action');
                        $.post(url, $(form).serializeArray(),
                            function (result) {
                                if (result.success) {
                                    $('.modal').modal('hide');

                                    $('#lnkEmailVerification').removeClass('fa-warning');
                                    $('#lnkEmailVerification').addClass('fa-check');

                                    $('#lnkEmailSend').show();

                                    console.log();
                                } else {
                                    console.log();
                                }
                            });
                    } else {
                        form.submit(function (e) { });
                    }

                }
            });
        }
    };

    var _actionHandler = function () {

        $('#btnEmailValidate').unbind("click").bind("click", function (e) {

            e.preventDefault();
            var emailAddress = $('#txtEmail').val();
            var postUrl = "/Home/VerifyEmailAddressAjax";
            var postData = { emailAddress: emailAddress };
            
            $.ajax({
                url: postUrl,
                type: 'POST',
                dataType: 'json',
                data: postData,
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result.success) {
                        $('#dvEmailValidateResult').html(result.successData);
                        console.log();
                    }
                    else {
                        console.log();
                    }
                },
                error: function (objAjaxRequest, strError) {
                    var respText = objAjaxRequest.responseText;
                    console.log(respText);
                }

            });

            return false;

        });

    };

    var _initializeForm = function () {

    };

    var initializeEmailVerification = function () {
        _validateForm();
        _initializeForm();
        _actionHandler();
    };

    return {
        init: initializeEmailVerification
    };
}();