import React, { useState } from 'react'
import { useForm, Controller, useFieldArray } from 'react-hook-form'
import { PlusCircleOutlined } from '@ant-design/icons'
import { Row, Input, Drawer, Select, Checkbox, Col } from 'antd';
import './style.scss';
import { Button } from 'antd';
const Create = (props) => {


    const [visibleUpdate, setVisibleUpdate] = useState(false);
    const [columnData, setColumnData] = useState({});

    const dataTypes = [
        { value: 'int', label: 'int' },
        { value: 'bigint', label: 'bigint' },
        { value: 'decimal', label: 'decimal' },
        { value: 'bit', label: 'bit' },
        { value: 'nvarchar', label: 'text' },
        { value: 'datetime', label: 'datetime' }
    ]
    const { handleSubmit, errors, getValues, setValue, control, trigger,watch,formState } = useForm({
        validateCriteriaMode: 'all',
        mode: 'onChange',
        //validationSchema: getValidateSetting()

    });

    const watchDataType = watch('dataType')
    const { fields, append, prepend, remove, swap, move, insert } = useFieldArray({
        control,
        name: "columns"
    });

    //submit data
    const submitForm = (values) => {
        console.log(values,fields);
    }
    const onAddNewColumn = () => {
        setVisibleUpdate(true);
    }

    const onSaveColumn = () => {
        console.log(errors);
        if(formState.isValid){
            let formValue = getValues();
            let column = {
                columnName:formValue.columnName,
                dataType:formValue.dataType,
                nullable:formValue.nullable,
                length:formValue.length,
                precision:formValue.precision,
                scale:formValue.scale,
            }
            fields.push(column);
            setVisibleUpdate(false);
        }
    };

    const formatDataTypeColumn =(item)=>{
        if(item.dataType=='decimal')
            return `${item.dataType}(${item.precision},${item.scale})`;
        else if(item.dataType=='nvarchar'){
            return `${item.dataType}(${item.length})`;
        }
        
        return item.dataType;
    };
    const updateColumn = (column) => {


        return (
            <React.Fragment>
                <Drawer
                    title="Add new column"
                    placement="right"
                    closable={true}
                    onClose={() => setVisibleUpdate(false)}
                    visible={visibleUpdate}
                    maskClosable={false}
                    width={500}
                    footer={
                        <>
                            <Row>
                                <Button
                                    type="primary"
                                    onClick={onSaveColumn}
                                >
                                    OK
                                </Button>

                                <Button
                                    onClick={() => setVisibleUpdate(false)}
                                    style={{ margin: '0 10px' }}
                                >
                                    Cancel
                                </Button>
                            </Row>
                        </>
                    }
                >
                    <Row className="form-row">
                        <Controller
                            name="columnName"
                            placeholder="Column Name"
                            control={control}
                            onChange={
                                () => trigger('columnName')
                            }

                            as={
                                <Input
                                    type="text"
                                />
                            }
                        />
                    </Row>
                    <Row className="form-row">
                        <Controller
                            name="dataType"
                           
                            control={control}
                             render ={({onChange,name, onBlur, value})=>(
                                <Select
                                    type="text"
                                    options={dataTypes}
                                    style={{ width: '100%' }}
                                    value={value}
                                    onChange={(value)=>{
                                        
                                        onChange(value);
                                    }}
                                    placeholder="data type"
                                    name={name}
                                >

                                </Select>
                                )
                            }
                        />
                    </Row>
                    <Row className="form-row" style={{ display: (watchDataType == 'nvarchar') ? 'flex' : 'none' }}>
                        <Controller
                            name="length"
                            placeholder="length"
                            control={control}
                            as={
                                <Input
                                    type="number"
                                />
                            }
                        />
                    </Row>

                    <Row className="form-row" style={{ display: (watchDataType == 'decimal') ? 'flex' : 'none' }}>
                        <Col span={11}>
                            <Controller
                                name="precision"
                                placeholder="precision"
                                control={control}
                                as={
                                    <Input
                                        type="number"
                                    />
                                }
                            />
                        </Col>
                        <Col span={2} />
                        <Col span={11}>
                            <Controller
                                name="scale"
                                placeholder="scale"
                                control={control}
                                as={
                                    <Input
                                        type="number"
                                    />
                                }
                            />
                        </Col>

                    </Row>

                    <Row className="form-row">
                        <Controller
                            name="nullable"
                            placeholder="nullable"
                            control={control}
                            as={
                                <Checkbox
                                    style={{ width: '100%' }}
                                >
                                    Nullable
                               </Checkbox>


                            }
                        />
                    </Row>

                </Drawer>
            </React.Fragment>
        )
    }


    return (
        <React.Fragment>
            <form onSubmit={handleSubmit(submitForm)} >
                <Row>
                    <Controller
                        as={<Input
                            type='text'
                        ></Input>}
                        name='tableName'
                        control={control}
                    >

                    </Controller>
                </Row>
                <table className="fixtable">
                    <thead>
                        <th className="index_column" >
                            #
                        </th>
                        <th>
                            Column name
                        </th>
                        <th>
                            Data type
                        </th>
                        <th>
                            Nullable
                        </th>
                        <th>
                            <a href="javascript:void(0)" onClick={onAddNewColumn}>
                                <PlusCircleOutlined />
                            </a>
                        </th>
                    </thead>
                    {fields.map((item, index) => {
                        return (
                            <tr key={`row${index}`}>
                                <td>
                                    {index + 1}
                                </td>
                                <td>
                                    {item.columnName}
                                </td>
                                <td>
                                    {formatDataTypeColumn(item)}
                                </td>
                                <td style={{width:60, textAlign:'center'}}>
                                    <Checkbox
                                        checked={item.nullable}
                                        disabled={true}
                                    >

                                    </Checkbox>
                                </td>
                                <td style={{width:200, textAlign:'center'}}>
                                    
                                </td>
                            </tr>
                        )
                    })}
                </table>
                {updateColumn()}

                <Row style={{textAlign:'right'}}>
                    <Button type="primary"
                     onClick={handleSubmit(submitForm)}
                    >
                        Save
                    </Button>
                </Row>
            </form>
        </React.Fragment>
    )
}

export default Create;