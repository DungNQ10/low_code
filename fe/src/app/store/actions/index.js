export const login = (username, password) => {
	return {
		type: 'LOGIN',
		username,
		password,
	};
};

export const loginSuccess = (userInfo) => {
	return {
		type: 'LOGIN_SUCCESS',
		userInfo,
	};
};

