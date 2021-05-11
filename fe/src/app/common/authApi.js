import Axios from "axios";
export function fetchAccessToken(uid, pwd) {
  let tokenUrl = process.env.REACT_APP_AUTH_URL + "token";
  let client_id = "ro.client";
  let client_secret = "secret";
  let data = {
    username: uid,
    password: pwd,
    grant_type: "password",
    scope: "openid offline_access wgd_api",
    client_id: client_id,
    client_secret: client_secret,
  };
  const transformRequest = (jsonData = {}) =>
    Object.entries(jsonData)
      .map((x) => `${encodeURIComponent(x[0])}=${encodeURIComponent(x[1])}`)
      .join("&");
  return Axios({
    url: tokenUrl,
    method: "POST",
    data: data,
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    transformRequest: (data) => {
      return transformRequest(data);
    },
  });
}
