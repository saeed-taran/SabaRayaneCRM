function tabClicked(tab) {
    var tabHeader = $(tab).parent().parent();
    var currentActiveTab = tabHeader.find('active');
    var currentActiveTabPane = $('#' + $(currentActiveTab).data("bs-target"));
    currentActiveTab.removeClass("show").removeClass("active").addClass("fade");
    currentActiveTabPane.removeClass("show").removeClass("active").addClass("fade");

    var tabPaneToShow = $('#' + $(tab).data("bs-target"));
    tab.removeClass("fade").addClass("show active");
    tabPaneToShow.removeClass("fade").addClass("show active");
}