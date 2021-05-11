import React, { Component } from "react";
import { InputNumber } from "antd";
import "../style.scss";
const CInputNumber = ({
  hasLabel = true,
  label,
  placeholder,
  min = 0,
  max = 100000,
  step = 1,
  value,
  required = false,
  onChange = () => {},
  ...props
}) => {
  return (
    <React.Fragment>
      <div className="base-component">
        {hasLabel && (
          <div className="label">
            {label} {required && <span className="required">*</span>}
          </div>
        )}

        <InputNumber
          //value={value}
          min={min} 
          max={max}
          step={step}
          onChange={(e) => {
            onChange(e);
          }}
          {...props}
          style={{ width: "100%" }}></InputNumber>
      </div>      
     
    </React.Fragment>
  );
};

export default CInputNumber;
