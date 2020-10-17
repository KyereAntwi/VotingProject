import React from "react";
import "./notification.css";

const Notification = ({ notification }) => {
  return notification.type === "error" ? (
    <div className="notification error">{notification.message}</div>
  ) : notification.type === "warning" ? (
    <div className="notification warning">{notification.message}</div>
  ) : (
    <div className="notification success">{notification.message}</div>
  );
};

export default Notification;
