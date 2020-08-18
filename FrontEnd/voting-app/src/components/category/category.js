import React from "react";
import "./category.css";

const Category = ({ category, history }) => {
  return (
    <div
      onClick={() => history.push(`/nominees/${category.id}`)}
      className="category"
    >
      <p>{category.theme}</p>
    </div>
  );
};

export default Category;
