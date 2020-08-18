import React from "react";
import { useQuery } from "react-query";

import Poll from "../poll/poll";
import "./polling-list.css";

const fetchPolls = async () => {
  const token = localStorage.getItem("token");
  const response = await fetch("https://localhost:5001/api/V1/Poll", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  const data = await response.json();
  return data;
};

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
