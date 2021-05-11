import React from 'react';
import Divider from './components';
import icon from '../../../../../src/app/assets/images/toolbaricon/Divider.svg';

// You are obviously not limited to material-ui, but we really enjoy
// the material-ui svg icons!
// import StarIcon from 'material-ui/svg-icons/toggle/star'

// This is the ReactJS component which you can find below this snippet


export default {
  Component: Divider,
  //Icon display on toolbar
//   IconComponent: <StarIcon />,
  //component name to check when use in editor
  name: 'Divider',
  version: '0.0.1',
  //text display on toolbar
  text: '구분 선',
  description:'가로 선 구분',
  IconComponent:<img src={icon}  />

};