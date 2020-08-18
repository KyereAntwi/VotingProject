import React, { useContext } from "react";
import { NavLink } from "react-router-dom";

import { UserContext } from "../../store/contexts/user-context";

import "./navbar.css";

const Navbar = (props) => {
  const { user, setUser } = useContext(UserContext);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setUser();
    props.history.push("/login");
  };

  return (
    <header>
      <div className="container header-main">
        <h3>Triumph Voting System</h3>
        <nav>
          <ul>
            <li>
              <NavLink to="/">Getting Started Page</NavLink>
            </li>
            <li>
              <NavLink to="/administrator">Administration</NavLink>
            </li>
            <li>
              <NavLink to="/eco">Electoral Commission Officer</NavLink>
            </li>
            {user ? (
              <li>
                <button onClick={handleLogout}>Logout</button>
              </li>
            ) : null}
          </ul>
        </nav>
      </div>
    </header>
  );
};

export default Navbar;
