import React, { useState, useRef, useEffect } from "react";
import "../../Common/style.scss";
import PluginTooltip from "../../Common/pluginTooltip";
import DatePicker from "../../../../../app/components/datepicker";
import { withLocalize } from "react-localize-redux";
import * as yup from "yup";
import { useForm, Controller } from "react-hook-form";
import InputText from "../../../../../app/components/input";
import { useSelector, useDispatch } from "react-redux";
import TYPES, { PageMode } from "../../../store/constant";
import {
    actionUpdateFormData,
    actionDispatchAction,
    actionRemoveDispatchAction
} from '../../../store/action';
import { yupResolver } from "@hookform/resolvers/yup";
import { useParams } from "react-router-dom";
import { DispatchEvent } from "../../../constants";
import { Button, Row, Table, Modal,notification } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
const { post, get,del } = require("../../../../../app/common/apiUtil");
const {confirm} = Modal;
const GridComponent = React.memo((props) => {
  console.log(props);
  const { onChange, readOnly } = props;
  const dispatch = useDispatch();
  const formData = useSelector((state) => {
    return state.TemplateManagement.formData;
  });
  const [searchData, setSearchData] = useState({});
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(false);

  let { id } = useParams();
  const oryStore = useSelector((state) => state.TemplateManagement.oryStore);
  const eventSubcribe = useSelector(
    (state) => state.TemplateManagement.dispatchAction.data
  );
  let item = props.data || {
    pageSize:10,
    pageIndex:1
  };
  const [columns, setColumns] = useState([]);
  const [dataSource, setDataSource] = useState([]);
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const getValidateSetting = function () {
    if (readOnly) {
      return yup.object().shape();
    } else return yup.object().shape();
  };

  const { errors, control, getValues, setValue, trigger } = useForm({
    validateCriteriaMode: "all",
    mode: "onChange",
    resolver: yupResolver(getValidateSetting()),
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

  const setFormData = (value) => {
    setValue("fieldValue", value);
  };
  const setFormKey = (value) => {
    item.formKey = value;
  };

  const getList = ()=>{
    
    if(oryStore.pageMode!=PageMode.FunctionPage)
      return;
    var {listApi } = item;
    var params = {

    };
    listApi.parameters = listApi.parameters||[];
    listApi.parameters.map(c=>{
      params[c.name] = searchData[c.mapping];
    });
    
    params.paging = {
      pageSize:item.pageSize||10,
      currentPage:item.pageIndex||1
    }
    var url = listApi.apiUrl.indexOf('http')==0?listApi.apiUrl:  process.env.REACT_APP_API_URL + listApi.apiUrl;
    setLoading(true);
    if(listApi.method=='Post'){
     var k =  post(url,params);
     k.then(response=>{
       console.log(response);
       setDataSource(response.data.list);
       setTotal(response.data.pager.rowsCount);
       setLoading(false);
     })
    }

    console.log(params);
  }

  const execSearch = ()=>{
    dispatch(actionDispatchAction('search'));
  }
  useEffect(() => {
    item.name = item.name || "";
    item.getSubmitData = getSubmitData;
    item.validateSubmitEvent = validateSubmitEvent;
    item.setFormData = setFormData;
    item.setFormKey = setFormKey;
    setFormValue();
    onChange(item);
    execSearch();
  }, []);
  useEffect(() => {
    console.log('formdata',formData);
    var data =  formData.get(item.formKey);
    if(data)
      setSearchData(data.form);

     
  }, [formData]);

  useEffect(() => {
    if (item.subscribeEvent && eventSubcribe.get(item.subscribeEvent)) {
      console.log("dispatch event subcrible:", item.subscribeEvent);
      //todo: search data
      console.log("template data",item);
      dispatch( actionRemoveDispatchAction(item.subscribeEvent));
      getList();
    }
    else if(item.subscribeEvent && eventSubcribe.get('research')){
      item.pageIndex = 1;
      onChange(item);
      execSearch();
      dispatch( actionRemoveDispatchAction('research'));
    }
  }, [eventSubcribe]);

  const onChangeFormDataStore = (e, name, onChangeFormField) => {
    onChangeFormField(e.target.value);
    trigger(name);
    if (readOnly) {
      let data = `{"${item.mappingField}":"${e.target.value}"}`;
      dispatch(actionUpdateFormData(TYPES.FORMDATA.FORM, id, JSON.parse(data)));
      console.log(item.formKey);
      if (item[DispatchEvent.ON_CHANGE]) {
        dispatch(actionDispatchAction(item[DispatchEvent.ON_CHANGE]));
        console.log("dispatch event onchange:", item[DispatchEvent.ON_CHANGE]);
      }
    }
  };

  const pushAction = (action, data)=>{
    console.log(action,data);
    switch (action) {
      case 'edit':
        dispatch(
          actionUpdateFormData(TYPES.FORMDATA.FORM, item.formKey, { isModalVisible: true, itemId:data.id })
        );
        break;
      case 'delete':
        confirm({
          content:'Bạn có chắc chắn muốn xóa không?',
          onOk:()=>{
            deleteItem(data);
          }
        })
      break;
      default:
        break;
    }
  }

  const deleteItem = (data)=>{
    if(oryStore.pageMode!=PageMode.FunctionPage)
    return;
    var {deleteApi } = item;
    var params = {

    };
    deleteApi.parameters = deleteApi.parameters||[];
    deleteApi.parameters.map(c=>{
      params[c.name] = data[c.mapping];
    });
    var url = deleteApi.apiUrl.indexOf('http')==0?deleteApi.apiUrl:  process.env.REACT_APP_API_URL + deleteApi.apiUrl;
    var k = undefined;
    switch (deleteApi.method) {
      case 'Post':
        k = post(url,params);
        break;
      case 'Delete':        
        k = del(url, deleteApi.parameters.length==1? data[deleteApi.parameters[0].mapping]: params)  
      break;
        
      default:
        break;
    }
    if(k){
     k.then(response=>{
       console.log(response);
       dispatch( actionDispatchAction(item.subscribeEvent));
       notification.success({
         message:'Xóa dữ liệu thành công.'
       })
     })
    }

    console.log(params);

  }
  useEffect(() => {
    if (item.columns) {
      let cols = [];
      item.columns.map((c) => {
        var col = {
          title: c.label,
          dataIndex: c.field,
        };
        if(c.width)
        {
          col.width = parseInt(  c.width);
        }
          
        cols.push(col);
      });
      let dtSource = [];
      for (let index = 0; index < 100; index++) {
        let dt = {};
        item.columns.map((c) => {
          dt[c.field] = c.label + " " + index;
        });
        dtSource.push(dt);
      }
      if(oryStore.pageMode==PageMode.PreviewPage||oryStore.pageMode==PageMode.TemplatePage)
        setDataSource(dtSource);
      
      //action column
      cols.push({
        title:'Thao tac',
        dataIndex:'id',
        width:200,
        render:(text,record, index)=>{
          console.log(text,record,index);
          return (
            <div style={{textAlign:'center'}}>
              <Button shape="round" icon={<EditOutlined/>} onClick={()=>{pushAction('edit',record)}} size="small"></Button>
              <Button shape="round" icon={<DeleteOutlined />} onClick={()=>{pushAction('delete',record)}} size="small"></Button>
            </div>
          )
        }
      });
      setColumns(cols);
    }
  }, [item.columns]);

  const onSelectChange = (selectedRowKeys) => {
    console.log("selectedRowKeys changed: ", selectedRowKeys);
    setSelectedRowKeys(selectedRowKeys);
  };
  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange,
  };

  const onPageIndexChange = (pageIndex, pageSize)=>{
    item.pageIndex = pageIndex;
    item.pageSize = pageSize;
    onChange(item);
    execSearch();
  };

  const onPageSizeChange = (pageIndex, pageSize)=>{
    item.pageIndex = pageIndex;
    item.pageSize = pageSize;
    onChange(item);
    execSearch();
  }


  return (
    <div style={{ padding: "10px 0" }}>
      <Table
        columns={columns}
        dataSource={dataSource}
        rowKey={item.rowKey}
        rowSelection={item.hasSelect == true ? rowSelection : null}
        loading={loading}
        pagination={{
          showSizeChanger:true,
          showQuickJumper:true,
          pageSize :item.pageSize,
          current:item.pageIndex,
          total:total,
          onChange:onPageIndexChange,
          onShowSizeChange:onPageSizeChange

        }}
      />
    </div>
  );
});

export default withLocalize(GridComponent);
