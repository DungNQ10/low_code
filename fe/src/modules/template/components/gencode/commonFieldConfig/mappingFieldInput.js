import React from 'react';
import { Row, Select } from 'antd';
const MappingFieldInput = (props)=>{

    const fieldConfigMethods = [
        {label:"input",value:"input"},
        {label:'from table',value:'table'}
    ]
    const tables = [
        'table1', 'table2', 'table3'
    ]

    return(
        <React.Fragment>
            <Row>
                <Select
                    options = {fieldConfigMethods}
                >   
                </Select>
            </Row>
            <Row>
                <Select
                    options = {fieldConfigMethods}
                    placeholder="select table"

                >   
                </Select>
            </Row>
        </React.Fragment>
    )
}
export default MappingFieldInput