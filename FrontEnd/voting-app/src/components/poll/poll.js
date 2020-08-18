import React from "react";
import { Link } from "react-router-dom";

import "./poll.css";

const Poll = ({ poll }) => {
  return (
    <div className="card">
      <h3 className="theme">{poll.theme}</h3>
      <p className="description">{poll.description}</p>
      {poll.id ? <Link to={`/categories/${poll.id}`}>Select Poll</Link> : null}
    </div>
  );
};

export default Poll;
