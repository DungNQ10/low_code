import React, { useState, useRef, useEffect } from "react";
import "../../Common/style.scss";
import { withLocalize } from "react-localize-redux";
import { Row, Checkbox, Select, Divider, Col, Input, Button } from "antd";
import * as yup from "yup";
import { useForm, Controller } from "react-hook-form";
import InputText from "../../../../../app/components/input";
import { useSelector, useDispatch } from "react-redux";
import { yupResolver } from "@hookform/resolvers/yup";
import { useParams } from "react-router-dom";
import DispatchConfig from '../../Common/dispatchConfig';
import SubscribeConfig from "../../Common/subscribeConfig";
const { Option } = Select;

const InputComponentConfig = React.memo((props) => {
  const { onChange, readOnly } = props;
  const dispatch = useDispatch();
  const formData = useSelector((state) => {
    return state.TemplateManagement.formData;
  });
  let { id } = useParams();
  const oryStore = useSelector((state) => state.TemplateManagement.oryStore);
  console.log(oryStore);

  let item = props.data || {};
  const getValidateSetting = function () {
    if (readOnly) {
      return yup.object().shape();
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
    resolver: yupResolver(getValidateSetting()),
  });
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
  useEffect(() => {
    item.name = item.name || "";
    setFormValue();
    onChange(item);
    console.log("useParams", useParams);
  }, []);

  useEffect(() => {
    console.log("formData", formData);
  }, [formData]);

  return (
    <React.Fragment>
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
      <DispatchConfig
         onChangePluginValue={onChange}
         pluginValues={props.data}
      >

      </DispatchConfig>
      <SubscribeConfig
        onChangePluginValue={onChange}
        pluginValues={props.data}
      >

      </SubscribeConfig>
    </React.Fragment>
  );
});

export default withLocalize(InputComponentConfig);
