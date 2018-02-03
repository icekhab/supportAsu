$('#change_password').click(function () {
    if($('#change_password_form').valid())
    {
        startLoader();
        $.ajax({
            url: changePassUrl,
            data: {
                NewPassword: $('#change_password_form').find('#NewPassword').val(),
                OldPassword:$('#change_password_form').find('#OldPassword').val()
            },
            type: 'POST',
            cache: false,
            success: function (result) {
                $('#change_password_form').find('#NewPassword').val('');
                $('#change_password_form').find('#OldPassword').val('');
                stopLoader();
            }
        });
    }
})

$.validator.addMethod("password_v", function (value, element) {
    var resultV;
    if ($(element).val() != '') {
        $.ajax({
            url: checkPassUrl,
            data: {
                password: $(element).val()
            },
            type: 'POST',
            cache: false,
            async: false,
            success: function (result) {
                resultV = result == true;
            }
        });
    }
    else {
        resultV = true;
    }
    return resultV;
}, 'Не вірний пароль');