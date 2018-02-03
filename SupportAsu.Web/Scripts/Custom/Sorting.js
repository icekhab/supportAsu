$('.sortable-column').click(function ()
{
    if ($(this).hasClass('active-sort')) {
        if ($(this).attr('order') == 'asc') {
            $(this).removeClass('sort-asc');
            $(this).attr('order', 'desc');
            $(this).addClass('sort-desc');
        }
        else {
            $(this).removeClass('sort-desc');
            $(this).attr('order', 'asc');
            $(this).addClass('sort-asc');
        }
    }
    else {
        $('.sortable-column').each(function () {
            $(this).removeClass('active-sort');
            $(this).removeClass('sort-desc');
            $(this).removeClass('sort-asc');
            $(this).removeAttr('order');
        });
        $(this).addClass('active-sort');
        $(this).addClass('sort-asc');
        $(this).attr('order', 'asc');
    }
})