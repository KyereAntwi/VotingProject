import React from "react";
import { useQuery } from "react-query";

import "./nominee.css";

const fetchNomineeData = async (nomId) => {};

const Nominee = ({ nomineeId }) => {
  const { status } = useQuery(() => fetchNomineeData(nomineeId));

  if (status === "loading") return <div>Loading ...</div>;

  return <div className="nominee">Nominee works ...</div>;
};

export default Nominee;
