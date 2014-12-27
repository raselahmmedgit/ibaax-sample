/// <reference path="ibas.jquery.ajax.js" />
/// <reference path="itms.jquery.ex.js" />
/// <reference path="ajaxfileupload.js" />

var tajax = function (webMethod, isAsynchronous) {
    var _webMethod,
        _isAsynchronous,
        _waitMsg = 'Processing.......',
        _successCallback,
        _failedCallback = function (serverData) {
            alert(serverData.responseText);
        };

    var _params = new Object();

    _webMethod = root + webMethod;
    _isAsynchronous = isAsynchronous;

    function centerProgress($relativeElement, $element) {
        var lParentHeight = $relativeElement.height();
        var lParentWidth = $relativeElement.width();
        var lThisHeight = $element.height();
        var lThisWidth = $element.width();

        if (lParentHeight > lThisHeight || lParentWidth > lThisWidth) {
            var lTop = '';
            var lLeft = '';
            lTop = (lParentHeight - lThisHeight) / 2 + 'px';
            lLeft = (lParentWidth - lThisWidth) / 2 + 'px';
            $element.css('top', lTop);
            $element.css('left', lLeft);
        }
    }
    
    function addProgress() {
        var div = '<div id="modalajaxbackground" class="modalajaxbackground"></div>';
        $('body').append(div);
        var divProgress = '<div id="modalajaxprogresscontainer" class="modalajaxprogresscontainer">'
                            + '<img id="progressimg" src="' + root + 'Contents/Images/ajaxprogress.gif"/>'
                            + '<div>' + _waitMsg + '</div>'
                            + '</div>';
        $('body').append(divProgress);
        centerProgress($(window), $('#modalajaxprogresscontainer'));        
    }

    function removeProgress() {
        $('#modalajaxbackground,#modalajaxprogresscontainer').remove();
    }

    var actualLibrary = {

        successCallback: function (callback) {
            _successCallback = callback;
            return this;
        },

        failedCallback: function (callback) {
            _failedCallback = callback;
            return this;
        },

        addParameter: function (parameterName, parameterValue) {
            _params[parameterName] = parameterValue;
            return this;
        },

        callWaitMsg: function (waitMsg) {
            _waitMsg = waitMsg;
            return this;
        },

        callFunction: function (successCallback) {
            _successCallback = successCallback;

            $.ajax({
                type: 'POST',
                url: _webMethod,
                data: JSON.stringify(_params),
                processData: false,
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                beforeSend: addProgress,
                complete: removeProgress,
                success: function (serverData) {
                    try {
                        if (serverData.d)
                            if (serverData.d.flag)
                                if (serverData.d.flag == -99) window.location.href = root + serverData.d.msg;
                        _successCallback(serverData);
                    } catch (err) { alert(err.message ? err.message : err); }
                },
                error: function (serverData) {
                    try { _failedCallback(serverData); } catch (err) { alert(err.message ? err.message : err); }
                },
                async: _isAsynchronous
            });
        }
    }

    return actualLibrary;
}
