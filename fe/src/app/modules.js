import { lazy } from 'react';
import reducerTemplate from 'modules/template/store/reducer';
import sagaTemplate from 'modules/template/store/saga';
//Module config
const modules = {

  TemplateManagement: {
    path: '/template',
    component: lazy(() => import('../modules/template')),
    reducer: reducerTemplate,
    saga: sagaTemplate
  },
  DynamicForm: {
    path: '/dynamicform',
    component: lazy(() => import('../modules/dynamicform')),
    // reducer: reducerTemplate,
    // saga: sagaTemplate
  }
};

let sagas = [];
let reducers = {};

Object.keys(modules).forEach(moduleName => {
  
  //append modules's reducer to root reducer
  if (modules[moduleName]['reducer']) {
    reducers[moduleName] = modules[moduleName]['reducer'];
  }
  //append modules's sage to root reducer
  if (modules[moduleName]['saga']) {
    sagas = sagas.concat(modules[moduleName]['saga']);
  }
});

export const moduleSagas = sagas;
export const moduleReducers = reducers;

export default modules;