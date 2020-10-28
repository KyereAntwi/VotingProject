import React, {useState} from "react";
import { Card } from "react-bootstrap";
import { registerUser } from "../../helpers/mutations";
import { generateVoterCode } from "../../helpers/querries";

const ECO = () => {

  const [code, setCode] = useState();
  const [username, setUsername] = useState("");
  const [response, setResponse] = useState();
  
  const handleGenerateNewCode = () => {
    generateVoterCode().then(c => {
      if(c) {
        setCode(c.username)
      }
    })
  };

  const handleNewVoterRegistration = (e) => {
    e.preventDefault();
    const user = {
      username: username,
      role: "Voter"
    }
    registerUser(user).then(u => {
      if (u.token === null) {
        setResponse({
          status: true,
          message: `User with username: ${user.username} was registered successfully`
        })
      } else {
        setResponse({
          status: false,
          message: "Registration failed, try again"
        })
      }
    });
  }

  return (
  <div className="container">
    <div className="row mt-4">
      <div className="col-md-4"></div>
      <div className="col-md-4">
        {code ? (
          <div className="row">
            <div className="col-12">
              <p className="alert alert-success">New generated voter code is <span className="badge badge-pill">{code}</span></p>
            </div>
          </div>
        ) : null}

        {response ? (
          (<div className="row">
          <div className="col-12">
            <p className={
              response.status ? "alert alert-success" : "alert alert-danger"
            }> Registration was successfull if color code is green and failed if color is red </p>
          </div>
        </div>)
        ) : null}

        <div className="row mt-4">
          <div className="col-12">
            <button onClick={handleGenerateNewCode} className="btn btn-block btn-secondary">Generate a new voter code</button>
          </div>
        </div>

        <div className="divider"></div>

        <div className="row mt-4">
          <div className="col-12">
            <Card>
              <Card.Body>
                <form onSubmit={handleNewVoterRegistration} className="mt-4">
                  <div className="form-group">
                    <input type="text" max="8" className="form-control" placeholder="Voter Code here ..." onChange={(e) => setUsername(e.target.value)}/>
                  </div>
                  <div className="form-group">
                    <button type="submit" className="btn btn-sm btn-primary">Register Voter</button>
                  </div>
                </form>
              </Card.Body>
            </Card>
          </div>
        </div>
      </div>
      <div className="col-md-4"></div>
    </div>
  </div>);
};

export default ECO;
