function ApplyFilter() {
    var selectedStatuses = $('#status_list li div > span[data-checked="true"]').map(function () { return $(this).attr("data-val") }).get();
    $.ajax({
        url: getClaimsUrl,
        type: 'GET',
        contentType: 'application/json',
        data: {
            page: 1,
            DateStart: $('#datestart').val(),
            DateEnd: $('#dateend').val(),
            Author: $('#author').val(),
            statuses: selectedStatuses.length == 0 ? new Array() : selectedStatuses.join(),
            isAll: $('#isAll').val(),
            Column: $('.active-sort').attr('name'),
            Order: $('.active-sort').attr('order')
        },
        cache: false,
        success: function (result) {
            $('#claims').html(result);
        }
    });
}

$('.sortable-column').click(ApplyFilter);


$('#claim_pager a').click(function (e) {
    if (this.href != '') {
        var selectedStatuses = $('#status_list li div > span[data-checked="true"]').map(function () { return $(this).attr("data-val") }).get();
        e.preventDefault();
        $.ajax({
            url: this.href,
            data: {
                DateStart: $('#datestart').val(),
                DateEnd: $('#dateend').val(),
                Author: $('#author').val(),
                statuses: selectedStatuses.length == 0 ? new Array() : selectedStatuses.join(),
                isAll: $('#isAll').val(),
                Column: $('.active-sort').attr('name'),
                Order: $('.active-sort').attr('order')
            },
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#claims').html(result);
                window.scrollTo(0, 0);
            }
        });
    }
    return false;
}); 