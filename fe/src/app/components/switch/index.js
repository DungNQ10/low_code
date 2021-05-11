import React, { Component } from "react";
import { Switch } from "antd";
import "../style.scss";
const SwitchField = ({
  hasLabel = true,
  label,
  defaultValue = false,
  type = "text",
  value,
  options = [],
  required = false,
  onChange = () => { },
  ...props
}) => {
  return (
    <React.Fragment>
      <div className="base-component">
        <Switch
          onChange={(e) => {
            onChange(e);
          }}
          defaultChecked={defaultValue}
        >
          {hasLabel && (
            <span className="label">
              {label} {required && <span className="required">*</span>}
            </span>
          )}
        </Switch>
      </div>
    </React.Fragment>
  );
};

export default SwitchField;
