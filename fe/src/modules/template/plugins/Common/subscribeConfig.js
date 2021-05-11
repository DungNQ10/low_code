import { Row } from "antd";
import React, { useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import InputText from "../../../../app/components/input";
import CheckBox from "../CheckBox";
import { DispatchEvent } from "../../constants/index";
const SubscribeConfig = ({ onChangePluginValue, pluginValues, ...props }) => {
  const { getValues, setValue, trigger, control, errors } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
  });

  let item = pluginValues || {};
  const onChangeFormData = () => {
    let values = getValues();
    onChangePluginValue({ ...pluginValues, ...values });
  };
  const setFormValue = () => {
    setValue('subscribeEvent', item['subscribeEvent']);
  };

  useEffect(() => {
    setFormValue();
  }, []);
  return (
    <React.Fragment>
      <Row>
        <Controller
          name={'subscribeEvent'}
          control={control}
          render={({ onChange, onBlur, name, value }) => (
            <InputText
              type="text"
              name={name}
              label={"Subscribe event"}
              value={value}
              onBlur={(e) => {
                onBlur();
                trigger(name);
                onChangeFormData(name, e.target.value);
              }}
              onChange={(e) => onChange(e.target.value)}
            />
          )}
        />
        {errors && errors.name && errors.name.message && (
          <div className="inline-error" style={{ paddingTop: 10 }}>
            {errors.name.message}
          </div>
        )}
      </Row>
      
    </React.Fragment>
  );
};

export default SubscribeConfig;
