
$(document).ready(init());
function init() {
    $("#sessionValue").text("00.00.0000 00:00:00");
    timeNowCall();
    timeNowTimer();
    GetAssetList();
}
function timeNowTimer() {
    var interval = setInterval(function () {
        // call backend to update the value in session
        $.ajax({
            type: 'post',
            url: 'GetServerTime',
            success: function (result) {
                $("#sessionValue").text(result.data);
            }
        })
    }, 1000); // Run for each second
}
function timeNowCall() {
    $.ajax({
        type: 'post',
        url: 'GetServerTime',
        success: function (result) {
            $("#sessionValue").text(result.data);
        }
    })
}

function GetAssetList() {
    var interval = setInterval(function () {
        $.ajax({
            type: 'post',
            url: 'GetAssetList',
            dataType: 'json',
            success: function (data) {
                $.each(data, function (i, item) {
                    const ps = document.getElementById(item.name + "_PingState");
                    ps.innerHTML = '';
                    const span = document.createElement('span');
                    span.classList.add('dot');
                    if (item.pingState) {
                        span.classList.add('dotBackgroundGreen');
                        ps.appendChild(span);
                    }
                    else {
                        span.classList.add('dotBackgroundRed');
                        ps.appendChild(span);
                    }
                    document.getElementById(item.name + "_PingResponseTime").innerHTML = item.pingResponseTime + "ms";
                    document.getElementById(item.name + "_CpuUsage").innerHTML = item.cpuUsage + "%";
                    document.getElementById(item.name + "_MemoryAvailable").innerHTML = item.memoryAvailable;
                });
            },
        });
    }, 5000); 
}