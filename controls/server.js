var express = require("express");
var app = express();
var http = require("http").createServer(app);
var path = require("path");
var io = require("socket.io")(http);
app.use(function (req, res, next) {
  var filename = path.basename(req.url);
  var extension = path.extname(filename);
  if (extension === '.js')
    console.log("The file " + filename + " was requested.");
  next();
});
app.use(express.static(__dirname + "dist"));

app.get("/", function (req, res) {
  console.log("sending client.html");

  res.sendFile(__dirname + "/dist/index.html");
});



app.listen(3000, function () {
  console.log('Example app listening on port ' + 3000 + '!');
});

let number = 0;

let unitySocket;

io.attach(8080);
console.log("starting websockets on port 8080");
io.on("connection", function (socket) {
  console.log("a user connected");
  socket.on("disconnect", function (data) {
    console.log("user disconnected", socket.id);
    io.to(unitySocket).emit("destroy", { id: socket.id });
  });
  socket.on("spawn", data => {
    io.to(unitySocket).emit("spawn", data);
  });
  socket.on("left", function () {
    console.log("LEFT");
    io.to(unitySocket).emit("left", { id: socket.id });
  });
  socket.on("right", function () {
    console.log("RIGHT");
    io.to(unitySocket).emit("right", { id: socket.id });
  });
  socket.on("down", function () {
    console.log("DOWN");
    io.to(unitySocket).emit("down", { id: socket.id });
  });
  socket.on("up", function () {
    console.log("UP");
    io.to(unitySocket).emit("up", { id: socket.id });
  });
  socket.on("fire", function () {
    console.log("FIRE");
    io.to(unitySocket).emit("fire", { id: socket.id });
  });
  socket.on("registerGame", function (data) {
    unitySocket = data.id;
  });
  socket.on("spawn", data => {
    console.log("SPAWN: ", data);
  });
});
