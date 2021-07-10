addEventListener("click", function () {
    console.log("fullscreen on");

    let element = document.documentElement,
        request = element.requestFullscreen;
    request.call(element);
});