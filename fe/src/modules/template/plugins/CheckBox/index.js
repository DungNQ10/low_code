import React, {Component} from 'react'
import CheckBox from './components'
import { MedicineBoxOutlined } from '@ant-design/icons';

// import icon from '../../../../../src/app/assets/images/toolbaricon/Button.svg'

// You are obviously not limited to material-ui, but we really enjoy
// the material-ui svg icons!
// import StarIcon from 'material-ui/svg-icons/toggle/star'

// This is the ReactJS component which you can find below this snippet


export default {
  Component: CheckBox,
  //Icon display on toolbar
//   IconComponent: <StarIcon />,
  //component name to check when use in editor
  name: 'Check_Box',
  version: '0.0.1',
  //text display on toolbar
  text: 'Check Box',
  description:'Check Box',
  IconComponent:<MedicineBoxOutlined />

}