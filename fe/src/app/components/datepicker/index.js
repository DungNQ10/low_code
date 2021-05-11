import React, { Component } from "react";
import { Input ,DatePicker } from "antd";
import "../style.scss";
const InputDatePicker = ({
  hasLabel = true,
  label,
  placeholder,
  type = "text",
  value,
  pickerType= "date" ,
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
           <DatePicker 
                value={value}
                onChange={(e) => {
                    onChange(e);
                }}
                {...props}
                placeholder={placeholder}
                picker={pickerType}
                style={{ width: "100%" }}/>
        
        </div>
    </React.Fragment>
  );
};

export default InputDatePicker;
