this.buildMasterObject = function (elementList) {
        //var elementList = this;
        var masterData = {};

        var itemWithDot = new Array();
        var itemWithoutDot = new Array();

        $(elementList).each(function (index, item) {
            if ($(item).attr('data-modelproperty').toString().indexOf('.') === -1) {
                itemWithoutDot.push(item);
            } else {
                itemWithDot.push(item);
            }
        });

        $.each(itemWithoutDot, function (index, item) {
            if ($(item).find('option').size() > 0) {
                masterData[$(item).attr('data-modelproperty')] = $(item).val();
            } else if ($(item).attr('type') == 'text' || $(item).attr('type') == 'hidden') {
                if ($(item).attr('data-modelproperty-type') == 'datetime')
                    masterData[$(item).attr('data-modelproperty')] = $(item).datePostbackFormat();
                else
                    masterData[$(item).attr('data-modelproperty')] = $(item).trimmedValue();
            } else if ($(item).is('TEXTAREA')) {
                masterData[$(item).attr('data-modelproperty')] = $(item).trimmedValue();
            } else if ($(item).attr('type') == 'radio' && $(item).attr('checked') == 'checked') {
                masterData[$(item).attr('data-modelproperty')] = $(item).val();
            } else if ($(item).attr('type') == 'checkbox') {
                if ($(item).attr('checked') == 'checked') {
                    masterData[$(item).attr('data-modelproperty')] = true;
                } else {
                    masterData[$(item).attr('data-modelproperty')] = false;
                }
            }
        });

        //    childelementList = $("[data-modelproperty*='.']");
        if (itemWithDot.length > 0) {//**************************has child object*******************************
            var childElement = new Array();
            $.each(itemWithDot, function (index, item) {
                childElement.push($(item).attr('data-modelproperty').toString().split('.', 1).toString());
            });
            childElement = $.unique(childElement);
            for (i = 0; i < childElement.length; i++) {
                masterData[childElement[i]] = {};
                $.each(itemWithDot, function (index, item) {
                    var arrayChildItem = $(item).attr('data-modelproperty').toString().split('.', 2);
                    if (childElement[i] == arrayChildItem[0])
                        masterData[childElement[i]][arrayChildItem[1]] = $(item).val();
                });
            };

        }; //**************************build child object complete*******************************


        return masterData;
    }