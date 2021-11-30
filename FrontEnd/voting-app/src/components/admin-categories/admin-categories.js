import React, {useState, useEffect} from 'react';
import { createCategory } from '../../helpers/mutations';
import { fetchAllCategoriesOfPoll } from '../../helpers/querries';
import SpinnerLoader from '../../ui/spinner/spinner';
import CategoryDetails from '../category-details/category-details';
import {Modal, Button, ButtonGroup} from "react-bootstrap";

const AdminCategories = (props) => {
    const [categories, setCategories] = useState([]);
    const [loadingList, setLoadingList] = useState(true);
    const [loadingError, setLoadingError] = useState(false);
    const [selectedCategory, setSelectedCategory] = useState();
    const [showAddCategoryModal, setShowAddCategoryModel] = useState(false);
    const [theme, setTheme] = useState("");

    useEffect(() => {
        fetchAllCategoriesOfPoll(props.match.params.pollId)
        .then(res => {
            if(res.length > 0) {
                setCategories(res);
                setLoadingList(false);
            } else {
                setLoadingList(false);
            }
        })
        .catch(err => {
            setLoadingError(true);
            console.error(err);
        })
    }, [props.match.params.pollId]);

    const handleAddCategory = (e) => {
        e.preventDefault();
        const newCategory = {
            theme: theme,
            pollId: props.match.params.pollId
        }

        createCategory(newCategory).then(res => {
            if(res.theme) {
                setCategories([...categories, res])
                setTheme("");
                setShowAddCategoryModel(false);
            }
        })
        .catch(error => {
            console.log(error)
            setShowAddCategoryModel(false);
        })
    }

    const changeSelectedCategory= (cat) => {
        setSelectedCategory(cat);
    }

    return ( 
    <div className="container">
        <div className="row mt-5">
            <div className="col-4">
                <h5 className="mt-5 text-light">Categories List</h5>
                <hr />

                <ButtonGroup aria-label="Categories Button">
                    <Button variant="secondary" onClick={() => setShowAddCategoryModel(true)}>+ Add Category</Button>
                    <Button variant="primary" 
                            onClick={() => window.open(`https://localhost:5001/result/${props.match.params.pollId}`)}>
                                View Results
                    </Button>
                </ButtonGroup>

                <hr />

                {loadingError ? <p className="alert alert-danger">There was an error loading.</p> : 
                    loadingList ? 
                    <SpinnerLoader /> : 
                    <table className="table table-stripped table-hover mt-5">
                        <tbody>
                            {categories.length > 0 ? categories.map(cat =>
                                <tr key={cat.id} 
                                    onClick={() => changeSelectedCategory(cat)}
                                    style={{cursor: "pointer"}}>
                                    <td>{cat.theme}</td>
                                </tr>
                            ) : 
                                <tr className="table-warning"><td>Empty category list</td></tr>
                            }
                        </tbody>
                    </table>
                }
            </div>
            <div className="col-8">
                <CategoryDetails category={selectedCategory}/>
            </div>
        </div>

        <Modal show={showAddCategoryModal} onHide={() => setShowAddCategoryModel(false)}>
            <Modal.Header closeButton>
                <Modal.Title>Add a Category</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <form onSubmit={handleAddCategory}>
                    <div className="form-group">
                        <input type="text" 
                                onChange={(e) => setTheme(e.target.value)}
                                className="form-control" 
                                placeholder="Category Theme here ..."
                                required/>
                    </div>

                    <div className="form-group">
                        <button type="submit" className="btn btn-success">Submit Category</button>
                    </div>
                </form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={() => setShowAddCategoryModel(false)}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    </div>
    );
}
 
export default AdminCategories;