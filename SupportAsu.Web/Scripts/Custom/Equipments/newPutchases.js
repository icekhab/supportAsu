$(document).delegate('.btn-new-putchases', 'click', function (e) {
    $('.modal-add-putchas').show();
});
//$(document).delegate('.save-putchases', 'click',function(e){

//	$('.help-error').html(''); 

//	var name = $('#input_name').val();
//	var date = $('#input_date').val();
//	var note = $('#input_note').val();

//	if(name == ''){
//		$('#input_name').closest('.input-field').find('.help-error').html('Поле обов\'язкове для заповлення');
//	}else if(date == ''){
//		$('#input_date').closest('.input-field').find('.help-error').html('Не вірний формат дати');
//	}
//	else{
//		addPutchases(name, date, note);	
//	}

//});

$("#putchases-form").submit(function (event) {
    if (!$("#putchases-form").valid())
        return;
    event.preventDefault();

    var name = $('#input_name').val();
    var date = $('#input_date').val();
    var note = $('#input_note').val();
    addPutchases(name, date, note);
});

function addPutchases(name, date, note) {
    startLoader();
    $.ajax({
        type: "POST",
        url: location.origin + '/planpurchase/createorupdate',
        data: {
            'Name': name,
            'Date': date,
            'Note': note
        },
        success: function (r) {
            htmlAddPutchases(r.Name, r.Date, r.Note, r.Id);
            stopLoader();
        },
        error: function (thrownError) {
            //выводим ошибку
            stopLoader();
            alert('Помилка додавання');
        }
    });

}

function htmlAddPutchases(name, date, note, id) {
    //var now = new Date();
    //var formated_date = now.format("yyyy-mm-dd");


    var src = location.origin + '/planpurchase/delete';
    var srcDetail = location.origin + '/plan/' + id;

    var html = '<li class="claim-item" data-id=' +
        id +
        ' data-src=' +
        src +
        '>' +
        '<div class="claim-theme"><span>' +
        name +
        '</span></div>' +
        '<div class="claim-date"><span>' +
        date +
        '</span></div>' +
        '<div class="eq-note"><span>' +
        note +
        '</span></div>' +
        '<div class="claim-theme"><a href="' + srcDetail + '" class="btn--orange putchases-edit-btn"><i class="fa fa-pencil" aria-hidden="true"></i></a><button href="" class="btn--red btn-remove-equipment remove-pop-up-btn"><i class="fa fa-trash-o" aria-hidden="true"></i></button></div>' +
        '</li>';

    $('.claims-list').prepend(html);
    $('.modal-add-putchas').hide();
    //$(this).closest('.modal').hide();
    $('#input_name').val('');
    $('#input_date').val('');
    $('#input_note').val('');
}



$(document).delegate('#filter_putchases button','click',function(){
    requestFilter(1);
});

$(document).delegate('#planpurchase_pager a', 'click', function (e) {
    //if (this.href != '') {
    e.preventDefault();
    var page;
    if ($.isNumeric(e.currentTarget.text)) {
        page = e.currentTarget.text;
    }
    else if ($(e.currentTarget).attr('rel') === "next") {
        page = Number.parseInt($('#planpurchase_pager li.active a')[0].text) + 1;
    }
    else if ($(e.currentTarget).attr('rel') === "prev") {
        page = Number.parseInt($('#planpurchase_pager li.active a')[0].text) - 1;
    }
    else if (e.currentTarget.parentNode.className === "PagedList-skipToLast") {
        page = $('#number_pages').val();
    }
    else if (e.currentTarget.parentNode.className === "PagedList-skipToFirst") {
        page = 1;
    }
    else {
        return;
    }

    requestFilter(page);
    window.scrollTo(0, 0);
    //$.ajax({
    //    url: this.href,
    //    data: {
    //        DateStart: $('#datestart').val(),
    //        DateEnd: $('#dateend').val(),
    //        Author: $('#author').val(),
    //        statuses: selectedStatuses.length == 0 ? new Array() : selectedStatuses.join(),
    //        isAll: $('#isAll').val(),
    //        Column: $('.active-sort').attr('name'),
    //        Order: $('.active-sort').attr('order')
    //    },
    //    type: 'GET',
    //    cache: false,
    //    success: function (result) {
    //        $('#claims').html(result);
    //        window.scrollTo(0, 0);
    //    }
    //});
    //}
    return false;
});


var requestFilter = function (page) {
    startLoader();

    var name = $('#filter_name').val();
    var from = $('#filter_from').val();
    var to = $('#filter_to').val();

    

    var url = location.origin + '/planTableList/' + page + '/';
    var urlparam = '';
    if (name) {
        urlparam += 'name=' + name;
    }
    if (from) {
        if (name)
            urlparam += '_';
        urlparam += 'from=' + from;
    }
    if (to) {
        if (name || from)
            urlparam += '_';
        urlparam += 'to=' + to;
    }
    //window.location.replace(url);

    $.ajax({
        type: "GET",
        url: url + urlparam,
        cache: false,
        success: function (result) {
            history.pushState('', '', location.origin + '/plans/' + page + '/' + urlparam);
            $('#putchases_table').html(result);
            stopLoader();
        },
        error: function (thrownError) {
            //выводим ошибку
            alert('Помилка фільтрування');
            stopLoader();
        }
    });
}