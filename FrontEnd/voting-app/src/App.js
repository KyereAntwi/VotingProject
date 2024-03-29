import React, { useContext } from "react";
import { Switch, Route, Redirect } from "react-router-dom";

import GettingStarted from "./pages/getting-started/getting-started";
import Login from "./pages/login/login";
import Categories from "./pages/categories/categories";
import Nominations from "./pages/nominations/nominations";
import Administrator from "./pages/administrator/administrator";
import ECO from "./pages/electoral-commission-officer/eco";

import { UserContext } from "./store/contexts/user-context";
import AdminCategories from "./components/admin-categories/admin-categories";

const App = (props) => {
  const { user } = useContext(UserContext);

  return (
    <div className="App">
      <Switch>
          <Route
                path="/polls/:pollId"
                exact
                render={(props) =>
                  user ? (
                    user.role === "Administrator" ? (
                      <AdminCategories {...props}/>
                    ) : (
                      <Redirect to="/login" />
                    )
                  ) : (
                    <Redirect to="/login" />
                  )
                }
          />
          <Route
            path="/eco"
            exact
            render={(props) =>
              <ECO />
            }
          />
          <Route
            path="/administrator"
            exact
            render={(props) =>
              user ? (
                user.role === "Administrator" ? (
                  <Administrator />
                ) : (
                  <Redirect to="/login" />
                )
              ) : (
                <Redirect to="/login" />
              )
            }
          />
          <Route
            path="/categories/:pollId"
            render={(props) =>
              user ? <Categories {...props} /> : <Redirect to="/login" />
            }
          />
          <Route
            path="/nominees/:categoryId"
            render={(props) =>
              user ? <Nominations {...props} /> : <Redirect to="/login" />
            }
          />
          <Route path="/login" render={(props) => <Login {...props} />} />
          <Route
            path="/start"
            render={(props) =>
              user.username ? <GettingStarted {...props}/> : <Redirect to="/login" />
            }
          />
          <Route path="/" exact render={(props) => <Redirect to="/login" />} />
          <Route path="**" render={(props) => <Redirect to="/start" />} />
        </Switch>
    </div>
  );
};

export default App;
