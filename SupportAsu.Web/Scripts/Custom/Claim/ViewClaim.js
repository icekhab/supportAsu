$(function () {
    $('#status').change(function () {
        $.ajax({
            url: changeStatusUrl,
            data: {
                statusId: $('#status').val()
            },
            type: 'GET',
            cache: false,
            success: function (result) {
                location.reload();
            }
        });
    });
    $('#approve_link').click(function () {
        $.ajax({
            url: approveUrl,
            type: 'GET',
            cache: false,
            success: function (result) {
                document.location.href = homeUrl;
            }
        });
    });
    $('#reject_link').click(function () {
        $.ajax({
            url: rejectUrl,
            type: 'GET',
            cache: false,
            success: function (result) {
                document.location.href = homeUrl;
            }
        });
    });
})