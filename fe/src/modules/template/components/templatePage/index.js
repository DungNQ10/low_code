import { createEmptyState } from "@react-page/core";
import React, { useState, useRef, useEffect } from "react";
import { useForm, Controller } from "react-hook-form";
import dynamicList from "../../services/api";

import * as yup from "yup";
import "../../style.scss";
import OryEditor from "../editor/oryEditor";
import { Button, Row, Collapse } from 'antd';
import InputText from "../../../../app/components/input";
import {
  actionDynamicFormSave,
  actionDynamicFormGetById,
  actionSetPageMode,
} from "../../store/action";
import { useDispatch, useSelector } from "react-redux";
import { PageMode } from "../../store/constant";
import FormApiConfig from '../../plugins/Common/formApiConfig';
const { Panel } = Collapse;


const TemplatePage = (props) => {
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
  // const defaultApiInfo = [
  //   {
  //     apiType: "LoadEditData",
  //     apiUrl: "",
  //   },
  // ];
  // Editor Value State
  const [editorValue, setEditorValue] = useState(createEmptyState);
  const [dynamicFormData, setDynamicFormData] = useState(null);
  const [formApi,setFormApi] = useState({});
  const dynamicFormDataStore = useSelector(
    (state) => state.TemplateManagement.dynamicFormData
  );



  const onChangeFormApi = (apiName,values)=>{
    let k = {...formApi};
    k[apiName] = values;
    setFormApi(k);
  }
  useEffect(() => {
    if (dynamicFormDataStore)
      setDynamicFormData(dynamicFormDataStore.get(props.match.params.id || 0));
  }, [dynamicFormDataStore]);
  const handleSubmitEvent = async () => {
    if (refOryEditor && refOryEditor.current) {
      let valid = await refOryEditor.current.validateSubmitEvent();
      if (valid) {
        let data = refOryEditor.current.getSubmitData();
      }
    }
  };

  const handleCreate = async () => {
    if (globalStore.pageMode == PageMode.PreviewPage) {
      await handleSubmitEvent();
    } else {
      let formDatas = { ...getValues() };
      formDatas.id = props.match.params.id || 0;
      formDatas.Content = JSON.stringify(editorValue);
      formDatas.apiInfo = JSON.stringify(formApi);
      dispatch(actionDynamicFormSave(formDatas));
    }
  };
  useEffect(() => {
    
    dispatch(actionSetPageMode({ ...globalStore }));
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
      setFormApi(JSON.parse(dynamicFormData.apiInfo) );
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
            {/* <Row>
              <Controller
                as={
                  <InputText
                    type="textarea"
                    label="Api Info"
                    rows="5"
                    onBlur={() => triggerValidation("apiInfo")}
                    defaultValue={JSON.stringify(defaultApiInfo)}
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
            </Row> */}
            <Row>
              <Collapse defaultActiveKey={["1"]} accordion style={{width:'100%'}}>
                <Panel header="Detail api" key="1">
                  <FormApiConfig
                    onChangeFormApi={onChangeFormApi}
                    apiName="detailApi"
                    data={formApi}
                  ></FormApiConfig>
                </Panel>
                <Panel header="Save api" key="2">
                  <FormApiConfig        
                    onChangeFormApi={onChangeFormApi}          
                    apiName="saveApi"
                    data={formApi}
                  ></FormApiConfig>
                </Panel>
                <Panel header="Update api" key="3">
                <FormApiConfig        
                    onChangeFormApi={onChangeFormApi}          
                    apiName="updateApi"
                    data={formApi}
                  ></FormApiConfig>
                </Panel>
              </Collapse>
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

export default TemplatePage;
