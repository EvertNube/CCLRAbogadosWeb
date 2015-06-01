function displayInactive() {
    $("#btn-showInactive").click(function () {
        $(".inactive").fadeToggle(300);
    });   
}

$(document).ready(function () {
    displayInactive();
});