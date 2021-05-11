import { Switch, Route } from "react-router-dom";
import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  actionListDynamic,
  actionListDynamicRESULT,
} from "../template/store/action";
import { put } from "redux-saga/effects";
import TemplateForm from "./components";
import { PageMode } from "../template/store/constant";

function DynamicForm(props) {
  const dispatch = useDispatch();
  // const path = props.match.path;
  // const list = useSelector(state => {
  //     console.log(state);
  //     return state.TemplateManagement
  // });
  // console.log("data", props);
  useEffect(() => {
    const payload = {
      paramaters: [],
      query: "select  * from Attachments",
      paging: {
        pageSize: 0,
        currentPage: 0,
        rowsCount: 0,
      },
    };
    dispatch(actionListDynamic(payload));
  }, []);

  return (
    <Switch>
      
      <Route
        path="/dynamicform/form/:id/:itemId"
        render={(props) => (
          <TemplateForm
            globalStore={{ pageMode: PageMode.FunctionPage, siteMode: "Admin" }}
            formKey = {props.match.params.id}
            itemId={props.match.params.itemId}
            {...props}
          />
        )}
      />
      <Route
        path="/dynamicform/form/:id"
        render={(props) => (
          <TemplateForm
            globalStore={{ pageMode: PageMode.FunctionPage, siteMode: "Admin" }}
            formKey = {props.match.params.id}
            itemId={""}
            {...props}
          />
        )}
      />
    </Switch>
  );
}

export default DynamicForm;
