const io = require("socket.io-client");
const host = process.env.HOST || "play.joehart.fun";
const port = process.env.PORT || 3000;
const address = `wss://${host}:${port}`;

function hslToRgb(h, s, l) {
  let r, g, b;
  if (s == 0) {
    r = g = b = l;
  } else {
    const hue2rgb = (p, q, t) => {
      if (t < 0) t += 1;
      if (t > 1) t -= 1;
      if (t < 1 / 6) return p + (q - p) * 6 * t;
      if (t < 1 / 2) return q;
      if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
      return p;
    }
    const q = l < 0.5 ? l * (1 + s) : l + s - l * s;
    const p = 2 * l - q;
    r = hue2rgb(p, q, h + 1 / 3);
    g = hue2rgb(p, q, h);
    b = hue2rgb(p, q, h - 1 / 3);
  }
  return [Math.round(r * 255), Math.round(g * 255), Math.round(b * 255)];
}

function getRandomColor() {
  // Generate a random hue from 0 to 1
  const hue = Math.random();
  // Convert the HSL color to RGB
  const [r, g, b] = hslToRgb(hue, 1, 0.5);
  // Return the RGB color in hexadecimal format
  return '#' + [r, g, b].map(x => {
    const hex = x.toString(16);
    return hex.length === 1 ? '0' + hex : hex;
  }).join('');
}

if (localStorage.getItem("color") == null) {
  localStorage.setItem("color", getRandomColor());
}

const color = localStorage.getItem("color");
const nameInput = document.getElementById("name");
if (localStorage.getItem("playerId") == null) {
  let uuid = self.crypto.randomUUID();
  localStorage.setItem("playerId", uuid);
}

const name = localStorage.getItem("name");
const playerId = localStorage.getItem("playerId")
if (name) {
  nameInput.value = name;
}

try {
  const ghostDiv = document.getElementById("ghost");

  console.log("connecting to " + address);
  const socket = io();
  const handleSubmitForm = (e) => {
    e.preventDefault();

    console.log("submitting form");
    localStorage.setItem("name", e.target[0].value);
    ghostDiv.style.backgroundColor = `${color}`;
    socket.emit("spawn", { id: playerId, name: e.target[0].value, color: color });
    return false;
  };

  window.handleSubmitForm = handleSubmitForm
  const leftButton = document.getElementById("left");
  const rightButton = document.getElementById("right");
  const upButton = document.getElementById("up");
  const downButton = document.getElementById("down");

  const handleLeftButton = () => {
    console.log("left button clicked");
    socket.emit("left", { id: playerId });
  };
  const handleRightButton = () => {
    socket.emit("right", { id: playerId });
  };
  const handleUpButton = () => {
    socket.emit("up", { id: playerId });
  };
  const handleDownButton = () => {
    socket.emit("down", { id: playerId });
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