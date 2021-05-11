import React, { useState, useRef, isValidElement, useEffect } from 'react';
import '../../Common/style.scss';

import { Row, Input } from 'antd';
import EditModel from '../../Common/editModel';
import { withLocalize } from 'react-localize-redux';
import PluginTooltip from '../../Common/pluginTooltip';


const onInput = function (onChange, value) {
    try {
        let val = parseInt(value);
        if (val == 0)
            val = 1;
        onChange({ value: val });
    } catch (error) {
        onChange(1);
    }

};

const Space = React.memo((props) => {
    const {
        state,
        readOnly,
        onChange,
        translate,
        isEditMode,
        isInsertMode,
        isPreviewMode,
        isResizeMode,
        isLayoutMode
    } = props;

    let showTitle = isEditMode || isInsertMode || isResizeMode || isLayoutMode;
    console.log(props);
    const refContainer = useRef(null);
    const handleClickOutside = function (event) {
        const { target } = event;
        if (target == refContainer.current || refContainer.current.contains(target)) {
            setOpenModel(true);
        }
        else {

            setOpenModel(false);

        }

    };
    let [firstLoad, setFirtload] = useState(true);
    let item = state || {};
    useEffect(() => {
        return () => {
            if (!item.value) {
                if (firstLoad) {
                    onChange({ value: 24 });
                    setFirtload(false);
                }
            }


        };
    }, firstLoad);

    // const [data, setData] = useState({ title: "0", subTitle: "" });
    const [openModel, setOpenModel] = useState(false);
    //close dialog when change mode
    useEffect(() => {
        if (!isEditMode && openModel) {
            setOpenModel(false);
        }
    });



    if (!isPreviewMode) {

        return (
            <React.Fragment>
                <PluginTooltip
                    show={showTitle}
                    title="공간"
                >
                    <div style={{ paddingLeft: 10, paddingRight: 10, paddingTop: 8, paddingBottom: 8, border: (openModel == true ? '1px solid #002c5f' : 'none'), margin: '0 10px' }} ref={refContainer}>
                        <div style={{ width: '100%', height: item.value || 24 }} >
 cin chao
                        </div>
                    </div>

                </PluginTooltip>

                <EditModel onClickOutside={handleClickOutside} open={openModel && (!readOnly)} onClose={() => setOpenModel(false)}>

                    <div style={{ width: '899px', height: 'auto' }}>
                        <Row>

                            <Input
                                type="number"
                                name="value"
                                inputTitle={'높이 (pxxx)'}
                                classname_input="select-1"
                                onChange={(e) => { onInput(onChange, e.target.value); }}
                                value={item.value}
                                min={1}
                            />

                        </Row>

                    </div>

                </EditModel>

            </React.Fragment>

        );
    }

    // If we are not in edit mode, remove the input field
    return (
        <PluginTooltip
            show={showTitle}
            title="공간"
        >
            <div style={{ paddingLeft: 10, paddingRight: 10 }}>
                <div style={{ width: '100%', height: item.value || 24, border: 'none' }} >
            cin chao
                </div>
            </div>
        </PluginTooltip>

    );
});

export default withLocalize(Space);