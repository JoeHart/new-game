var express = require("express");
var app = express();
var http = require("http").createServer(app);
var io = require("socket.io")(http);

const users = [];

app.use(express.static("public"));

app.get("/", function(req, res) {
  res.sendFile(__dirname + "/client.html");
});

app.get("/display", function(req, res) {
  res.sendFile(__dirname + "/display.html");
});

io.on("connection", function(socket) {
  console.log("a user connected");
  socket.emit("names", users);
  socket.on("disconnect", function() {
    console.log("user disconnected");
  });
  socket.on("login", name => {
    console.log("logging in, ", name);
    if (users.indexOf(name) < 0) users.push(name);
    io.emit("names", users);
  });
});

http.listen(3000, function() {
  console.log("listening on *:3000");
});
