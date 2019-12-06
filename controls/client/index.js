import io from "socket.io-client";
// const socket = io("ws://192.168.0.184:3000");
const socket = io("ws://10.151.98.19:8080");
const leftButton = document.getElementById("left");
const rightButton = document.getElementById("right");
const fireButton = document.getElementById("fire");
const handleLeftButton = () => {
  socket.emit("left");
};
const handleRightButton = () => {
  socket.emit("right");
};
const handleFireButton = () => {
  socket.emit("fire");
  fireButton.disabled = true;
  fireButton.innerHTML = "Reloading...";
  setTimeout(function() {
    fireButton.disabled = false;
    fireButton.innerHTML = "Fire";
  }, 2000);
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
fireButton.onclick = handleFireButton;
