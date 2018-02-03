$(document).delegate('.btn_modal_update', 'click', function (e) {
    startLoader();
    var id = $(this).closest('li').attr('data-id');
    $.ajax({
        type: "GET",
        url: location.origin + '/updatepurchasedetail/' + id,
        success: function (obj) {
            //$('#popup_update_form').html(html);
            $("#form_update_plan_detail").find('.detail_input_id').val(obj.Id);
            $("#form_update_plan_detail").find('.detail_input_name').val(obj.Name);
            $("#form_update_plan_detail").find('.detail_input_count').val(obj.Count);
            $("#form_update_plan_detail").find('.detail_input_note').val(obj.Note);
            stopLoader();
            $('.modal-edit-putchas-detail').show();
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
    var id = form.find('.detail_input_id').val();
    var name = form.find('.detail_input_name').val();
    var count = form.find('.detail_input_count').val();
    var note = form.find('.detail_input_note').val();
    var purchasePlanId = $('#purchasePlanId').val();
    var obj = {};

    if (form.attr('id') === 'form_update_plan_detail') {
        obj = { Id: id, Name: name, Count: count, Note: note, PurchaseId: purchasePlanId };
    } else {
        obj = { Name: name, Count: count, Note: note, PurchaseId: purchasePlanId };
    }
    return obj;
}

var requestCreateOrUpdateDetail = function (data, successFunc, form) {
    startLoader();
    $.ajax({
        type: "POST",
        url: location.origin + '/purchasedetail/createorupdate',
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

var getHtmlDetail = function (detail) {
    var src = location.origin + '/plandetail/delete';
    return '<li class="claim-item" data-id=' +
        detail.Id +
        ' data-src=' +
        src +
        '>' +
        '<div class="claim-theme"><span>' +
        detail.Name +
        '</span></div>' +
        '<div class="claim-date"><span>' +
        detail.Count +
        '</span></div>' +
        '<div class="eq-note"><span>' +
        detail.Note +
        '</span></div>' +
        '<div class="claim-theme"><button class="btn--orange single-putchases-edit-btn btn_modal_update"><i class="fa fa-pencil" aria-hidden="true"></i></button><button class="btn--red btn-remove-equipment remove-pop-up-btn"><i class="fa fa-trash-o" aria-hidden="true"></i></button>' +
        '</li>';
}

$("#form_create_plan_detail").submit(function (event) {
    var form = $("#form_create_plan_detail");
    event.preventDefault();
    var data = getObj(form);
    if (!data)
        return;
    var func = function (detail) {
        var html = getHtmlDetail(detail);
        $('.plan-list-detail').prepend(html);
        $(form).find('input').val('');
        $(form).find('textarea').val('');

    }
    requestCreateOrUpdateDetail(data, func, form);
});

$("#form_update_plan_detail").submit(function (event) {
    var form = $("#form_update_plan_detail");

    event.preventDefault();
    var data = getObj(form);
    if (!data)
        return;
    var func = function (detail) {
        var html = '<div class="claim-theme"><span>' +
            detail.Name +
            '</span></div>' +
            '<div class="claim-date"><span>' +
            detail.Count +
            '</span></div>' +
            '<div class="eq-note"><span>' +
            detail.Note +
            '</span></div>' +
        '<div class="claim-theme"><button class="btn--orange single-putchases-edit-btn btn_modal_update"><i class="fa fa-pencil" aria-hidden="true"></i></button><button class="btn--red btn-remove-equipment remove-pop-up-btn"><i class="fa fa-trash-o" aria-hidden="true"></i></button>';
        $('[data-id = "' + detail.Id + '"]').html(html);
        $('.modal-edit-putchas-detail').hide();
    }
    requestCreateOrUpdateDetail(data, func, form);
});




$("#putchases-form").submit(function (event) {
    if (!$("#putchases-form").valid())
        return;
    event.preventDefault();

    var name = $('#input_name').val();
    var date = $('#input_date').val();
    var note = $('#input_note').val();
    var id = $('#input_id').val();
    addPutchases(id, name, date, note);
});

function addPutchases(id, name, date, note) {
    startLoader();
    $.ajax({
        type: "POST",
        url: location.origin + '/planpurchase/createorupdate',
        data: {
            'Id': id,
            'Name': name,
            'Date': date,
            'Note': note
        },
        success: function (r) {
            //htmlAddPutchases(r.Name, r.Date, r.Note, r.Id);
            stopLoader();
        },
        error: function (thrownError) {
            //выводим ошибку
            stopLoader();
            alert('Помилка додавання');
        }
    });
}