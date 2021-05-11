import React, { memo } from "react";
import { Rate } from "antd";
import "../style.scss";

const RateField = memo(props => {

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
      <Rate
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

export default RateField;
