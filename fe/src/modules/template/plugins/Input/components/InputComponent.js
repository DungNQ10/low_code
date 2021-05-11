import React, { useState, useRef, useEffect } from "react";
import "../../Common/style.scss";
import PluginTooltip from "../../Common/pluginTooltip";
import DatePicker from "../../../../../app/components/datepicker";
import { withLocalize } from "react-localize-redux";
import * as yup from "yup";
import { useForm, Controller } from "react-hook-form";
import InputText from "../../../../../app/components/input";
import { useSelector, useDispatch } from 'react-redux';
import TYPES, { PageMode } from '../../../store/constant';
import { actionUpdateFormData, actionDispatchAction } from '../../../store/action';
import { yupResolver } from '@hookform/resolvers/yup';
import {useParams} from 'react-router-dom';
import { DispatchEvent } from '../../../constants';

const InputComponent = React.memo((props) => {
  console.log(props)
  const {
    onChange,
    readOnly
  } = props;
  const dispatch = useDispatch();
  const formData = useSelector((state) => {
    return state.TemplateManagement.formData;
  });
  
  const oryStore = useSelector((state) => state.TemplateManagement.oryStore);
  const eventSubcribe = useSelector((state)=> state.TemplateManagement.dispatchAction.data);
  let item = props.data||{};
  const getValidateSetting = function () {
   
    if (readOnly) {
      let fieldValue = yup.string().nullable();
     
      if(item.required){
        fieldValue = fieldValue.required("Yêu cầu nhập.");
      }
      return yup.object().shape({
        fieldValue:fieldValue
      });
    }
    else 
        return  yup.object().shape();
  };

  
  const { errors, control, getValues, setValue, trigger } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
    resolver: yupResolver( getValidateSetting()),
  });

  const setFormValue = () => {
    setValue("name", item.name || "Label");
    setValue("required", item.required || false);
    setValue("mappingField", item.mappingField || "");
  };

  const getSubmitData = () => {
    return {
      name: "InputText",
      field: item.mappingField || "",
      fieldValue: getValues().fieldValue || false,
    };
  };
  const validateSubmitEvent = () => {
    return trigger();
  };

  const setFormData = (value)=>{
    setValue('fieldValue',value);
    let data =`{"${item.mappingField}":"${value}"}`;
    debugger;
    dispatch(actionUpdateFormData(TYPES.FORMDATA.FORM,item.formKey,JSON.parse(data)));
  }
  const setFormKey = (value)=>{
    item.formKey = value;
  }

  const initComponent =()=>{
    if(!item.setFormData){
      item.name = item.name||'';
      item.getSubmitData = getSubmitData;
      item.validateSubmitEvent = validateSubmitEvent;
      item.setFormData = setFormData;
      item.setFormKey = setFormKey;
      setFormValue();
      onChange(item);}
  }

  initComponent();
  useEffect(() => {
    item.name = item.name||'';
    item.getSubmitData = getSubmitData;
    item.validateSubmitEvent = validateSubmitEvent;
    item.setFormData = setFormData;
    item.setFormKey = setFormKey;
    setFormValue();
    onChange(item);
    console.log("useParams",useParams)
  }, []);
  useEffect(()=>{
    console.log("formData",formData);
  },[formData])
   
  useEffect(()=>{
    if(item.subscribeEvent && eventSubcribe.get(item.subscribeEvent)){
      console.log("dispatch event subcrible:", item.subscribeEvent)
    }
  },[eventSubcribe]);
  const onChangeFormDataStore =(e, name,onChangeFormField)=>{
    debugger;
    onChangeFormField(e.target.value);
    trigger(name);
    if(readOnly){
      let data =`{"${item.mappingField}":"${e.target.value}"}`;      
      dispatch(actionUpdateFormData(TYPES.FORMDATA.FORM,item.formKey,JSON.parse(data)));
      console.log(item.formKey);
      if(item[DispatchEvent.ON_CHANGE]){
        dispatch(actionDispatchAction(item[DispatchEvent.ON_CHANGE]))
        console.log("dispatch event onchange:", item[DispatchEvent.ON_CHANGE]);
      }
    }
  }

 
  return (
      <div style={{ padding: "10px 0" }}>
        <Controller
          name="fieldValue"
          render={({ onChange, onBlur, name, value }) => (
            <InputText
              type="text"
              label={item.name}
              value={value}
              required={item.required}
              onChange={(e) => {
                onChangeFormDataStore(e,name,onChange);
              }}
              onBlur={(e) => {
                onBlur();
                trigger("fieldValue");
              }}
              name={name}
            />
          )}
          control={control}
        />
        {errors && errors.fieldValue && errors.fieldValue.message && (
          <div className="inline-error" style={{ paddingTop: 10 }}>
            {errors.fieldValue.message}
          </div>
        )}
      </div>
    
  );
});

export default withLocalize(InputComponent);
