import React, { useState, useRef,useEffect } from 'react';
import '../../Common/style.scss';
import PluginTooltip from '../../Common/pluginTooltip';
import DatePicker from '../../../../../app/components/datepicker';
import { withLocalize } from 'react-localize-redux';
import { Row, Checkbox, Select , Divider , Col , Input , Button } from 'antd';
import * as yup from 'yup';
import { useForm, Controller } from 'react-hook-form';
import InputText from '../../../../../app/components/input';
import EditModel from '../../Common/editModel';
import SelectBox from '../../../../../app/components/selectbox';
import { PlusOutlined, DeleteOutlined } from '@ant-design/icons';
import RadioInput from '../../../../../app/components/radio';


const { Option } = Select;

const RadioPlugin = React.memo((props) => {
    const {
        state,
        readOnly,
        onChange,
        translate,
        isEditMode,
        isInsertMode,
        isPreviewMode,
        isResizeMode,
        isLayoutMode } = props;

    const { oryStore } = {};//useSelector(state => state.TemplateManagement.oryStore);
    let showTitle = isEditMode || isInsertMode || isResizeMode || isLayoutMode;

    const [openModel, setOpenModel] = useState(false);

   

    let item = state || {};
     //set default value
     if (JSON.stringify(state) == JSON.stringify({})) {
        item.name = 'Label';
        item.valid = true;
        item.errors = {};
        item.options = [{ label: 'Radio', value: 'Radio' }];
    }

    const getValidateSetting = function () {
        let schema = yup.object().shape({
            name: yup.string()
                .required(translate('commonMessage.C_MSG_12'))
                .max(256, translate('commonMessage.C_MSG_15', { number: 256 })),
            hyperlink: yup.string()
                .required(translate('commonMessage.C_MSG_12'))
                .max(2048, translate('commonMessage.C_MSG_15', { number: 2048 }))
                .url(translate('linkConnection.urlInvalid'))
        });
        return schema;
    };

    const { errors, control, getValues, setValue, triggerValidation } = useForm({
        validateCriteriaMode: 'all',
        mode: 'onChange',
        validationSchema: getValidateSetting()
    });

    const refContainer = useRef(null);
    const handleClickOutside = function (event) {
        const { target } = event;
        if (target == refContainer.current || refContainer.current.contains(target)) {
            setOpenModel(true);
        }
        else {

            if (item.errors && item.errors.url) {

                onChange({ url: '', errors: {} });
            }
            setOpenModel(false);

        }

    };

    const onChangeFormData = (name, value) => {
        setValue(name, value);
        triggerValidation(name);
        let values = getValues() || {};
        onChange({ ...item, ...values });
    };

    const setFormValue = () => {
        setValue('name', item.name || 'Label');
        setValue('newtab', item.newtab);
        setValue('hyperlink', item.hyperlink || false);
        setValue('redirect', item.redirect || false);
        setValue('fontsize', item.fontsize || 16);
        setValue('width', item.width || 100);
        setValue('height', item.height || 46);
        setValue('placeholder', item.placeholder || "Select data");
        setValue('pickerType', item.type || "date");        

    };

    const addItemOptions = ()=> {
        let options = [...item.options];
        options.push({value: "", label : ""});
        item.options = options;
        onChange({ ...item});
    }

    const removeItemOptions = (optionItem, index) => {
        let options = [...item.options];
        options.splice(index, 1);
        item.options = options;
        onChange({ ...item});
    }

    const onChangeItemOption = (value, index , name) => {
       let options = [...item.options];
       let optionItem = options[index];
       optionItem[name] = value;
       item.options = options;
       onChange({ ...item});
    }


    useEffect(() => {
        setFormValue();
        onChange(item);
    }, []);

    useEffect(() => {
        if (!isEditMode && openModel) {
            setOpenModel(false);
        }
    });


    if (!isPreviewMode) {

        return (
            <form>
            <React.Fragment>
                <PluginTooltip
                    show={showTitle}
                    title={'Check box'}
                >
                    <div ref={refContainer} >
                        <div style={{ padding: '8px 0' }} className={`${openModel ? 'active-component' : ''}`} >
                        
                 <RadioInput
                    label={item.name}
                    required={item.required}
                    placeholder = {item.placeholder}
                    options ={item.options}
                ></RadioInput>
            </div>
                            
                        
                    </div>
                </PluginTooltip>

                <EditModel onClickOutside={handleClickOutside} open={openModel && (!readOnly)} onClose={() => setOpenModel(false)}>

                    <div style={{ width: '100%' }}>

                        <Row>
                            <Controller
                                as={
                                    <InputText type="text"
                                        name="name"
                                        classname_input="input-1"
                                        label={'Label'}
                                    />
                                  
                                }
                                name="name"
                                control={control}
                                onBlur={() => {
                                    triggerValidation('name');
                                }}
                                onChange={([e]) => {
                                    onChangeFormData('name', e.target.value);
                                    return e.target.value;
                                }}
                            />
                            {(errors && errors.name && errors.name.message && <div className="inline-error" style={{ paddingTop: 10 }}>{errors.name.message}</div>)}
                        </Row>
                      
                        
                        <Row style={{ marginTop: 16 }}>
                            <Controller
                                as={
                                    <Checkbox
                                        className="template-checkbox"
                                        name="redirect"
                                        style={{ fontSize: '16px', color: '#000000' }}

                                    >
                                        {'required'}
                                    </Checkbox>
                                }
                                name="required"
                                control={control}
                                onChange={([e]) => {
                                   console.log(e);
                                    onChangeFormData('required', e.target.checked);
                                    return e.target.checked;
                                }}
                            />

                        </Row>                        
                        <Divider />
                        {/* <Row>
                            <h5>Mapping field config</h5>
                        </Row>   */}
                        <Row style={{ marginTop: 16 }}>
                            <Controller
                                as={
                                    <SelectBox  
                                        name="scourceFrom"
                                        classname_input="input-1"
                                        label={'Scource From'}
                                        options = {
                                            [
                                                {value : '1' , label : 'From table'},
                                                {value : '2' , label : 'From api'},
                                                {value : '3' , label : 'Define'}
                                            ]
                                        }
                                        //defaultValue={'date'}
                                        />                                    
                                }
                                name="scourceFrom"
                                control={control}
                                onChange={([e]) => {
                                   console.log(e.target);
                                    onChangeFormData('scourceFrom', e);
                                    return e;
                                }}
                            />

                        </Row>    
                        {item.scourceFrom === "1"|| item.scourceFrom === "2" ? (
                            <React.Fragment>
                                {item.scourceFrom === "1" ? (
                                     <Row style={{ marginTop: 16 }}>
                                     <Controller
                                         as={
                                             <InputText type="text"
                                                 name="table"
                                                 classname_input="input-1"
                                                 label={'Chọn table'}
                                             />
                                         
                                         }
                                         name="placeholder"
                                         control={control}
                                         onBlur={() => {
                                             triggerValidation('table');
                                         }}
                                         onChange={([e]) => {
                                             onChangeFormData('table', e.target.value);
                                             return e.target.value;
                                         }}
                                     />
                                    
                                    
                              </Row>
                                ): null}
                           {item.scourceFrom === "2" ? (
                                     <Row style={{ marginTop: 16 }}>
                                     <Controller
                                         as={
                                             <InputText type="text"
                                                 name="apiUrl"
                                                 classname_input="input-1"
                                                 label={'API URL'}
                                             />
                                         
                                         }
                                         name="placeholder"
                                         control={control}
                                         onBlur={() => {
                                             triggerValidation('apiUrl');
                                         }}
                                         onChange={([e]) => {
                                             onChangeFormData('apiUrl', e.target.value);
                                             return e.target.value;
                                         }}
                                     />
                                    
                                    
                              </Row>
                                ): null}
                            
                          <Row style={{ marginTop: 16 }}>
                                 <Controller
                                    as={
                                        <InputText type="text"
                                            name="labelField"
                                            classname_input="input-1"
                                            label={"Label Field"}
                                        />
                                    
                                    }
                                    name="placeholder"
                                    control={control}
                                    onBlur={() => {
                                        triggerValidation('labelField');
                                    }}
                                    onChange={([e]) => {
                                        onChangeFormData('labelField', e.target.value);
                                        return e.target.value;
                                    }}
                                />

                          </Row>
                           <Row style={{ marginTop: 16 }}>
                            <Controller
                                    as={
                                        <InputText type="text"
                                            name="valueField"
                                            classname_input="input-1"
                                            label={'Value Field'}
                                        />
                                    
                                    }
                                    name="placeholder"
                                    control={control}
                                    onBlur={() => {
                                        triggerValidation('valueField');
                                    }}
                                    onChange={([e]) => {
                                        onChangeFormData('valueField', e.target.value);
                                        return e.target.value;
                                    }}
                                />

                         </Row>
                         </React.Fragment>
                        ): null}     

                        {item.scourceFrom === "3" ? (
                             <Row style={{ marginTop: 16 }}>
                                 <div className="base-component">
                                    <div className="label">
                                        Options
                                    </div>
                                    <Row style={{ marginTop: 16 }} gutter={16}>
                                        <Col span={10}>Label</Col>
                                        <Col span={10}>Value</Col>
                                        <Col span={4}>
                                             <Button type="primary" shape="circle" icon={<PlusOutlined />} onClick={addItemOptions}/>
                                        </Col>
                                    </Row >
                                    {item.options.map((optionItem, i) => {
                                       return  (<Row style={{ marginTop: 10 }} gutter={16} key={i}>
                                            <Col span={10}>
                                                <Input type='text' value={optionItem.label} onChange={(e) => onChangeItemOption(e.target.value, i , 'label')}/>
                                            </Col>
                                            <Col span={10}>
                                                 <Input type='text' value={optionItem.value} onChange={(e) => onChangeItemOption(e.target.value, i , 'value')}/>
                                            </Col>
                                            <Col span={4}>
                                                    <Button type="primary" danger  shape="circle" icon={<DeleteOutlined />} onClick={()=> removeItemOptions(optionItem, i)}/>

                                            </Col>
                                        </Row>)
                                    })}
                                </div>
                                
                             </Row>
                        ): null}
                       
                        <Row style={{ marginTop: 16 }}>
                                <Controller
                                    as={
                                        <InputText type="text"
                                            name="mappingField"
                                            classname_input="input-1"
                                            label={'Mapping Field'}
                                        />
                                    
                                    }
                                    name="placeholder"
                                    control={control}
                                    onBlur={() => {
                                        triggerValidation('mappingField');
                                    }}
                                    onChange={([e]) => {
                                        onChangeFormData('mappingField', e.target.value);
                                        return e.target.value;
                                    }}
                                />
                         </Row>   

                    </div>

                </EditModel>

            </React.Fragment>
            </form>
        );
    }
    return (
        <PluginTooltip
            show={showTitle}
            title={'Check box'}
        >

            <div style={{padding: '10px 0' }} >
                <RadioInput
                    label={item.name}
                    required={item.required}
                    placeholder = {item.placeholder}
                    options ={item.options}
                ></RadioInput>
            </div>
        </PluginTooltip>

    );

});

export default withLocalize(RadioPlugin);