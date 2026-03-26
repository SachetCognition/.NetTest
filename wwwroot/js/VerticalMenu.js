/* this Jquery extension is used for the Menu Component */
(function ($) {
    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.VerticalMenu = function (element, options) {

        // Public attributes
        this.element = element;

        ///<summary>
        ///init events
        ///</summary>
        var init = function () {
            attachEvent();
            attachLinkEvents();
        };

        var attachLinkEvents = function () {

            var clickHandler = function (menuItemInfo) {
                var toSubmit = true;
                var menuItemOptions = menuItemInfo.menuItemOptions;
                if (menuItemOptions.OnClick) {
                    //click handler 'this' reference contains the element for which click was called, passing it as this to call onclick handler
                    //defined by the view
                    toSubmit = menuItemInfo.menuItemOptions.OnClick.call(menuItemInfo.thisContext, menuItemInfo.e);
                }
                if (toSubmit) {
                    var subForm = document.forms;
                    if (menuItemOptions.CausesValidation) {
                        if (!$(subForm[0]).valid()) {
                            return;
                        }
                    }
                    //<AR = '2015.309917-D.01' date = '07/03/2016' author = 'mangoel'>
                    if (!($('#hdnFormActionName').length > 0)) {
                        var formAction = document.createElement('input');
                        formAction.type = 'hidden';
                        formAction.name = 'hdnFormActionName';
                        formAction.id = 'hdnFormActionName';
                        formAction.value = subForm[0].action;
                        subForm[0].appendChild(formAction);
                    } else {
                        $('#hdnFormActionName').val(subForm[0].action);
                    }
                    //</AR>
                    if (subForm[0].action.indexOf('menuname') === -1) {
                        if (menuItemOptions.ActionUrl && menuItemOptions.ActionUrl != "#") {
                            subForm[0].action = menuItemOptions.ActionUrl;
                        }

                    }
                    subForm[0].action = subForm[0].action.replace('menuname', menuItemOptions.ActionUrl);
                    var nameToSend = menuItemOptions.Name;
                    if (nameToSend) {
                        //if no value is provided then use the name itself.
                        var valueToSend = nameToSend;
                        var input = document.createElement('input');
                        input.type = 'hidden';
                        input.name = nameToSend;
                        input.value = valueToSend;

                        subForm[0].appendChild(input);
                    }
                    if (menuItemOptions.CausesValidation) {
                        $(subForm[0]).submit();
                    } else {
                        subForm[0].submit();
                    }
                }
            };

            //set click handler for menu items
            var setUpClickFunction = function(menuItemProperties, item) {
                item.click(function (e) {
                    var toPass = {
                        menuItemOptions: menuItemProperties,
                        e: e,
                        thisContext: this
                    };
                    clickHandler(toPass);
                });
            };
            
            for (var indexOption = 0; indexOption < options.LinkRenderOptions.length; indexOption++) {
                var menuItemAttributes = options.LinkRenderOptions[indexOption];
                var menuItem = $("#" + menuItemAttributes.Id);
                setUpClickFunction(menuItemAttributes, menuItem);
            }
        };

        ///<summary>
        ///Attach events
        ///</summary>
        var attachEvent = function () {
            $('.navbar ul.nav-vertical li').click(manageOpenClose);
            $('.navbar ul.nav-vertical li.first a').click(selectedMenuItem);
            $('.navbar ul.nav-vertical li').each(manageAccesibility, "");
            $('.navbar .nav-vertical li .dropdown-toggle').not('.navbar .nav-vertical .dropdown-menu').click(manageLevel);
        };

        ///<summary>
        ///Manage Accessibility
        ///</summary>
        var manageAccesibility = function () {
            appendOpenCloseSpan(this);
        };

        ///<summary>
        ///Append Open Close Span
        ///</summary>
        var appendOpenCloseSpan = function (curentObject) {
            if ($(curentObject).hasClass('active')) {
                appendOpenSpan(curentObject);
            } else {
                appendCloseSpan(curentObject);
            }
        };

        ///<summary>
        ///Append  Close Span
        ///</summary>
        var appendCloseSpan = function (curElem) {
            var accessSpanClose = $(curElem).find('a.dropdown-toggle > span.hide-access.open');
            if (accessSpanClose.length > 0) {
                accessSpanClose[0].innerHTML = options.accessTextClose;
                accessSpanClose.addClass('close');
                accessSpanClose.removeClass('open');
            }
        };

        ///<summary>
        ///Append  Open Span
        ///</summary>
        var appendOpenSpan = function (curElem) {
            var accessSpan = $(curElem).find('a.dropdown-toggle > span.hide-access.close');
            if (accessSpan.length > 0) {
                accessSpan[0].innerHTML = options.accessTextOpen;
                accessSpan.addClass('open');
                accessSpan.removeClass('close');
            }
        };

        ///<summary>
        ///Open close menu
        ///</summary>
        var manageOpenClose = function () {
            $('.navbar ul.nav-vertical li').each(function () {
                if (this)
                    appendCloseSpan(this);
            });
            var currentElement = this;
            if ($(currentElement).hasClass('active')) {
                appendCloseSpan(currentElement);
            } else {
                $.each($(currentElement).parent().children(), function (i, val) {
                    if ($(val).hasClass('active') && $(val).find('a')[0].id != $(currentElement).find('a')[0].id) {
                        $(val).removeClass('active');
                    }
                });
                $(currentElement).addClass('active');
                appendOpenSpan(currentElement);
            }
        };

        ///<summary>
        ///manage Class in Open Close Span
        ///</summary>
        var manageOpenCloseSpan = function (curElem) {
            $('#navbar ul.nav-vertical li').each(function () {
                if ($(curElem).hasClass('active')) {
                    var accessSpanClose = $(curElem).find('a.dropdown-toggle > span.hide-access.close');
                    if (accessSpanClose.length > 0) {
                        accessSpanClose[0].innerHTML = options.accessTextOpen;
                        accessSpanClose.addClass('open');
                        accessSpanClose.removeClass('close');
                    }
                } else {
                    var accessSpan = $(curElem).find('a.dropdown-toggle > span.hide-access.open');
                    if (accessSpan.length > 0) {
                        accessSpan[0].innerHTML = options.accessTextClose;
                        accessSpan.addClass('close');
                        accessSpan.removeClass('open');
                    }
                }
            });

        };
        ///<summary>
        ///Manage level1 click
        ///</summary>
        var manageLevel = function () {
            if ($(this).parent().find('.dropdown-menu').css("display") == 'none') {
                $(".nav-vertical li").each(function () {
                    if ($(this).find('.dropdown-menu').css("display") == 'block') {
                        $(this).find('.dropdown-menu').animate({ height: 'toggle' });
                        $(this).removeClass('active');
                        manageOpenCloseSpan($(this));
                        if ($(this).find('.dropdown-submenu').css("display") == 'block') {
                            $(this).find('.dropdown-submenu').animate({ height: 'toggle' });
                            $(this).removeClass('active');
                        }
                    }
                });

                $(".nav-vertical li").each(function () {

                    if ($(this).attr("class") != "active") {

                        $(this).find('.dropdown-menu').css("display", "none");


                    }
                });
                $(this).parent().parent().find('li.first').removeClass('active');
                $(this).parent().addClass('active');
                manageOpenCloseSpan($(this).parent());

            } else {
                var accessSpan = $($(this).parent()).find('a.dropdown-toggle > span.hide-access.open');
                if (accessSpan.length > 0) {
                    accessSpan[0].innerHTML = options.accessTextClose;
                    accessSpan.addClass('close');
                    accessSpan.removeClass('open');
                }
            }
           
            if ($(this).parent().find('.dropdown-menu').length > 0) {
                manageMenuToggle(this);
                return false;
            } else {
                
               selectedMenuItem(this.id);
            }


            return true;
        };

        ///<summary>
        ///Manage level1 click
        ///</summary>
        var manageMenuToggle = function (curelement) {
            var cls = parseInt($(curelement).parent().find(".dropdown-menu li").length);
            if (cls != 0) {
                $(curelement).parent().find('.dropdown-menu').animate({ height: 'toggle' });
            }
        };

        var selectedMenuItem = function () {
            
            setSelectedMenuItem(this.id);
        };

        var setSelectedMenuItem = function (itemName) {
           
            $("input[name=" + options.MenuName + "]").val(itemName);
        };
        // Constructor code
        $.extend(this, options);

        init();
    };

    // JQuery extension function
    $.fn.verticalMenu = function (options) {
        var name = "verticalMenu";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.VerticalMenu(element, opt);
        },
         options).data(name);
    };

}(jQuery));