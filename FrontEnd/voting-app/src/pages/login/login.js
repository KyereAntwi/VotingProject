import React, { useState, useContext } from "react";
import "./login.css";

import { UserContext } from "../../store/contexts/user-context";

const Login = (props) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { setUser } = useContext(UserContext);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const response = await fetch(
      "https://localhost:5001/api/V1/Identity/Login",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          username: username,
          password: password,
        }),
      }
    ).catch((err) => {
      console.log(err);
    });

    const data = await response.json();
    setUser(data.user);
    localStorage.setItem("token", data.token);
    props.history.push("/");
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
