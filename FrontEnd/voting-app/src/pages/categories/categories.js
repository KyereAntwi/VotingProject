import React from "react";
import { useQuery } from "react-query";
import Category from "../../components/category/category";

import "./categories.css";

const fetchCategories = async (pollId) => {
  const token = localStorage.getItem("token");
  const response = await fetch(
    `https://localhost:5001/api/V1/Poll/${pollId}/Category`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );
  const data = await response.json();
  return data;
};
const Categories = (props) => {
  const { status, data, error } = useQuery("categories", () =>
    fetchCategories(props.match.params.pollId)
  );

  if (status === "loading") return <p>Loading ...</p>;
  if (status === "error") return console.log(error);

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
