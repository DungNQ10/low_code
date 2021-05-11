import React, { Component } from 'react'
import Slide from './components'
import { MedicineBoxOutlined } from '@ant-design/icons';



export default {
  Component: Slide,
  //Icon display on toolbar
  //   IconComponent: <StarIcon />,
  //component name to check when use in editor
  name: 'Slide',
  version: '0.0.1',
  //text display on toolbar
  text: 'Slide',
  description: 'Slide',
  IconComponent: <MedicineBoxOutlined />

}