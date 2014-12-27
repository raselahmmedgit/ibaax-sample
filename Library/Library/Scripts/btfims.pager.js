/// <reference path="jquery-1.6.2.js" />
/// <reference path="ibas.jquery.extension.js" />
/// <reference path="ibas.pager.js" />
/// <reference path="../../../Scripts/jquery-1.6.2.js" />


var tPager = function () {
    var thisInstance = this;

    var _workingTable;
    var _pageStyle;
    var _totalRow;
    var _rowPerPage;
    var _totalPage;
    var _pagePerScreen;
    var _pagerID = false;

    this.DataPopulationCallback = null;

    this.InitiatePager = function (pagerID, workingTable, rowPerPage, pagePerScreen, pageStyle) {
        _workingTable = '#' + workingTable;
        _rowPerPage = rowPerPage;
        _pagePerScreen = pagePerScreen;
        _pageStyle = pageStyle;
        _pagerID = pagerID;
    }

    this.RemovePager = function () {
        $('#' + _pagerID).remove();
    }

    this.SetTotalRows = function (totalRows) {
        var prevTotalRow = _totalRow;
        _totalRow = totalRows;
        _totalPage = Math.ceil(_totalRow / _rowPerPage);

        if (totalRows == 0) {
            DrawPager(0, 0, false, false);
        }

        var showLast = true;

        var endPage;
        if (_pagePerScreen >= _totalPage) {
            showLast = false;
            endPage = _totalPage;
        } else {
            endPage = _pagePerScreen;
        }

        if (prevTotalRow != _totalRow) {
            DrawPager(1, endPage, false, showLast);
        }
    }

    var DrawPager = function (startPage, endPage, showFirst, showLast) {
        var content = '<div id="' + _pagerID + '" class="' + _pageStyle + '">';
        content = content + (showFirst == true ?
                            '<input id="btn" type="button" value="<<" /><input id="btn" type="button" value="<" />' :
                            '<input id="btn" disabled="disabled" type="button" value="<<" /><input id="btn" disabled="disabled" type="button" value="<" />');

        if (startPage > 0 && endPage > 0) {
            for (var i = startPage; i <= endPage; i++) {
                content = content + '<input id="btn" type="button" value="' + i + '" />';
            }
        }

        content = content
                    + (showLast == true ?
                        '<input id="btn" type="button" value=">" /><input id="btn" type="button" value=">>" />' :
                        '<input id="btn" disabled="disabled" type="button" value=">" /><input id="btn" disabled="disabled" type="button" value=">>" />');

        content = content + '</div>';
        $('#' + _pagerID).remove();
        $(_workingTable).after(content);
        $('#' + _pagerID).addClass(_pageStyle);
        $('#' + _pagerID).width($(_workingTable).width());
        $('#' + _pagerID + ' input').unbind('click').click(function (event) { PageButtonClicked(event, $(this)); });
        $('#' + _pagerID + ' input').addClass('pagerbutton');
        
        if (_totalRow > 0) $('#' + _pagerID + ' input:eq(2)').trigger('click', this);
    }

    var PageButtonClicked = function (event, button) {
        if ($.isFunction(thisInstance.DataPopulationCallback) == false) { throw 'You must set DataPopulationCallback properly.'; }

        var startPage, endPage;
        var startIndex, endIndex, skipCount, takeCount;
        var showFirst = true, showLast = true;

        if ($(button).val() == '>') {
            startPage = Number($(button).prev().val()) + 1;
            endPage = Number($(button).prev().val()) + _pagePerScreen
            if (_totalPage <= endPage) { endPage = _totalPage; }
        } else if ($(button).val() == '>>') {
            startPage = Math.ceil(_totalPage / _pagePerScreen) * _pagePerScreen;
            endPage = _totalPage;
        } else if ($(button).val() == '<') {
            startPage = Number($(button).next().val()) - _pagePerScreen
            endPage = Number($(button).next().val()) - 1;
            if (_totalPage <= endPage) { endPage = _totalPage; }
        } else if ($(button).val() == '<<') {
            startPage = 1; endPage = _pagePerScreen;
            if (_totalPage <= endPage) { endPage = _totalPage; }
        } else {
            endIndex = _rowPerPage * Number($(button).val());
            startIndex = endIndex - _rowPerPage + 1;
            skipCount = startIndex - 1;
            takeCount = _rowPerPage;

            $('#' + _pagerID + ' input').addClass('pagerbutton');
            $(button).removeClass('pagerbutton').addClass('selectedbutton');

            thisInstance.DataPopulationCallback(startIndex, endIndex, skipCount, takeCount);
        }

        if ($(button).val() == '>' || $(button).val() == '>>' || $(button).val() == '<' || $(button).val() == '<<') {
            if (startPage == 1) { showFirst = false; }
            if (endPage >= _totalPage) { showLast = false; }
            DrawPager(startPage, endPage, showFirst, showLast);            
        }
    }

}
