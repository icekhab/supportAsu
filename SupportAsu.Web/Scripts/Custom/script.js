$(document).ready(function () {

    $(document).delegate('.close-modal-btn', 'click', function (event) {
        $(this).closest('.modal').hide();
    });
    $('.open-filters-btn').on('click', function () {
        var filters = $(this).closest('.filters').find('.filter-section')
        if ($(filters).hasClass('close')) {
            $(filters).removeClass('close');
            $(this).html('Сховати фільтри');
        }
        else {
            $(filters).addClass('close');
            $(this).html('Показати фільтри');
        }

    });
    $('.exit-btn').click(function () {
        window.location.href = logoffUrl;
    });
    var isActive = false;

    $('.js-menu').on('click', function () {
        if (isActive) {
            $(this).removeClass('active');
            $('body').removeClass('menu-open');
        } else {
            $(this).addClass('active');
            $('body').addClass('menu-open');
        }

        isActive = !isActive;
    });
    setSelectedValue();
    $(document).delegate(".ckeckbox-form span", 'click', function (event) {
        var checkbox = $(this).parent().find('.sa-checkbox');
        var input = $(this).parent().find('input[type=checkbox]');
        if ($(checkbox).attr('data-checked') == 'false') {
            $(checkbox).attr({ 'data-checked': 'true' });
            $(input).prop('checked', true);
            $(checkbox).removeClass('false');
            $(checkbox).addClass('true');
            $(this).parent().find("input").prop('checked', true);
        } else {
            $(checkbox).attr({ 'data-checked': 'false' });
            $(input).prop('checked', false);
            $(checkbox).removeClass('true');
            $(checkbox).addClass('false');
            $(this).parent().find("input").prop('checked', false);
        }
        $(input).change();

    });
    $(document).delegate('.new-subtask button', 'click', function (event) {
        $(this).html('Успішно створено');
        $(this).removeClass('btn--orange');
        $(this).addClass('btn--green');
    });



    $(document).delegate(".sa-select", 'click', function (event) {
        $(this).find('.sa-list').slideToggle();
    });

    $(document).delegate(".sa-list li", 'click', function (event) {
        var value = $(this).html();
        var id = $(this).attr("value");
        $(this).closest('.sa-select').find('input.sa-input').val(value);
        var hiddenField = $(this).closest('.sa-select').find('input.sa-hidden-input');
        $(hiddenField).val(id);
        $(hiddenField).change();
        if ($(hiddenField).closest("form").length) {
            $(hiddenField).valid();
        }
    });
   
    $('.main').on('swipeLeft', function () {
        $('.main-nav').animate({ left: '-320px' }, 200);
    });
    if (typeof isFormExists != "undefined")
    {
        if (isFormExists && $('form').length > 0) {
            $("form").data("validator").settings.ignore = "";
        }
    }
    else
    {
        if ($('form').length > 0) {
            $("form").data("validator").settings.ignore = "";
        }
    }

});

function openMenu() {
    $('.main-nav').animate({ left: '0px' }, 200);
}
function startLoader() {
    var content = '<div class="loader"><div align="center" class="cssload-fond"><div class="cssload-container-general"><div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_1"> </div></div><div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_2"> </div></div><div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_3"> </div></div><div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_4"> </div></div></div></div></div>'
    $('body').append(content);
}
function stopLoader() {
    $('.loader').remove();
}

function setSelectedValue()
{
    $('.sa-select').each(function () {
        var checkeditem = $(this).find('ul.sa-list li.checked');
        var value = $(checkeditem).html();
        var id = $(checkeditem).attr("value");
        $(checkeditem).closest('.sa-select').find('input.sa-input').val(value);
        $(checkeditem).closest('.sa-select').find('input.sa-hidden-input').val(id);
    });
}
function initSelectedValue()
{
    $('.sa-select').each(function () {
        var value = $(this).find('.sa-hidden-input').val();
        $(this).find('ul.sa-list li').each(function () { $(this).removeClass('checked')});
        var li = $(this).find('ul.sa-list li[value="'+value+'"]').addClass('checked');
    });
    setSelectedValue();
}