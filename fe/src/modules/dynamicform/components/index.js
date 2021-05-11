import { createEmptyState } from "@react-page/core";
import React, { useState, useRef, useEffect } from "react";
import { useForm, Controller } from "react-hook-form";
import { notification, Spin } from "antd";
import { useHistory } from "react-router-dom";
import * as yup from "yup";
import "../../template/components/templatePage/form/style.css";
import { useDispatch, useSelector } from "react-redux";
import OryEditor from "../../template/components/editor/oryEditor";
import {
    actionDynamicFormGetById,
    actionSetPageMode,
    actionDynamicFormSubmit,
    actionDynamicFormGetEditData,
    actionLoadDataFromUrl,
    actionSubmitDataToUrl,
    actionClearAfterFormSaveSusccess,
    actionUpdateFormData,
    actionRemoveDispatchAction
} from '../../template/store/action';
import TYPES from "../../template/store/constant";
import { useParams } from "react-router-dom";
import { get,post } from '../../../app/common/apiUtil';

const TemplateForm = (props) => {

  const{formKey,itemId} = props;
  const [] = useState(false);
  const refOryEditor = useRef(null);
  const dispatch = useDispatch();
  let history = useHistory();
  const getValidateSettings = () => {
    let schema = yup.object().shape({
      name: yup.string().required("Trường này yêu cầu nhập."),
      tableName: yup.string().required("Trường này yêu cầu nhập."),
    });
    return schema;
  };
  const {
    handleSubmit,
    control,
    getValues,
    setValue,
    trigger,
    errors,
  } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
    validationSchema: getValidateSettings(),
  });
  const { globalStore } = props;

  // Editor Value State
  const [editorValue, setEditorValue] = useState(createEmptyState);
  const dynamicFormDataStore = useSelector((state) => {
    console.log(state.TemplateManagement.dynamicFormData);
    return state.TemplateManagement.dynamicFormData
   
    // return state.TemplateManagement.dynamicFormData.data;
  });

  const [dynamicFormData, setDynamicFormData] = useState();
