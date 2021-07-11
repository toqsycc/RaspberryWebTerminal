const canvas = document.querySelector('canvas');
const context = canvas.getContext('2d');
const FRAME_PATH = 'http://localhost:5000/camera/';
async function loop() {
    while (true) {
        await draw();
    }
}
function draw() {
    const image = new Image(canvas.width, canvas.height);
    image.src = `${FRAME_PATH}?ts=${Date.now()}`;
    image.alt="";
    return new Promise((resolve, reject) => {
        image.addEventListener("error", (event) => reject(event.error), {
            once: true
        });
        image.addEventListener("load", () => {
                const frameId = requestAnimationFrame(() =>
                    context.drawImage(image,  0, 0, canvas.width, canvas.height));
                resolve(frameId);
            },
            {once: true});
    });
}
window.addEventListener("load", () => loop());
