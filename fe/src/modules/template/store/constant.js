const TYPES = {

    PAGE_MODE:'PAGE_MODE_TEMPLATE',
    DYNAMIC_LIST_REQUEST : 'DYNAMIC_LIST_REQUEST',
    DYNAMIC_LIST_REQUESTSUCCESS :'DYNAMIC_LIST_REQUESTSUCCESS',

    DYNAMIC_LIST_PAGING_REQUEST :'DYNAMIC_LIST_PAGING_REQUEST',
    DYNAMIC_LIST_PAGING_REQUEST_SUCCESS :'DYNAMIC_LIST_PAGING_REQUEST_SUCCESS',
    DYNAMIC_LIST_PAGING_REQUEST_FAILE :'DYNAMIC_LIST_PAGING_REQUEST_FAILE',

    DYNAMIC_FORM_REQUEST: 'DYNAMIC_FORM_REQUEST',
    DYNAMIC_FORM_REQUEST_SUCCESS: 'DYNAMIC_FORM_REQUEST_SUCCESS',
    DYNAMIC_FORM_REQUEST_FAILE: 'DYNAMIC_FORM_REQUEST_FAIL',
    
    DYNAMIC_FORM_SAVE: 'DYNAMIC_FORM_SAVE',
    DYNAMIC_FORM_SAVE_SUCCESS: 'DYNAMIC_FORM_SAVE_SUCCESS',
    DYNAMIC_FORM_SAVE_LOADING: 'DYNAMIC_FORM_SAVE_LOADING',
    DYNAMIC_FORM_SAVE_FAILE: 'DYNAMIC_FORM_SAVE_FAILE',

    DYNAMIC_FORM_GET_BY_ID: 'DYNAMIC_FORM_GET_BY_ID',
    DYNAMIC_FORM_GET_BY_ID_SUCCESS: 'DYNAMIC_FORM_GET_BY_ID_SUCCESS',
    DYNAMIC_FORM_GET_BY_ID_LOADING: 'DYNAMIC_FORM_GET_BY_ID_LOADING',
    DYNAMIC_FORM_GET_BY_ID_FAILE: 'DYNAMIC_FORM_GET_BY_ID_FAILE',

    DYNAMIC_FORM_SUBMIT: 'DYNAMIC_FORM_SUBMIT',
    DYNAMIC_FORM_SUBMIT_SUCCESS: 'DYNAMIC_FORM_SUBMIT_SUCCESS',
    CLEAR_AFTER_FORM_SUBMIT_SUCCESS: 'CLEAR_AFTER_FORM_SUBMIT_SUCCESS',

    TEMPLATE_GET_BY_ID : "TEMPLATE_GET_BY_ID",
    TEMPLATE_GET_BY_ID_SUCCESS:"TEMPLATE_GET_BY_ID_SUCCESS",
    TEMPLATE_GET_BY_ID_FAILE:"TEMPLATE_GET_BY_ID_FAILE",
    TEMPLATE_GET_BY_ID_LOADING:"TEMPLATE_GET_BY_ID_LOADING",

    DYNAMIC_FORM_GET_EDIT_DATA: 'DYNAMIC_FORM_GET_EDIT_DATA',
    DYNAMIC_FORM_GET_EDIT_DATA_SUCCESS: 'DYNAMIC_FORM_GET_EDIT_DATA_SUCCESS',
    DYNAMIC_FORM_GET_EDIT_DATA_LOADING: 'DYNAMIC_FORM_GET_EDIT_DATA_LOADING',

    LOAD_FORM_DATA_FROM_URL: 'LOAD_FORM_DATA_FROM_URL',
    SUBMIT_FORM_DATA_TO_URL: 'SUBMIT_FORM_DATA_TO_URL',
    TEMPLATE_DISPATCH_ACTION:"TEMPLATE_DISPATCH_ACTION",
    TEMPLATE_DISPATCH_ACTION_REMOVE:"TEMPLATE_DISPATCH_ACTION_REMOVE",
    FORMDATA:{
        FILTER :"FORMDATA_FILTER",
        LIST:"FORMDATA_LIST",
        FORM:"FORMDATA_FORM",
        EDITMODAL:"FORMDATA_EDIT_MODAL"
    }
}

export const  PageMode = {
    TemplatePage: 'TemplatePage',
    FunctionPage: 'FunctionPage',
    PreviewPage:'PreviewPage'
}

export default TYPES;