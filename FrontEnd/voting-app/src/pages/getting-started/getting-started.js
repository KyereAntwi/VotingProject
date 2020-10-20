import React from "react";
import "./getting-started.css";

import PollingList from "../../components/polling-list/polling-list";

const GettingStarted = () => (
  <div className="container">
    <div className="row">
      <div className="col-md-12">
        <h1>Welcome! <i>student voter</i></h1>
      </div>
    </div>

    <div className="row">
      <PollingList />
    </div>
  </div>
);

export default GettingStarted;
