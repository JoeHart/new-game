import React, { useEffect, useState } from "react";

const WaitingRoom = ({ socket, username }) => {
  const [users, setUsers] = useState([]);
  useEffect(() => {
    socket.on("names", names => setUsers(names));
  }, []);

  return (
    <div>
      <h1>Hello {username}</h1>
      <div>
        You're in the room with:
        <ul>
          {users.map(user => (
            <li>{user}</li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default WaitingRoom;
