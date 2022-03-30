export module Site {

    document.addEventListener("DOMContentLoaded", function (event) {
        initTimeNow();
    });

    function initTimeNow() {
        $("#sessionValue").text("00.00.0000 00:00:00");
        timeNowCall();
        timeNowTimer();

        window.addEventListener('scroll', function () {
            if (window.scrollY > 0) {
                var navbarTop = document.getElementById('navbarTop');
                navbarTop.classList.add('fixed-top');
                var navbarHeight = navbarTop.offsetHeight;
                document.body.style.paddingTop = navbarHeight + 'px';
            } else {
                var navbarTop = document.getElementById('navbarTop');
                navbarTop.classList.remove('fixed-top');
                document.body.style.paddingTop = '0';
            }
        });
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

    export function UrlActionWithParameter(url: string, param: number) {
        var result: string = url.substring(0, url.length - 1) + String(param);
        return result;
    }

}