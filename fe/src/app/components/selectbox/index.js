import React, { Component } from "react";
import { Col, Select, Form } from "antd";
import "../style.scss";
const { Option } = Select;

const SelectBox = ({
  hasLabel = true,
  label,
  placeholder,
  defaultValue,
  options = [] ,
  width = 120,
  disabled = false,
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

        <Select
          options={options}
          placeholder={placeholder}
          defaultValue={defaultValue}
          disabled={disabled}
          {...props}
          style={{ width: "100%" }}
          onChange={(e) => {
            onChange(e);
          }}
        ></Select>
      </div>
      {/* <Col span={24}>
          <Form.Item
                  label="Type"
                  
                >
                  <Select 
                    placeholder={placeholder}
                    options={options}
                    placeholder={placeholder}
                    defaultValue={defaultValue}
                    disabled={disabled}
                    {...props}
                    //style={{ width: '100%' }}
                    onChange={(e) => {
                      onChange(e);
                    }}
                    >
                  
                  </Select>
                </Form.Item>
                </Col>  */}
    </React.Fragment>
  );
};

export default SelectBox;
