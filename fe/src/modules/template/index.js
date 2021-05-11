import { Switch, Route } from "react-router-dom";
import TemplatePage from "./components/templatePage";
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from "react-redux";
import { actionListDynamic, actionListDynamicRESULT } from './store/action';
import { put } from "redux-saga/effects";
import router from "./router"

function Template(props) {

    const dispatch = useDispatch();
    // const path = props.match.path;
    // const list = useSelector(state => {
    //     console.log(state);
    //     return state.TemplateManagement
    // });
    // console.log("data", props);
    useEffect(() => {
        const payload = {
            "paramaters": [

            ],
            "query": "select  * from Attachments",
            "paging": {
                "pageSize": 0,
                "currentPage": 0,
                "rowsCount": 0
            }
        };
        dispatch(actionListDynamic(payload));
    }, []);

    return (
        <Switch>
            {router.map((i, index) => (
                <Route key={index + "template"} path={i.path} exact render={(props) => <i.component {...i.props} {...props} />} />
            ))}

        </Switch>

    );
}

export default Template