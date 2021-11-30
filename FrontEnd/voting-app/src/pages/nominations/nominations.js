import React, {useState} from "react";
import { useQuery } from "react-query";
import Nominee from "../../components/nominee/nominee";
import { performVote } from "../../helpers/mutations";
import {Modal, Image, Button} from "react-bootstrap";

import { fetchNominations } from "../../helpers/querries";

import "./nominations.css";

const Nominations = (props) => {
  const [showModal, setShowModal] = useState(false);
  const [setNominee, setSetNominee] = useState();

  const { status, data } = useQuery("nominees", () =>
    fetchNominations(props.match.params.categoryId)
  );

  if (status === "loading") return <div>Loading ...</div>;
  if (status === "error") {
    return <div>Error occured while loading data ...</div>;
  }

  const handleVote = (nominee) => {
    setSetNominee(nominee);
    setShowModal(true);
  }

  const handleNomineeShow = () => setShowModal(false);

  const continueToVote = () => {
    const vote = {
      nomineeId: setNominee.id,
      categoryId: props.match.params.categoryId,
      username: localStorage.getItem("username")
    }

    performVote(vote).then(res => {
      if (res.id) {
        props.history.push(`/categories/${localStorage.getItem("pollId")}`);
      }
    }).catch(err => {})
  };

  return (
    <div className="container">
      <div className="row mt-4">
        <div className="col-12 text-center text-light">
          <h6>Select a Nominee below</h6>
        </div>
      </div>
      <div className="row mt-4">
        {data ? (
          data.map((nominee) => (
            <Nominee key={nominee.id} nominee={nominee} vote={handleVote}/>
          ))
        ) : (
          <p>There are no nominees in this category</p>
        )}
      </div>

      {setNominee ? 
        <Modal show={showModal} onHide={handleNomineeShow}>
        <Modal.Header closeButton>
          <Modal.Title>Confirm your vote</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="row">
            <div className="col-12 align-content-center text-center">
              <Image className="mt-2" src={`https://localhost:5001${setNominee.imageUrl}`} width="200" height="200" roundedCircle />
              <Button className="mt-2" variant="success" onClick={continueToVote}>
                Yes continue to vote for {setNominee.fullname}
              </Button>
            </div>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="warning" onClick={handleNomineeShow}>
            No don't Vote for this person
          </Button>
        </Modal.Footer>
      </Modal> : null}
    </div>
  );
};

export default Nominations;
