import React, { useContext } from "react";
import {Alert, NavLink} from 'react-bootstrap';
import { useQuery } from "react-query";
import Category from "../../components/category/category";

import { UserContext } from "../../store/contexts/user-context";
import { fetchCategories } from "../../helpers/querries";

import "./categories.css";
import SpinnerLoader from "../../ui/spinner/spinner";

const Categories = (props) => {
  const { user } = useContext(UserContext);

  const { status, data, error } = useQuery("categories", () =>
    fetchCategories(props.match.params.pollId, user.username)
  );

  if (status === "loading") return (
    <div className="container">
      <div className="row justify-content-center align-items-center text-center">
        <div className="col-12">
          <SpinnerLoader />
        </div>
      </div>
    </div>
    );

  if (status === "error") return (
      <div className="container justify-content-center align-items-center">
        <div className="row">
          <div className="col-md-12">
            <p className="alert">There was a problem trying to load the categories: {error}</p>
          </div>
        </div>
      </div>
    );

  return (
    <div className="container">
      <div className="row text-center mt-5">
        <div className="col-md-12">
          <h4 className="text-light">Select a category below to proceed</h4>
        </div>
      </div>

      <div className="row">
      {data.length > 0 ? (
        data.map((cat) => <Category key={cat.id} pollId={props.match.params.pollId} category={cat} {...props} />)
      ) : (
        <div className="col-md-12 justify-content-center align-items-center text-center">
          <a href="/login" className="btn btn-sm btn-info">There are no more categories to vote for, Click button to End!</a>
        </div>
      )}
      </div>
    </div>
  );
};

export default Categories;
