import React, { useState } from "react";

const Login = ({ socket, setUsername }) => {
  const [name, setName] = useState("");
  console.log(socket);
  const handleJoin = () => {
    console.log(socket);
    socket.emit("login", name);
    setUsername(name);
  };
  const handleChange = evt => {
    setName(evt.target.value.toLowerCase());
  };
  const constainerStyle = {
    display: "flex",
    flexDirection: "column",
    height: "100%"
  };
  const growStyle = { flexGrow: 1 };
  const inputStyle = { height: "3rem", fontSize: "2rem" };
  const buttonStyle = { height: "3rem", fontSize: "2rem" };
  return (
    <div style={constainerStyle}>
      <h1>Enter your name:</h1>
      <input style={inputStyle} value={name} onChange={handleChange} />
      <button style={buttonStyle} onClick={handleJoin}>
        Join
      </button>
    </div>
  );
};

export default Login;
