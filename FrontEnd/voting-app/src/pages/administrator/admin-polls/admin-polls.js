import React, {useState, useEffect} from "react";
import {Link} from "react-router-dom";

import { fetchPolls } from "../../../helpers/querries";

import SpinnerLoader from "../../../ui/spinner/spinner";
import {Modal, Button, Form} from "react-bootstrap";
import {createPoll, deletePoll} from "../../../helpers/mutations";

const AdminPolls = (props) => {
  const [show, setShow] = useState(false);
  const [theme, setTheme] = useState("");
  const [description, setDescriptioin] = useState("");
  const [startedDateTime, setStartedDateTime] = useState();
  const [endedDateTime, setEndedDateTime] = useState();
  const [buttonLable, setButtonLable] = useState("Submit Poll");
  const [loading, setLoading] = useState(false);
  const [deleteError, setDeleteError] = useState(false);
  const [polls, setPolls] = useState([]);
  const [listLoading, setListLoading] = useState(true);
  const [dontrun] = useState(true);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    setButtonLable("Loading ...! Poll is been submitted");
    const poll = {
      theme,
      description,
      startedDateTime,
      endedDateTime
    }
    
    createPoll(poll).then(response => {
      if(response) {
        setPolls([...polls, response]);
        setLoading(false);
        setButtonLable("Submit Poll");
        setShow(false);
      }
    });
  };

  const handleDelete = (id) => {
    // setDeleteLoading("Deleting ...");
    deletePoll(id).then(response => {
      if (response) {
        // setDeleteLoading("Delete");
        setPolls(polls.filter(c => c.id !== response.id));
      } else {
        setDeleteError("There was a problem deleting the poll")
      };
    });
  }

   useEffect(() => {
    setListLoading(true);
    fetchPolls().then(res => {
      if(res) {
        setPolls(res);
        setListLoading(false);
      }
    });
  }, [dontrun]);

  return (
    <div className="row">
      <div className="col-12">
      <h3 className="mt-4 text-light">List of polls <span className="btn btn-link" onClick={handleShow}> + New Poll</span></h3> 

        {deleteError ? (
                <p className="alert alert-danger">There was an error loading polls</p>
              ) : null}

              {listLoading ? (
                <SpinnerLoader />
              ) : (
                <table className="table table-striped">
                  <tbody>
                    {polls.length > 0 ?
                      polls.map(poll =>
                        <tr key={poll.id}>
                          <td>{poll.theme}</td>
                          <td>{poll.description}</td>
                          <td><Link to={`/polls/${poll.id}`} className="btn btn-link">View</Link></td>
                          <td><button className="btn btn-link text-danger" onClick={() => handleDelete(poll.id)}>Delete</button></td>
                        </tr>
                    ) : (
                      <tr>
                        <td className="table-warning">There are no polls to diaplay</td>
                      </tr>
                    )}
                  </tbody>
                </table>
              )}

        <Modal show={show} onHide={handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>Create Poll</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form onSubmit={handleSubmit}>
              <Form.Group controlId="pollTheme">
                <Form.Control type="text" 
                              placeholder="Poll theme here ..." 
                              required
                              onChange={(e) => setTheme(e.target.value)}/>
                <Form.Text className="text-muted">
                  The theme of the poll is the identification of the poll. Students would see this
                </Form.Text>
              </Form.Group>

              <Form.Group controlId="pollDescription">
                <Form.Control as="textarea" 
                              rows="3" 
                              placeholder="Poll description here ..." 
                              required
                              onChange={(e) => setDescriptioin(e.target.value)}/>
                <Form.Text className="text-muted">
                  The description of the poll would be a summary of what the poll is for
                </Form.Text>
              </Form.Group>

              <Form.Group controlId="pollStartTime">
                <Form.Label>Poll Starting Date</Form.Label>
                <Form.Control type="date"
                              onChange={(e) => setStartedDateTime(e.target.value)}/>
                <Form.Text className="text-muted">
                  What date is the polls starting
                </Form.Text>
              </Form.Group>

              <Form.Group controlId="pollEndingTime">
                <Form.Label>Poll Ending Date</Form.Label>
                <Form.Control type="date"
                              onChange={(e) => setEndedDateTime(e.target.value)}/>
                <Form.Text className="text-muted">
                  What date is the polls starting
                </Form.Text>
              </Form.Group>

              <Button type="submit" disabled={loading}>{buttonLable}</Button>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={handleClose} disabled={loading}>
              Close
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    </div>
  );
};

export default AdminPolls;
