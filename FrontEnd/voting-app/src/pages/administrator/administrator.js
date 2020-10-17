import React from "react";
import { Route } from "react-router-dom";
import AdminHome from "../admin-home/admin-home";
import AdminPolls from "./admin-polls/admin-polls";
import AdminNavbar from "../../ui/admin-navbar/admin-navbar";

import "./administrator.css";

const Administrator = (props) => {
  return (
    <div className="container">
      <div className="admin-main">
        <div className="side-nav">
          <AdminNavbar />
        </div>
        <div className="nav-page">
          <Route path="/administrator" component={AdminHome} />
          <Route path="/administrator/polls" component={AdminPolls} />
        </div>
      </div>
    </div>
  );
};

export default Administrator;
