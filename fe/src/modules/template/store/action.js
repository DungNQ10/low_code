import TYPES from './constant';
export const actionListDynamic = (payload) => {
  return { type: TYPES.DYNAMIC_LIST_REQUEST, payload:payload };
};

export const actionListDynamicRESULT = (payload) => {
  return { type: TYPES.DYNAMIC_LIST_REQUESTSUCCESS, payload:payload };
};

export const actionDynamicFormList = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_REQUEST, payload:payload };
};
/**Dynamic form save */
export const actionDynamicFormSave = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_SAVE, payload:payload };
};
export const actionDynamicFormSaveLoading = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_SAVE_LOADING, payload:payload };
};
export const actionDynamicFormSaveFailed = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_SAVE_FAILE, payload:payload };
};

export const actionClearAfterFormSaveSusccess = (payload) => {
  return { type: TYPES.CLEAR_AFTER_FORM_SUBMIT_SUCCESS, payload:payload };
};

/**Dynamic form get edit data */
export const actionDynamicFormGetEditData = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_GET_EDIT_DATA, payload:payload };
};
export const actionDynamicFormGetEditDataLoading = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_GET_EDIT_DATA_LOADING, payload:payload };
};
// export const actionDynamicFormGetEditDataFailed = (payload) => {
//   return { type: TYPES.DYNAMIC_FORM_SAVE_FAILE, payload:payload };
// };


export const actionLoadDataFromUrl = (payload) => {
  return { type: TYPES.LOAD_FORM_DATA_FROM_URL, payload:payload };
};

export const actionSubmitDataToUrl = (payload) => {
  return { type: TYPES.SUBMIT_FORM_DATA_TO_URL, payload:payload };
};

/**Dynamic form get by Id */
export const actionDynamicFormGetById = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_GET_BY_ID, payload:payload };
};
export const actionDynamicFormGetByIdLoading = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_GET_BY_ID_LOADING, payload:payload };
};
export const actionDynamicFormGetByIdFailed = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_GET_BY_ID_FAILE, payload:payload };
};
export const actionSetPageMode = (payload) => {
  
  return { type: TYPES.PAGE_MODE, payload:payload };
};

export const actionDynamicFormSubmit = (payload) => {
  return { type: TYPES.DYNAMIC_FORM_SUBMIT, payload:payload };
};

export const actionUpdateFormData = (type,formId, data) => {
  
  return { type: type, payload:{formId:formId,data:data} };
};

export const actionDispatchAction = (actionKey)=>{
  return {type:TYPES.TEMPLATE_DISPATCH_ACTION, actionKey:actionKey};
}

export const actionRemoveDispatchAction = (actionKey)=>{
  return {type:TYPES.TEMPLATE_DISPATCH_ACTION_REMOVE, actionKey:actionKey};
}