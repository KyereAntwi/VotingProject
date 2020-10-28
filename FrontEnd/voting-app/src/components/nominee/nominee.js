import React from "react";
import { Card } from "react-bootstrap";

import "./nominee.css";

const Nominee = (props) => {

return (
<div className="col">
    <Card onClick={() => props.vote(props.nominee)} style={{width: '16rem', height: '26rem', cursor: 'pointer'}}>
        <Card.Img variant="top" src={"https://localhost:5001" + props.nominee.imageUrl}/>
        <Card.Body>
            <Card.Subtitle>{props.nominee.fullname}</Card.Subtitle>
        </Card.Body>
    </Card>
</div>);
};

export default Nominee;