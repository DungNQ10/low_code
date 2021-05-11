import React, { useState, useRef, useEffect } from "react";
import "../../Common/style.scss";
import { withLocalize } from "react-localize-redux";
import { Row, Checkbox, Select, Divider, Col, Input, Button } from "antd";
import * as yup from "yup";
import { useForm, Controller } from "react-hook-form";
import InputText from "../../../../../app/components/input";
import { useSelector, useDispatch } from "react-redux";
import { yupResolver } from "@hookform/resolvers/yup";
import { useParams } from "react-router-dom";
import DispatchConfig from '../../Common/dispatchConfig';
const { Option } = Select;

function SubmitButtonConfig(props) {
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
                    .required("Yêu cầu nhập")
                    .max(256, "Độ dài không vượt quá 256"),
            });
            return schema;
        }
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
        let data = { ...item, ...values };
        onChange(data);
    };

    const setFormValue = () => {
        setValue('name', item.name || 'Submit');
        setValue('newtab', item.newtab);
        setValue('hyperlink', item.hyperlink || false);
        setValue('redirect', item.redirect || false);
        setValue('fontsize', item.fontsize || 16);
        setValue('width', item.width || 100);
        setValue('height', item.height || 46);
    };
    useEffect(() => {
        item.name = item.name || "";
        setFormValue();
        onChange(item);
        console.log("useParams", useParams);
    }, []);

    useEffect(() => {
        console.log("formData", formData);
    }, [formData]);

    return (
        <React.Fragment>
            <div style={{ width: "100%" }}>
                <Row>
                    <Controller
                        name="name"
                        control={control}
                        render={({ onChange, onBlur, value, name }) => (
                            <InputText
                                type="text"
                                classname_input="input-1"
                                label={"Button Name"}
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

                    {errors && errors.name && errors.name.message && (
                        <div className="inline-error" style={{ paddingTop: 10 }}>
                            {errors.name.message}
                        </div>
                    )}
                </Row>
                <Row className="mt-row">
                    <Col span="8">
                        <Controller
                            name="fontsize"
                            control={control}
                            render={({ onChange, onBlur, value, name }) => (
                                <InputText
                                    type="number"
                                    classname_input="input-1"
                                    label={"Font size(px)"}
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

                    </Col>
                    <Col span="8" style={{ paddingLeft: 20 }}>
                        <Controller
                            name="height"
                            control={control}
                            render={({ onChange, onBlur, value, name }) => (
                                <InputText
                                    type="number"
                                    classname_input="input-1"
                                    label={"Height(px)"}
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


                    </Col>
                    <Col span="8" style={{ paddingLeft: 20 }}>
                        <Controller
                            name="width"
                            control={control}
                            render={({ onChange, onBlur, value, name }) => (
                                <InputText
                                    type="number"
                                    classname_input="input-1"
                                    label={"Width(px)"}
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

                    </Col>
                </Row>
                <Row style={{ marginTop: 16 }}>
                    <Controller                     
                      name="redirect"
                      control={control}
                      render={({onChange, value , name})=> (
                        <Checkbox
                            className="template-checkbox"
                            name="redirect"
                            style={{ fontSize: "16px", color: "#000000" }}
                            value={value}
                            onChange={(e)=> {
                                onChangeFormData("redirect", e.target.checked);
                                onChange(e.target.checked) ;
                            }}
                        >
                            {"Redirect"}
                        </Checkbox>
                      )}                      
                    />
                  </Row>
                {item.redirect && (
                    <React.Fragment>
                      <Row style={{ marginTop: 16 }}>
                      <Controller
                      name="hyperlink"
                      control={control}
                      render={({ onChange, onBlur, value, name }) => (
                        <InputText
                          type="text"
                          classname_input="input-1"
                          label={"URL"}
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
                       
                        {errors &&
                          errors.hyperlink &&
                          errors.hyperlink.message && (
                            <div
                              className="inline-error"
                              style={{ paddingTop: 10 }}
                            >
                              {errors.hyperlink.message}
                            </div>
                          )}
                      </Row>                   
                    </React.Fragment>
                  )}   
                    <DispatchConfig
                    onChangePluginValue={onChange}
                    pluginValues={props.data}
                ></DispatchConfig>  
                        
            </div>
        </React.Fragment>
    )
}

export default SubmitButtonConfig
