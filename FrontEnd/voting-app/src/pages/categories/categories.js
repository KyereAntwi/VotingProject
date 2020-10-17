import React, { useContext } from "react";
import { useQuery } from "react-query";
import Category from "../../components/category/category";

import { UserContext } from "../../store/contexts/user-context";
import { fetchCategories } from "../../helpers/querries";

import "./categories.css";

const Categories = (props) => {
  const { user } = useContext(UserContext);

  const { status, data } = useQuery("categories", () =>
    fetchCategories(props.match.params.pollId, user.username)
  );

  if (status === "loading") return <p>Loading ...</p>;
  if (status === "error") {
  }

  if (data.length === 0) {
  }

  return (
    <div className="categories-list">
      <p className="head">Select Category below to vote</p>
      {data.length > 0 ? (
        data.map((cat) => <Category key={cat.id} category={cat} {...props} />)
      ) : (
        <p>The categories list is empty</p>
      )}
    </div>
  );
};

export default Categories;
