var connection = new signalR.HubConnectionBuilder().withUrl('/stream').build();
var canvas = document.getElementById('canvas').getContext('2d');

function draw() {
    canvas.clearRect(0, 0, canvas.width, canvas.height);
    var img = new Image();
    img.onload = function()
    {
        canvas.drawImage(img, 0, 0);
    }
    img.src = 'http://localhost:5000/camera/?random';
}

connection.start().then(() => connection.invoke('SendFrame'));

connection.on('NewFrameReceived', function () {
    console.log("Frame received and updated");
    draw();
});

function getNewFrame() {
    caches.open('v1').then(function (cache) {
        cache.delete('?random').then(function () {
            console.log("Older frame removed");
        });
    });
    draw();
    connection.invoke('SendFrame').catch(function (err) {
        return console.error(err.toString());
    });
}

$(document).ready(function () {
    refresh = setInterval("getNewFrame()", 42);
});