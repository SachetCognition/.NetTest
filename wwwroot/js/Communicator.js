(function ($) {
    // Alias
    var $s = $.sav2000;
    
    // Constructor
    $s.Communicator = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        var init = function () {
            userElement = $('#' + options.id);
            if (userElement != null) {
                getCommunicatorActiveXObject();
                attachEvents();
                disableContextMenu();
                setTimeout(onCommunicatorStatusChange, 1000);
                if (options.DelayCommunicator > 0) {
                    setInterval(onCommunicatorStatusChange, options.DelayCommunicator);
                }
            }

        }, attachEvents = function () {

            if ((nameCtrl != null) && (nameCtrl.PresenceEnabled == 1)) {
                userElement.mouseover(showCommunicatorPopup);
                userElement.mouseout(hideCommunicatorPopup);

                //Bind the Event "OnStatusChange" with ActiveX Plug-in object.
                //nameCtrl.OnStatusChange = onCommunicatorStatusChange;
            }

        }, getCommunicatorActiveXObject = function () {
            try {
                if (window.ActiveXObject || (window.hasOwnProperty && window.hasOwnProperty('ActiveXObject'))) {
                    nameCtrl = new ActiveXObject("Name.NameCtrl.1");
                    try {
                        //Updating the ActiveX plug-in Object wit status.
                        nameCtrl.GetStatus(options.userEmail, 'users');
                    } catch (e) {
                        window.console && console.log("Exception in GetStatus" + "\n" + e.toString());
                    }
                    if (nameCtrl == null) {
                        return;
                    } 
                    if (nameCtrl.PresenceEnabled == 0) {
                        window.console && console.log("After Get call and Event Binding, nameCtrl.PresenceEnabled : " + nameCtrl.PresenceEnabled);
                    }
                } 
            } catch (e) {
                try {
                    window.console && console.log("ActiveX Object Not Found for Name Space" + "Name.NameCtrl.1" + "\n" + e.toString());
                    //check with different namespace.
                    nameCtrl = new ActiveXObject("Name.NameCtrl");
                } catch (ex) {
                    window.console && console.log("ActiveX Object Not Found for Name Space" + "Name.NameCtrl" + "\n" + e.toString());
                } 
                
            }
        }, disableContextMenu = function () {
            userElement.bind("contextmenu", function () {
                return false;
            });
        }, onCommunicatorStatusChange = function () {
               if ((nameCtrl != null) && (nameCtrl.PresenceEnabled == 1)) {
                   status = nameCtrl.GetStatus(options.userEmail, 'users');

                   var presenceClass = getCommunicatorString(status);
                   window.console && console.log("Version 2 Communicator Status Changed " + "PresenceEnabled : " + nameCtrl.PresenceEnabled + "\n" + "Status: " + status + "=" + presenceClass + " user name:" + options.userEmail);
                   //Updating the CSS for Component as per the returned status.
                   if (presenceClass != null) {
                       removePresenceClasses(userElement.find('img'));
                       userElement.find('img').addClass(presenceClass);
                   }
               }

           }, getCommunicatorString = function (statusCode) {
            var imgElement = userElement.find('img');
            switch (statusCode) {
                case 0:
                    imgElement.attr("src", "/Images/lync_available.png");
                    return 'available';
                case 1:                    
                    imgElement.attr("src", "/Images/lync_offline.png");
                    return 'signout';
                case 2:
                    imgElement.attr("src", "/Images/lync_away.png");
                    return 'away'; 
                case 3:
                    imgElement.attr("src", "/Images/lync_busy.png");
                    return 'busy';
                case 4:
                    imgElement.attr("src", "/Images/lync_away.png");
                    return 'berightback';
                case 9:
                    imgElement.attr("src", "/Images/lync_DND.png");
                    return 'donotdisturb';
                    
                case 16:
                case 5:
                    imgElement.attr("src", "/Images/lync_DND.png");
                    return 'inacall';
                case 6:
                case 7:
                    imgElement.attr("src", "/Images/lync_busy.png");
                    return 'in a meeting';
                case 8:
                case 10:
                case 15:
                default:
                    return '';
            }
        }, removePresenceClasses = function (jqueryObj) {
            jqueryObj.removeClass('available');
            jqueryObj.removeClass('signout');
            jqueryObj.removeClass('away');
            jqueryObj.removeClass('busy');
            jqueryObj.removeClass('donotdisturb');
            jqueryObj.removeClass('berightback');
            jqueryObj.removeClass('inacall');
        }, showCommunicatorPopup = function () {
            if (!nameCtrl) {
                return;
            }
            var eLeft = $(this)[0].offsetLeft;//$(target).offset().left;
            var x = eLeft - $(window).scrollLeft();

            var eTop = $(this)[0].offsetTop; //$(target).offset().top;
            var y = eTop - $(window).scrollTop();
            
            nameCtrl.ShowOOUI(options.userEmail, 0, x, y);
        }, hideCommunicatorPopup = function () {
            if (!nameCtrl) {
                return;
            }
            nameCtrl.HideOOUI();
        }, status, userElement, nameCtrl;

        init();
    };

    // JQuery extention function
    $.fn.communicator = function (options) {
        var name = "communicator";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.Communicator(element, opt);
        },
        options).data(name);
    };
}(jQuery));