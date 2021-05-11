import React, { memo } from "react";
import { Slider } from "antd";
import "../style.scss";

const SliderField = memo(props => {

  let {
    hasLabel = true,
    label,
    defaultValue,
    disabled = false,
    required = false,
    onChange = () => { }, ...rest } = props;

  return (
    <div className="base-component">
      {hasLabel && (
        <div className="label">
          {label} {required && <span className="required">*</span>}
        </div>
      )}
      <Slider
        {...rest}
        defaultValue={defaultValue}
        disabled={disabled}
        onChange={(e) => {
          onChange(e);
        }}
      />

    </div>

  );
});

export default SliderField;
