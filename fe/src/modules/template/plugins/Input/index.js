import React, {Component} from 'react'
import InputComponent from './components/InputComponent';
import InputComponentConfig from './components/InputComponentConfig';

export default  {
  Renderer: (props) => {
    return (<InputComponent {...props} />)
  },
  id: 'inputText',
  title: 'Input text',
  description: 'Input text',
  version: 1,
  controls: {
    type: 'custom',
    Component: (props)=>(
      <InputComponentConfig {...props} />
    )
  },
};
 