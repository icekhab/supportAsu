$(function () {
    $("#CreatedAt").rules("remove", "required");
    $("#CreatedAt").rules("remove", "date");
    $("#Id").rules("remove", "required");
})
$('.prophylaxis_form').submit(function (e) {
    e.preventDefault();
    if ($('#'+$(this).attr('id')).valid()) {
        startLoader();
        $.ajax({
            url: addeditUrl,
            data: {
                Id: $(this).find('#Id').val(),
                AuditoryId: $(this).find('#AuditoryId').val(),
                DayId: $(this).find('#DayId').val(),
                Note: $(this).find('#Note').val(),
                CreatedAt: $(this).find('#CreatedAt').val(),
                ResponsibleId: $(this).find('#ResponsibleId').val(),
                LessonId: $(this).find('#LessonId').val(),
            },
            type: 'POST',
            cache: false,
            success: function (result) {
                GetList();
                if ($(this).find('#Id').val() != 0) {
                    $('.modal-edit-prophylaxis').hide();
                }
            }
        });
    }
});

function GetList() {
    $.ajax({
        url: listUrl,
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#putchases').empty().append(result);
            stopLoader();
        }
    });
}
$(document).delegate('.prophylaxis-edit-btn', 'click', function (event) {
    $.ajax({
        url: editUrl,
        data: {
            id: $(this).attr("data-id")
        },
        type: 'POST',
        cache: false,
        success: function (result) {
            var form = $('#prophylaxis_edit_form');
            $(form).find('#AuditoryId').val(result.AuditoryId);
            $(form).find('#DayId').val(result.DayId);
            $(form).find('#Id').val(result.Id);
            $(form).find('#LessonId').val(result.LessonId);
            $(form).find('#Note').val(result.Note);
            $(form).find('#ResponsibleId').val(result.ResponsibleId);
            initSelectedValue()
            stopLoader();
            $('.modal-edit-prophylaxis').show();
        }
    });
});
//$('.prophylaxis-edit-btn').click(function (e) {
//   $.ajax({
//       url: editUrl,
//       data:{
//       id:$(this).attr("data-id")
//       },
//        type: 'POST',
//        cache: false,
//        success: function (result) {
//            var form = $('#prophylaxis_edit_form');
//            $(form).find('#AuditoryId').val(result.AuditoryId);
//            $(form).find('#DayId').val(result.DayId);
//            $(form).find('#Id').val(result.Id);
//            $(form).find('#LessonId').val(result.LessonId);
//            $(form).find('#Note').val(result.Note);
//            $(form).find('#ResponsibleId').val(result.ResponsibleId);
//            initSelectedValue()
//            stopLoader();
//            $('.modal-edit-prophylaxis').show();
//        }
//    });
//});
