import React from "react";
import "./getting-started.css";

import PollingList from "../../components/polling-list/polling-list";
import { NavLink } from "react-router-dom/cjs/react-router-dom.min";

const GettingStarted = (props) => { 
  return(
    <div className="container">
      <div className="row mt-4">
        <div className="col-md-12 text-center text-light">
          <h1>Welcome! <i>student voter</i></h1>
          <br />
          <h6>Select a poll to start</h6>
        </div>
      </div>

      <div className="row justify-content-center align-items-center mt-6">
        <PollingList {...props}/>
      </div>

      <div className="row mt-4">
        <div className="col-12 text-center">
          <NavLink to="/administrator">Administration Page</NavLink> | <NavLink to="/eco">EC Page</NavLink>
        </div>
      </div>
    </div>
)};

export default GettingStarted;
