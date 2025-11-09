/* this Jquery extension is used for the Menu Component */
(function ($) {
    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.HorizontalMenu = function (element, options) {
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
                        if (menuItemOptions.Value && menuItemOptions.Value !== null && menuItemOptions.Value !== '') {
                            valueToSend = menuItemOptions.Value;
                        }
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
            var setUpClickFunction = function (menuItemProperties, item) {
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
            $('#' + element[0].id + ' .nav > li a').not('#' + element[0].id + ' .dropdown-menu a').click(manageClickFirstLevel);
            //<author='siddsharma' desc='removed manageopen function and applied open class in htmlbuilder itself'>

            $('#' + element[0].id + ' .dropdown-menu a').click(manageClickSecondLevel);
        };

        ///<summary>
        ///Manage the click on the 1st level of the horizontal menu (level 3)
        ///</summary>
        var manageClickFirstLevel = function () {
            var subForm = document.forms;
            if (options.causeValidation) {
                if (!$(subForm[0]).valid()) {
                    return;
                }
            }
                var level3 = $(this).parent();
                var parentLi = $('#' + element[0].id + ' .nav').children('li').index(level3);
                var redirect = $(this).attr('href');
                if (redirect != null && redirect.indexOf("#") >= 0) {
                    if (parentLi != null) {
                        var allMenus = $('#' + element[0].id + ' .nav li');
                        allMenus.removeClass('active');
                        //var allOpenHideAccessSpans = $('#' + element[0].id + ' .hide-access.open');
                        var allOpenHideAccessSpans = $('#' + element[0].id + ' .nav > li a > span.hide-access.open');
                        if (allOpenHideAccessSpans.length > 0) {
                            allOpenHideAccessSpans[0].innerHTML = options.accessTextClose;
                            allOpenHideAccessSpans.addClass('close');
                            allOpenHideAccessSpans.removeClass('open');
                        }
                        var activeMenu = $('#' + element[0].id + ' .nav > li').eq(parentLi);
                        activeMenu.addClass("active");
                        var activeMenuHideAccessSpan = activeMenu.find("a > span.hide-access.close");
                        if (activeMenuHideAccessSpan.length > 0) {
                            activeMenuHideAccessSpan[0].innerHTML = options.accessTextOpen;
                            activeMenuHideAccessSpan.addClass('open');
                            activeMenuHideAccessSpan.removeClass('close');
                        }
                        allMenus.not(".active").find(".dropdown-menu").hide();
                        if (activeMenu.find(".dropdown-menu").length > 0) {
                            activeMenu.find(".dropdown-menu").show();
                            level3.parent('ul').addClass('open');
                        } else if (activeMenu.find(".dropdown-menu").length == 0 && level3.parent('ul').hasClass('open')) {
                            level3.parent('ul').removeClass('open');
                        }

                    }
                
            }

        };

        ///<summary>
        ///Manage the click on the 1st level of the horizontal menu (level 3)
        ///</summary>
        var manageClickSecondLevel = function () {
            var level4 = $(this).parent('li');
            var currentLi = $(this).parents('.dropdown-menu').children('li').index(level4);
            $('#' + element[0].id + ' .dropdown-menu li').removeClass('active');
            //Put the clicked 2nd level item in orange just before the redirection to have the orange color on the item during the loading of the next page
            $('#' + element[0].id + ' .nav li.active .dropdown-menu li').eq(currentLi).addClass("active");
            return true;
        };

        //<author='siddsharma' desc='removed manageopen function and applied open class in htmlbuilder itself'>

        // Constructor code
        $.extend(this, options);

        init();


    };

    // JQuery extension function
    $.fn.horizontalMenu = function (options) {
        var name = "horizontalMenu";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.HorizontalMenu(element, opt);
        }, options
        ).data(name);
    };

}(jQuery));

