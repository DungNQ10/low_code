import React, { memo } from "react";
import { Transfer } from "antd";
import moment from 'moment';
import "../style.scss";
// {
//   key: i.toString(),
//   title: `content${i + 1}`,
//   description: `description of content${i + 1}`,
//   disabled: i % 3 < 1,
// }
const TransferField = memo(props => {
  function filterOption(inputValue, option) {
    return option.description.indexOf(inputValue) > -1;
  }
  let {
    hasLabel = true,
    label,
    defaultValue = [],
    disabled = false,
    required = false,
    options = [],
    showSearch = true,
    onChange = () => { },
    handleSearch = () => { },
    ...rest } = props;

  return (
    <div className="base-component">
      {hasLabel && (
        <div className="label">
          {label} {required && <span className="required">*</span>}
        </div>
      )}
      <Transfer
        {...rest}
        showSearch={showSearch}
        dataSource={options}
        filterOption={filterOption}
        targetKeys={defaultValue}
        onChange={onChange}
        onSearch={handleSearch}
        render={item => item.title}
      />
    </div>

  );
});

export default TransferField;
