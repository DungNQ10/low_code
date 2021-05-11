import React, { useState, useRef, useEffect } from "react";
import "../../Common/style.scss";
import PluginTooltip from "../../Common/pluginTooltip";
import { withLocalize } from "react-localize-redux";
import { Row, Checkbox, Divider } from "antd";
import * as yup from "yup";
import { useForm, Controller } from "react-hook-form";
import TransferField from "../../../../../app/components/transfer";
import EditModel from "../../Common/editModel";
import { useSelector } from 'react-redux';
import { PageMode } from '../../../store/constant';
import { yupResolver } from "@hookform/resolvers/yup";
import InputText from '../../../../../app/components/input';


const SlidePlugin = React.memo((props) => {
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

  const oryStore = useSelector(state => state.TemplateManagement.oryStore);
  let showTitle = isEditMode || isInsertMode || isResizeMode || isLayoutMode;
  const [openModel, setOpenModel] = useState(false);
  const handleForm =
    (oryStore.pageMode == PageMode.FunctionPage ||
      oryStore.pageMode == PageMode.PreviewPage
    );
  let item = state || {};
  const getValidateSetting = function () {
    if (handleForm) {
      return yup.object().shape({});
    }
    else {
      let schema = yup.object().shape({
        name: yup
          .string()
          .required("Yêu cầu nhập")
          .max(256, "Độ dài không vượt quá 256")
      });
      return schema;
    }

  };

  const { errors, control, getValues, setValue, trigger } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
    resolver: yupResolver(getValidateSetting()),
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
    return {
      name: 'transfer',
      field: state.mappingField || '',
      fieldValue: getValues().fieldValue || false
    }
  };
  const validateSubmitEvent = () => {
    trigger();
  };

  useEffect(() => {
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
          <PluginTooltip show={showTitle} title={"Transfer"}>
            <div ref={refContainer}>
              <div
                style={{ padding: "8px 0" }}
                className={`${openModel ? "active-component" : ""}`}
              >
                <TransferField
                  label={item.name}
                  required={item.required}
                  placeholder={item.placeholder}
                  options={item.options}
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
    <PluginTooltip show={showTitle} title={"Transfer"}>
      <div style={{ padding: "10px 0" }}>
        <Controller
          name="fieldValue"
          render={({ onChange, onBlur, name, value }) => (
            <TransferField
              label={item.name}
              required={item.required}
              placeholder={item.placeholder}
              options={item.options}
              checked={value || false}
              onChange={(e) => {
                onChange(e.target.checked);
                onChangeFormData(name, e.target.checked);
              }}
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

export default withLocalize(SlidePlugin);
