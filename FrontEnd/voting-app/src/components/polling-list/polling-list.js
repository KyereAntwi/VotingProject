import React from "react";
import { useQuery } from "react-query";

import { fetchPolls } from "../../helpers/querries";
import SpinnerLoader from "../../ui/spinner/spinner";

import Poll from "../poll/poll";
import "./polling-list.css";

const PollingList = (props) => {
  const { status, data, error } = useQuery("polls", fetchPolls);

  if (status === "loading") return (
    <div className="col-md-12 justify-content-center align-items-center">
      <SpinnerLoader />
    </div>
  );
  
  if (status === "error") return (
    <div className="col-md-12 d-flex justify-content-center text-center">
      <p className="alert alert-danger">There was an error loading Polls: {error}</p>
    </div>
  );

  return (
    <>
      {data.length > 0 ? (
        data.map((poll) => <Poll key={poll.id} poll={poll} {...props}/>)
      ) : (
        <div className="col-md-12 d-flex justify-content-center text-center">
          <p className="alert alert-info">There was no polls to load</p>
        </div>
      )}
    </>
  );
};

export default PollingList;
