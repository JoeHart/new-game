var express = require("express");
var app = express();
var http = require("http").createServer(app);
var io = require("socket.io")(http);

app.use(express.static("public"));

app.get("/", function (req, res) {
  console.log("sending client.html");
  res.sendFile(__dirname + "/client.html");
});

app.get("/display", function (req, res) {
  res.sendFile(__dirname + "/display.html");
});

let number = 0;

io.on("connection", function (socket) {
  console.log("a user connected");

  socket.on("disconnect", function () {
    console.log("user disconnected");
  });

  socket.on("increment", function () {
    number++;
    console.log(number);
    io.emit("updateCount", number);
  });

  socket.on("deploy", function () {
    number = number * 2;
    io.emit("updateCount", number);
  });

  socket.emit("updateCount", number);
});

http.listen(3000, function () {
  console.log("listening on *:3000");
});
