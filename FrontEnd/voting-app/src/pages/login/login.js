import React, { useState, useContext } from "react";
import "./login.css";

import { UserContext } from "../../store/contexts/user-context";

const Login = (props) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { mutation, errorMessage } = useContext(UserContext);

  const handleSubmit = (e) => {
    e.preventDefault();
    const user = { 
      username: username, 
      password: password.length > 0 ? password : null };
      mutation(user).then(() => {
        props.history.push("/start");
      });
  };

  return (
    <div className="container">
      <div className="row mt-5">
        <div className="col-md-12 text-center">
        <h2 className="text-light">Login with credentials</h2>
        {errorMessage ? <p className="alert alert-warning">{errorMessage}</p> : null}
        </div>
      </div>

      <div className="row mt-5">
        <div className="col-12 text-center justify-content-center align-items-center">
          <div className="card" style={{width: '18rem', margin: '20px auto'}}>
            <div className="card-bod">
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <input
                  className="form-control"
                  type="text"
                  value={username}
                  placeholder="Username here ..."
                  onChange={(e) => {
                    setUsername(e.target.value);
                  }}
                  required
                />
              </div>
              <div className="form-group">
                <input
                  className="form-control"
                  type="password"
                  value={password}
                  placeholder="Password here ..."
                  onChange={(e) => {
                    setPassword(e.target.value);
                  }}
                />
              </div>

              <button className="btn btn-sm btn-suceess" type="submit">
                Login ...
              </button>
            </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
