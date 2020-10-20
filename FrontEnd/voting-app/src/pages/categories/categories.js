import React, { useContext } from "react";
import {Alert} from 'react-bootstrap';
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

  if (status === "loading") return <SpinnerLoader />;
  if (status === "error") {
    return (
    <div className="col-md-12">
      <Alert variant="warning">
        There was an error loading Category list: {error}
      </Alert>
    </div>
    );
  }

  if (data.length === 0) {
    return(
      <div className="col-md-12">
        <Alert variant="info">
          There are no Categories to display
        </Alert>
      </div>
    )
  }

  return (
    <div className="container">
      <div className="row">
        <div className="col-md-12">
          <h1>Select a category below to procedd</h1>
        </div>
      </div>

      <div className="row">
      {data.length > 0 ? (
        data.map((cat) => <Category key={cat.id} category={cat} {...props} />)
      ) : (
        <div className="col-md-12">
          <Alert variant="info">
            There are no categories to display!
          </Alert>
        </div>
      )}
      </div>
    </div>
  );
};

export default Categories;
