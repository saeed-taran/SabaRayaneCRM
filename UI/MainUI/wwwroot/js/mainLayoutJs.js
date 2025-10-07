var profileContextMenuIsHidden = true;

function mainLayoutLoaded() {
    document.getElementById("menuBar").addEventListener("transitionend", menuBarTransitionEnd);

    $(document).click(function (event) {
        if ($(event.target).closest('#profileToggleIcon').length === 0) {
            $("#profileContextMenu").addClass("d-none");
            profileContextMenuIsHidden = true;
        }
    });
}

function menuBarTransitionEnd(e) {
    if ($("#menuBar").width() > 0) {
        $('#innerMenuBar').css("width", '');
    }
}

function showMenuBar() {
    $('#menuBar').css('width', '');

    $('#restoreMenuButton').addClass('d-none');
}

function hideMenuBar() {
    var innerMenuBarWidth = $('#innerMenuBar').width();
    $('#innerMenuBar').css("width", innerMenuBarWidth + "px");
    $('#menuBar').css("width", "0");

    $('#restoreMenuButton').removeClass('d-none');
}

function toggleMenuBar() {
    if ($('#menuBar').width() == 0)
        showMenuBar();
    else
        hideMenuBar();
}

function toggleProfileContextMenu() {
    if (profileContextMenuIsHidden) {
        $("#profileContextMenu").removeClass("d-none");
    }
    else {
        $("#profileContextMenu").addClass("d-none");
    }

    profileContextMenuIsHidden = !profileContextMenuIsHidden;
}