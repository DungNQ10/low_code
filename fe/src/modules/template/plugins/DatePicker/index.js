import React, {Component} from 'react'
import InputDatePlugin from './components'
import { MedicineBoxOutlined } from '@ant-design/icons';

// import icon from '../../../../../src/app/assets/images/toolbaricon/Button.svg'

// You are obviously not limited to material-ui, but we really enjoy
// the material-ui svg icons!
// import StarIcon from 'material-ui/svg-icons/toggle/star'

// This is the ReactJS component which you can find below this snippet


export default {
  Component: InputDatePlugin,
  //Icon display on toolbar
//   IconComponent: <StarIcon />,
  //component name to check when use in editor
  name: 'InputDatePicker',
  version: '0.0.1',
  //text display on toolbar
  text: 'Input date',
  description:'Input date',
  IconComponent:<MedicineBoxOutlined />

}