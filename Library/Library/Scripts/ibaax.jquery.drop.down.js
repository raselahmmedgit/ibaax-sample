/// <reference path="jquery-1.6.2.js" />
/// <reference path="json2.js" />
/// <reference path="itms.jquery.ex.js" />

var ibaaxDropDown = function (elementid, webMethod, isAsynchronous) {

    var $ddl = $('#' + elementid),
        _webMethod,
        _waitMsg = 'Processing.......',
        _isAsynchronous,
        _requestType = 'GET',
        _successCallback;

    var _params = new Object();

    _webMethod = root + webMethod;

    _isAsynchronous = isAsynchronous;

    function addProgress() {
        var div = '<div id="modalajaxbackground" class="modalajaxbackground"></div>';
        $('body').append(div);
        var divProgress = '<div id="modalajaxprogresscontainer" class="modalajaxprogresscontainer">'
                            + '<img id="progressimg" src="' + root + 'Contents/Images/ajaxprogress.gif" />'
                            + '<div>' + _waitMsg + '</div>'
                            + '</div>';
        $('body').append(divProgress);
        centerProgress($(window), $('#modalajaxprogresscontainer'));
    }

    function removeProgress() {
        $('#modalajaxbackground,#modalajaxprogresscontainer').remove();
    }

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

    function sucessMethod(serverData) {
        try {
            $ddl.find('option').remove().end();
            $('<option value="' + '-1' + '">' + 'Select From List' + '</option>').appendTo($ddl);
            $(serverData).each(function (index, item) {
                var displayMember, valueMember, otherMembers = '';
                for (var prop in item) {
                    if (prop.toUpperCase() == 'VALUE') valueMember = item[prop];
                    else if (prop.toUpperCase() == 'TEXT') displayMember = item[prop];
                    else otherMembers = otherMembers + ' ' + prop + '="' + item[prop] + '"';
                }
                $('<option value="' + valueMember + '"' + otherMembers + ' >' + displayMember + '</option>').appendTo($ddl);
            });

            if ($ddl.attr('multiple') == 'multiple') { $ddl.find('option[value="-1"]').remove(); }
            else $ddl.find('option[value="-1"]').attr('selected', 'selected');

            if ($.isFunction(_successCallback)) _successCallback();
        } catch (err) { alert(!err.message ? err : err.message); }
        return this;
    }

    var actualLibrary = {
        requestType: function (httpVerb) {
            _requestType = httpVerb;
        },
        clearDropDown: function () {
            $ddl.find('option').remove().end();
            $('<option value="' + '-1' + '">' + 'Select From List' + '</option>').appendTo($ddl);
            return this;
        },

        triggerChange: function () {
            $ddl.trigger('change');
        },

        addParameter: function (parameterName, parameterValue) {
            _params[parameterName] = parameterValue;
            return this;
        },

        load: function (successCallback) {
            _successCallback = successCallback;

            var disableFlag = $ddl.attr('disabled');

            $.ajax({
                type: _requestType,
                url: _webMethod,
                data: JSON.stringify(_params),
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                //beforeSend: addProgress,
                //complete: removeProgress,
                success: function (serverData) {
                    sucessMethod(serverData);
                },
                error: function (serverData) {
                    try {
                        alert('DropDown Load Error. DropDownID:' + $ddl.attr('id') + '--' + serverData.responseText);
                    } catch (err) {
                        alert(!err.message ? err : err.message);
                    }
                },
                async: _isAsynchronous
            });
        }
    }
    return actualLibrary;
}

