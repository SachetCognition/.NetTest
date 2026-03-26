(function($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.BreadCrumbs = function(element, options) {
        // Public attributes
        var that = this;
        this.moduleName = "BreadCrumbs";
        this.lastItemId = "lastItem";
        this.element = element;

        // Privileged functions
        this.applySmoothDivScroll = function() {
            this.element.smoothDivScroll({
                visibleHotSpotBackgrounds: "always",
                startAtElementId: this.lastItemId
            });
        };

        // Constructor code
        $.extend(this, options);

        // Enable smoothDivScroll
        this.applySmoothDivScroll();

        // For each element bind on click event
        if (this.onItemClick) {
            $(document).on("click", "#" + element[0].id + " a", this.onItemClick);
        }
        
        // Apply smoothDivScroll on each breadcrumbs modification
        element.on("push pop clear bind", function () {
            that.applySmoothDivScroll();
        });
    };

    // Item of the breadcrumbs
    $s.BreadCrumbs.item = function(title, url) {
        this.title = title;
        this.url = url;
    };

    // Public functions
    $s.BreadCrumbs.prototype = {
    
        // Add an item at the end of the breadcrumbs
        // ignoreEvent can be specified to not trigger the push event
        push: function (item, ignoreEvent) {
            if (item instanceof $s.BreadCrumbs.item) {
                // Update previous last item
                var scrollableArea = $(".scrollableArea", this.element);
                var last = $(".scrollableArea", this.element).children().last();
                var newSpan = $("<span id='lastItem' class='" + this.cssItemActive + "'>" + item.title + "</span>");
                if (last.length) {
                    last.removeAttr("id").removeClass("active");
                    var title = last.text();
                    last.empty();
                    last.append("<a class='" + this.cssItem + "' href='" + this.lastItemUrl + "'>" + title + "</a>");
                    // Add separator
                    var separator = $("<span class='" + this.cssSeparator + "'>&gt;</span>").insertAfter(last);
                    // Create new last item
                    newSpan.insertAfter(separator);
                } else {
                    // Create new last item
                    scrollableArea.append(newSpan);
                }
                // Update last item url
                this.lastItemUrl = item.url;
                if (!ignoreEvent) {
                    // Trigger push event
                    this.element.trigger("push", item);
                }
            } else {
                $s.error(this.moduleName, "push", arguments , "Invalid item type");
            }
        },
        
        // Remove the last item of the breadcrumbs and return it
        pop: function () {
            var spans = $(".scrollableArea", this.element).children();
            var last = spans.last();
            if (last.length) {
                var removedItem = new $s.BreadCrumbs.item(last.text(), this.lastItemUrl);
                // Remove last item and separator
                spans.slice(-2).remove();
                spans = $(".scrollableArea", this.element).children();
                // Update last item
                last = spans.last();
                var a = $("a", last);
                var title = a.text();
                // Update last item url
                this.lastItemUrl = a.attr("href");
                a.remove();
                last.attr("id", this.lastItemId);
                last.addClass(this.cssItemActive);
                last.text(title);
                // Trigger pop event
                this.element.trigger("pop", removedItem);
                return removedItem;
            } else {
                return {};
            }
        },
        
        // Clear all items of the breadcrumbs
        clear: function() {
            $(".scrollableArea", this.element).empty();
            // Update last item url
            this.lastItemUrl = "";
            // Trigger clear event
            this.element.trigger("clear");
        },
        
        // Bind from array of items
        bind: function(items) {
            var that = this;

            if (items instanceof Array) {
                var scrollableArea = $(".scrollableArea", this.element);
                scrollableArea.empty();
                $(items).each(function () {
                    // Call push without triggering push event
                    that.push(this, false);
                });
                // Update last item url
                this.lastItemUrl = items.length ? items[items.length-1].url : "";
                // Trigger bind event
                this.element.trigger("bind", items);
            }
        }
    };

    // JQuery extension function
    $.fn.breadCrumbs = function(options) {
        var name = 'breadCrumbs';
        return $s.create(
            this,
            name,
            function(element, opt) {
                return new $s.BreadCrumbs(element, opt);
            },
            options).data(name);
    };

}(jQuery));
