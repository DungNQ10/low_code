//  import  { createEmptyState, Provider } from "@react-page/core/lib-es";
import Editor, {  Value } from "@react-page/editor";
import "@react-page/editor/lib/index.css";
import "@react-page/core/lib/index.css";
import { Trash, ThemeProvider } from "@react-page/ui";

import "@react-page/ui/lib/index.css";
import React, { useEffect, useRef, useState } from "react";
import { withLocalize } from "react-localize-redux";
import EmptyContent from "../../plugins/EmptyContent";
import Space from "../../plugins/Space";
// import CkEditorField from '../../../../app/components/CkEditorField';
import "../../style.scss";

// import Toolbar from "./customToolbar";
// import DisplayModeToggle from "./customDisplayMode";
// import Editable from "./customEditor/Editable";
import { useDispatch } from "react-redux";
import "../../plugins/Common/style.scss";
import SubmitButton from "../../plugins/SubmitButton";
import InputPlugin from "../../plugins/Input";
import DatePickerPlugin from "../../plugins/DatePicker";
import SelectBoxPlugin from "../../plugins/SelectBox";
import InputNumberPlugin from "../../plugins/InputNumber";
import CheckBoxPlugin from "../../plugins/CheckBox";
import RadioPlugin from "../../plugins/Radio";
import { SlidePlugin, TransferPlugin } from "../../plugins";
import { PageMode } from "../../store/constant";
import FormModal from "../../plugins/FormModal";
import Grid from "../../plugins/Grid";

const plugins = [
  Space,
  InputPlugin,
  SubmitButton,
  SelectBoxPlugin,
  RadioPlugin,
  FormModal,
  Grid
  // , SubmitButton, EmptyContent,
  //   InputPlugin, DatePickerPlugin, SelectBoxPlugin, InputNumberPlugin,
  //   CheckBoxPlugin, RadioPlugin, SlidePlugin, TransferPlugin, FormModal
];

const isMobile = {
  Android: function () {
    return navigator.userAgent.match(/Android/i);
  },
  BlackBerry: function () {
    return navigator.userAgent.match(/BlackBerry/i);
  },
  iOS: function () {
    return navigator.userAgent.match(/iPhone|iPad|iPod/i);
  },
  Opera: function () {
    return navigator.userAgent.match(/Opera Mini/i);
  },
  Windows: function () {
    return navigator.userAgent.match(/IEMobile/i);
  },
  any: function () {
    return (
      isMobile.Android() ||
      isMobile.BlackBerry() ||
      isMobile.iOS() ||
      isMobile.Opera() ||
      isMobile.Windows()
    );
  },
};
const pluginNames = {
  term: "term",
  address: "Address",
  Registration_Form: "Registration_Form",
  Additional_Information: "Additional_Information",
  Submit_Button: "Submit_Button",
  Survey: "Survey",
  Agency: "Agency",
  CarMaster: "CarMaster",
};
const findNested = function (obj, key, memo) {
  var i,
    proto = Object.prototype,
    ts = proto.toString,
    hasOwn = proto.hasOwnProperty.bind(obj);

  if ("[object Array]" !== ts.call(memo)) memo = [];

  for (i in obj) {
    if (hasOwn(i)) {
      if (i === key) {
        memo.push(obj[i]);
      } else if (
        "[object Array]" === ts.call(obj[i]) ||
        "[object Object]" === ts.call(obj[i])
      ) {
        findNested(obj[i], key, memo);
      }
    }
  }

  return memo;
};

const findPlugins = function (data) {
  let res = findNested(data, "default");
  let result = [];
  for (let index = 0; index < res.length; index++) {
    const item = res[index];
    result.push(item);
  }

  return result;
};

const localEditorValue = {}; // createEmptyState();

