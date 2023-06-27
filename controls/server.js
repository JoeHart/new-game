var express = require("express");
var app = express();
var path = require("path");
const asset_dir_path = "dist/";

const port = process.env.PORT || 3000;

app.use(function (req, res, next) {
  var filename = path.basename(req.url);
  var extension = path.extname(filename);
  if (extension === '.js')
    console.log("The file " + filename + " was requested.");
  next();
});
// app.use(express.static(__dirname + "/dist"));
app.use(express.static(asset_dir_path, {
  index: false,
  setHeaders: (response, file_path, file_stats) => {
    // This function is called when “serve-static” makes a response.
    // Note that `file_path` is an absolute path.

    // Logging work
    const relative_path = path.join(asset_dir_path, path.relative(asset_dir_path, file_path));
    console.info(`@${Date.now()}`, "GAVE\t\t", relative_path);
  }
}));

app.get("/", function (req, res) {
  console.log("sending client.html");

  res.sendFile(__dirname + "/dist/index.html");
});



const server = app.listen(port, function () {
  console.log('Example app listening on port ' + port + '!');
});

var io = require("socket.io")(server, {
  origins: '*:*',
  transports: ['websocket', 'polling']
});


let number = 0;

let unitySocket;

io.attach(server);
// console.log("starting websockets on port 3001");
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
