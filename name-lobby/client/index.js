import io from "socket.io-client";
import React, { useState, useEffect } from "react";
import ReactDOM from "react-dom";
import useSocket from "use-socket.io-client";
import Login from "./components/login";
import WaitingRoom from "./components/WaitingRoom";

const SOCKET_URL = "http://192.168.0.7:3000";

const Main = () => {
  const [socket] = useSocket(SOCKET_URL);
  socket.connect();
  const [username, setUsername] = useState("");
  const [currentGame, setCurrentGame] = useState(null);

  if (!username) {
    return <Login socket={socket} setUsername={setUsername} />;
  }

  return <WaitingRoom socket={socket} username={username} />;
};

var mountNode = document.getElementById("app");
ReactDOM.render(<Main />, mountNode);
