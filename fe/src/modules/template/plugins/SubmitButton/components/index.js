import React, { useState, useRef } from 'react';
import '../../Common/style.scss';

import { Row, Checkbox, Modal, Col } from 'antd';
import EditModel from '../../Common/editModel';
import { withLocalize } from 'react-localize-redux';
import * as yup from 'yup';
import PluginTooltip from '../../Common/pluginTooltip';
import InputText from '../../../../../app/components/input';
import { useEffect } from 'react';

import { useSelector } from 'react-redux';
import { useForm, Controller } from 'react-hook-form';
import { PageMode } from '../../../store/constant';


const SubmitButton = React.memo((props) => {
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

    const  oryStore  = useSelector(state => state.TemplateManagement.oryStore)||{};
    let showTitle = isEditMode || isInsertMode || isResizeMode || isLayoutMode;
    const btn = useRef(null);
    const getValidateSetting = function () {
        let schema = yup.object().shape({
            name: yup.string()
                .required(translate('commonMessage.C_MSG_12'))
                .max(256, translate('commonMessage.C_MSG_15', { number: 256 })),
            hyperlink: yup.string()
                .required(translate('commonMessage.C_MSG_12'))
                .max(2048, translate('commonMessage.C_MSG_15', { number: 2048 }))
                .url(translate('linkConnection.urlInvalid'))
        });
        return schema;
    };

    const { errors, control, getValues, setValue, trigger } = useForm({
        validateCriteriaMode: 'all',
        mode: 'onChange',
        validationSchema: getValidateSetting()
    });

    const refContainer = useRef(null);
    const handleClickOutside = function (event) {
        const { target } = event;
        if (target == refContainer.current || refContainer.current.contains(target)) {
            setOpenModel(true);
        }
        else {

            if (item.errors && item.errors.url) {

                onChange({ url: '', errors: {} });
            }
            setOpenModel(false);

        }

    };



    let schema = yup.object().shape({
        name: yup.string()
            .required(translate('commonMessage.C_MSG_12'))
            .max(256, translate('commonMessage.C_MSG_15', { number: 256 })),
        url: yup.string()
            .required(translate('commonMessage.C_MSG_12'))
            .max(256, translate('commonMessage.C_MSG_15', { number: 2048 }))
            .url(translate('linkConnection.urlInvalid'))
    });


    // const [data, setData] = useState({ title: "0", subTitle: "" });
    const [openModel, setOpenModel] = useState(false);
    let item = state || {};
    //close dialog props when change mode
    useEffect(() => {
        if (!isEditMode && openModel) {
            setOpenModel(false);
        }
        // fitWidth();
    });
    //set default value
    if (JSON.stringify(state) == JSON.stringify({})) {

        item.name = 'Submit';
        item.valid = true;
        item.errors = {};
    }



    const fitWidth = () => {
        if (btn && btn.current) {
            console.log(btn.current);
            var parentWidth = btn.current.parentNode.clientWidth;
            if (item.width && parentWidth < item.width) {
                btn.current.firstChild.style.width = (parentWidth - 20) + 'px';
            }
        }
    };

    //pass data to call outside component
    const validate = async function (dataInput) {

        let data = dataInput || item;
        schema
            .validate(data, { abortEarly: false }).then(function () {
                onChange({ errors: {}, valid: true });
            }).catch(function (e) {

                let errors = {};
                e.inner.map(c => {
                    errors[c.path] = {
                        msg: c.message
                    };
                });
                onChange({ errors: errors, valid: false });

            });

    };

    //set disabledButton
    const setDisabledButton = (value) => {
        setValue('disabled', value);
        trigger();
    };


    //modal success
    const modalSuccess = () => {
        const modal = Modal.success({
            content:
                <React.Fragment>
                    <div className='success-content'>
                        {/* <img src={successImage}></img> */}
                        <span className="message">Submit successfully.</span>
                    </div>
                </React.Fragment>,
            className: 'success-modal',
            okText: 'OK',
            cancelText: 'OK',
            width: 576,
            onOk: () => {
                setTimeout(() => {
                    window.scrollTo(0,0);
                }, 300);
               
                if (item.redirect === true) {
                    let link = document.createElement('a');
                    link.setAttribute('href', item.hyperlink);
                    if (item.newtab === true) {
                        link.setAttribute('target', '_blank');
                    };
                    link.click();
                   
                }
            }
        });

        return modal;
    };
    //redirect function
    const doAfterSubmit = () => {
       
        modalSuccess();
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
    //set loading data

    useEffect(() => {
        item.validate = validate;
        item.setDisabledButton = setDisabledButton;
        item.doAfterSubmit = doAfterSubmit;
        setFormValue();
        onChange(item);
    }, []);

    //set formdata when change mode
    useEffect(() => {
        if (isEditMode) {
            setFormValue();
        }

    }, [isEditMode]);

    const isPreviewPage = ()=>{
        return (oryStore.pageMode == PageMode.FunctionPage 
            || oryStore.pageMode == PageMode.TemplatePage
            );
    };

    // If readOnly is false, it means that we are in edit mode!

    const getDisabledButton = () => {

        let disabedButton = false;
        if (getValues() && getValues().disabled) {
            disabedButton = getValues().disabled;
        };
        return disabedButton;
    };

    const onChangeFormData = (name, value) => {
        setValue(name, value);
        trigger(name);
        let values = getValues() || {};
        onChange({ ...item, ...values });
    };

    if (!isPreviewMode) {

        return (
          <form>
            <React.Fragment>
              <PluginTooltip show={showTitle} title={"Submit button"}>
                <div ref={refContainer}>
                  <div
                    style={{ textAlign: "center", padding: "8px 0" }}
                    className={`${openModel ? "active-component" : ""}`}
                  >
                    <div className="sc-AxjAm hJONUL button-component" ref={btn}>
                      <button
                        type="button"
                        className="ant-btn base-button ant-btn-default"
                        style={{
                          borderRadius: 0,
                          width: (item.width || 100) + "px",
                          height: (item.height || 46) + "px",
                          display: "inline-flex",
                          alignItems: "center",
                          overflow: "hidden",
                          maxWidth: "100%",
                        }}
                      >
                        <span
                          style={{
                            fontSize: item.fontsize + "px",
                            margin: "auto",
                            width: "100%",
                            lineHeight: "normal",
                            overflow: "hidden",
                          }}
                        >
                          {item.name || "Submit"}
                        </span>
                      </button>
                    </div>
                  </div>
                </div>
              </PluginTooltip>

              <EditModel
                onClickOutside={handleClickOutside}
                open={openModel && !readOnly}
                onClose={() => setOpenModel(false)}
              >
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
                      as={
                        <Checkbox
                          className="template-checkbox"
                          name="redirect"
                          style={{ fontSize: "16px", color: "#000000" }}
                        >
                          {"Redirect"}
                        </Checkbox>
                      }
                      name="redirect"
                      control={control}
                      onChange={([e]) => {
                        console.log(e);
                        onChangeFormData("redirect", e.target.checked);
                        return e.target.checked;
                      }}
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
                      <Row style={{ marginTop: 16 }}>
                        <Controller
                          as={
                            <Checkbox
                              className="template-checkbox"
                              name="newtab"
                              style={{ fontSize: "16px", color: "#000000" }}
                            >
                              {"New tab"}
                            </Checkbox>
                          }
                          name="newtab"
                          control={control}
                          onChange={([e]) => {
                            onChangeFormData("newtab", e.target.checked);
                            return e.target.checked;
                          }}
                        />
                      </Row>
                    </React.Fragment>
                  )}
                </div>
              </EditModel>
            </React.Fragment>
          </form>
        );
    }

    // If we are not in edit mode, remove the input field
    return (
        <PluginTooltip
            show={showTitle}
            title={'Submit button'}
        >

            <div style={{ textAlign: 'center', padding: '10px 0' }} ref={btn} >
                <button
                    type={isPreviewPage() ? 'submit' : 'button'}
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
                    // disabled={getDisabledButton()}
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
                <Controller
                    as={
                        <input
                            type="hidden"
                        />
                    }
                    name="disabled"
                    control={control}
                    onBlur={() => {
                        trigger('disabled');
                    }}


                />
            </div>
        </PluginTooltip>

    );
});

export default withLocalize(SubmitButton);