import React, { useState, useEffect } from 'react';
import '../../Common/style.scss';
import { withLocalize } from 'react-localize-redux';
import PluginTooltip from '../../Common/pluginTooltip';

const Divider = React.memo((props) => {
    const {
        state,
        readOnly,
        onChange,
        isEditMode,
        isInsertMode,
        isResizeMode,
        isLayoutMode
    } = props;

    let showTitle = isEditMode|| isInsertMode|| isResizeMode|| isLayoutMode;
    console.log(props);
    let [firstLoad,setFirtload] = useState(true);
    let item = state || {};
    useEffect(()=>{
        return ()=>{
            if(!item.value){
                if(firstLoad){
                    onChange({value:24});
                    setFirtload(false);
                }
            }
            
           
        };
    },firstLoad);

    // const [data, setData] = useState({ title: "0", subTitle: "" });
    const [] = useState(false);
    
  
    

    if (!readOnly) {

        return (
            <React.Fragment>
                <PluginTooltip
                    show = {showTitle}
                    title="선 구분"
                >
                <div style={{padding:'8px 0', margin:'0 10px', backgroundColor:"#F6F3F2"}}>
                   <hr style={{width:'100%', border:'1px solid #CCC'}}></hr>
                </div>
               
                </PluginTooltip>
                
                {/* <EditModel onClickOutside={handleClickOutside} open={openModel && (!readOnly)} onClose={()=>setOpenModel(false)}>

                    <div style={{ width: "896px", height: "auto"}}>
                        <Row>

                        <InputText
                                type="number"
                                name="value"
                                inputTitle={"높이 (px)"}
                                classname_input="select-1"
                                hidden={true}
                                onChange = {(e)=>{onInput(onChange,e.target.value)}} 
                                value={item.value}
                                min={1}
                            />
                           
                        </Row>

                    </div>

                </EditModel> */}

            </React.Fragment>

        );
    }

    // If we are not in edit mode, remove the input field
    return (
        <PluginTooltip
            show = {showTitle}
            title="선 구분"
        >
        <div style={{padding:'20px 10px'}}>
                   <hr style={{width:'100%', border:'1px solid #CCC'}}></hr>
                </div>
        </PluginTooltip>
        
    );
});

export default withLocalize(Divider);