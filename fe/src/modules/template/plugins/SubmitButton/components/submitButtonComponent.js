import React, { useEffect } from 'react'
import {Button } from "antd";
import { useSelector, useDispatch } from 'react-redux';
import { PageMode } from '../../../store/constant';
import { DispatchEvent } from '../../../constants';
import { actionDispatchAction } from '../../../store/action';

function SubmitButtonComponent(props) {
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
    let item =  props.data||{};
    const isPreviewPage = ()=>{
        return (oryStore.pageMode == PageMode.FunctionPage 
            || oryStore.pageMode == PageMode.TemplatePage
            );
    };

    const setFormKey = (value) => {
        item.formKey = value;
        onChange(item);
      };
    const onSubmitEvent=(e)=>{
        debugger;
        if(!isPreviewPage())
            {
                e.preventDefault();
                return;
            }
           
        if(item[DispatchEvent.ON_CLICK])
        {
            e.preventDefault();
            dispatch(actionDispatchAction(item[DispatchEvent.ON_CLICK]));
        }
        else{
            e.preventDefault();
            dispatch(actionDispatchAction(`$submitform$_${item.formKey}`));
        }
    }
    const initComponent =()=>{
        if(!item.setFormKey){
          item.setFormKey = setFormKey;
          onChange(item);}
      }
    
      initComponent();
    useEffect(()=>{
        item.setFormKey = setFormKey;
        onChange(item);
    },[]);
    return (
        <div style={{ textAlign: 'center', padding: '10px 0' }}  >
                <button
                    type={isPreviewPage() ? 'submit' : 'button' }
                    onClick={onSubmitEvent}
                    className="ant-btn base-button ant-btn-default"
                    style={{
                        borderRadius: 0,
                        width: (item.width || 100) + 'px',
                        height: (item.height || 46) + 'px',
                        display: 'inline-flex',
                        alignItems: 'center',
                        overflow: 'hidden',
                        maxWidth:'100%'
                    }}
                >
                    <span
                        style={{
                            fontSize: item.fontsize + 'px',
                            margin: 'auto',
                            width: '100%',
                            lineHeight: 'normal',
                            overflow: 'hidden'
                        }}
                    >
                        {item.name || 'Submit'}
                    </span>
                </button>
            </div>
    )
}

export default SubmitButtonComponent
