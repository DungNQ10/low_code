import React , {useEffect , useState} from 'react'
import SelectBox from '../../../../../app/components/selectbox';
import { loadDataFromUrl , dynamicQuery} from '../../../services/api';
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";
import TYPES, { PageMode } from '../../../store/constant';
import { useSelector, useDispatch } from 'react-redux';
import {useParams} from 'react-router-dom';
import { actionUpdateFormData } from '../../../store/action';

function SelectBoxComponent(props) {
    const  oryStore  = useSelector(state => state.TemplateManagement.oryStore)||{};
    const dispatch = useDispatch();

    let { id } = useParams();

    const [options, setOptions] = useState([])

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
    let item =  props.data||{};

    var handleForm =  oryStore.pageMode == PageMode.PreviewPage;

    const isPreviewPage = ()=>{
        return (oryStore.pageMode == PageMode.FunctionPage 
            || oryStore.pageMode == PageMode.TemplatePage
            );
    };

    useEffect(() => {
        item.name = item.name||'';
        //item.getSubmitData = getSubmitData;
        //item.validateSubmitEvent = validateSubmitEvent;
        item.setFormData = setFormData;
        setFormValue();
        onChange(item);
        console.log("useParams",useParams)
      }, []);


      const setFormValue = () => {
        setValue("name", item.name || "Label");
        setValue("required", item.required || false);
        setValue("mappingField", item.mappingField || "");
      };

    const setFormData = (value)=>{
        setValue('fieldValue',value);
    }

      

    const getValidateSetting = function () {
        console.log("readOnly", readOnly);
      if (handleForm) {
        let fieldValue = yup.string();
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

   

    async function loadDataOptionFromApi (url){
            let data = await loadDataFromUrl(url);
            if(item.labelField && item.valueField){
              let listOptions =  data.map(i => {
                    return {
                        label : i[item.labelField],
                        value  : i[item.valueField]
                    }
                });
                setOptions(listOptions);
            }
    }

    async function loadDataOptionDynamicSql (sql){
        let obj = {};
        obj.Query = sql;
        let data = await dynamicQuery(obj);
        if(item.labelField && item.valueField){
            let listOptions =  data.map(i => {
                  return {
                      label : i[item.labelField],
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

    useEffect(() => {
        if(item.scourceFrom === "2" && item.apiUrl){
            loadDataOptionFromApi(item.apiUrl);
        }
        if(item.scourceFrom === "1" && item.sql){
            loadDataOptionDynamicSql(item.sql);
        }
        if(item.scourceFrom === "3"){
            setOptions(item.options);
        }
    }, [item.scourceFrom, item.apiUrl , item.labelField , item.valueField , item.sql, item.options])

    return (
        <div style={{padding: '10px 0' }} >
            <Controller
                name="fieldValue"
                render={({ onChange, onBlur, name, value }) => (
                    <SelectBox
                        label={item.name || 'Label' }
                        required={item.required}
                        value= {value}
                        placeholder = {item.placeholder || "placeholder"}
                        options ={options||[]}
                        onChange={(e) => {
                            onChangeFormDataStore(e,name,onChange);
                        }}
                    ></SelectBox>
                )}
                control={control}
                />
                
    </div>
    )
}

export default SelectBoxComponent
