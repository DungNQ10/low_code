import { Row, Col, Button } from "antd";
import React, { useEffect, useState } from "react";
import { Controller, useForm, useFieldArray } from "react-hook-form";
import InputText from "../../../../app/components/input";
import CheckBox from "../CheckBox";
import { DispatchEvent, API_METHODS } from "../../constants/index";
import SelectBox from "../../../../app/components/selectbox";
const FormApiConfig = ({ apiName, onChangeFormApi, data, ...props }) => {
  const { getValues, setValue, trigger, control, errors } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
  });
  const { fields, append, prepend, remove, swap, move, insert } = useFieldArray(
    {
      control,
      name: "parameters",
    }
  );

  const methods = [
    {
      value: API_METHODS.Get,
      label: API_METHODS.Get,
    },
    {
      value: API_METHODS.Post,
      label: API_METHODS.Post,
    },
    {
      value: API_METHODS.Delete,
      label: API_METHODS.Delete,
    },
  ];

  let item = data || {};

  const setFormValue = () => {
    debugger;
    let apiValues = item[apiName] || {};
    apiValues.parameters = apiValues.parameters || [];
    console.log([...apiValues.parameters]);
    setValue("parameters", [...apiValues.parameters]);
    setValue("method", apiValues.method);
    setValue("apiUrl", apiValues.apiUrl);
  };
  const onChangeColumnsData = () => {
    let values = getValues() || {};
    onChangeFormApi(apiName, { ...values });
  };
  useEffect(() => {
    setFormValue();
  }, [data]);
  return (
    <React.Fragment>
      <Row>
        <Col span={8}>
          <Controller
            name="method"
            control={control}
            render={({ onChange, onBlur, name, value }) => (
              <SelectBox
                name={name}
                label={"Method"}
                options={methods}
                value={value}
                onChange={(selectedValue) => {
                  onChange(selectedValue);
                  onChangeColumnsData();
                }}
              />
            )}
          />
          {errors && errors.method && errors.method.message && (
            <div className="inline-error" style={{ paddingTop: 10 }}>
              {errors.method.message}
            </div>
          )}
        </Col>
        <Col span={16}>
          <Controller
            name={"apiUrl"}
            control={control}
            render={({ onChange, onBlur, name, value }) => (
              <InputText
                type="text"
                name={name}
                label={"API Url"}
                value={value}
                onBlur={(e) => {
                  onBlur();
                  trigger(name);
                  onChangeColumnsData();
                }}
                onChange={(e) => onChange(e.target.value)}
              />
            )}
          />
          {errors && errors.apiUrl && errors.apiUrl.message && (
            <div className="inline-error" style={{ paddingTop: 10 }}>
              {errors.apiUrl.message}
            </div>
          )}
        </Col>
      </Row>
      <div className="ant-table">
        <div className="ant-table-container">
          <div className="ant-table-content">
            <table>
              <thead className="ant-table-thead">
                <tr>
                  <th className="ant-table-cell">Name</th>
                  <th className="ant-table-cell">Mapping</th>
                  <th className="ant-table-cell">Value</th>
                  <th style={{ width: 100, textAlign: "center" }}>
                    <Button onClick={() => append({})}>Add</Button>
                  </th>
                </tr>
              </thead>
              <tbody className="ant-table-tbody">
                {fields.map((item, index) => {
                  return (
                    <tr key={`item_${item.id}`}>
                      <td>
                        <Controller
                          name={`parameters.${index}.name`}
                          control={control}
                          defaultValue={item.name}
                          render={({ onChange, onBlur, value, name }) => (
                            <InputText
                              type="text"
                              label={""}
                              value={value}
                              onBlur={(e) => {
                                onChangeColumnsData();
                              }}
                              onChange={(e) => {
                                onChange(e.target.value);
                              }}
                              name={name}
                            />
                          )}
                        />
                      </td>
                      <td>
                        <Controller
                          name={`parameters[${index}].mapping`}
                          control={control}
                          defaultValue={item.mapping}
                          render={({ onChange, onBlur, value, name }) => (
                            <InputText
                              type="text"
                              label={""}
                              value={value}
                              onBlur={(e) => {
                                onChangeColumnsData();
                              }}
                              onChange={(e) => {
                                onChange(e.target.value);
                              }}
                              name={name}
                            />
                          )}
                        />
                      </td>
                      <td>
                        <Controller
                          name={`parameters[${index}].value`}
                          control={control}
                          defaultValue={item.value}
                          render={({ onChange, onBlur, value, name }) => (
                            <InputText
                              type="text"
                              label={""}
                              value={value}
                              onBlur={(e) => {
                                onChangeColumnsData();
                              }}
                              onChange={(e) => {
                                onChange(e.target.value);
                              }}
                              name={name}
                            />
                          )}
                        />
                      </td>

                      <td>
                        <Button onClick={() => remove(index)}>X</Button>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </React.Fragment>
  );
};

export default FormApiConfig;
