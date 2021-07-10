var connection = new signalR.HubConnectionBuilder().withUrl('/stream').build();
var refresh;

connection.start().then(() => connection.invoke('SendFrame'));

connection.on('NewFrameReceived', function () {
    console.log("Frame received and updated");
    document.getElementById("streamingSource").src = "http://localhost:5000/camera/?random";
});

function getNewFrame() {
    /*
    caches.open('v1').then(function (cache) {
        cache.delete('?random').then(function () {
            console.log("Older frame removed");
        });
    });
    */
    connection.invoke('SendFrame').catch(function (err) {
        return console.error(err.toString());
    });
}

$(document).ready(function () {
    refresh = setInterval("getNewFrame()", 16);
});