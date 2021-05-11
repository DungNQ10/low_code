import { combineReducers } from 'redux';
import * as Types from '../types/bannerManagement';

export const list = (state = { data: { data: [], total: 0}, loading: false, error: null}, action) => {
  switch (action.type) {
    case Types.BANNER_LIST_SUCCESS:
      return { ...state, data: action.data };
    case Types.BANNER_LIST_LOADING:
      return { ...state, loading: action.loading };
    case Types.BANNER_LIST_ERROR:
      return { ...state, error: action.error };

    default:
      return state;
  }
};

export const create = (state = { data: {}, loading: false, error: null}, action) => {
  switch (action.type) {
    case Types.BANNER_CREATE_SUCCESS:
      return { ...state, data: action.data };
    case Types.BANNER_CREATE_LOADING:
      return { ...state, loading: action.loading };
    case Types.BANNER_CREATE_ERROR:
      return { ...state, error: action.error };

    default:
      return state;
  }
};

export const edit = (state = { data: {}, loading: false, error: null}, action) => {
  switch (action.type) {
    case Types.BANNER_EDIT_SUCCESS:
      return { ...state, data: action.data };
    case Types.BANNER_EDIT_LOADING:
      return { ...state, loading: action.loading };
    case Types.BANNER_EDIT_ERROR:
      return { ...state, error: action.error };

    default:
      return state;
  }
};

export default combineReducers({ list, create, edit });
