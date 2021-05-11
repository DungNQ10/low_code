import React, { Component } from "react";
import { Checkbox } from "antd";
import "../style.scss";
const InputDatePicker = ({
  hasLabel = true,
  label,
  defaultValue = [],
  type = "text",
  value,
  options = [],
  required = false,
  onChange = () => {},
  ...props
}) => {
  return (
    <React.Fragment>
      <div className="base-component">
        <Checkbox
          onChange={(e) => {
            onChange(e);
          }}
          defaultValue={defaultValue}
        >
          {hasLabel && (
          <span className="label">
            {label} {required && <span className="required">*</span>}
          </span>
        )}
        </Checkbox>
      </div>
    </React.Fragment>
  );
};

export default InputDatePicker;
