import React, { createContext, useState } from "react";

export const NotificationContext = createContext();

const NotificationContextProvider = ({ children }) => {
  const [notifications, setNotifications] = useState([]);
  return (
    <NotificationContext.Provider value={{ notifications, setNotifications }}>
      {children}
    </NotificationContext.Provider>
  );
};

export default NotificationContextProvider;
