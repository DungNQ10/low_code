import React, {Component} from 'react'
import FormModalComponent from './components/FormModalComponent';
import FormModalComponentConfig from './components/FormModalComponentConfig';

export default  {
  Renderer: (props) => {
    return (<FormModalComponent {...props} />)
  },
  id: 'FormModal',
  title: 'Form Modal',
  description: 'Form Modal',
  version: 1,
  controls: {
    type: 'custom',
    Component: (props)=>(
      <FormModalComponentConfig {...props} />
    )
  },
};
 