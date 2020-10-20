import { Alert } from "bootstrap";
import React from "react";
import { useQuery } from "react-query";

import { fetchPolls } from "../../helpers/querries";
import SpinnerLoader from "../../ui/spinner/spinner";

import Poll from "../poll/poll";
import "./polling-list.css";

const PollingList = () => {
  const { status, data, error } = useQuery("polls", fetchPolls);

  if (status === "loading") return (
    <div className="col-md-12">
      <SpinnerLoader />
    </div>
  );
  if (status === "error") return (
    <div className="col-md-12">
      <Alert variant="warning">
        There was an error loading Polls: {error}
      </Alert>
    </div>
  );

  return (
    <div className="polling-list">
      {data.length > 0 ? (
        data.map((poll) => <Poll key={poll.id} poll={poll} />)
      ) : (
        <div className="col-md-12">
          <Alert variant="info">
            There are no polls to display
          </Alert>
        </div>
      )}
    </div>
  );
};

export default PollingList;
