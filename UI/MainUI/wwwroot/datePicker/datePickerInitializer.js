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
    $(selector).mask("0000r00r00", {
        translation: {
            'r': {
                pattern: /[\/]/,
                fallback: '/'
            },
            placeholder: "__/__/____"
        },
        selectOnFocus: true
    });

    $(selector).blur(function () {
        setTimeout(function () { jalaliDatepicker.hide() }, 200);
    });

    $(selector).keyup(function (e) {
        if (e.which == 13) // Enter key
            $(this).blur();
    });
}

function initMiladiDateInputs(inputClass) {
    setTimeout(function () {
        $('.DateInput').datepicker({
            dateFormat: 'yy/mm/dd',
            onSelect: function (dateText) {
                this.dispatchEvent(new Event('change'));
            }
        });
    }, 100);
}