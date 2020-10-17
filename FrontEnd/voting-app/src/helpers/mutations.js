const BASE_URL = "https://localhost:5001/api/V1";

export const logIn = async (user) => {
  const response = await fetch(`${BASE_URL}/Identity/Login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: user.username,
      password: user.password,
    }),
  });

  const data = await response.json();
  localStorage.setItem("token", data.token);
  localStorage.setItem("username", data.user.username);
  return data.user;
};
