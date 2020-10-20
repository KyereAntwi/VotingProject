import React from "react";
import { Card } from "react-bootstrap";

import "./poll.css";

const Poll = ({ props }) => {
  const handleClick = () => {
    props.history.push(`category/${props.poll.id}`)
  };

  return (
    <Card onClick={handleClick} style={{ width: '18rem' }}>
      <Card.Body>
        <Card.Title>{props.poll.theme}</Card.Title>
        <Card.Text>{props.poll.description}</Card.Text>
      </Card.Body>
    </Card>
  );
};

export default Poll;
