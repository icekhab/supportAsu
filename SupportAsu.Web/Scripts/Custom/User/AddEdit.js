$('.user_form').submit(function (e) {
    e.preventDefault();
    if ($('#' + $(this).attr('id')).valid()) {
        startLoader();
        $.ajax({
            url: addeditUrl,
            data: {
                Login: $(this).find('#Login').val(),
                Name: $(this).find('#Name').val(),
                SurName: $(this).find('#SurName').val(),
                Email: $(this).find('#Email').val(),
                Phone: $(this).find('#Phone').val(),
                isCreate: $(this).attr('data-isvalid') == "True",
                isProfile: typeof (isProfile) == "undefined" ? false : isProfile
            },
            type: 'POST',
            cache: false,
            success: function (result) {
                if (typeof (isProfile) == "undefined") {
                    GetUserList();
                    $('.modal-edit-user').hide();
                }
                stopLoader();
            }
        });
    }
});
$.validator.addMethod("login_v", function (value, element) {
    var resultV;
    if ($(element).closest('form').attr('data-isvalid') == "True") {
        $.ajax({
            url: checkLoginUrl,
            data: {
                login: $(element).val()
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
}, 'Користувач з таким логіном вже існує');
$('#user_search_btn').click(function () {
    GetUserList();
})
function GetUserList() {
    if ($('#search_user').val() != '') {
        startLoader();
        $.ajax({
            url: userListUrl,
            data: {
                filter: $('#search_user').val()
            },
            type: 'POST',
            cache: false,
            success: function (result) {
                $('#users_list').empty().append(result)
                stopLoader();
            }
        });
    }
}
$(document).delegate('.single-users-edit-btn', 'click', function (event) {
    $.ajax({
        url: editUserUrl,
        data: {
            login: $(this).attr("data-id")
        },
        type: 'POST',
        cache: false,
        success: function (result) {
            var form = $('#edit_user_form');
            $(form).find('#Login').val(result.Login);
            $(form).find('#Name').val(result.Name);
            $(form).find('#SurName').val(result.SurName);
            $(form).find('#Email').val(result.Email);
            $(form).find('#Phone').val(result.Phone);
            stopLoader();
            $('.modal-edit-user').show();
        }
    });
});