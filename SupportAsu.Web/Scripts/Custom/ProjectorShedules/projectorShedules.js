$(document).delegate('.edit-projector-btn', 'click', function (e) {
    startLoader();
    var id = $(this).parent().attr('data-id');
    $.ajax({
        type: "GET",
        url: location.origin + '/shedule/' + id,
        success: function (obj) {
            $("#update_projector_shedule_form").find('.input_projector_shedule_id').val(obj.Id);
            $("#update_projector_shedule_form").find('.input_projector_shedule_teacher').val(obj.Teacher);
            $("#update_projector_shedule_form").find('.input_projector_shedule_auditory_id').val(obj.AuditoryId);
            $("#update_projector_shedule_form").find('.input_projector_shedule_week_id').val(obj.WeekId);
            $("#update_projector_shedule_form").find('.input_projector_shedule_day_id').val(obj.DayId);
            $("#update_projector_shedule_form").find('.input_projector_lesson_id').val(obj.LessonId);
            $("#update_projector_shedule_form").find('.input_projector_note').val(obj.Note);
            $("#update_projector_shedule_form").find('.input_projector_responsible_id').val(obj.ResponsibleId);
            initSelectedValue();
            stopLoader();
            $('.modal-edit-projector').show();
        },
        error: function (thrownError) {
            //выводим ошибку
            stopLoader();
            alert('Помилка');
        }
    });

});

var getObj = function (form) {
    if (!form.valid())
        return false;
    var id = form.find('.input_projector_shedule_id').val();
    var teacher = form.find('.input_projector_shedule_teacher').val();
    var auditoryId = form.find('.input_projector_shedule_auditory_id').val();
    var weekId = form.find('.input_projector_shedule_week_id').val();
    var dayId = form.find('.input_projector_shedule_day_id').val();
    var lessond = form.find('.input_projector_lesson_id').val();
    var responsible = form.find('.input_projector_responsible_id').val();

    var note = form.find('.input_projector_note').val();
    var obj = {};
    if (form.attr('id') === 'update_projector_shedule_form') {
        obj = { Id: id, Teacher: teacher, AuditoryId: auditoryId, WeekId: weekId, DayId: dayId, LessonId: lessond, Note: note, ResponsibleId: responsible };
    } else {
        obj = { Teacher: teacher, AuditoryId: auditoryId, WeekId: weekId, DayId: dayId, LessonId: lessond, Note: note, ResponsibleId: responsible };
    }
    return obj;
}

var requestCreateOrUpdateDetail = function (data, successFunc) {
    startLoader();
    $.ajax({
        type: "POST",
        url: location.origin + '/insertorupdate',
        data: data,
        success: function (r) {
            //htmlAddPutchases(r.Name, r.Date, r.Note, r.Id);
            successFunc(r);

            stopLoader();
        },
        error: function (thrownError) {
            //выводим ошибку
            stopLoader();
            alert('Помилка');
        }
    });
}

var getHtmlCreate = function (detail) {
    //var src = location.origin + '/plandetail/delete';
    var src = location.origin + '/shedule/delete';

    return '<div data-id="' + detail.Id + '" data-src="' + src + '">' +
        getHtmlUpdate(detail) +
        '</div>' +
        '<div class="projector-note"><span>' +
                                           (detail.Note == null ? '' : detail.Note) +
                                        '</span></div>';
}

var getHtmlUpdate = function (detail) {
    return '<span class="p-teacher">' + detail.Teacher + '</span>' +
        '<span class="p-auditory">' + detail.Auditory.Value + '</span>' +
        '<span class="p-teacher responsible_name">' + detail.ResponsibleName + '</span>' +
        '<button class="edit-projector-btn"><i class="fa fa-pencil" aria-hidden="true"></i></button>' +
        '<button class="remove-projector-btn"><i class="fa fa-trash-o" aria-hidden="true"></i></button>';
}

$("#create_projector_shedule_form").submit(function (event) {
    var form = $("#create_projector_shedule_form");
    event.preventDefault();
    var data = getObj(form);
    if (!data)
        return;
    var func = function (detail) {
        var html = getHtmlCreate(detail);
        $('#lesson_block_' + detail.Week.Code + '_' + detail.Day.Code + '_' + detail.Lesson.Code).html(html);

        $(form).find('input').val('');
        $(form).find('textarea').val('');
        form.find('.input_projector_shedule_id').val(0);
    }
    requestCreateOrUpdateDetail(data, func, form);
});

$("#update_projector_shedule_form").submit(function (event) {
    var form = $("#update_projector_shedule_form");

    event.preventDefault();
    var data = getObj(form);
    if (!data)
        return;
    var func = function (detail) {
        $('[data-id = "' + detail.Id + '"]').html('');
        var html = getHtmlCreate(detail);
        $('#lesson_block_' + detail.Week.Code + '_' + detail.Day.Code + '_' + detail.Lesson.Code).html(html);
        $('.modal-edit-projector').hide();
    }
    requestCreateOrUpdateDetail(data, func, form);
});



$(document).delegate('.remove-projector-btn', 'click', function (event) {
    var itemContent = $(this).closest('div.sheduleIdBlock');
    var itemId = $(itemContent).attr('data-id');
    var itemSrc = $(itemContent).attr('data-src');
    showRemovePopup(itemId, itemSrc);
});

//$(document).delegate('.remove-item-btn', 'click', function (event) {
//    var itemId = $(this).attr('data-remove-id');
//    var itemSrc = $(this).attr('data-remove-src');
//    removeItem(itemId, itemSrc);
//});

//$(document).delegate('.close-small-pop-up-btn', 'click', function (event) {
//    $(this).closest('.modal').remove();
//});

//function showRemovePopup(itemId, itemSrc) {
//    html = `<div class="modal" style="display:block">
//		<div class="small-pop-up">
//			<span>Ви дійсно хочете видалити?</span>
//			<button class="btn--red remove-item-btn" data-remove-id=` + itemId + ` data-remove-src = ` + itemSrc + `>Так</button>
//			<button class="btn--green close-small-pop-up-btn">Ні</button>
//		</div>
//	</div>`
//    $('body').append(html);
//}
//function removeItem(itemId, itemSrc) {
//    startLoader();
//    $.ajax({
//        type: "POST",
//        url: itemSrc,
//        data: { 'id': itemId },
//        success: function (r) {
//            $(`[data-id = ` + itemId + `]`).remove();
//            $('.small-pop-up').closest('.modal').remove();
//            stopLoader();
//        },
//        error: function (thrownError) {
//            //выводим ошибку
//            alert('Помилка видалення');
//            stopLoader();
//        }
//    });
//}

