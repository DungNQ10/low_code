import React, {Component} from 'react'
import SubmitButton from './components/submitButtonComponent'

import SubmitButtonConfig from './components/submitButtonConfig'

export default  {
  Renderer: (props) => {
    return (<SubmitButton {...props} />)
  },
  id: 'submitButton',
  title: 'Submit Button',
  description: 'Submit Button',
  version: 1,
  controls: {
    type: 'custom',
    Component: (props)=>(
      <SubmitButtonConfig {...props} />
    )
  },
};
 