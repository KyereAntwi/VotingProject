import React, { useState, useContext } from "react";
import "./login.css";

import { UserContext } from "../../store/contexts/user-context";

const Login = (props) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { mutation } = useContext(UserContext);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const user = { username, password };
    await mutation(user);
    props.history.push("/start");
  };

  return (
    <div className="login-page">
      <h2>Login with credentials</h2>
      <form onSubmit={handleSubmit}>
        <div className="control-group">
          <input
            type="text"
            value={username}
            placeholder="Username here ..."
            onChange={(e) => {
              setUsername(e.target.value);
            }}
            required
          />
        </div>
        <div className="control-group">
          <input
            type="password"
            value={password}
            placeholder="Password here ..."
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />
        </div>

        <button className="btn" type="submit">
          Login ...
        </button>
      </form>
    </div>
  );
};

export default Login;
