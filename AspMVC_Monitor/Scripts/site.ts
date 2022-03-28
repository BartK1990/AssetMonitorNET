
document.addEventListener("DOMContentLoaded", function (event) {
    initTimeNow();
});

function initTimeNow() {
    $("#sessionValue").text("00.00.0000 00:00:00");
    timeNowCall();
    timeNowTimer();
}

function timeNowTimer() {
    setInterval(function () {
        $.ajax({
            type: 'post',
            url: '/Home/GetServerTime',
            success: function (result) {
                $("#sessionValue").text(result.data);
            }
        })
    }, 1000);
}

function timeNowCall() {
    $.ajax({
        type: 'post',
        url: '/Home/GetServerTime',
        success: function (result) {
            $("#sessionValue").text(result.data);
        }
    })
}