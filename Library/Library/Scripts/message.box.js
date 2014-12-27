

// supported options : ok,cancel,yes,no,done

function MessageBox(title, message) {

    var _title = title, _message = message, _buttonSet = 'ok', _hexVal='#000000';

    var _ok = function () { $('#messageboxcontainer,#messageboxmodalbackground').remove(); $('body').css('overflow', 'auto'); },
        _yes = function () { $('#messageboxcontainer,#messageboxmodalbackground').remove(); $('body').css('overflow', 'auto'); },
        _no = function () { $('#messageboxcontainer,#messageboxmodalbackground').remove(); $('body').css('overflow', 'auto'); };

    var _okButton = '<input id="MessageBoxOK" type="button" value="OK" class="messageboxbutton"/>',
        _yesButton = '<input id="MessageBoxYES" type="button" value="Yes" class="messageboxbutton"/>',
        _noButton = '<input id="MessageBoxNO" type="button" value="No" class="messageboxbutton"/>';

    function centerMessageBox() {
        var lParentHeight = $('#messageboxmodalbackground').height();
        var lParentWidth = $('#messageboxmodalbackground').width();
        var lThisHeight = $('#messageboxcontainer').height();
        var lThisWidth = $('#messageboxcontainer').width();

        if (lParentHeight > lThisHeight || lParentWidth > lThisWidth) {
            var lTop = '';
            var lLeft = '';
            lTop = (lParentHeight - lThisHeight) / 2 + 'px';
            lLeft = (lParentWidth - lThisWidth) / 2 + 'px';
            $('#messageboxcontainer').css('top', lTop);
            $('#messageboxcontainer').css('left', lLeft);
        }
    }

    var actualLibrary = {
        onOKClick: function (callback) {
            _ok = function () {
                $('#messageboxcontainer,#messageboxmodalbackground').remove();
                //$('body').css('overflow', 'auto');
                callback();
            };
            return this;
        },
        onYesClick: function (callback) {
            _yes = function () {
                $('#messageboxcontainer,#messageboxmodalbackground').remove();
                //$('body').css('overflow', 'auto');
                callback();
            };
            return this;
        },
        onNoClick: function (callback) {
            _no = function () {
                $('#messageboxcontainer,#messageboxmodalbackground').remove();
                //$('body').css('overflow', 'auto');
                callback();
            };
            return this;
        },
        fontColor: function (hexVal) {
            _hexVal = hexVal;
            return this;
        },
        show: function (buttonSet) {
            if (buttonSet != 'yesno') _buttonSet = 'ok'; else _buttonSet = buttonSet;

            //$('html, body').animate({ scrollTop: 0 }, 'fast');
            var div = '<div id="messageboxmodalbackground" class="messageboxmodalbackground"></div>';
            $('body').append(div);//.css('overflow', 'hidden');

            var divContent = '<div id="messageboxcontainer" class="messageboxcontainer">'
                                + '<div id="messageboxtitle" class="messageboxtitle">'
                                + _title
                                + '<a id="messageboxClose">Close[X]</a></div>'
                                + '<p style="color:' + _hexVal + ';">' + _message + '</p></div>';

            $('body').append(divContent);

            $('#messageboxClose').click(function () { $('#messageboxcontainer,#messageboxmodalbackground').remove(); });

            centerMessageBox();

            $('#messageboxcontainer').draggable({ 'handle': '#messageboxtitle' });

            if (_buttonSet == 'ok') {
                if (_ok) {
                    if ($('#MessageBoxOK').size() > 0) $('#MessageBoxOK').remove();
                    $('#messageboxcontainer p').after(_okButton);
                    $('#MessageBoxOK').click(_ok).focus();
                }
            }
            else {
                if (_yes) {
                    if ($('#MessageBoxYES').size() > 0) $('#MessageBoxYES').remove();
                    $('#messageboxcontainer p').after(_yesButton);
                    $('#MessageBoxYES').click(_yes);
                }
                if (_no) {
                    if ($('#MessageBoxNO').size() > 0) $('#MessageBoxNO').remove();
                    $('#messageboxcontainer p').after(_noButton);
                    $('#MessageBoxNO').click(_no).focus();
                }
            }
        }
    };

    return actualLibrary;
}