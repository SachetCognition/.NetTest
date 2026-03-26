// Override of the defaults jquery validator to fits specific needs of components.
jQuery.validator.setDefaults({

    ignore: '.ignore',//':hidden:not(.notIgnore),.ignore',
    highlight: function (element, errorClass, validClass) {
        var accordianHeader = $(element).parents().prev('.noSelect');

        if (accordianHeader.attr("class") == "noSelect") {
            accordianHeader.click();
        }

        if (element.type === "radio") {
            this.findByName(element.name).addClass(errorClass).removeClass(validClass);
            this.findByName(element.name).removeClass('success').addClass('error');
        } else {
            $(element).addClass(errorClass).removeClass(validClass);
            $(element).closest('div').removeClass('success').addClass('error');
        }
       
    },
    showErrors: function () {
        this.defaultShowErrors();
        if ($("form").attr("data-info") != "success") {
            var activeSpan = $(document).find('span.field-validation-error').first();
            if (activeSpan.length > 0) {
                var clientAnchor = activeSpan.closest('a');
                if (clientAnchor.length > 0) {
                    clientAnchor.attr("tabindex", 0);
                    clientAnchor.addClass("selectionFocus");
                    clientAnchor.focus();

                    $('html, body').animate({
                        scrollTop: ($(activeSpan).offset().top) - 400
                    }, 500);
                }
            }
            $("form").attr("data-info", "success");
        }
    },
    unhighlight: function (element, errorClass, validClass) {
        if (element.type === "radio") {
            this.findByName(element.name).removeClass(errorClass).addClass(validClass);
            this.findByName(element.name).removeClass('error').addClass('success');
        } else {
            $(element).removeClass(errorClass).addClass(validClass);
            $(element).closest('div').removeClass('error').addClass('success');
        }
        
    }
});
