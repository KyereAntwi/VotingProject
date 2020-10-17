import React from "react";
import { useQuery } from "react-query";
import Nominee from "../../components/nominee/nominee";

import { fetchNominations } from "../../helpers/querries";

import "./nominations.css";

const Nominations = (props) => {
  const { status, data, error } = useQuery("nominees", () =>
    fetchNominations(props.match.params.categoryId)
  );

  if (status === "loading") return <div>Loading ...</div>;
  if (status === "error") {
    console.log(error);
    return <div>Error occured while loading data ...</div>;
  }

  return (
    <div className="container">
      <div className="nominations-list">
        {data.length > 0 ? (
          data.map((nominee) => (
            <Nominee key={nominee.id} nomineeId={nominee.id} />
          ))
        ) : (
          <p>There are no nominees in this category</p>
        )}
      </div>
    </div>
  );
};

export default Nominations;