useEffect(()=>{
  if(dynamicFormDataStore)
    {
      let data = dynamicFormDataStore.get(formKey);
      setDynamicFormData(data);
    }
},[dynamicFormDataStore])

  const formsData = useSelector((state) => state.TemplateManagement?.formData);
  const dynamicFormEditData = useSelector((state) => {
    return state.TemplateManagement.dynamicFormEditData.data;
  });

  let saveSuccess = useSelector((state) => {
    return state.TemplateManagement.dynamicFormSave.saveSuccess;
  });
  let loading = useSelector((state) => {
    return (
      state.TemplateManagement.dynamicFormSave.loading ||
      state.TemplateManagement.dynamicFormEditData.loading
    );
  });

  const formData = useSelector((state) => {
    return state.TemplateManagement.formData;
  });

  const eventSubcribe = useSelector(
    (state) => state.TemplateManagement.dispatchAction.data
  );

  useEffect(()=>{
    var submitEventKey = `$submitform$_${formKey}`;
    if(eventSubcribe.get(submitEventKey)){
      handleSubmitEvent();
      dispatch(actionRemoveDispatchAction(submitEventKey))
    }
  },[eventSubcribe])
  const handleSubmitEvent = async () => {
        
    if (refOryEditor && refOryEditor.current) {
      let valid = await refOryEditor.current.validateSubmitEvent();
      if(!valid)
        return;
      if (valid) {
        let data = null;
        if (formsData) {
          data = formsData.get(formKey)?.form;
        }

        let apiInfo = JSON.parse(dynamicFormData.apiInfo);
        if(!itemId)
        {//insert
          var {saveApi } = apiInfo;
          var params = {
            
          };
          saveApi.parameters = saveApi.parameters||[];
          saveApi.parameters.map(c=>{
            params[c.name] = data[c.mapping];
          });
          if(!params.id)
            params.id=itemId||0;
          var url = saveApi.apiUrl.indexOf('http')==0?saveApi.apiUrl:  process.env.REACT_APP_API_URL + saveApi.apiUrl;
          if(saveApi.method=='Post'){
          var k =  post(url,params);
          k.then(response=>{
            console.log(response);          
          })
          }

          console.log(params);
        }
        else //update
        {
          var saveApi  = apiInfo.updateApi;
          var params = {
            
          };
          saveApi.parameters = saveApi.parameters||[];
          saveApi.parameters.map(c=>{
            params[c.name] = data[c.mapping];
          });
          if(!params.id)
            params.id=itemId||0;
          
          var url = saveApi.apiUrl.indexOf('http')==0?saveApi.apiUrl:  process.env.REACT_APP_API_URL + saveApi.apiUrl;
          if(saveApi.method=='Post'){
          var k =  post(url,params);
          k.then(response=>{
            console.log(response);          
          })
          }

          console.log(params);
        }

        // let saveApiInfoApi = apiInfo.find((c) => c.apiType == "SaveData");

        // // Không có thông tin api save => lưu mặc định
        // if (!saveApiInfoApi || !saveApiInfoApi.apiUrl) {
        //   let submitData = {
        //     tableName: dynamicFormData.tableName,
        //     fields: Object.keys(data).map((c) => {
        //       return {
        //         fieldName: c,
        //         fieldValue: data[c],
        //         dataType: "string",
        //       };
        //     }),
        //   };
         
        //   if (itemId) {
        //     submitData.fields.push({
        //       fieldName: "id",
        //       fieldValue: itemId,
        //     });
        //   }
        //   dispatch(actionDynamicFormSubmit(submitData));
        // }

        // if (saveApiInfoApi && saveApiInfoApi.apiUrl) {
        //   if (itemId) {
        //     data.id = itemId;
        //   }
        //   dispatch(
        //     actionSubmitDataToUrl({
        //       api: saveApiInfoApi.apiUrl,
        //       submitData: data,
        //     })
        //   );
        // }
      }
    }
  };

  useEffect(() => {
    console.log("formData", formData);
  }, [formData]);

  useEffect(() => {
    console.log(formKey, itemId);
    dispatch(actionSetPageMode({ ...globalStore }));
    let id = formKey;
    if (id) {
      dispatch(actionDynamicFormGetById({ id: id }));
    }
  }, []);

  useEffect(() => {
    if (dynamicFormEditData) {
      refOryEditor.current.setFormData(dynamicFormEditData[0]);
    }
  }, [dynamicFormEditData]);

  useEffect(() => {
    if (dynamicFormData && saveSuccess) {
      let apiInfo = JSON.parse(dynamicFormData.apiInfo);
      let saveApiInfoApi = apiInfo.find((c) => c.apiType == "SaveData");

      if (saveApiInfoApi && saveApiInfoApi.redirect) {
        history.push(saveApiInfoApi.redirect);
      }

      if (saveApiInfoApi && saveApiInfoApi.notificationSuccess) {
        notification.success({
          message: saveApiInfoApi.notificationSuccess,
        });
      }

      dispatch(actionClearAfterFormSaveSusccess());
    }
  }, [saveSuccess]);

  useEffect(() => {
    console.log("dynamicFormData",dynamicFormData)
    if (dynamicFormData) {
      setValue("name", dynamicFormData.name);
      setValue("path", dynamicFormData.path);
      setValue("tableName", dynamicFormData.tableName);
      setValue("description", dynamicFormData.description);
      setEditorValue(JSON.parse(dynamicFormData.content));
      if (itemId) {
        try {

          var apiInfo = JSON.parse(dynamicFormData.apiInfo);
          let {detailApi} = apiInfo;
          detailApi.parameters = detailApi.parameters||[];
          var params = {};
          detailApi.parameters.map((c,index)=>{
            if(index==0)
              params[c.name] = itemId;
          });
          var url = detailApi.apiUrl.indexOf('http')==0?detailApi.apiUrl:  process.env.REACT_APP_API_URL + detailApi.apiUrl;
          var k = undefined;
          switch (detailApi.method) {
            case 'Post':
              k = post(url,params);
              break;
            case 'Get':        
              k = get(url, params)  
            break;
              
            default:
              break;
          }
          if(k){
          k.then(response=>{
            console.log(response);
            refOryEditor.current.setFormData(response.data);
          })
          }


          // var editApi = apiInfo.find((c) => c.apiType == "LoadEditData");
          // if (editApi && editApi.query && !editApi.apiUrl) {
          //   var params = [];
          //   editApi.parameters.map((c) => {
          //     params.push({
          //       name: c.name,
          //       value:
          //         c.name == "id"
          //           ? itemId
          //           : dynamicFormData[c.name],
          //     });
          //   });

          //   dispatch(
          //     actionDynamicFormGetEditData({
          //       paramaters: params,
          //       query: editApi.query,
          //     })
          //   );
          // }
          // if (editApi && editApi.apiUrl && itemId) {
          //   let apiUrl = editApi.apiUrl;
          //   editApi.parameters.forEach((c) => {
          //     apiUrl = apiUrl.replace(
          //       `@${c.name}`,
          //       c.name == "id"
          //         ? itemId
          //         : dynamicFormData[c.name]
          //     );
          //   });
          //   dispatch(actionLoadDataFromUrl(apiUrl));
          // }
        } catch (error) {}
      }
      setTimeout(() => {
        refOryEditor.current.setFormKey(formKey);
      }, 500);
    }
  }, [dynamicFormData,itemId]);

  useEffect(()=>{
    setTimeout(() => {
      refOryEditor.current.setFormKey(formKey);
    }, 500);
  },[formKey])
  return (
    <React.Fragment>
      <Spin tip="Loading..." spinning={loading}>
        <div>
          <form onSubmit={handleSubmit(handleSubmitEvent)}>
            <div className={"template-page-main "}>
              {!!editorValue && (
                <>
                  <OryEditor
                    editorValue={editorValue}
                    onChange={setEditorValue}
                    globalStore={globalStore}
                    editorRef={refOryEditor}
                    templateBackground={"#ffffff"}
                    formKey={formKey}
                  ></OryEditor>
                </>
              )}
            </div>
          </form>
        </div>
      </Spin>
    </React.Fragment>
  );
};

export default TemplateForm;
