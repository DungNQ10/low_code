import React, { memo } from "react";
import { TimePicker } from "antd";
import moment from 'moment';
import "../style.scss";

const TimePickerField = memo(props => {

  let {
    hasLabel = true,
    label,
    defaultValue = moment('00:00:00', 'HH:mm:ss'),
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
      <TimePicker
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

export default TimePickerField;
