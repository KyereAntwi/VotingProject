import React, { createContext, useState } from "react";
import { logIn } from "../../helpers/mutations";
import { toast } from "react-toastify";

export const UserContext = createContext();

const UserContextProvider = (props) => {
  const [user, setUser] = useState({});

  const mutation = async (newUser) => {
    console.log(newUser);
    const res = await logIn(newUser);
    console.log(res);

    if(res.username) {
      setUser(res);
    } else {
      toast("Error occured in logging in");
    }
  }

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
  };

  return (
    <UserContext.Provider value={{ user, mutation, logout }}>
      {props.children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;
