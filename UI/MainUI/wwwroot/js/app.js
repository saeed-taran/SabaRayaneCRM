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