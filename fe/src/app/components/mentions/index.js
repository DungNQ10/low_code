import React, { memo } from "react";
import { Mentions } from "antd";
import "../style.scss";
const { Option } = Mentions;


const MentionField = memo(props => {

  let {
    hasLabel = true,
    label,
    placeholder,
    defaultValue,
    options = [],
    width = 120,
    disabled = false,
    required = false,
    rows = "1",
    prefix = ['@', '#'],
    onChange = () => { }, ...rest } = props;

  return (
    <div className="base-component">
      {hasLabel && (
        <div className="label">
          {label} {required && <span className="required">*</span>}
        </div>
      )}
      <Mentions
        {...rest}
        defaultValue={defaultValue}
        disabled={disabled}
        placeholder={placeholder}
        prefix={prefix}
        rows={rows}
        style={{ width: "100%" }}
        onChange={(e) => {
          onChange(e);
        }}

      >
        {
          options.map(i =>
            <Option key={i} value={i}>{i}</Option>
          )
        }
      </Mentions>
    </div>

  );
});

export default MentionField;
