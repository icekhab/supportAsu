$(function () {
    $("#dicValueForm .field-validation-error").empty();
})
function Save() {
    if ($('#dicValueForm').valid()) {
        $('#dicValueForm').submit();
    }
}
function OnBegin() {
}
function OnSuccess(data) {
    $("#dictionary_value").setGridParam({ datatype: 'json', page: 1, postData: { itemId: $('#DicId').val() } }).trigger('reloadGrid');
    Close();
}
function Close()
{
    $('#DV_dialog').dialog('destroy').html("").remove();
}
$.validator.addMethod("code_c", function (value, element) {
    var result;
    $.ajax({
        url: validUrl,
        cache: false,
        data:
            {
                itemId: $('#Id').val(),
                code: $('#Code').val(),
                dicId: $('#DicId').val()
            },
        async: false,
        type: 'POST',
        success: function (data) {
            result = data;
        }
    });
    return result;
}, 'Запис з таким кодов вже існує');