import React, { useContext } from "react";
import { Switch, Route, Redirect } from "react-router-dom";

import Navbar from "./ui/navbar/navbar";
import Footer from "./ui/footer/footer";
import GettingStarted from "./pages/getting-started/getting-started";
import Login from "./pages/login/login";
import Categories from "./pages/categories/categories";
import Nominations from "./pages/nominations/nominations";
import Administrator from "./pages/administrator/administrator";
import ECO from "./pages/electoral-commission-officer/eco";

import { UserContext } from "./store/contexts/user-context";

const App = () => {
  const { user } = useContext(UserContext);

  return (
    <div className="App">
      <Navbar />
      <Switch>
        <Route
          path="/eco"
          render={(props) => (user.role = "" ? <ECO /> : <Redirect to="/" />)}
        />
        <Route
          path="/Administrator"
          render={(props) =>
            (user.role = "Administrator" ? (
              <Administrator />
            ) : (
              <Redirect to="/" />
            ))
          }
        />
        <Route
          path="/categories/:pollId"
          render={(props) => (user ? <Categories /> : <Redirect to="/login" />)}
        />
        <Route
          path="/nominees/:categoryId"
          render={(props) =>
            user ? <Nominations /> : <Redirect to="/login" />
          }
        />
        <Route path="/login" component={Login} />
        <Route path="/" exact component={GettingStarted} />
        <Route path="**" component={GettingStarted} />
      </Switch>
      <Footer />
    </div>
  );
};

export default App;
