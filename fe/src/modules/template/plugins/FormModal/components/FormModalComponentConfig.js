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
const { Option } = Select;

const FormModalComponentConfig = React.memo((props) => {
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
        templateId: yup
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
    setValue("templateId", item.templateId || "");
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
            name="templateId"
            control={control}
            render={({ onChange, onBlur, name, value }) => (
              <InputText
                type="text"
                name={name}
                label={"Template Id"}
                value={value}
                onBlur={(e) => {
                  onBlur();
                  trigger(name);
                  onChangeFormData(name, e.target.value);
                }}
                onChange={(e) => setValue(name, e.target.value)}
              />
            )}
          />
          {errors && errors.templateId && errors.templateId.message && (
            <div className="inline-error" style={{ paddingTop: 10 }}>
              {errors.templateId.message}
            </div>
          )}
        </Row>

        

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
    </React.Fragment>
  );
});

export default withLocalize(FormModalComponentConfig);
