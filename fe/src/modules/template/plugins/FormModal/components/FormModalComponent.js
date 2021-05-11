import React, { useEffect, useState } from "react";
import "../../Common/style.scss";
import { withLocalize } from "react-localize-redux";
import { Controller, useForm } from "react-hook-form";
import { useSelector, useDispatch } from "react-redux";
import TYPES, { PageMode } from '../../../store/constant';
import { actionUpdateFormData } from "../../../store/action";
import { yupResolver } from "@hookform/resolvers/yup";
import { useParams } from "react-router-dom";
import { Modal } from "antd";
import TemplateForm from '../../../../dynamicform/components';

const FormModalComponent = React.memo((props) => {
  console.log(props);
  const { onChange, readOnly } = props;
  let item = props.data || {};
  const dispatch = useDispatch();
  const formData = useSelector((state) => {
    
    if (!item.formKey) return null;
    var data = state.TemplateManagement.formData;
    if (data && data.get(item.formKey)) return data.get(item.formKey).form;
    else return null;
  });
  const { errors, control, getValues, setValue, trigger } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange"
  });
  const oryStore = useSelector((state) => state.TemplateManagement.oryStore);
  

  const setFormKey = (value) => {
    item.formKey = value;
    onChange(item);
  };
  const [timeStemp, setTimeStemp] = useState(null);

  useEffect(() => {
    item.isModalVisible = false;
    item.setFormKey = setFormKey;
    onChange(item);
    console.log("useParams", useParams);
  }, []);
  useEffect(() => {
    
    item.isModalVisible =formData?( formData.isModalVisible || false):false;
    item.itemId = formData?formData.itemId||'':'';
    setValue("isModalVisible",item.isModalVisible);
    setTimeStemp(new Date());
    onChange(item);
    
  }, [formData]);

  //cancel dialog
  const cancelDialog = () => {
    item.isModalVisible = false;
    dispatch(
      actionUpdateFormData(TYPES.FORMDATA.FORM, item.formKey, { isModalVisible: false, itemId:'' })
    );
  };
 
  return (
    <div style={{ padding: "10px 0" }}>
        <Controller
          name="isModalVisible"
          render={({ onChange, onBlur, name, value }) => (
              <input 
                type="hidden"
                value={value}
                name={name}
              />
          )}
          control={control}
        />
        
      <Modal
        title="Basic Modal"
        visible={item.isModalVisible}
        onCancel={cancelDialog}
      >
        <TemplateForm 
        globalStore={{ pageMode: PageMode.FunctionPage, siteMode: "Admin" }}
        formKey={item.templateId}   
        itemId={item.itemId}
       />
     
     
       </Modal>
      
    </div>
  );
});

export default withLocalize(FormModalComponent);
