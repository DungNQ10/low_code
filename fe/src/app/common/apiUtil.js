import axios from 'axios';
// var urljoin = require("url-join");

// export function make(...args) {
//   var baseUrl = process.env.REACT_APP_API_URL;
//   var path = urljoin(args);
//   return urljoin(baseUrl, path);
// }

export const get = async (url,params) => {
  return await call(url, "GET", params);
};

export const post = async (url, model) => {
  return await call(url, "POST", model);
};

export const guestPost = async (url, model) => {
  return await call(url, "POST", model, true);
};
export const guestGet = async (url, model) => {
  return await call(url, "GET", model, true);
};


export const put = async (url, model) => {
  return await call(url, "PUT", model);
};

export const del = async (url, model) => {
  return await call(url, "DELETE", model);
};
export const upload = async (url, file, savePath) => {
  var model = new FormData();
  model.append("file", file);
  if (savePath) model.append("model", JSON.stringify({ savePath: savePath }));
  return await call(url, "UPLOAD", model);
};
export const multiUpload = async (url, files, savePath) => {
  var model = new FormData();
  let i = 0;
  files.forEach(file => {
    model.append("file" + i, file);
    i++;
  });
  if (savePath) model.append("model", JSON.stringify({ savePath: savePath }));

  return await call(url, "UPLOAD", model);
};


const call = async (url, method, model, noAuth) => {
  var access_token = undefined;
  try {
    var strToken = localStorage.getItem("token");
    var token = strToken;
    if (token) access_token = token;
    else access_token = null;

    var headers = {
      Accept: "application/json",
      "Content-Type": "application/json",
      'Authorization': "Bearer " + access_token
    };

    if (noAuth||!access_token) headers.Authorization = undefined;

    var config = {
        url: url,
        method: method,
        headers: headers
    };
    if(method=="GET")
        config.params =model;
    else config.data =model;
    var request = await axios(config);
    let rest= request.data;
    
    // await request.then((response) => {
    //     rest =  response.data;
    // }).catch((err) => {
    //     console.log(err);
    //     return undefined;
    // });
    return rest;
  }catch(err){
      console.log(err);
      return undefined;
  }
};
