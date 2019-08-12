import io from "socket.io-client";
const socket = io("http://192.168.0.7:3000");
const button = document.getElementById("increment");
const display = document.getElementById("number");
const handleButton = () => {
  socket.emit("increment");
};

socket.on("increment", num => {
  console.log("increment from server");
  console.log(display);
  display.innerHTML = num;
});

button.onclick = handleButton;

export default handleButton;
