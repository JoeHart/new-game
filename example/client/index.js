import io from "socket.io-client";

const socket = io("ws://10.151.98.19:3000");
const button = document.getElementById("increment");
const button2 = document.getElementById("deploy");
const display = document.getElementById("number");

const handleButton = () => {
  socket.emit("increment");
};
const handleButton2 = () => {
  socket.emit("deploy");
};

socket.on("updateCount", num => {
  console.log("increment from server");
  console.log(display);
  display.innerHTML = num;
});

button.onclick = handleButton;
button2.onclick = handleButton2;

export default handleButton;
