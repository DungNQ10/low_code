import React from "react";
import { Radio  } from "antd";
import "../style.scss";
const InputDatePicker = ({
  hasLabel = true,
  label,
  defaultValue ,
  type = "text",
  value,
  options = [] ,
  name='radio' ,
  optionType = 'default',
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
           <Radio.Group 
                options={options} 
                onChange={(e) => {
                    onChange(e);
                  }}
                defaultValue={defaultValue} 
                optionType={optionType}
                name={name}/>
        
        </div>
    </React.Fragment>
  );
};

export default InputDatePicker;
