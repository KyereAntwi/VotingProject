const BASE_URL = "https://localhost:5001/api/V1";

export const fetchPolls = async () => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/Poll`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  const data = await response.json();
  return data;
};

export const fetchCategories = async (pollId, username) => {
  const token = localStorage.getItem("token");
  const response = await fetch(
    `${BASE_URL}/Poll/${pollId}/AvailableCategories/${username}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );
  const data = await response.json();
  return data;
};

export const fetchNominations = async (categoryId) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/Category/${categoryId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  const data = await response.json();
  return data;
};

export const fetchUserInfo = async () => {
  const token = localStorage.getItem("token");
  const username = localStorage.getItem("username");
  const response = await fetch(`${BASE_URL}/Identity/User/${username}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  const { user } = await response.json();
  return user;
};
