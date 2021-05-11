import { Table } from "antd";
import React, { Component, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { actionDynamicFormList } from "../../store/action";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
const DynamicFormList = (props) => {
  const columns = [
    { key: "name", title: "Name", dataIndex: "name" },
    { key: "description", title: "Description", dataIndex: "description" },
    { key: "tableName", title: "TableName", dataIndex: "tableName" },
    {
      key: "action",
      title: "Action",
      dataIndex: "action",
      render: (text, record, index) => {
        return (
          <div>
            <Link to={`/template/${record.id}`}>
              <EditOutlined />
            </Link>
            <a href="javascript:void(0)">
              <DeleteOutlined />
            </a>
          </div>
        );
      },
    },
  ];
  const dispatch = useDispatch();
  const pageSize = 2;
  const data = useSelector(
    (state) => state.TemplateManagement.dynamicFormList.data
  );
  const [list, setList] = useState([]);
  const [pageIndex, setPageIndex] = useState(1);
  const [total, setTotal] = useState(0);
 
  const loadData = () =>{
    const payload = {
        paging: {
          pageSize: pageSize,
          currentPage: pageIndex,
        },
        search: "",
      };
      dispatch(actionDynamicFormList(payload));
  }
  useEffect(() => {
    loadData();
  }, []);

  useEffect(() => {
    loadData();
  }, [pageIndex]);

  useEffect(() => {
    if (data && data.list) {
      setList(data.list);
      setTotal(data.pager.rowsCount);
    }
  }, [data]);

  return (
    <React.Fragment>
      <Table
        columns={columns}
        pagination={{
          position: ["bottom"],
          pageSize: pageSize,
          total: total || 0,
          onChange: (page,pageSize)=>{
              setPageIndex(page);
          }
        }}
        dataSource={list}
      />
    </React.Fragment>
  );
};

export default DynamicFormList;
