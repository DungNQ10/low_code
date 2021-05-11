import React, {  } from 'react';
import { withLocalize } from 'react-localize-redux';




const EmptyContent = (props) => {
    const {
        isPreviewMode
    } = props;
 
    if (!isPreviewMode) {

        return (
            <React.Fragment>
               <div style={{width:'100%', height:20}}></div>
            </React.Fragment>

        );
    }

    // If we are not in edit mode, remove the input field
    return (
      <React.Fragment></React.Fragment>
    );
};

export default withLocalize(EmptyContent);