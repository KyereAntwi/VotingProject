import React from "react";
import "./getting-started.css";

import PollingList from "../../components/polling-list/polling-list";

const GettingStarted = () => (
  <div className="container getting-started">
    <div className="introduction">
      <p>Vote with ease ...</p>
      <h2>Getting</h2>
      <h2>Started!</h2>
    </div>

    <PollingList />
  </div>
);

export default GettingStarted;
