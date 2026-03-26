(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.DualList = function (element, options) {
        // Public attributes
        this.element = element;

        var init = function () {
            attachEvents();
            populateHiddenField();
        },
            attachEvents = function () {
                if (options.btnAllLefttoRightId != null) {
                    $('#' + options.btnAllLefttoRightId).click(addAll);
                }
                if (options.btnAllRightToLeft != null) {
                    $('#' + options.btnAllRightToLeft).click(removeAll);
                }
                if (options.btnOneLeftToRight != null) {
                    $('#' + options.btnOneLeftToRight).click(addSelect);
                }
                if (options.btnOneRightToLeft != null) {
                    $('#' + options.btnOneRightToLeft).click(removeSelect);
                }
               
                // Attach double click event with left and right list box if the DualList is updatable, i.e. leftListBoxId is not null
                if (options.leftListBoxId != null) {
                    $('#' + options.leftListBoxId).on("dblclick", null, '#' + options.rightListBoxId, selectByDblClick);
                    $('#' + options.rightListBoxId).on("dblclick", null, '#' + options.leftListBoxId, selectByDblClick);
                    $('#' + options.rightListBoxId).on("click", null, '#' + options.rightListBoxId, selectByClick);
                    $('#' + options.leftListBoxId).on("click", null, '#' + options.leftListBoxId, selectByClick);

                }
            },
            addAll = function () {
                var maxLimit = false;
                var retval = false;

                if (!BeforeItemAdd("ADDALL")) {
                    return false;
                }

                if (options.maxLimitValue > 0) {
                    maxLimit = maxLimitReached(true, options.maxLimitValue, options.maxLimitErrorMessage);
                }
                if (!maxLimit) {
                    // To move all items from left to right
                    var atLeastOneOptionMoved = false;
                    $('#' + options.leftListBoxId + ' option').each(function () {
                        // Append all items of left with right lest box
                        $(this).appendTo('#' + options.rightListBoxId);
                        $(this)[0].selected = false;
                        atLeastOneOptionMoved = true;
                    });
                    if (atLeastOneOptionMoved) {
                        populateHiddenField();
                        if (options.itemAdded) {
                            retval = options.itemAdded.call(this);
                        }
                        $('#' + options.rightListBoxId).focus();
                    }
                }
                return retval;
            },
            /*<summary>
           To remove all items from right to left on click of "Remove All" button of dual list
        <return>false</return>
        </summary>*/
            removeAll = function () {
                var isMandatory = false;
                var retval = false;
                var atLeastOneOptionMoved = false;
                // To move all items from right to left
                $('#' + options.rightListBoxId + ' option').each(function () {
                    // append all items to left list box
                    if (isMandatoryField($(this))) {
                        isMandatory = true;
                    } else {
                        $(this).appendTo('#' + options.leftListBoxId);
                        $(this)[0].selected = false;
                        atLeastOneOptionMoved = true;
                    }
                });
                // Show error message for mandatory
                if (isMandatory) {
                    alert(options.mandatoryFieldErrorMessage);
                }
                if (atLeastOneOptionMoved) {
                    if (options.toOrderLeftList) {
                        sortLeftList();
                    }
                    populateHiddenField();
                    if (options.itemRemoved) {
                        retval = options.itemRemoved.call(this);
                    }
                    $('#' + options.leftListBoxId + ' option').focus();
                }
                return retval;
            },

            sortLeftList = function () {
                $('#' + options.leftListBoxId + ' option').sort(function (a, b) {
                    //return (a.innerHTML > b.innerHTML) ? 1 : -1;
                    return a.innerHTML.toLowerCase().localeCompare(b.innerHTML.toLowerCase());
                }).appendTo($('#' + options.leftListBoxId));
            },

        /*<summary>
           To add all selected items in selected list on click of "Add Selected" button of dual list
        <return>false</return>
        </summary>*/
            addSelect = function () {
                var maxLimit = false;
                var retval = false;

                if (!BeforeItemAdd("ADDSELECT")) {
                    return false;
                }

                if (options.maxLimitValue > 0) {
                    maxLimit = maxLimitReached(false, options.maxLimitValue, options.maxLimitErrorMessage);
                }
                
                if (!maxLimit) {
                    // To move all selected items from left to right
                    var selectedList = $("#" + options.leftListBoxId + " > option:selected");

                    if (selectedList.length > 0) {
                        var selectedIndex;
                        var selectedItems = selectedList.clone();
                        selectedIndex = $("#" + options.leftListBoxId + " > option:selected")[0].index;
                        selectedList.remove();
                        $('#' + options.rightListBoxId).append(selectedItems);

                        if (selectedIndex != -1 && selectedIndex < $('#' + options.leftListBoxId + ' option').length) {
                            $('#' + options.leftListBoxId + ' option')[selectedIndex].selected = true;
                            $('#' + options.leftListBoxId).focus();
                        }
                    }

                    if (selectedList.length > 0) {
                        populateHiddenField();
                        if (options.itemAdded) {
                            retval = options.itemAdded.call(this);
                        }
                        $('#' + options.rightListBoxId).focus();
                    }
                }
                return retval;
            },
            /*<summary>
          To remove all selected items from selected list on click of "Remove Selected" button of dual list
        <return>false</return>
        </summary>*/
            removeSelect = function () {
                var isMandatory = false;
                var retval = false;
                // To move all selected items from right to left
                //var atLeastOneOptionMoved = false;
                var selectedIndex = -1;

                // Check if mandatory fields are present
                var mandatoryFields = $('#' + options.rightListBoxId + ' option:selected[value$="_1"]');
                isMandatory = mandatoryFields.length > 0;

                // if mandatory fields are found
                if (isMandatory) {
                    for (var i = 0; i < mandatoryFields.length; i++) {
                        mandatoryFields[i].selected = false;
                    }
                }
                
                // Get selected list from the list of option and clone it
                var selectedList = $("#" + options.rightListBoxId + " > option:selected");
                if (selectedList.length > 0) {
                    var selectedItems = selectedList.clone();

                    // Add the selected items to left side list
                    $('#' + options.leftListBoxId).append(selectedItems);

                    // Remove the data from right side list
                    selectedList.remove();

                    if (selectedIndex != -1 && selectedIndex < $('#' + options.rightListBoxId + ' option').length) {
                        $('#' + options.rightListBoxId + ' option')[selectedIndex].selected = true;
                        $('#' + options.rightListBoxId).focus();
                    }
                }

                /// Show the mandatory error message in case list is mandatory
                if (isMandatory) {
                    alert(options.mandatoryFieldErrorMessage);
                }

                if (selectedList.length > 0) {
                    if (options.toOrderLeftList) {
                        sortLeftList();
                    }
                    populateHiddenField();
                    if (options.itemRemoved) {
                        retval = options.itemRemoved.call(this);
                    }
                    $('#' + options.leftListBoxId).focus();
                }

                return retval;
            },
            /*<summary>
            To allow user to select item in one list at a time(Remark:196).
        <params>event</params>
        </summary>*/
            selectByClick = function () {
                var selectedRightBoxValue = $('#' + options.rightListBoxId).val();
                var selectedLeftBoxValue = $('#' + options.leftListBoxId).val();

                if (this.id == options.leftListBoxId) {
                    if ($('#' + options.rightListBoxId).has('option').length > 0) {
                        if (selectedRightBoxValue && selectedLeftBoxValue) {
                            //to remove selection from right list if user has selected item from left list .
                            $('#' + options.rightListBoxId).prop('selectedIndex', -1);
                        }
                    }
                } else if (this.id == options.rightListBoxId) {
                    if (selectedLeftBoxValue && selectedRightBoxValue) {
                        //to remove selection from left list if user has selected item from left list .
                        $('#' + options.leftListBoxId).prop('selectedIndex', -1);

                    }

                }
            },
            /*<summary>
            To move selected item by double click on item
        <params>event</params>
        </summary>*/
            selectByDblClick = function (event) {                
                var otherSelect = event.data;
                var option;
                var atLeastOneOtionMoved = false;
                var isRight = false;
                var retval = false;
            //alert(event.data);

                if (event.target.tagName == "OPTION") {
                    option = $(event.target);
                } else {
                    var optionslist = event.target.options;
                    for (var i = 0, n = optionslist.length; i < n; i++) {
                        if (optionslist[i].selected == true) {
                            option = optionslist[i];
                            break;
                        }
                    }
                }
                if (option) {
                    if (otherSelect == '#' + options.leftListBoxId) {
                        isRight = false;

                        if (isMandatoryField($(option))) {
                            alert(options.mandatoryFieldErrorMessage);
                        } else {
                            var index1 = $(option)[0].index;
                            $(option).appendTo(otherSelect);
                            $(option)[0].selected = false;
                                                          
                            if (index1 < $('#' + options.rightListBoxId + ' option').length) {
                                $('#' + options.rightListBoxId + ' option')[index1].selected = true;
                            }                  
                            atLeastOneOtionMoved = true;
                            //<AR='2015.011987-A.01' Author='RC' Date='20.8.2015' Iteration='ITR2'
                            if (options.toOrderLeftList) {
                                sortLeftList();
                            }
                            //</AR>
                            $('#' + options.leftListBoxId).focus();
                        }
                    } else {
                        isRight = true;
                        if (!BeforeItemAdd("DBLCLICK")) {
                            return false;
                        }

                        var maxLimit = false;
                        if (options.maxLimitValue > 0) {
                            maxLimit = maxLimitReached(false, options.maxLimitValue, options.maxLimitErrorMessage);
                        }
                        if (!maxLimit) {
                            var index = $(option)[0].index;
                            $(option).appendTo(otherSelect);
                            $(option)[0].selected = false;
                            if (index < $('#' + options.leftListBoxId + ' option').length) {
                                $('#' + options.leftListBoxId + ' option')[index].selected = true;
                            }
                            atLeastOneOtionMoved = true;
                            $('#' + options.rightListBoxId).focus();
                        }
                    }
                }
                if (atLeastOneOtionMoved) {
                    populateHiddenField();
                    if (isRight) {
                        if (options.itemRemoved) {
                            retval = options.itemRemoved.call(this);
                        }
                    } else {
                        if (options.itemAdded) {
                            retval = options.itemAdded.call(this);                           
                        }
                    }
                }
                return retval;
            },
            /*<summary>
            To populate hidden fields for dual list
        </summary>*/
            populateHiddenField = function () {
                var hiddenFieldRight = new Array();
                // To populate left hidden fields
                if (options.leftListBoxId != null) {
                    var hiddenFieldLeft = new Array();
                    $('#' + options.leftListBoxId + ' option').each(function () {
                        // All items Separated by ;
                        hiddenFieldLeft.push($(this)[0].value);
                    });

                    if (hiddenFieldLeft.length > 0) {
                        $('#' + options.availableItemModelBindName).attr('value', JSON.stringify(hiddenFieldLeft));
                    } else {
                        $('#' + options.availableItemModelBindName).attr('value', '');
                    }
                }

                // To populate right hidden fields
                $('#' + options.rightListBoxId + ' option').each(function () {
                    // All items Separated by ;
                    hiddenFieldRight.push($(this)[0].value);
                });
                if (hiddenFieldRight.length > 0) {
                    $('#' + options.selectedItemModelBindName).attr('value', JSON.stringify(hiddenFieldRight));
                } else {
                    $('#' + options.selectedItemModelBindName).attr('value', '');
                }
            },


            /*<summary>
            To check for mandatory field
        <params>option</params>
        <return>boolean value</return>
        </summary>*/
            isMandatoryField = function (option) {
                if (option[0].value.split("_").length == 2) {
                    if (option[0].value.split("_")[1] == "1") {
                        return true;
                    }
                }
                return false;
            },
              BeforeItemAdd = function (SenderId) {
                  var retval = true;
                  if (options.OnBeforeItemAdding) {
                      retval = options.OnBeforeItemAdding(SenderId);
                  }
                  return retval;
              },
            maxLimitReached = function (bolPToutS, intMaxLimit, strMessage) {
                var optionsListboxRight = $('#' + options.rightListBoxId + ' option');
                var optionsListboxLeft = $('#' + options.leftListBoxId + ' option');
                var nbElementToAdd = 0;
                if (bolPToutS == true) {
                    nbElementToAdd = optionsListboxLeft.length;
                } else {
                    for (var i = 0; i < optionsListboxLeft.length; i++) {
                        if (optionsListboxLeft[i].selected == true) {
                            nbElementToAdd = nbElementToAdd + 1;
                        }
                    }
                }
                if ((nbElementToAdd + optionsListboxRight.length) > intMaxLimit) {
                    //TPOAR03F01T01CE06
                    alert(strMessage);
                    return true;
                } else {
                    return false;
                }
            };
        // Constructor code
        $.extend(this, options);

        init();
    };

    // JQuery extention function
    $.fn.dualList = function (options) {
        var name = "dualList";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.DualList(element, opt);
        },
        options).data(name);
    };

}(jQuery));