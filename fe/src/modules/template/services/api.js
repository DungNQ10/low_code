const { post, get } = require("../../../app/common/apiUtil");

const baseUrl = process.env.REACT_APP_API_URL;
const dynamicList = async (data) => {
   
    var url =`${baseUrl}/api/v1/extractdb/dynamicquery`;
  let res =   await post(url,data);
  return res;
};

const dynamicListWithPaging = async (data) => {
   
    var url =`${baseUrl}/api/v1/extractdb/dynamicQueryWithPaging`;
  let res =   await post(url,data);
  return res;
};

const dynamicFormList = async (data)=>{
  var url =`${baseUrl}/api/v1/dynamicForm/list`;
  let res = await post(url,data);
  return res;
}

const dynamicFormSave = async (data)=>{
  var url =`${baseUrl}/api/v1/dynamicForm/update`;
  let res = await post(url,data);
  return res;
}

const dynamicFormGetById = async (data)=>{
  var url =`${baseUrl}/api/v1/dynamicForm/get_by_id`;
  let res = await get(url,data);
  return res;
}

const loadDataFromUrl = async (api)=>{
  let link = api;
  if(api && (!api.startsWith('http') && !api.startsWith('https'))){
    link = baseUrl + api;
  }
  let res = await get(link);
  
  return res;
}

const saveDataToApi = async (data )=>{
  let link = data.api;
  if(data.api && (!data.api.startsWith('http') && !data.api.startsWith('https'))){
    link = baseUrl + data.api;
  }
  let res = await post(link,data.submitData);
  return res;
}

const dynamicFormSubmit = async (data)=>{
  var url =`${baseUrl}/api/v1/extractdb/dynamicsave`;
  let res = await post(url,data);
  return res;
}

const dynamicFormGetEditData = async (data)=>{
  var url =`${baseUrl}/api/v1/extractdb/dynamicquery`;
  let res = await post(url,data);
  return res;
}

const dynamicQuery = async (data)=>{
  var url =`${baseUrl}/api/v1/extractdb/dynamicquery`;
  let res = await post(url,data);
  if(res.data){
    return res.data;
  }
  return res;
}


export  {dynamicList, dynamicListWithPaging, dynamicFormList, dynamicFormSave, dynamicFormGetById,dynamicFormSubmit,dynamicFormGetEditData , loadDataFromUrl , saveDataToApi , dynamicQuery};