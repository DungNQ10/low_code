import React, { Component } from "react";
import { Input, Col, Form } from "antd";
import "../style.scss";
import TextArea from "antd/lib/input/TextArea";
const InputText = ({
  hasLabel = true,
  label,
  placeholder,
  type = "text",
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
        {type == "textarea" ? (
          <TextArea
            
            placeholder={placeholder}
            value={value}
            onChange={(e) => {
              onChange(e);
            }}
            
            {...props}

          ></TextArea>
        ) : (
          <Input
            type={type}
            placeholder={placeholder}
            value={value}
            onChange={(e) => {
              onChange(e);
            }}
            {...props}
          ></Input>
        )}
      </div>
    </React.Fragment>
  );
};

export default InputText;
