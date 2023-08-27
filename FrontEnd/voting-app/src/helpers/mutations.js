const BASE_URL = "https://myvotingapi.azurewebsites.net/api/V1";

export const logIn = async (user) => {
  const response = await fetch(`${BASE_URL}/Identity/Login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: user.username,
      password: user.password ? user.password : null,
    }),
  });

  const data = await response.json();
  // localStorage.setItem("token", data.token);
  // localStorage.setItem("username", data.user.username);
  return data;
};

export const createPoll = async (poll) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/poll`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({
      theme: poll.theme,
      description: poll.description,
      startedDateTime: poll.startedDateTime,
      endedDateTime: poll.endedDateTime
    })
  });

  const data = await response.json();
  return data;
};

export const createNominee = async (formData) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/Nominee`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
    },
    body: formData
  });

  const data = await response.json();
  return data;
}

export const createCategory = async(cat) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/poll/${cat.pollId}/category`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({
      theme: cat.theme,
      pollId: cat.pollId
    })
  });

  const data = await response.json();
  return data;
}

export const addNomineeToCategory = async(catId, nomId) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/category/${catId}/nominee/${nomId}`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  var res = await response.json();
  return res;
}

export const performVote = async (vote) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/voter/vote`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({
      nomineeId: vote.nomineeId,
      categoryId: vote.categoryId,
      username: vote.username
    })
  });

  const data = await response.json();
  return data;
}

export const removeNomineeFromCategory = async(catId, nomId) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/category/${catId}/nominee/${nomId}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  var res = await response.json();
  return res;
}

export const registerUser = async (user) => {
  console.log(user);
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/identity/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({
      username: user.username,
      password: user.password ? user.password : null,
      role: user.role
    })
  });

  const data = await response.json();
  return data;
}

export const deletePoll = async (pollId) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/poll/${pollId}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    }
  });

  const data = await response.json();
  return data;
}

export const deleteNominee = async (nomineeId) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/nominee/${nomineeId}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    }
  });

  const data = await response.json();
  return data;
}

export const deleteWorker = async (username) => {
  const token = localStorage.getItem("token");
  const response = await fetch(`${BASE_URL}/identity/deleteuser/${username}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    }
  });

  const data = await response.json();
  return data;
}
