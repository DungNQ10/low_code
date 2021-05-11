import { combineReducers } from "redux";
import TYPES from "./constant";

const INNIT_FORM_SAVE_SATE = {
  data: null,
  loading: false,
  error: null,
  saveSuccess: false,
};
export const listDynamic = (state = { data: [] }, action) => {
  switch (action.type) {
    case TYPES.DYNAMIC_LIST_REQUESTSUCCESS:
      return { ...state, data: action.data };
    default:
      return state;
  }
};

export const dynamicFormList = (state = { data: [] }, action) => {
  switch (action.type) {
    case TYPES.DYNAMIC_FORM_REQUEST_SUCCESS:
      return { ...state, data: action.data };
    default:
      return state;
  }
};

export const dynamicFormSave = (state = INNIT_FORM_SAVE_SATE, action) => {
  console.log("dynamicFormSave");
  switch (action.type) {
    case TYPES.DYNAMIC_FORM_SAVE_SUCCESS:
      return { ...state, data: action.data, saveSuccess: true };
    case TYPES.DYNAMIC_FORM_SAVE_LOADING:
      return { ...state, loading: action.payload };
    case TYPES.DYNAMIC_FORM_SAVE_FAILE:
      return { ...state, error: action.error };
    case TYPES.CLEAR_AFTER_FORM_SUBMIT_SUCCESS:
      return { ...INNIT_FORM_SAVE_SATE };
    default:
      return state;
  }
};

export const dynamicFormData = (state = new Map(), action) => {
  console.log("action.type", action.type, action);
  if (!action.payload) return state;
  var templateId = action.payload.templateId;
  if (templateId) {
    switch (action.type) {
      case TYPES.DYNAMIC_FORM_GET_BY_ID_SUCCESS:      
        state.set(templateId, action.payload.data);
        return new Map(state);
        break;
      default:
        break;
    }
    return state;
  } else {
    return state;
  }
  // switch (action.type) {
  //   case TYPES.DYNAMIC_FORM_GET_BY_ID_SUCCESS:
  //     return { ...state, data: action.data };
  //   case TYPES.DYNAMIC_FORM_GET_BY_ID_LOADING:
  //     return { ...state, loading: action.payload };
  //   case TYPES.DYNAMIC_FORM_GET_BY_ID_FAILE:
  //     return { ...state, error: action.error };
  //   default:
  //     return state;
  // }
};

export const templateData = (state = new Map(), action) => {
  if (!action.payload) return state;
  var templateId = action.payload.templateId;
  if (templateId) {
    switch (action.type) {
      case TYPES.TEMPLATE_GET_BY_ID_SUCCESS:
        state.set(templateId, action.payload.data);
        return new Map(state);
        break;
      default:
        break;
    }
    return state;
  } else {
    return state;
  }
};

export const dynamicFormEditData = (
  state = { data: null, loading: false, error: null },
  action
) => {
  switch (action.type) {
    case TYPES.DYNAMIC_FORM_GET_EDIT_DATA_SUCCESS:
      return { ...state, data: action.data };
    case TYPES.DYNAMIC_FORM_GET_EDIT_DATA_LOADING:
      return { ...state, loading: action.payload };
    default:
      return state;
  }
};

export const oryStore = (state = {}, action) => {
  switch (action.type) {
    case TYPES.PAGE_MODE:
      return { ...state, ...action.payload };
    default:
      return state;
  }
};

export const formData = (state = new Map(), action) => {
  if (!action.payload) return state;
  var formId = action.payload.formId;
  if (formId) {
    var data = state.get(formId);
    if (!data)
      data = {
        filter: {},
        list: [],
        form: {},
        editModal: {
          show: false,
          itemId: 0,
        },
      };
    switch (action.type) {
      case TYPES.FORMDATA.FILTER:
        data.filter = { ...data.filter, ...action.payload.data };
        state.set(formId, data);
        return new Map(state);
        break;
      case TYPES.FORMDATA.LIST:
        data.list = [...action.payload.data];
        state.set(formId, data);
        return new Map(state);
        break;
      case TYPES.FORMDATA.FORM:
        debugger;
        data.form = { ...data.form, ...action.payload.data };
        state.set(formId, data);
        return new Map(state);
        break;
      case TYPES.FORMDATA.EDITMODAL:
        data.editModal = { ...data.editModal, ...action.payload.data };
        state.set(formId, data);
        return new Map(state);
        break;
      default:
        break;
    }
    return state;
  } else {
    return state;
  }
};
export const dispatchAction = (state = { data: new Map() }, action) => {
   
  switch (action.type) {
    case TYPES.TEMPLATE_DISPATCH_ACTION:  
      var {data} =state;
      data.set(action.actionKey, new Date());
      return { ...state,data: new Map(data)};
    case TYPES.TEMPLATE_DISPATCH_ACTION_REMOVE:     
      var data1 =state.data;
      data1.delete(action.actionKey);
      return { ...state,data: new Map(data1)};
    default:
      return state;
  }
};

export default combineReducers({
  listDynamic,
  dynamicFormList,
  dynamicFormSave,
  dynamicFormData,
  dynamicFormEditData,
  oryStore,
  formData,
  templateData,
  dispatchAction,
});
