import React from 'react';
import {Spinner}  from 'react-bootstrap';

const SpinnerLoader = () => {
    return ( 
    <Spinner animation="grow" variant="primary" role="status">
        <span className="sr-only">Loading ...</span>
    </Spinner>
   );
}
 
export default SpinnerLoader;