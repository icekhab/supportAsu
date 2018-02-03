$(function () {
    var remember;
    var id;

    $(document).delegate('.ar-task-desc', 'click', function (event) {
        $.ajax({
            url: viewUrl,
            type: 'POST',
            data: {
                id: $(this).attr('id'),
                isEdit:false
            },
            cache: false,
            success: function (result) {
                $('.arcive-description-tasks').empty().append('<div class="pop-up">' + result + '</div>');
                    //.append(result);
                $('.arcive-description-tasks').show();
            }
        });
    });
    $(document).delegate('.task-turn', 'click', function (event) {
        var id = $(this).attr('id');
        ChangeStatus(id, 'Done');
        $('li#task'+id).remove();
    });
    $(document).delegate('.dashboard-issue', 'click', function (event) {
        $.ajax({
            url: viewUrl,
            type: 'POST',
            data: {
                id: $(this).attr('id')
            },
            cache: false,
            success: function (result) {
                $('.task-description-modal').empty().append(result);
                $('.task-description-modal').show();
            }
        });
    });

    $(document).delegate('.dashboard .dashboard-status', 'click', function (event) {
        if ($(document).width() <= 740) {
            $(this).next().slideToggle();
        }
    });
});
function ChangeStatus(id, status) {
    $.ajax({
        url: changetatusUrl,
        type: 'POST',
        data: {
            id: id,
            status: status
        },
        cache: false,
        success: function (result) {
            if (status == 'Archive') {
                $('#' + id).remove();
            }
            stopLoader();
        }
    });
}
function init(taskId, dashboardClass) {
    $('#' + taskId).draggable({
        opacity: 0.9,// opacity whan you drag
        containment: "#" + dashboardClass,
        cancel: "a",//extention for drag
        start: function () {
            remember = $(this).html();
            id = $(this).attr('id');
        }
    });
    $('.droppable').droppable({
        //accept: '.',
        hoverClass: "ui-state-highlight",
        drop: function (event, ui) {
            ui.draggable.remove();
            $(this).find('.help-dropp').append(`<div id=` + id + ` class=" dashboard-issue ui-draggable" ></div>`);
            $('#' + id).html(remember);
            if ($(this).parent().attr('status') == 'Done') {
                $('#' + id).find('.issue-date').hide();
                $('#' + id).find('.to-archive').show();
            }
            else {

                $('#' + id).find('.issue-date').show();
                $('#' + id).find('.to-archive').hide();
            }
            startLoader();
            ChangeStatus(id, $(this).parent().attr('status'))

            $('#' + id).draggable({
                opacity: 0.9,
                containment: "#" + dashboardClass,
                cancel: "a",//extention for drag
                start: function () {
                    remember = $(this).html();
                    id = $(this).attr('id');
                }
            });
        }
    });
}