function fillMenuSearchDropDown(){
    $("#searchMenuInput").empty();
    $("#searchMenuInput").append($("<option></option>"));

    var links = $(".nav-item").children("a");
    for (i = 0; i < links.length; i++) {
        var option = $("<option>");
        option.text($($(links[i])).text());
        option.val($(links[i]).attr('href'));
        $("#searchMenuInput").append(option);
    }
}

function collapseAccordionByHref(href) {
    var aElement = $("a[href='" + href + "']");
    var containingCollaps = aElement.parents(".accordion-collapse");
    containingCollaps.collapse('show');
}

function insertTextAtCursor(textareaId, text) {
    const textarea = document.getElementById(textareaId);
    if (!textarea) return;

    if (document.activeElement !== textarea) {
        textarea.focus();
    }

    const start = textarea.selectionStart;
    const end = textarea.selectionEnd;

    textarea.value = textarea.value.substring(0, start)
        + text
        + textarea.value.substring(end);

    textarea.selectionStart = textarea.selectionEnd = start + text.length;

    textarea.dispatchEvent(new Event('change'));
}