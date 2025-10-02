var dateTimeKind;
var jalaliDatePickerWatcherRegistered = false;

function setDateTimeKind(kind) {
    dateTimeKind = kind;
    if (kind == "Shamsi" && !jalaliDatePickerWatcherRegistered) {
        jalaliDatepicker.startWatch();
        jalaliDatePickerWatcherRegistered = true;
    }
}

function initDatePickers(inputClass) {
    if (dateTimeKind == "Shamsi")
        initShamsiDateInputs(inputClass);
    else
        initMiladiDateInputs(inputClass);
}

function initShamsiDateInputs(inputClass) {
    $('.' + inputClass).attr('data-jdp', '');
    setDateInputMask('.' + inputClass);
}

function setDateInputMask(selector) {
    Inputmask("9999/99/99", {
        placeholder: "____/__/__",
        showMaskOnHover: false,
        showMaskOnFocus: true
    }).mask($(selector));

    $(document).keyup(function (e) {
        if (e.which == 13 || e.which == 27 || e.which == 9) {
            jalaliDatepicker.hide();
        }
    });
}

function initMiladiDateInputs(inputClass) {
    Inputmask("9999/99/99", {
        placeholder: "____/__/__",
        showMaskOnHover: false,
        showMaskOnFocus: true
    }).mask($('.DateInput'));

    setTimeout(function () {
        $(document).on("focus", ".DateInput", function () {
            if (!$(this).hasClass("hasDatepicker")) {
                $(this).datepicker({
                    dateFormat: 'yy/mm/dd',
                    changeYear: true,
                    onSelect: function (dateText) {
                        this.dispatchEvent(new Event('change'));
                    }
                });
            }
        });

        $(".DateInput").on("keydown", function (e) {
            if (e.key === "Enter" || e.keyCode === 13) {
                $(this).datepicker("hide");
            }
        });

        $(".DateInput").on("click", function (e) {
            $(this).datepicker("show");
        });
    }, 100);
}