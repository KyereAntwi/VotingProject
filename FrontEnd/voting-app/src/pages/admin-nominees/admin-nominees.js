import React, {useEffect, useState} from 'react';
import {Modal, Button} from 'react-bootstrap'
import { createNominee, deleteNominee } from '../../helpers/mutations';
import { fetchAllExistingNominees } from '../../helpers/querries';
import SpinnerLoader from '../../ui/spinner/spinner';

const AdminNominees = () => {
    const [showModal, setModalShow] = useState(false);
    const [nominations, setNominations] = useState([]);
    const [nominationsListLoading, setNominationsListLoading] = useState(true);
    const [fetchError, setFetchError] = useState(false);
    const [fullname, setFullname] = useState("");
    const [avatar, setAvatar] = useState();
    const [dontrunNominee] = useState(true);

    useEffect(() => {
        setNominationsListLoading(true);
        fetchAllExistingNominees().then((res) => {
            if (res){
                setNominations(res);
                setNominationsListLoading(false);
            } else {
                setFetchError(true);
                setNominationsListLoading(false);
            }
        })
    }, [dontrunNominee]);

    const handleNomineeClose = () => setModalShow(false);
    const handleNomineeShow = () => setModalShow(true);

    const handleAddNominee = (e) => {
        e.preventDefault();

        console.log(fullname, avatar)
        let formData = new FormData();
        formData.append("fullname", fullname);
        formData.append("avatar", avatar);

        createNominee(formData).then(res => {
            if(res.fullname) {
                setNominations([...nominations, res]);
                setFullname("");
                setAvatar(null);
                setModalShow(false);
            } else {
              setModalShow(false);
            }
        });
    }

    const handleDeleteNominee = (id) => {
      deleteNominee(id).then(res => {
        if(res) {
          setNominations(nominations.filter(nom => nom.id !== res.id))
        } else {
          setFetchError(true);
        }
      })
    }

    return ( 
    <div className="row">
      <div className="col-12">
      <h3 className="mt-4 text-light">List of nominees <span className="btn btn-link" onClick={handleNomineeShow}> + New Nominee</span></h3>
            {fetchError ? (
              <p className="alert alert-danger">There was an error loading nominees</p>
            ) : null}

            {nominationsListLoading ? (
              <SpinnerLoader />
            ) : (
              <table className="table table-striped">
                <tbody>
                  {nominations.length > 0 ?
                    nominations.map(nom =>
                      <tr key={nom.id}>
                        <td><img className="img img-circle" width="50" height="50" src={"https://localhost:5001" + nom.imageUrl} alt=""/></td> 
                        <td>{nom.fullname}</td>
                        <td><button className="btn btn-link text-danger" onClick={() => handleDeleteNominee(nom.id)}>Delete</button></td>
                      </tr>
                  ) : (
                    <tr>
                      <td className="table-warning">There are no nominees to display</td>
                    </tr>
                  )}
                </tbody>
              </table>
            )}

        <Modal show={showModal} onHide={handleNomineeShow}>
        <Modal.Header closeButton>
          <Modal.Title>Add a new Nominee</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form encType="multipart/form-data" onSubmit={handleAddNominee}>
              <div className="form-group">
                  <input onChange={(e) => setFullname(e.target.value)}
                        className="form-control" 
                        type="text" 
                        placeholder="Nominees fullname here ..." 
                        required/>
              </div>
              <div className="form-group">
                  <input onChange={(e) => setAvatar(e.target.files[0])}
                        type="file" 
                        required/>
              </div>

              <div className="form-group">
                    <input type="submit" className="btn btn-secondary btn-block" />
              </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleNomineeClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
      </div>
    </div>
     );
}
 
export default AdminNominees;