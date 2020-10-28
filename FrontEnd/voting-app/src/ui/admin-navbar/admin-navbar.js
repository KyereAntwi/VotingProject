import React from "react";
import { Link } from "react-router-dom";

const AdminNavbar = () => {
  return (
    <table className="table table-hover">
      <tbody>
        <tr>
          <td><Link to="/administrator">Polls List</Link></td>
        </tr>
        <tr>
          <td><Link to="/administrator/nominees">Nominee List</Link></td>
        </tr>
        <tr>
          <td><Link to="/administrator/categories">Category List</Link></td>
        </tr>
      </tbody>
    </table>
  );
};

export default AdminNavbar;
