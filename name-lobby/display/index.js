import io from "socket.io-client";
const socket = io("http://192.168.0.7:3000");
const namesDisplay = document.getElementById("names");

socket.on("names", names => {
  console.log("getting names");
  namesDisplay.innerHTML = names.join(",");
});
