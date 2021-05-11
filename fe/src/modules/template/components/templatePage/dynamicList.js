import { createEmptyState } from "@react-page/core";
import React, { useState, useRef, useEffect } from "react";
import { useForm, Controller } from "react-hook-form";

import * as yup from "yup";
import "../../style.scss";
import OryEditor from "../editor/oryEditor";
import { Button, Row } from "antd";
import InputText from "../../../../app/components/input";
import {
    actionDynamicFormSave,
    actionDynamicFormGetById,
    actionSetPageMode
} from '../../store/action';
import { useDispatch, useSelector } from "react-redux";
import { PageMode } from "../../store/constant";

const DynamicList = (props) => {
  const [] = useState(false);
  const refOryEditor = useRef(null);
  const dispatch = useDispatch();
  const getValidateSettings = () => {
    let schema = yup.object().shape({
      name: yup.string().required("Trường này yêu cầu nhập."),
      tableName: yup.string().required("Trường này yêu cầu nhập."),
    });
    return schema;
  };
  const {
    handleSubmit,
    control,
    getValues,
    setValue,
    triggerValidation,
    errors,
  } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
    validationSchema: getValidateSettings(),
  });

  const { globalStore } = props;
  const defaultApiInfo = [
    {
      apiType:"LoadEditData",
      apiUrl:"",
      
    }
  ]
  // Editor Value State
  const [editorValue, setEditorValue] = useState(createEmptyState);
  const dynamicFormData = useSelector((state) => {
    return state.TemplateManagement.dynamicFormData.data;
  });

  const handleSubmitEvent = async()=>{
    if(refOryEditor && refOryEditor.current){
     let valid =  await refOryEditor.current.validateSubmitEvent();
     if(valid){
       let data = refOryEditor.current.getSubmitData();
     }
    }
  };

  const handleCreate = async () => {
    
    if(globalStore.pageMode==PageMode.PreviewPage){
      await handleSubmitEvent();
    }
    else{
      let formDatas = { ...getValues() };
      formDatas.id = props.match.params.id||0;
      formDatas.Content = JSON.stringify(editorValue);
      dispatch(actionDynamicFormSave(formDatas));
    }
    
  };
  useEffect(() => {
    //dynamiclist
    dispatch(actionSetPageMode({...globalStore}))
    let id = props.match.params.id;
    if (id) {
      dispatch(actionDynamicFormGetById({ id: id }));
    }
  }, []);

  useEffect(() => {
    if (dynamicFormData) {
      setValue("name", dynamicFormData.name);
      setValue("path", dynamicFormData.path);
      setValue("tableName", dynamicFormData.tableName);
      setValue("description", dynamicFormData.description);
      setValue("apiInfo", dynamicFormData.apiInfo);
      setEditorValue(JSON.parse(dynamicFormData.content));
    }
  }, [dynamicFormData]);
  return (
    <React.Fragment>
      <div>
        <form onSubmit={handleSubmit(handleCreate)}>
          <div style={{ padding: 10 }}>
            <Button type="primary" onClick={handleSubmit(handleCreate)}>
              Save
            </Button>

            <Row>
              <Controller
                as={
                  <InputText
                    type="text"
                    name="name"
                    label="Tên form"
                    onBlur={() => triggerValidation("name")}
                  />
                }
                name="name"
                control={control}
              />
              {errors && errors.name && errors.name.message && (
                <div className="inline-error" style={{ paddingTop: 10 }}>
                  {errors.name.message}
                </div>
              )}
            </Row>
            <Row>
              <Controller
                as={
                  <InputText
                    type=" textarea"
                    label="Description"
                    onBlur={() => triggerValidation("description")}
                  />
                }
                name="description"
                control={control}
              />
              {errors && errors.description && errors.description.message && (
                <div className="inline-error" style={{ paddingTop: 10 }}>
                  {errors.description.message}
                </div>
              )}
            </Row>
            <Row>
              <Controller
                as={
                  <InputText
                    type="text"
                    label="Path"
                    onBlur={() => triggerValidation("path")}
                  />
                }
                name="path"
                control={control}
              />
              {errors && errors.path && errors.path.message && (
                <div className="inline-error" style={{ paddingTop: 10 }}>
                  {errors.path.message}
                </div>
              )}
            </Row>
            <Row>
              <Controller
                as={
                  <InputText
                    type="text"
                    label="Table's Name"
                    onBlur={() => triggerValidation("tableName")}
                  />
                }
                name="tableName"
                control={control}
              />
              {errors && errors.tableName && errors.tableName.message && (
                <div className="inline-error" style={{ paddingTop: 10 }}>
                  {errors.tableName.message}
                </div>
              )}
            </Row>
            <Row>
              <Controller
                as={
                  <InputText
                    type="textarea"
                    label="Api Info"
                    rows = "5"
                    onBlur={() => triggerValidation("apiInfo")}
                    defaultValue = {JSON.stringify(defaultApiInfo)}
                  />
                }
                name="apiInfo"
                control={control}
              />
              {errors && errors.tableName && errors.tableName.message && (
                <div className="inline-error" style={{ paddingTop: 10 }}>
                  {errors.tableName.message}
                </div>
              )}
            </Row>
          </div>
          <div className={"template-page-main "}>
            {!!editorValue && (
              <>
                <OryEditor
                  editorValue={editorValue}
                  onChange={setEditorValue}
                  globalStore={globalStore}
                  editorRef={refOryEditor}
                  templateBackground={"#ffffff"}
                ></OryEditor>
              </>
            )}
          </div>
        </form>
      </div>
    </React.Fragment>
  );
};

export default DynamicList;

