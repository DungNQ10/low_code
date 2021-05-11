import React, {Component} from 'react'
import RadioComponent from './components/radioComponent'
import RadioComponentConfig from './components/radioComponentConfig'



export default  {
  Renderer: (props) => {
    return (<RadioComponent {...props} />)
  },
  id: 'radio',
  title: 'Radio',
  description: 'Radio Box',
  version: 1,
  controls: {
    type: 'custom',
    Component: (props)=>(
      <RadioComponentConfig {...props} />
    )
  },
};