const loginSuccess = (state, action) => {
  return { ...state, userInfo: action.userInfo };
};

const authReducer = (state = { }, action) => {
  switch(action.type) {
    case 'LOGIN':
      return { ...state, loading: true };
    case 'LOGIN_SUCCESS':
      return loginSuccess(state, action);
    default:
      return state;
  }
};

export default authReducer;
