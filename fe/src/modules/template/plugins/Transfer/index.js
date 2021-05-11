import React, { Component } from 'react'
import Transfer from './components'
import { MedicineBoxOutlined } from '@ant-design/icons';



export default {
  Component: Transfer,
  //Icon display on toolbar
  //   IconComponent: <StarIcon />,
  //component name to check when use in editor
  name: 'Transfer',
  version: '0.0.1',
  //text display on toolbar
  text: 'Transfer',
  description: 'Transfer',
  IconComponent: <MedicineBoxOutlined />

}