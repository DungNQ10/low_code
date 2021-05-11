import { createEmptyState } from "@react-page/core";
import React, { useState, useRef, useEffect } from "react";
import { useForm, Controller } from "react-hook-form";
import * as yup from "yup";
import "modules/template/style.scss";
import OryEditor from "../../editor/oryEditor";
import {
  Row,
  PageHeader,
  Tabs,
  Button,
  Statistic,
  Descriptions,
  Col,
} from "antd";

import InputText from "app/components/input";
import {
  actionDynamicFormSave,
  actionDynamicFormGetById,
  actionSetPageMode,
} from "modules/template/store/action";
import { useDispatch, useSelector } from "react-redux";
import { PageMode } from "modules/template/store/constant";
import "./style.css";
import { dynamicFormData } from '../../../store/reducer';
const TemplatePage = (props) => {
  
  const{formKey,itemId} = props;
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
  const [dynamicFormData,setDynamicFormData] = useState(null);
  // Editor Value State
  const [editorValue, setEditorValue] = useState(createEmptyState);
  const dynamicFormDataStore = useSelector((state) => {
    return state.TemplateManagement.dynamicFormData;
  });

  useEffect(()=>{
    if(dynamicFormDataStore)
      setDynamicFormData(dynamicFormDataStore.get( props.match.params.id||0))
  },[dynamicFormDataStore])

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
    console.log("dynamicFormData",dynamicFormData);
    if (dynamicFormData) {
      setValue("name", dynamicFormData.name);
      setValue("path", dynamicFormData.path);
      setValue("tableName", dynamicFormData.tableName);
      setValue("description", dynamicFormData.description);
      setEditorValue(JSON.parse(dynamicFormData.content));
    }
  }, [dynamicFormData]);
  return (
    <div>
      <PageHeader
        className="page-white mb-10 site-page-header-responsive "
        onBack={() => window.history.back()}
        title="Tạo template"
        subTitle="automatic"
        extra={[
          <Button key="1" type="primary" onClick={handleSubmit(handleCreate)}>
            Lưu lại
          </Button>,
        ]}
      ></PageHeader>
      <form onSubmit={handleSubmit(handleCreate)}>
        <div className="page-white  mb-10" style={{ padding: 10 }}>
          <Row gutter={[16, 16]}>
            <Col span={12}>
              <Controller
                as={
                  <InputText
                    type="text"
                    name="name"
                    label="Tên mẫu"
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
            </Col>
            <Col span={12}>
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
            </Col>

            <Col span={12}>
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
            </Col>
            <Col span={12}>
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
            </Col>
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
  );
};

export default TemplatePage;
