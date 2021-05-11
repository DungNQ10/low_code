import { combineReducers } from 'redux';
import Type from '../types/common';

const defaultSate = {
  error: {
    status: 200,
    message: null
  }
};

export default combineReducers({
  error: (state = defaultSate.error, action) => {
    switch(action.type) {
      case Type.SHOW_ERROR:
        return {...state, ...action.error };
      default:
        return state;
    }
  }
});