var io = require("socket.io")({
  transports: ["websocket"]
});

let number = 0;

let unitySocket;

io.attach(8080);

io.on("connection", function(socket) {
  console.log("a user connected");
  socket.on("disconnect", function(data) {
    console.log("user disconnected", socket.id);
    io.to(unitySocket).emit("destroy", { id: socket.id });
  });
  socket.on("spawn", data => {
    io.to(unitySocket).emit("spawn", data);
  });
  socket.on("left", function() {
    console.log("LEFT");
    io.to(unitySocket).emit("left", { id: socket.id });
  });
  socket.on("right", function() {
    console.log("RIGHT");
    io.to(unitySocket).emit("right", { id: socket.id });
  });
  socket.on("fire", function() {
    console.log("FIRE");
    io.to(unitySocket).emit("fire", { id: socket.id });
  });
  socket.on("registerGame", function(data) {
    unitySocket = data.id;
  });
  socket.on("spawn", data => {
    console.log("SPAWN: ", data);
  });
});
