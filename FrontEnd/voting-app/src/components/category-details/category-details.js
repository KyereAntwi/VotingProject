import React, {useState, useEffect} from 'react';
import { fetchAllExistingNominees, fetchNominations } from '../../helpers/querries';
import {Image} from "react-bootstrap";
import { addNomineeToCategory, removeNomineeFromCategory } from '../../helpers/mutations';

const CategoryDetails = (props) => {
    const [nominations, setNominations] = useState([]);
    const [catNominations, setCatNominations] = useState([]);
    const [runAllNom] = useState(false);

    useEffect(() => {
        setCatNominations([]);
        if(props.category) {
            fetchNominations(props.category.id).then(res => {
                if(res) {
                    setCatNominations(res);
                }
            }).catch(err => {})
        }
    }, [props.category]);

    useEffect(() => {
        fetchAllExistingNominees().then(res => {
            if(res.length > 0) {
                if(catNominations.length > 0) {
                    res.forEach(el => {
                        if(!catNominations.includes(el)) {
                            setNominations([...nominations, el])
                        }
                    });
                }else{
                    setNominations(res)
                }
            }
        }).catch(err => {})
    }, [runAllNom]);

    const handleAddNomineeToCategory = (nominee) => {
        addNomineeToCategory(props.category.id, nominee.id)
        .then(() => {
            setCatNominations([...catNominations, nominee]);
        }).catch(err => {
            console.log(err);
        })
    }

    const handleRemoveNomineeFromCategory = (nominee) => {
        removeNomineeFromCategory(props.category.id, nominee.id)
        .then(() => {
            setCatNominations(catNominations.filter(nom => nom.id !== nominee.id));
        }).catch(err => {
            console.log(err);
        })
    }

    return ( props.category ? (
        <>
            <div className="row">
                <div className="col-12">
                    <h4 className="text-light">List of Nominess in Category</h4>
                    <table className="table table-striped table-hover mt-3">
                        <tbody>
                            {catNominations.length > 0 ? catNominations.map(cat => 
                                <tr key={cat.id}>
                                    <td>
                                        <Image src={`https://localhost:5001/${cat.imageUrl}`} width="50" height="50" roundedCircle />
                                    </td>
                                    <td>{cat.fullname}</td>
                                    <td><button className="btn btn-sm btn-danger"
                                                onClick={() => handleRemoveNomineeFromCategory(cat)}>Remove</button></td>
                                </tr>
                            ) : 
                            <tr className="table-warning"><td>There are no nominations in the category yet!</td></tr>}
                        </tbody>
                    </table>
                </div>
            </div>

            <div className="row">
                <div className="col-12">
                    <h4 className="text-light">List of All Nominees</h4>
                    <div className="table-responsive mt-3">
                        <table className="table table-striped table-hover">
                            <tbody>
                                {nominations.length > 0 ? nominations.map(cat => 
                                    <tr key={cat.id}>
                                        <td>
                                            <Image src={`https://localhost:5001/${cat.imageUrl}`} width="50" height="50" roundedCircle />
                                        </td>
                                        <td>{cat.fullname}</td>
                                        <td><button className="btn btn-sm btn-primary"
                                                    onClick={() => handleAddNomineeToCategory(cat)}>+ Add</button></td>
                                    </tr>
                                ) : 
                                <tr className="table-warning"><td>There are no nominations</td></tr>}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </>
    ) : null );
}
 
export default CategoryDetails;