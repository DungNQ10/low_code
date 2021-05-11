import React,{useState, useEffect} from 'react'
import RadioInput from '../../../../../app/components/radio';
import { loadDataFromUrl , dynamicQuery} from '../../../services/api';
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";
import TYPES, { PageMode } from '../../../store/constant';
import { useSelector, useDispatch } from 'react-redux';
import {useParams} from 'react-router-dom';
import { actionUpdateFormData } from '../../../store/action';


function RadioComponent(props) {
    const [options, setOptions] = useState([])
    let { id } = useParams();
    const  oryStore  = useSelector(state => state.TemplateManagement.oryStore)||{};
    const dispatch = useDispatch();

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

        var handleForm =  oryStore.pageMode == PageMode.PreviewPage;


    const getValidateSetting = function () {
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

    let item =  props.data||{};
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

    async function loadDataOptionFromApi (url){
        let data = await loadDataFromUrl(url);
        if(item.labelField && item.valueField){
          let listOptions =  data.map(i => {
                return {
                    label : i[item.labelField] + '',
                    value  : i[item.valueField] + ''
                }
            });
            debugger;
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
    
    return (
        <div style={{padding: '10px 0' }} >
                <RadioInput
                    label={item.name}
                    required={item.required}
                    placeholder = {item.placeholder}
                    options ={options}
                ></RadioInput>
            </div>
    )
}

export default RadioComponent
