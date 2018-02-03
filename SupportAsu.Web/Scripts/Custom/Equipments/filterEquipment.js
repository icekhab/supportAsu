$(document).delegate('#filter_equipments', 'click', function () {
    var form = $(this).closest('.filter-form');
    request(1);
});


$(document).delegate('#eq_pager a', 'click', function (e) {
    //if (this.href != '') {
    e.preventDefault();
    var page;
    if ($.isNumeric(e.currentTarget.text)) {
        page = e.currentTarget.text;
    }
    else if ($(e.currentTarget).attr('rel') === "next") {
        page = Number.parseInt($('#eq_pager li.active a')[0].text) + 1;
    }
    else if ($(e.currentTarget).attr('rel') === "prev") {
        page = Number.parseInt($('#eq_pager li.active a')[0].text) - 1;
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

    request(page);
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


var request = function (page) {
    startLoader();
    var check = [];
    var number = $('#equipment_number').val();

    $('#equipment_auditories').find('.sa-checkbox').each(function () {
        //if ($(this).attr('data-checked') == true) {
        if ($(this).attr('data-checked') == 'true') {
            var val = $(this).attr('data-val');
            check.push(val);
        }
    });

    var url = location.origin + '/getlist/' + page + '/';
    var urlparam = '';
    if (number) {
        urlparam += 'number=' + number;
    }

    if (check && check.length) {
        if (number)
            urlparam += '_';
        urlparam += 'auditories=' + '[' + check.join() + ']';
    }
    //window.location.replace(url);

    $.ajax({
        type: "GET",
        url: url + urlparam,
        cache: false,
        success: function (result) {
            history.pushState('', '', location.origin + '/equipments/' + page + '/' + urlparam);
            $('#equipments-list-table').html(result);
            stopLoader();
        },
        error: function (thrownError) {
            //выводим ошибку
            alert('Помилка фільтрування');
            stopLoader();
        }
    });
}