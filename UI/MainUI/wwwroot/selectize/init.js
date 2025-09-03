var dynamicElementsClass = 'shakaDynamicDropDowns';
function initSelects(selector, minimumOptionCount, initialValue)
{
    $(selector).each(function () {
        if ($(this).find('option').length > minimumOptionCount)
        {
            $(this).css("display", "none");
            var selectElementId = $(this).attr("id");
            $(this).val(initialValue);
            var listElementId = "listFor" + selectElementId;
            var inputElementId = "inputFor" + selectElementId;

            var list = $("#" + listElementId);

            if (list.length == 0) {
                list = $("<datalist id='" + listElementId + "' class='" + dynamicElementsClass + "'>");
                $(this).parent().prepend(list);
            }

            var refreshOptions = false;
            var selectInputOptions = $(this).find("option");
            var dataListOptions = list.find("option");

            if (selectInputOptions.length != dataListOptions.length) {
                refreshOptions = true;
                console.log('lenght is different' + (selectInputOptions.length - 1) + ", " + dataListOptions.length);
            }
            else {
                for (var i = 0; i < selectInputOptions.length; i++) {
                    if ($(selectInputOptions[i]).val() != $(dataListOptions[i]).data('value')) {
                        refreshOptions = true;
                        console.log('differenc is:' + $(selectInputOptions[i]).val() + ', ' + $(dataListOptions[i]).data('value'));
                        break;
                    }
                }
            }

            if (refreshOptions) {
                list.empty();
                $(this).find("option").each(function () {
                    var value = $(this).val();
                    var text = $(this).text();
                    var listItem = $("<option value='" + text + "' data-value=" + value + ">");
                    list.append(listItem);
                });
            }

            var input = $("#" + inputElementId);
            if (input.length == 0) {
                input = $("<input id='" + inputElementId + "' onchange='selectionChanged(this)' data-selectId='" + selectElementId + "' data-dataListId='" + listElementId + "' class='" + dynamicElementsClass + " form-control forn-control-sm' list='" + listElementId + "' autocomplete='off'>");
                input.keydown(function (event) {
                    var key = event.keyCode || event.charCode;
                    if (key == 8 || key == 46) {
                        if ($(this).val() == "") {
                            selectionChanged(this);
                        }
                    }
                });
                $(this).parent().prepend(input);
            }

            input.val("");
            input.attr("placeholder", $("#" + selectElementId + " option:selected").text());
        }
    });
}

function clearSelects(selector) {
    $(selector).each(function () {
        $(this).parent().find("." + dynamicElementsClass).each(function () {
            $(this).remove();
        });
    });
}

function selectionChanged(sender) {
    var inputValue = $(sender).val();
    var dataListId = $(sender).attr("data-dataListId");
    var selectId = $(sender).attr("data-selectId");
    var value = document.querySelector("#" + dataListId + " option[value='" + inputValue + "']").dataset.value;
    $("#" + selectId).val(value);
    $("#" + selectId)[0].dispatchEvent(new Event('change'));

    $(sender).attr("placeholder", inputValue);
    $(sender).val("");
}

function initSelectize(selector, minimumOptionCount) {
    $(selector).each(function () {
        if ($(this).find('option').length > minimumOptionCount)
            $(this).selectize({
                plugins: ["remove_button"],
                onChange: function (value) {
                    var selectizeElement = $(this)[0];
                    var originalSelectElement = selectizeElement.$input["0"];
                    originalSelectElement.dispatchEvent(new Event('change'));
                },

                openOnFocus: false,
                onInitialize: function () {
                    var that = this;

                    this.$control.on("click", function () {
                        that.ignoreFocusOpen = true;
                        setTimeout(function () {
                            that.ignoreFocusOpen = false;
                        }, 50);
                    });
                },

                onFocus: function () {
                    if (!this.ignoreFocusOpen) {
                        this.open();
                    }
                }
            });
    });
}

function clearSelectize(selector) {
    $(selector).each(function () { // do this for every select with the 'combobox' class
        if ($(this)[0].selectize) { // requires [0] to select the proper object
            var value = $(this).val(); // store the current value of the select/input
            $(this)[0].selectize.destroy(); // destroys selectize()
            $(this).val(value);  // set back the value of the select/input
        }
    });
}
