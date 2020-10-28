import React from "react";
import AdminPolls from "./admin-polls/admin-polls";
import AdminNominees from "../admin-nominees/admin-nominees";
import AdminSystemWorkers from "../../components/admin-workers/admin-workers";

const Administrator = (props) => {
  return (
    <div className="container">
      <div className="row mt-4">
        <div className="col-2"></div>
        <div className="col-8">
          <AdminPolls />
          <hr />
          <AdminNominees />
          <hr />
          <AdminSystemWorkers />
        </div>
        <div className="col-2"></div>
      </div>
    </div>
  );
};

export default Administrator;
