import React from "react";
import { Card } from "react-bootstrap";
import "./category.css";

const Category = ({ category, history, pollId }) => {

  const visitNominationsPage = () => {
    localStorage.setItem("pollId", pollId);
    history.push(`/nominees/${category.id}`)
  }

  return (
    <div className="row">
      <div className="col-12 align-content-center text-center mt-4 ml-4">
        <Card onClick={visitNominationsPage} style={{width: '16rem', height: '12rem', cursor: 'pointer'}}>
          <Card.Body className="text-center justify-content-center align-items-center">
            <Card.Title>{category.theme}</Card.Title>
          </Card.Body>
        </Card>
      </div>
    </div>
  );
};

export default Category;
