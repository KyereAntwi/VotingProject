import React, {useState, useEffect} from 'react';
import { deleteWorker, registerUser } from '../../helpers/mutations';
import {getAllSystemWorkers} from "../../helpers/querries";
import {Modal, Button} from "react-bootstrap";
import {toast} from 'react-toastify';

const AdminSystemWorkers = () => {
    const [workers, setWorkers] = useState([]);
    const [laodError, setLoadError] = useState(false);
    const [showWorkerModal, setShowModal] = useState(false);

    const [usernameNew, setUsernameNew] = useState("");
    const [password, setPassword] = useState("");
    const [roleNew, setRoleNew] = useState("");
    const [runAgain] = useState(false);

    useEffect(() => {
        getAllSystemWorkers().then(res => {
            if (res) {
                setWorkers(res);
            } else {
                setLoadError(true);
                toast('There was an error loading workers');
            }
        })
    }, [runAgain]);

    const handleWorkerShow = () => setShowModal(true);
    const handleWorkerClose = () => setShowModal(false);

    const handleDeleteWorker = (username) => {
        deleteWorker(username).then(res => {
            if(res) {
                setWorkers(workers.filter(w => w.username !== username));
            }
        });
    }

    const handleAddWorker = (e) => {
        e.preventDefault();
        const user = {
            username: usernameNew,
            password: password,
            role: roleNew
        }
        registerUser(user).then(res => {
            if(res.username) {
                setWorkers([...workers, {username: usernameNew, role: roleNew}]);
                setShowModal(false);
            } else {
                setShowModal(false);
            }
        })
    }

    return ( <div className="row">
        <div className="col-12 mt-4">
            <h4 className="text-light">System Workers List <span className="btn btn-link btn-secondary" onClick={handleWorkerShow}> + Add a worker</span></h4>

            {laodError ? (<p className="alert alert-danger">There was an error loading System Users</p>)
            : workers.length > 0 ? (
            <table className="table table-striped mt-2">
                <tbody>
                    {workers.map(w =>
                        <tr key={w.username}>
                            <td>{w.username}</td>
                            <td>{w.role}</td>
                            <td><button className="btn-link text-warning" 
                                        title={"Delete " + w.username + " completely"}
                                        onClick={() => handleDeleteWorker(w.username)}>Remove</button></td>
                        </tr>
                    )}
                </tbody>
            </table>) : (<p className="alert alert-warning">There are no Workers to display</p>)}
        </div>

        <Modal show={showWorkerModal} onHide={handleWorkerClose}>
            <Modal.Header closeButton>
                <Modal.Title>Add a Worker</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <form onSubmit={handleAddWorker}>
                    <div className="form-group">
                        <input onChange={(e) => setUsernameNew(e.target.value)}
                                className="form-control" 
                                type="text" 
                                placeholder="Worker username here ..." 
                                required/>
                    </div>
                    <div className="form-group">
                        <input onChange={(e) => setPassword(e.target.value)}
                                type="password" 
                                minLength="8"
                                placeholder="Worker Password here"
                                required
                                className="form-control"/>
                    </div>
                    <div className="form-group">
                        <select className="form-control" value={roleNew} onChange={(e) => setRoleNew(e.target.value)}>
                            <option value="">Select a role ...</option>
                            <option value="Ecoffical">Electoral Commissioner</option>
                            <option value="Administrator">System Administrator</option>
                        </select>
                    </div>

                    <div className="form-group">
                            <input type="submit" value="Submit Form" className="btn btn-secondary btn-block" />
                    </div>
                </form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleWorkerClose}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    </div> );
}
 
export default AdminSystemWorkers;