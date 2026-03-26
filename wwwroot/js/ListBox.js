(function ($) {
    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.ListBox = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        var init = function () {
            attachEvents();
            populateHiddenField();

        }, attachEvents = function () {
            if (options.btnSortUp != null) {
                $('#' + options.btnSortUp).click(moveUp);
                $('#' + options.btnSortDown).click(moveDown);
            }
        },
        /*<summary>
        To move all selected items up on click of "UP" button of dual list
        <return>false</return>
        </summary>*/
        moveUp = function () {
            var retval = false;
            var selection = $('#' + options.id + ' option:selected');
            if (selection.length > 0) {
                var first = selection[0];
                if (first.index > 0) {
                    // To move all selected items up on click of UP button
                    selection.each(function (i, option) {
                        // Put the selected items on Top
                        $('#' + options.id + ' option:eq(' + ((option.index == 0) ? 0 : option.index - 1) + ')').before($(this));
                    });
                    if (options.selectedItemModelBindName != null) {
                        populateHiddenField();
                    }
                }
                //To set First Value Selected
                for (var i = 1; i < selection.length; i++) {
                    selection[i].selected = false;
                }
                if (options.onMoveUp) {
                    retval = options.onMoveUp.call(this);
                }
            }
            return retval;
        },
            /*<summary>
            To move all selected items down on click of "DOWN" button of dual list
            <return>false</return>
            </summary>*/
                moveDown = function () {
                    var retval = false;
                    window.jQuery.fn.reverse = [].reverse;
                    var nbOptions = $('#' + options.id + ' option').length;
                    var selection = $('#' + options.id + ' option:selected');
                    if (selection.length > 0) {
                        var last = selection[selection.length - 1];
                        if (last.index < nbOptions - 1) {
                            // To move all selected items down on click of DOWN button
                            selection.reverse().each(function (i, option) {
                                // Put the selected items below
                                var idx = option.index + 1;
                                if (idx < nbOptions) {
                                    $('#' + options.id + ' option:eq(' + idx + ')').after($(this));
                                } else {
                                    $('#' + options.id).append($(this));
                                }
                            });
                            if (options.selectedItemModelBindName != null) {
                                populateHiddenField();
                            }
                        }
                        //To set First Value Selected
                        for (var i = selection.length-1; i>0; i--) {
                            selection[i].selected = false;
                        }

                        if (options.onMoveDown) {
                            retval = options.onMoveDown.call(this);
                        }

                    }
                    return retval;
                },
                /*<summary>
                To populate hidden fields when this ListBox is the right list box of a DualList 
                and move up or move down are clicked.
                </summary>*/
                populateHiddenField = function () {
                    var hiddenFieldRight = new Array();
                    // To populate hidden fields 
                    $('#' + options.id + ' option').each(function () {
                        // All items Separated by ;
                        hiddenFieldRight.push($(this)[0].value);
                    });
                    if (hiddenFieldRight.length > 0) {
                        $('#' + options.selectedItemModelBindName).attr('value', JSON.stringify(hiddenFieldRight));
                    } else {
                        $('#' + options.selectedItemModelBindName).attr('value','');
                    }
                };

        init();

        if (this.onChange) {
            element.change(this.onChange);
        }
    };

    // JQuery extention function
    $.fn.listBox = function (options) {
        var name = "listBox";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.ListBox(element, opt);
        },
        options).data(name);
    };

}(jQuery));