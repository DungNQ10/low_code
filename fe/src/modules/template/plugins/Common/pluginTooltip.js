import React from 'react';
import { Tooltip } from 'antd';
const PluginTooltip = (props) => {
    const {
        show,
        title,
        children
    } = props;



    return (
        <React.Fragment>
            {show==true?(
                <Tooltip
                    title={title}
                >
                     {children}
                </Tooltip>

            ):(
                <React.Fragment>
                {children}
                </React.Fragment>
            )}
            
        </React.Fragment>

    );
};

export default PluginTooltip;