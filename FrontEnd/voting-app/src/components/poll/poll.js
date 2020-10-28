import React from "react";
import { Card } from "react-bootstrap";

import "./poll.css";

const Poll = (props) => {
  return (
    <div className="col d-flex justify-content-center text-center">
      <Card 
        onClick={() => props.history.push(`/categories/${props.poll.id}`)} 
        style={{ width: '16rem', height: '16rem', cursor: 'pointer' }}>
        <Card.Body>
          <Card.Title>{props.poll.theme}</Card.Title>
          <Card.Text>{props.poll.description}</Card.Text>
        </Card.Body>
      </Card>
    </div>
  );
};

export default Poll;