// const editor = new Editor({
//   plugins,
//   editables: [localEditorValue],
//   defaultPlugin: EmptyContent,
// });
const customComponent = (props)=>{
  return (
    <React.Fragment>
   {props.open&&<div>
    {props.pluginControls.props.children}
  </div>}
  </React.Fragment> 
  )
}
const OryEditor = React.memo(function ({
  onChange,
  editorValue,
  globalStore,
  formKey,
  ...props
}) {
  // const [loadedControl, setLoadedControl] = useState(false);
  var display = {
    edit: true,
    insert: true,
    move: true,
    resize: true,
    preview: true,
  };

  useEffect(() => {
    // setTimeout(() => {

    // }, 100);
    if (editorValue && editorValue.cells) {
     
    }

  }, [editorValue]);
  const [value, setValue] = useState(null);
  useEffect(() => {
   
  }, [globalStore.pageMode]);

  //const refProvider = useRef();
  const editorRef = function () {
    return {
      value: editorValue,
      getPlugins: function () {
        return findPlugins(editorValue);
      },
      //validate when submit event
      validateSubmitEvent: async function () {
        let plugins = findPlugins(editorValue);
        let valid = true;
        for (let index = 0; index < plugins.length; index++) {
          const item = plugins[index];
          if (!!item.validateSubmitEvent) {
            let validItem = await item.validateSubmitEvent();
            if (validItem == false) valid = false;
          }
        }
        return valid;
      },
      //validate when saveEvent
      validateEvent: function () {
        //TODO: validate event
        return true;
      },
      getSubmitData: function () {
        let plugins = findPlugins(editorValue);
        let res = [];
        for (let index = 0; index < plugins.length; index++) {
          const plugin = plugins[index];
          if (plugin.state.getSubmitData)
            res.push(plugin.state.getSubmitData());
        }

        return res;
      },
      validateSaveTemplate() {
        let errors = [];
        return errors;
      },
      validateSaveEvent: async function () {
        let plugins = findPlugins(editorValue);
        let usedPlugins = plugins.filter(
          (c) => c.plugin.name !== "EmptyContent"
        );
        let result = true;
        for (let index = 0; index < usedPlugins.length; index++) {
          const c = usedPlugins[index];
          if (c.state && c.state.validateSaveEvent) {
            let valid = await c.state.validateSaveEvent(c.state);
            if (valid == false) result = false;
          }
        }

        return result;
      },
      setDisabledSubmit: function () {
        let plugins = findPlugins(editorValue);
        let buttons = plugins.filter(
          (c) => c.plugin.name == pluginNames.Submit_Button
        );
        if (buttons.length > 0) {
          for (let index = 0; index < buttons.length; index++) {
            const btn = buttons[index];
            if (btn.state.setDisabledButton) btn.state.setDisabledButton(true);
          }
        }
      },
      doAfterSubmit: function () {
        let plugins = findPlugins(editorValue);
        let buttons = plugins.filter(
          (c) => c.plugin.name == pluginNames.Submit_Button
        );
        if (buttons.length > 0) {
          for (let index = 0; index < buttons.length; index++) {
            const btn = buttons[index];
            if (btn.state.doAfterSubmit) btn.state.doAfterSubmit();
          }
        }
      },
      setFormData(data) {
        let plugins = findPlugins(editorValue);
        for (let index = 0; index < plugins.length; index++) {
          const item = plugins[index];
          console.log(item);
          if (!!item.setFormData) {
            if (item.mappingField && data)
              item.setFormData(data[item.mappingField]);
          }
        }
      },
      setFormKey(data) {
        
        let plugins = findPlugins(editorValue);
        for (let index = 0; index < plugins.length; index++) {
          const item = plugins[index];
          console.log(item);
          if (item.setFormKey) {
            item.setFormKey(data);
          }
        }
      },
    };
  };

  useEffect(() => {
    props.editorRef.current = editorRef();
    return () => {
      props.editorRef.current = null;
    };
  });
  console.log(props.editorRef.current);

  const dispatch = useDispatch();

  const isPreviewPage = () => {
    if (globalStore && globalStore.pageMode == PageMode.FunctionPage) {
      {
        return true;
      }
    }
    return false;
  };

  return (
    <React.Fragment>
      <div
        className={`template-page-main ${
          isMobile.any() ? "" : "desktop-screen"
        }`}
        style={{
          backgroundColor: isPreviewPage()
            ? props.templateBackground || "unset"
            : "unset",
        }}
      >
        <div className="section-group">
          <div
            className="event-viewer shadow cmc-create-container"
            style={{ backgroundColor: props.templateBackground || "unset" }}
          >
            <Editor
              cellPlugins={plugins}
              value={editorValue}
              onChange={onChange}
              readOnly={isPreviewPage()}
              // components={{BottomToolbar:customComponent}}
            />
          </div>
        </div>
      </div>
    </React.Fragment>
  );
});

export default withLocalize(OryEditor);
