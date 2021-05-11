import React, {useEffect , useState} from 'react'
import { Row, Checkbox, Select, Divider, Col, Input, Button } from "antd";
import { useForm, Controller } from "react-hook-form";
import * as yup from "yup";
import InputText from "../../../../../app/components/input";
import { yupResolver } from "@hookform/resolvers/yup";
import { useParams } from "react-router-dom";
import SelectBox from "../../../../../app/components/selectbox"
import { PlusOutlined, DeleteOutlined } from '@ant-design/icons';
import TYPES, { PageMode } from '../../../store/constant';
import { useSelector, useDispatch } from 'react-redux';
import { actionUpdateFormData } from '../../../store/action';
import { loadDataFromUrl , dynamicQuery} from '../../../services/api';


function RadioComponentConfig(props) {
    const [options, setOptions] = useState([])
    let { id } = useParams();
    const dispatch = useDispatch();

    let item = props.data || {};
    if (JSON.stringify(item) == JSON.stringify({})) {
        item.name = 'Label';
        item.valid = true;
        item.errors = {};
        item.options = []
    }
    const { onChange, readOnly , translate} = props;


    const getValidateSetting = function () {
        let schema = yup.object().shape({
            name: yup.string()
                .required("Yêu cầu nhập")
                .max(256, "Độ dài không vượt quá 256")
        });
        return schema;
    };


    const { errors, control, getValues, setValue, trigger } = useForm({
        validateCriteriaMode: "all",
        mode: "onChange",
        resolver: yupResolver(getValidateSetting()),
    });

    const onChangeFormData = (name, value) => {
        setValue(name, value);
        trigger(name);
        let values = getValues() || {};
        onChange({ ...item, ...values });
    };

    const addItemOptions = ()=> {
        let options = [...item.options];
        options.push({name: "", label : ""});
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
        item.name = item.name || "";
        setFormValue();
        onChange(item);
        console.log("useParams", useParams);
    }, []);

    async function loadDataOptionDynamicSql (sql){
        let obj = {};
        obj.Query = sql;
        let data = await dynamicQuery(obj);
        if(item.labelField && item.valueField){
            let listOptions =  data.map(i => {
                  return {
                      label : i[item.labelField] ,
                      value  : i[item.valueField]
                  }
              });
              setOptions(listOptions);
          }
    }

    const onChangeFormDataStore =(val, name,onChangeFormField)=>{
        onChangeFormField(val);
        trigger(val + '');
        if(readOnly){
           let data =`{"${item.mappingField}":"${val}"}`;      
          dispatch(actionUpdateFormData(TYPES.FORMDATA.FORM,id,JSON.parse(data)));
        }
    }

    useEffect(() => {
        setOptions(item.options);
    }, [])

   


    const setFormValue = () => {
        setValue('name', item.name || 'Label');
        setValue('placeholder', item.placeholder || "Placeholder");
        setValue('required', item.required || false);
        setValue('scourceFrom', item.scourceFrom || "1");
    };

   

    return (
        <div>
            <div style={{ width: '100%' }}>

                        <Row>                           
                            <Controller
                                name="name"
                                control={control}
                                render={({ onChange, onBlur, value, name }) => (
                                    <InputText
                                        type="text"
                                        classname_input="input-1"
                                        label={"Label"}
                                        value={value}
                                        onBlur={(e) => {
                                            trigger(name);
                                            onChangeFormData(name, e.target.value);
                                        }}
                                        onChange={(e) => {
                                            onChange(e.target.value);
                                        }}
                                        name={name}
                                    />
                                )}
                            />
                            {(errors && errors.name && errors.name.message && <div className="inline-error" style={{ paddingTop: 10 }}>{errors.name.message}</div>)}
                        </Row>


                        <Row style={{ marginTop: 16 }}>
                            <Controller
                                name="required"
                                render={({ onChange, onBlur, name, value }) => (
                                <Checkbox
                                    className="template-checkbox"
                                    name="required"
                                    style={{ fontSize: "16px", color: "#000000" }}
                                    onChange={(e) => {
                                    onChange(e.target.checked);
                                    onChangeFormData(name, e.target.checked);
                                    }}
                                    checked={value}
                                >
                                    {"required"}
                                </Checkbox>
                                )}
                                control={control}
                            />
                            </Row>                        
                        <Divider />
                        {/* <Row>
                            <h5>Mapping field config</h5>
                        </Row>   */}
                       <Row style={{ marginBottom: 16 }}>                            
                            <Controller                                
                                name="scourceFrom"
                                control={control}
                                render={({ onChange, onBlur, name, value }) => (
                                    <SelectBox  
                                        name= {name}
                                        classname_input="input-1"
                                        label={'Scource From'}
                                        options = {
                                            [
                                                {value : '1' , label : 'From table'},
                                                {value : '2' , label : 'From api'},
                                                {value : '3' , label : 'Define'}
                                            ]
                                        }
                                        onChange={(values)=> {
                                            trigger(name);
                                            onChangeFormData(name, values);  
                                        }}
                                    
                                        />   
                                   )}
                            />

                 </Row>       
                 {item.scourceFrom === "1"|| item.scourceFrom === "2" ? (
                            <React.Fragment>
                                {item.scourceFrom === "1" ? (
                                     <Row style={{ marginTop: 16 }}>                                     
                                      <Controller
                                            name="sql"
                                            control={control}
                                            render={({ onChange, onBlur, value, name }) => (
                                                <InputText
                                                    type="text"
                                                    classname_input="input-1"
                                                    label={"Sql"}
                                                    value={value}
                                                    onBlur={(e) => {
                                                        trigger(name);
                                                        onChangeFormData(name, e.target.value);
                                                    }}
                                                    onChange={(e) => {
                                                        onChange(e.target.value);
                                                    }}
                                                    name={name}
                                                />
                                            )}
                                        />                                    
                                    
                                    
                              </Row>
                                ): null}
                           {item.scourceFrom === "2" ? (
                                     <Row style={{ marginTop: 16 }}>                                        
                                         <Controller
                                            name="apiUrl"
                                            control={control}
                                            render={({ onChange, onBlur, value, name }) => (
                                                <InputText
                                                    type="text"
                                                    classname_input="input-1"
                                                    label={"API URL"}
                                                    value={value}
                                                    onBlur={(e) => {
                                                        trigger(name);
                                                        onChangeFormData(name, e.target.value);
                                                    }}
                                                    onChange={(e) => {
                                                        onChange(e.target.value);
                                                    }}
                                                    name={name}
                                                />
                                            )}
                                        />       
                                        
                                </Row>
                                ): null}
                            
                          <Row style={{ marginTop: 16 }}>                                
                                 <Controller
                                            name="labelField"
                                            control={control}
                                            render={({ onChange, onBlur, value, name }) => (
                                                <InputText
                                                    type="text"
                                                    classname_input="input-1"
                                                    label={"Label Field"}
                                                    value={value}
                                                    onBlur={(e) => {
                                                        trigger(name);
                                                        onChangeFormData(name, e.target.value);
                                                    }}
                                                    onChange={(e) => {
                                                        onChange(e.target.value);
                                                    }}
                                                    name={name}
                                                />
                                            )}
                                        />       
                                

                          </Row>
                           <Row style={{ marginTop: 16 }}>                            
                                    <Controller
                                            name="valueField"
                                            control={control}
                                            render={({ onChange, onBlur, value, name }) => (
                                                <InputText
                                                    type="text"
                                                    classname_input="input-1"
                                                    label={"Value Field"}
                                                    value={value}
                                                    onBlur={(e) => {
                                                        trigger(name);
                                                        onChangeFormData(name, e.target.value);
                                                    }}
                                                    onChange={(e) => {
                                                        onChange(e.target.value);
                                                    }}
                                                    name={name}
                                                />
                                            )}
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
                                        trigger('mappingField');
                                    }}
                                    onChange={([e]) => {
                                        onChangeFormData('mappingField', e.target.value);
                                        return e.target.value;
                                    }}
                                />
                        </Row>   

                        </div>
        </div>
    )
}

export default RadioComponentConfig
