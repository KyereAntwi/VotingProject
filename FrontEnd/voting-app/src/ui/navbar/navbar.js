import React from "react";
import "./navbar.css";

const Navbar = () => {
  return (
    <header>
      <div className="container">
        <h3>Triumph Voting System</h3>
        <nav>
          <ul>
            <li>Getting Started Page</li>
            <li>Administration Page</li>
            <li>Electoral Commision Officers Page</li>
          </ul>
        </nav>
      </div>
    </header>
  );
};

export default Navbar;
