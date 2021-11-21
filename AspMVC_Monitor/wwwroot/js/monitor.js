
$(document).ready(init());
function init() {
    $("#sessionValue").text("00.00.0000 00:00:00");
    timeNowCall();
    timeNowTimer();
}
function timeNowTimer() {
    var interval = setInterval(function () {
        // call backend to update the value in session
        $.ajax({
            type: 'post',
            url: 'UpdateSession',
            success: function (result) {
                $("#sessionValue").text(result.data);
            }
        })
    }, 1000); // Run for each second
}
function timeNowCall() {
    $.ajax({
        type: 'post',
        url: 'UpdateSession',
        success: function (result) {
            $("#sessionValue").text(result.data);
        }
    })
}