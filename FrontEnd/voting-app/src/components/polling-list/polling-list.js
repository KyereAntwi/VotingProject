import React from "react";
import { useQuery } from "react-query";

import { fetchPolls } from "../../helpers/querries";

import Poll from "../poll/poll";
import "./polling-list.css";

const PollingList = () => {
  const { status, data, error } = useQuery("polls", fetchPolls);

  if (status === "loading") return <p>Loading ...</p>;
  if (status === "error") return console.log(error);

  return (
    <div className="polling-list">
      {data.length > 0 ? (
        data.map((poll) => <Poll key={poll.id} poll={poll} />)
      ) : (
        <p>There are no polls active at the moment</p>
      )}
    </div>
  );
};

export default PollingList;
