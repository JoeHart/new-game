const io = require("socket.io-client");
const host = process.env.HOST || "157.245.31.200";
const port = process.env.PORT || 3000;
const address = `ws://${host}:${port}`
try {

  console.log("connecting to " + address);
  const socket = io(address);
  const leftButton = document.getElementById("left");
  const rightButton = document.getElementById("right");
  const upButton = document.getElementById("up");
  const downButton = document.getElementById("down");

  const handleLeftButton = () => {
    console.log("left button clicked");
    socket.emit("left");
  };
  const handleRightButton = () => {
    socket.emit("right");
  };
  const handleUpButton = () => {
    socket.emit("up");
  };
  const handleDownButton = () => {
    socket.emit("down");
  };


  socket.on("connect", () => {
    socket.emit("spawn", { id: socket.id });
  });

  socket.on("increment", num => {
    console.log("increment from server");
    console.log(display);
    display.innerHTML = num;
  });

  socket.on("connect_error", (err) => {
    console.log(`connect_error due to ${err.message}`);
  });

  leftButton.onclick = handleLeftButton;
  rightButton.onclick = handleRightButton;
  upButton.onclick = handleUpButton;
  downButton.onclick = handleDownButton;

} catch (e) {
  console.log(e);
}