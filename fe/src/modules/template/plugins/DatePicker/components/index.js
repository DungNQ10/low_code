import React, { useState, useRef, useEffect } from "react";
import "../../Common/style.scss";
import PluginTooltip from "../../Common/pluginTooltip";
import DatePicker from "../../../../../app/components/datepicker";
import { withLocalize } from "react-localize-redux";
import { Row, Checkbox, Select, Divider, Col, Input, Button } from "antd";
import * as yup from "yup";
import { useForm, Controller } from "react-hook-form";
import InputText from "../../../../../app/components/input";
import EditModel from "../../Common/editModel";
import CheckboxInput from "../../../../../app/components/checkbox";
import { useSelector } from "react-redux";
import { PageMode } from "../../../store/constant";
import { yupResolver } from '@hookform/resolvers/yup';
const { Option } = Select;

const DatePickerPlugin = React.memo((props) => {
  const {
    state,
    readOnly,
    onChange,
    translate,
    isEditMode,
    isInsertMode,
    isPreviewMode,
    isResizeMode,
    isLayoutMode,
  } = props;

  const oryStore = useSelector((state) => state.TemplateManagement.oryStore);
  console.log(oryStore);
  let showTitle = isEditMode || isInsertMode || isResizeMode || isLayoutMode;
  const [openModel, setOpenModel] = useState(false);
  const handleForm =
    oryStore.pageMode == PageMode.FunctionPage ||
    oryStore.pageMode == PageMode.PreviewPage;
  let item = state || {};
  const getValidateSetting = function () {
    if (handleForm) {
      let fieldValue = yup.string();
      if(item.required){
        fieldValue = fieldValue.required("Yêu cầu nhập.");
      }
      return yup.object().shape({
        fieldValue:fieldValue
      });
    } else {
      let schema = yup.object().shape({
        name: yup
          .string()
          .required("Yêu cầu nhập")
          .max(256, "Độ dài không vượt quá 256"),
       
      });
      return schema;
    }
  };

  const { errors, control, getValues, setValue, trigger } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
    resolver: yupResolver( getValidateSetting()),
  });

  const refContainer = useRef(null);
  const handleClickOutside = function (event) {
    const { target } = event;
    if (
      target == refContainer.current ||
      refContainer.current.contains(target)
    ) {
      setOpenModel(true);
    } else {
      setOpenModel(false);
    }
  };

  const onChangeFormData = (name, value) => {
    setValue(name, value);
    trigger(name);
    let values = getValues() || {};
    let data = { ...item, ...values };
    onChange(data);
  };

  const setFormValue = () => {
    setValue("name", item.name || "Label");
    setValue("required", item.required || false);
    setValue("mappingField", item.mappingField || "");
  };

  const getSubmitData = () => {
      let value = getValues().fieldValue||'';
      if(value){
        value = new Date(value);
      }
    return {
      name: "DateField",
      field: state.mappingField || "",
      fieldValue: value,
    };
  };
  const validateSubmitEvent = () => {
    trigger();
  };

  useEffect(() => {
    item.name = item.name||'Label';
    item.getSubmitData = getSubmitData;
    item.validateSubmitEvent = validateSubmitEvent;
    setFormValue();
    onChange(item);
  }, []);

  useEffect(() => {
    if (!isEditMode && openModel) {
      setOpenModel(false);
    }
    if (openModel) {
      setFormValue();
    }
  }, [openModel, isEditMode]);

  if (!isPreviewMode) {
    return (
      <form>
        <React.Fragment>
          <PluginTooltip show={showTitle} title={"Input text"}>
            <div ref={refContainer}>
              <div
                style={{ padding: "8px 0" }}
                className={`${openModel ? "active-component" : ""}`}
              >
                <DatePicker
                  type="text"
                  label={item.name}
                  required={item.required}
                />
              </div>
            </div>
          </PluginTooltip>

          <EditModel
            onClickOutside={handleClickOutside}
            open={openModel && !readOnly}
            onClose={() => setOpenModel(false)}
          >
            <div style={{ width: "100%" }}>
              <Row>
                <Controller
                  name="name"
                  control={control}
                  render={({ onChange, onBlur, name, value }) => (
                    <InputText
                      type="text"
                      name={name}
                      label={"Label"}
                      value={value}
                      onBlur={(e) => {
                        onBlur();
                        trigger("name");
                        onChangeFormData(name, e.target.value);
                      }}
                      onChange={(e) => setValue(name, e.target.value)}
                    />
                  )}
                />
                {errors && errors.name && errors.name.message && (
                  <div className="inline-error" style={{ paddingTop: 10 }}>
                    {errors.name.message}
                  </div>
                )}
              </Row>

              <Row style={{ marginTop: 16 }}>
                <Controller
                  name="required"
                  render={({ onChange, onBlur, name, value }) => (
                    <Checkbox
                      className="template-checkbox"
                      name="redirect"
                      style={{ fontSize: "16px", color: "#000000" }}
                      onChange={(e) => {
                        onChange(e.target.checked);
                        onChangeFormData(name, e.target.checked);
                      }}
                      checked={value}
                    >
                      {"required"}
                    </Checkbox>
                  )}
                  control={control}
                />
              </Row>
              <Divider />

              <Row style={{ marginTop: 16 }}>
                <Controller
                  name="mappingField"
                  control={control}
                  render={({ onChange, onBlur, value, name }) => (
                    <InputText
                      type="text"
                      classname_input="input-1"
                      label={"Mapping Field"}
                      value={value}
                      onBlur={(e) => {
                        trigger("mappingField");
                        onChangeFormData(name, e.target.value);
                      }}
                      onChange={(e) => {
                        onChange(e.target.value);
                      }}
                      name={name}
                    />
                  )}
                />
              </Row>
            </div>
          </EditModel>
        </React.Fragment>
      </form>
    );
  }
  return (
    <PluginTooltip show={showTitle} title={"Check box"}>
      <div style={{ padding: "10px 0" }}>
        <Controller
          name="fieldValue"
          render={({ onChange, onBlur, name, value }) => (
            <DatePicker
              type="text"
              label={item.name}
              value={value}
              required={item.required}
              onChange={(e) => {
                
                onChange(e);
                trigger(name);
              }}
              onBlur={(e) => {
                trigger(name);
              }}
              name={name}
            />
          )}
          control={control}
        />
        {errors && errors.fieldValue && errors.fieldValue.message && (
          <div className="inline-error" style={{ paddingTop: 10 }}>
            {errors.fieldValue.message}
          </div>
        )}
      </div>
    </PluginTooltip>
  );
});

export default withLocalize(DatePickerPlugin);
