const io = require("socket.io-client");
// const socket = io("ws://192.168.0.184:3000");
try {


  const socket = io("ws://157.245.31.200/");
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

  leftButton.onclick = handleLeftButton;
  rightButton.onclick = handleRightButton;
  upButton.onclick = handleUpButton;
  downButton.onclick = handleDownButton;

} catch (e) {
  console.log(e);
}