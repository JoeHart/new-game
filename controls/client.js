const io = require("socket.io-client");
const host = process.env.HOST || "play.joehart.fun";
const port = process.env.PORT || 3000;
const address = `wss://${host}:${port}`;

const handleSubmitForm = (e) => {
  e.preventDefault();
  socket.emit("spawn", { id: socket.id, name: e.target[0].value });
  return false;
};

window.handleSubmitForm = handleSubmitForm

try {

  console.log("connecting to " + address);
  const socket = io();
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