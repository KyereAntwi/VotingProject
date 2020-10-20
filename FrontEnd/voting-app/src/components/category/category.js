import React from "react";
import { Card } from "react-bootstrap";
import "./category.css";

const Category = ({ category, history }) => {
  return (
    <div
      className="col"
    >
      <Card onClick={() => history.push(`/nominees/${category.id}`)}>
        <Card.Body>
          <Card.Title>{category.theme}</Card.Title>
        </Card.Body>
      </Card>
    </div>
  );
};

export default Category;
