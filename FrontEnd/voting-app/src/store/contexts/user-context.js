import React, { createContext } from "react";
import { useQuery, useMutation, queryCache } from "react-query";

import { fetchUserInfo } from "../../helpers/querries";
import { logIn } from "../../helpers/mutations";

export const UserContext = createContext();

const UserContextProvider = ({ children }) => {
  const { data: user, error } = useQuery("user", fetchUserInfo);

  const [mutation] = useMutation(logIn, {
    onSuccess: (newUser) => {
      queryCache.setQueryData("user", (current) => newUser);
    },
    onError: (error) => console.log(error),
  });

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    queryCache.setQueryData("user", null);
  };

  if (error) {
    console.log(error);
  }

  return (
    <UserContext.Provider value={{ user, mutation, logout }}>
      {children}
    </UserContext.Provider>
  );
};

export default UserContextProvider;
