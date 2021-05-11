import * as Types from '../types/bannerManagement';
import {getListBanner, getDetailBanner, updateBanner} from '../../services/api/index';

// list banner
export const listSuccess  = (data) => {
  return{ type: Types.BANNER_LIST_SUCCESS, data};
};
export const listLoading = ( loading ) => {
  return {type: Types.BANNER_LIST_LOADING, loading};
};
export const listError = ( error ) => {
  return {type: Types.BANNER_LIST_ERROR, error};
};

// =============create banner===============//
export const createSuccess  = (data) => {
  return{ type: Types.BANNER_CREATE_SUCCESS, data};
};
export const createLoading = ( loading ) => {
  return {type: Types.BANNER_CREATE_LOADING, loading};
};
export const createError = ( error ) => {
  return {type: Types.BANNER_CREATE_ERROR, error};
};

// edit banner
export const editSuccess  = (data) => {
  return{ type: Types.BANNER_EDIT_SUCCESS, data};
};
export const editLoading = ( loading ) => {
  return {type: Types.BANNER_EDIT_LOADING, loading};
};
export const editError = ( error ) => {
  return {type: Types.BANNER_EDIT_ERROR, error};
};

export const listBanner = (dispatch, page = 1, sitesId) => {
  const option = {
    page,
    size: 10,
    sitesId
  };
  dispatch(listLoading(true));
 
  // Call to API and get data
  getListBanner(option)
    .then(response => {
      dispatch(listLoading(false));
      return response.data;
    })
    .then(data => {
      dispatch(listSuccess(data));
    })
    .catch(err => {
      dispatch(listError(err));
      dispatch(listLoading(false));
    });
};

export const detailBanner =(dispatch, id) => {
  dispatch(editLoading(true));
  getDetailBanner(id).then(res => {
    dispatch(editSuccess(res.data.data));
    dispatch(editLoading(false));
  }).catch(err => {
    dispatch(editError(err));
    dispatch(editLoading(false));
  });
};

export const updateBannerManage = (dispatch) => {
  updateBanner().then(res => {
    dispatch(editSuccess(res.data.data));
    dispatch(editLoading(false));
  }).catch(err => {
    dispatch(editError(err));
    dispatch(editLoading(false));
  });
};

