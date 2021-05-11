import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import createSagaMiddleware from 'redux-saga';
import rootSaga from './sagas';
import reducers from './reducers';

import { localizeReducer } from 'react-localize-redux';

// Create Saga Middleware
const sagaMiddleware = createSagaMiddleware();
// Create ComposeEnhancers
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
// Root Reducer
const rootReducer = combineReducers({
  localize: localizeReducer,
  ...reducers
});

// Create Store
const store = createStore(
  rootReducer,
  composeEnhancers(applyMiddleware(sagaMiddleware))
);

sagaMiddleware.run(rootSaga);

export default store;
