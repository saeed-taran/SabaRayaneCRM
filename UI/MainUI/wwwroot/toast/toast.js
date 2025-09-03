function showToast(msg, cssClass) {
    $(".toast").each(function () {
        var currentBottom = parseInt($(this).css("bottom").replace("px", ""));
        var height = parseInt($(this).css("height").replace("px", ""));
        $(this).css("bottom", (currentBottom + height + 15) + "px");
    });

    var toast = $("<div class='end-0 me-5 toast show " + cssClass + "'>" + msg + "</div>");
    toast.appendTo('body');
    setTimeout(function () { $(toast).remove(); }, 2700);
}