import React, {Component} from 'react'
import SelectBox from './components/SelectBoxComponent'
import SelectBoxConfig from './components/SelectBoxConfig'
// import icon from '../../../../../src/app/assets/images/toolbaricon/Button.svg'

// You are obviously not limited to material-ui, but we really enjoy
// the material-ui svg icons!
// import StarIcon from 'material-ui/svg-icons/toggle/star'

// This is the ReactJS component which you can find below this snippet



export default  {
  Renderer: (props) => {
    return (<SelectBox {...props} />)
  },
  id: 'selectBox',
  title: 'Select Box',
  description: 'Select Box',
  version: 1,
  controls: {
    type: 'custom',
    Component: (props)=>(
      <SelectBoxConfig {...props} />
    )
  },
};