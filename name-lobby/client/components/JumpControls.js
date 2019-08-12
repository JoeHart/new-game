import React from "react";

const JumpControls = ({ socket, username }) => {
  const handleJump = () => {
    socket.emit("jump", username);
  };

  return (
    <div>
      <button width="100%" height="100%" onClick={handleJump}>
        Jump
      </button>
    </div>
  );
};
