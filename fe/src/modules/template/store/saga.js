import {
    dynamicFormList,
    dynamicList,
    dynamicFormSave,
    dynamicListWithPaging,
    dynamicFormGetById,
    dynamicFormSubmit,
    dynamicFormGetEditData,
    loadDataFromUrl,
    saveDataToApi
} from '../services/api';
import { takeEvery, put, call } from "redux-saga/effects";
import TYPES from "./constant";
import {
    actionListDynamic,
    actionDynamicFormSaveLoading,
    actionDynamicFormSaveFailed,
    actionDynamicFormGetByIdLoading,
    actionDynamicFormGetByIdFailed,
    actionSetPageMode,
    actionDynamicFormGetEditDataLoading
} from './action';
function* asynclistDynamic(action) {
  let res = yield dynamicList(action.payload);
  let data = res.data || [];
  yield put({ type: TYPES.DYNAMIC_LIST_REQUESTSUCCESS, data });
}

function* asyncDynamicListWithPaging(action) {
  let res = yield dynamicListWithPaging(action.payload);
  let data = res.data || {};
  yield put({ type: TYPES.DYNAMIC_LIST_PAGING_REQUEST_SUCCESS, data });
}

function* asyncDynamicFormList(action) {
  let res = yield dynamicFormList(action.payload);

  let data = res.data || [];
  yield put({ type: TYPES.DYNAMIC_FORM_REQUEST_SUCCESS, data });
}

function* asyncDynamicFormSave(action) {
  try {
     
    put(actionDynamicFormSaveLoading(true));
    let res = yield dynamicFormSave(action.payload);
    put(actionDynamicFormSaveLoading(false));
    let data = res.data || [];
    yield put({ type: TYPES.DYNAMIC_FORM_SAVE_SUCCESS, data });
  } catch (error) {
    put(actionDynamicFormSaveLoading(false));
    put(actionDynamicFormSaveFailed(error));
  }
}

function* asyncDynamicFormGetById(action) {
  try {
    
    put(actionDynamicFormGetByIdLoading(true));
    let res = yield dynamicFormGetById(action.payload);
    put(actionDynamicFormGetByIdLoading(false));
    let payload ={templateId:action.payload.id,data:res.data || {}} ;
    yield put({ type: TYPES.DYNAMIC_FORM_GET_BY_ID_SUCCESS, payload});
  } catch (error) {
    put(actionDynamicFormGetByIdLoading(false));
    put(actionDynamicFormGetByIdFailed(error));
  }
}

function* asyncDynamicFormGetEditData(action) {
  try {
    put(actionDynamicFormGetEditDataLoading(true));
    let res = yield dynamicFormGetEditData(action.payload);
    put(actionDynamicFormGetEditDataLoading(false));
    let data = res.data || {};
    yield put({ type: TYPES.DYNAMIC_FORM_GET_EDIT_DATA_SUCCESS, data });
  } catch (error) {
    put(actionDynamicFormGetEditDataLoading(false));
    // put(actionDynamicFormGetByIdFailed(error));
  }
}


function* asyncLoadFromDataFromUrl(action){
  try {   
    yield put(actionDynamicFormGetEditDataLoading(true));
    let res = yield loadDataFromUrl(action.payload);
    yield put(actionDynamicFormGetEditDataLoading(false));
    let data = res.data || res || {};
    let formData = [];
    formData.push(data);
    yield put({ type: TYPES.DYNAMIC_FORM_GET_EDIT_DATA_SUCCESS, data : formData });
  } catch (error) {
    yield put(actionDynamicFormGetEditDataLoading(false));
  }
}

function* asyncFormSaveToApi(action) {
  try {  
    yield put(actionDynamicFormSaveLoading(true));
    let res = yield saveDataToApi(action.payload);
    yield put(actionDynamicFormSaveLoading(false));
    let data = res.data || [];
    yield put({ type: TYPES.DYNAMIC_FORM_SAVE_SUCCESS, data });
  } catch (error) {
    yield  put(actionDynamicFormSaveLoading(false));
    yield put(actionDynamicFormSaveFailed(error));
  }
}

function setPageMode(action){
  put(actionSetPageMode(action.payload));
}
function * asyncDynamicFormSubmit(action){
  yield put(actionDynamicFormSaveLoading(true));
  let res = yield dynamicFormSubmit(action.payload);
  yield put(actionDynamicFormSaveLoading(false));
  let data = res.data || {};
  yield put({ type: TYPES.DYNAMIC_FORM_SUBMIT_SUCCESS, data });
}

export default [
  takeEvery(TYPES.DYNAMIC_LIST_REQUEST, asynclistDynamic),
  takeEvery(TYPES.DYNAMIC_FORM_REQUEST, asyncDynamicFormList),
  takeEvery(TYPES.DYNAMIC_FORM_SAVE, asyncDynamicFormSave),
  takeEvery(TYPES.DYNAMIC_LIST_PAGING_REQUEST, asyncDynamicListWithPaging),
  takeEvery(TYPES.DYNAMIC_FORM_GET_BY_ID, asyncDynamicFormGetById),
  takeEvery(TYPES.PAGE_MODE, setPageMode),
  takeEvery(TYPES.DYNAMIC_FORM_SUBMIT, asyncDynamicFormSubmit),
  takeEvery(TYPES.DYNAMIC_FORM_GET_EDIT_DATA, asyncDynamicFormGetEditData),
  takeEvery(TYPES.LOAD_FORM_DATA_FROM_URL, asyncLoadFromDataFromUrl),
  takeEvery(TYPES.SUBMIT_FORM_DATA_TO_URL, asyncFormSaveToApi)


];
