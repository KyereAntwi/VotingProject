import React, { createContext, useState } from "react";
import { logIn } from "../../helpers/mutations";

export const UserContext = createContext();

const UserContextProvider = (props) => {
  const [user, setUser] = useState({});
  const [errorMessage, setErrorMessage] = useState();

  const mutation = async (newUser) => {
    const res = await logIn(newUser);

    if(res.user) {
      localStorage.setItem("token", res.token);
      localStorage.setItem("username", res.user.username);
      setUser(res.user)
    } else {
      setErrorMessage("There was an error loggin in, try again with right credentials")
    }
  }

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
  };

  return (
    <UserContext.Provider value={{ user, mutation, logout, errorMessage }}>
      {props.children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;
