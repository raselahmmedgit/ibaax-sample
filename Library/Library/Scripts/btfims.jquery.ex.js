/// <reference path="jquery-1.8.2.js" />
/// <reference path="message.box.js" />

var root = location.protocol + '//' + location.host + '/btfims/';

var defaultRowPerPage = 10, defaultPagePerScreen = 10;

jQuery.fn.hideTableColumn = function (indexArr) {
    var $table = $(this);    
    $(indexArr).each(function (index, item) {
        var colIndex = $table.find('tbody tr:eq(0) td[data-field="' + item + '"]').prevAll().size();
        $table.find('thead tr:eq(0) th:eq(' + colIndex + ')').css({ 'display': 'none' });
        $table.find('tbody tr:eq(0) td:eq(' + colIndex + ')').css({ 'display': 'none' });
    });
}

jQuery.fn.showPopup = function () {
    var div = '<div id="modalbackground" class="modalbackground"></div>';
    $('body').append(div).css('overflow', 'hidden');
    $('.popup').css({
        'position': 'absolute',
        'left': ($(window).width() - $('.popup').outerWidth()) / 2,
        'top': ($(window).height() - $('.popup').outerHeight()) / 2
    });
    $(this).css({ 'display': 'block' });
}

jQuery.fn.closePopup = function () {
    $(this).css({ 'display': 'none' });
    $('#modalbackground').remove();
}

jQuery.fn.clearAll = function (callback) {
    if ($(this).find('input[type="hidden"]').size() > 0) {
        $(this).find('input[type="hidden"]').each(function () { $(this).val(''); });
    }
    if ($(this).find('input[type="text"]').size() > 0) {
        $(this).find('input[type="text"]').each(function () { $(this).val(''); });
    }
    if ($(this).find('input[type="password"]').size() > 0) {
        $(this).find('input[type="password"]').each(function () { $(this).val(''); });
    }
    if ($(this).find('input[type="checkbox"]').size() > 0) {
        $(this).find('input[type="checkbox"]').each(function () { $(this).removeAttr('checked'); });
    }
    if ($(this).find('textarea').size() > 0) {
        $(this).find('textarea').each(function () { $(this).val(''); });
    }
    if ($(this).find('select').size() > 0) {
        $(this).find('select').each(function () { $(this).val(-1); });
    }
    if (callback) callback();
    return this;
}

jQuery.fn.validateEntry = function () {
    var $container = $(this), returnFlag = true;
    $container.find('input,select').each(function () {
        var $elem = $(this);
        if ($elem.attr('data-msg')) {
            if ($.trim($elem.val()) == '') {
                MessageBox('Information', $elem.attr('data-msg'))
                    .onOKClick(function () { $elem.focus();})
                    .show();
                returnFlag = false;
                return;
            }
        }
    });
    return returnFlag;
}