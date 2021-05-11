import React, { Component } from "react";
import JSONInput from "react-json-editor-ajrm/index";
import locale from "react-json-editor-ajrm/locale/en";
const InputJson = ({
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
        <JSONInput
          placeholder={value ? JSON.parse(value) : null} // data to display
          theme="dark_vscode_tribute"
          locale={locale}
          height="550px"
          width="1000px"
          onChange={(e) => {
            if (e.error && e.error.reason) {
              return;
            }
            onChange(JSON.stringify(e.jsObject));
          }}
        />
      </div>
    </React.Fragment>
  );
};

export default InputJson;
