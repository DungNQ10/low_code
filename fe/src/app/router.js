import React from 'react';
import { Route, Switch } from 'react-router-dom';
import module from './modules';

export default () => {
    return (
        <Switch>

            <Route {...module.TemplateManagement} />
            <Route {...module.DynamicForm} />
        </Switch>
    );
};
