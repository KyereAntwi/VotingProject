import React from "react";
import { NavLink } from "react-router-dom";

const AdminNavbar = () => {
  return (
    <nav>
      <ul>
        <li>
          <NavLink to="/administrator">Admin Home</NavLink>
        </li>
        <li>
          <NavLink to="/administrator/polls">All Polls</NavLink>
        </li>
      </ul>
    </nav>
  );
};

export default AdminNavbar;
