import React, { useState, useRef, useEffect } from 'react';
import '../../Common/style.scss';
import { withLocalize } from 'react-localize-redux';
import {
  Row,
  Checkbox,
  Select,
  Divider,
  Col,
  Input,
  Button,
  Collapse,
} from 'antd';
import * as yup from 'yup';
import { useForm, Controller, useFieldArray } from 'react-hook-form';
import InputText from '../../../../../app/components/input';
import { useSelector, useDispatch } from 'react-redux';
import { yupResolver } from '@hookform/resolvers/yup';
import { useParams } from 'react-router-dom';
import DispatchConfig from '../../Common/dispatchConfig';
import SubscribeConfig from '../../Common/subscribeConfig';
import ApiConfig from '../../Common/apiConfig';
import { SetValues } from '../../../../../app/common/util';
const { Panel } = Collapse;
const { Option } = Select;

const GridComponentConfig = React.memo((props) => {
  const { onChange, readOnly } = props;
  const dispatch = useDispatch();
  const formData = useSelector((state) => {
    return state.TemplateManagement.formData;
  });
  let { id } = useParams();
  const oryStore = useSelector((state) => state.TemplateManagement.oryStore);
  console.log(oryStore);

  let item = props.data || {};
  const getValidateSetting = function () {
    if (readOnly) {
      return yup.object().shape();
    } else {
      let schema = yup.object().shape({
        name: yup
          .string()
          .required('Yêu cầu nhập')
          .max(256, 'Độ dài không vượt quá 256'),
      });
      return schema;
    }
  };

  const { errors, control, getValues, setValue, trigger } = useForm({
    validateCriteriaMode: 'all',
    mode: 'onChange',
    resolver: yupResolver(getValidateSetting()),
  });

  const { fields, append, prepend, remove, swap, move, insert } = useFieldArray(
    {
      control, // control props comes from useForm (optional: if you are using FormContext)
      name: 'columns', // unique name for your Field Array
      // keyName: "id", default to "id", you can change the key name
    }
  );
  const onChangeFormData = (name, value) => {
    setValue(name, value);
    trigger(name);
    let values = getValues() || {};
    let data = { ...item, ...values };
    onChange(data);
  };

  const onChangeColumnsData = () => {
    let values = getValues() || {};
    let data = { ...item, ...values };
    onChange(data);
  };

  const setFormValue = () => {
    SetValues({
      rowKey:item.rowKey||'id',
      mappingField: item.mappingField || '',
      columns: item.columns || [],
      hasSelect:item.hasSelect
    },setValue)
    // setValue('rowKey', item.rowKey||'id');
    // setValue('mappingField', item.mappingField || '');
    // setValue('columns', item.columns || []);
    // setValue('hasSelect',item.hasSelect);
  };
  useEffect(() => {
    item.name = item.name || '';
    setFormValue();
    onChange(item);
    console.log('useParams', useParams);
  }, []);

  useEffect(() => {
    console.log('formData', formData);
  }, [formData]);

  return (
    <React.Fragment>
      <div style={{ width: '100%' }}>
        <Row>
          <Controller
            name='rowKey'
            control={control}
            render={({ onChange, onBlur, name, value }) => (
              <InputText
                type='text'
                name={name}
                label={'RowKey'}
                value={value}
                onBlur={(e) => {
                  onBlur();
                  trigger(name);
                  onChangeFormData(name, e.target.value);
                }}
                onChange={(e) => onChange(e.target.value)}
              />
            )}
          />
          {errors && errors.name && errors.name.message && (
            <div className='inline-error' style={{ paddingTop: 10 }}>
              {errors.name.message}
            </div>
          )}
        </Row>

        
        <Row style={{ marginTop: 16 }}>
          <Controller
            name='hasSelect'
            render={({ onChange, onBlur, name, value }) => (
              <Checkbox
                className='template-checkbox'
                name='redirect'
                style={{ fontSize: '16px', color: '#000000' }}
                onChange={(e) => {
                  onChange(e.target.checked);
                  onChangeFormData(name, e.target.checked);
                }}
                checked={value}
              >
                {'Has select'}
              </Checkbox>
            )}
            control={control}
          />
        </Row>
        <Row style={{ marginTop: 16 }}>
          <Controller
            name='hasDelete'
            render={({ onChange, onBlur, name, value }) => (
              <Checkbox
                className='template-checkbox'
                name='redirect'
                style={{ fontSize: '16px', color: '#000000' }}
                onChange={(e) => {
                  onChange(e.target.checked);
                  onChangeFormData(name, e.target.checked);
                }}
                checked={value}
              >
                {'Has delete action'}
              </Checkbox>
            )}
            control={control}
          />
        </Row>
        <Collapse defaultActiveKey={['1']} accordion>
          <Panel header='Delete api' key='1'>
            <ApiConfig
              onChangePluginValue={onChange}
              pluginValues={item}
              apiName='deleteApi'
            ></ApiConfig>
          </Panel>
          <Panel header='List api' key='2'>
          <ApiConfig
              onChangePluginValue={onChange}
              pluginValues={item}
              apiName='listApi'
            ></ApiConfig>
          </Panel>
          <Panel header='This is panel header 3' key='3'></Panel>
        </Collapse>

        <Divider />
        <Row>
          <Col span={16}>
            <h3>Thông tin columns </h3>
          </Col>
          <Col span={8}></Col>
        </Row>
        <div className='ant-table'>
          <div className='ant-table-container'>
            <div className='ant-table-content'>
              <table>
                <thead className='ant-table-thead'>
                  <tr>
                    <th className='ant-table-cell'>Field</th>
                    <th className='ant-table-cell'>Label</th>
                    <th className='ant-table-cell'>Type</th>
                    <th className='ant-table-cell'>Width</th>
                    <th style={{ width: 100, textAlign: 'center' }}>
                      <Button
                        onClick={() => {
                          append({});
                        }}
                      >
                        add
                      </Button>
                    </th>
                  </tr>
                </thead>
                <tbody className='ant-table-tbody'>
                  {fields.map((item, index) => {
                    return (
                      <tr key={`item_${item.id}`}>
                        <td>
                          <Controller
                            name={`columns.${index}.field`}
                            control={control}
                            defaultValue={item.field}
                            render={({ onChange, onBlur, value, name }) => (
                              <InputText
                                type='text'
                                label={''}
                                value={value}
                                onBlur={(e) => {
                                  onChangeColumnsData();
                                }}
                                onChange={(e) => {
                                  console.log(name);
                                  onChange(e.target.value);
                                }}
                                name={name}
                              />
                            )}
                          />
                        </td>
                        <td>
                          <Controller
                            name={`columns[${index}].label`}
                            control={control}
                            defaultValue={item.label}
                            render={({ onChange, onBlur, value, name }) => (
                              <InputText
                                type='text'
                                label={''}
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
                            name={`columns[${index}].type`}
                            control={control}
                            render={({ onChange, onBlur, value, name }) => (
                              <InputText
                                type='text'
                                label={''}
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
                            name={`columns[${index}].width`}
                            control={control}
                            render={({ onChange, onBlur, value, name }) => (
                              <InputText
                                type='text'
                                label={''}
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
      </div>
      <SubscribeConfig
        onChangePluginValue={onChange}
        pluginValues={props.data}
      ></SubscribeConfig>
    </React.Fragment>
  );
});

export default withLocalize(GridComponentConfig);
