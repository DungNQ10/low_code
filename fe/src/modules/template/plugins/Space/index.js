import React, {Component} from 'react'
import Space from './components'
import { CellPlugin } from '@react-page/editor';
// import icon from '../../../../../src/app/assets/images/toolbaricon/Spacer.svg'

// You are obviously not limited to material-ui, but we really enjoy
// the material-ui svg icons!
// import StarIcon from 'material-ui/svg-icons/toggle/star'

// This is the ReactJS component which you can find below this snippet

const spacePlugin = {
  Renderer: (props) => {
    console.log(props);
    return(
    <span>xin d chao</span>)
  },
  id: 'myFirstCellPlugin',
  title: 'My first cell plugin',
  description: 'My first cell plugin just displays a title',
  version: 1,
  controls: {
    type: 'custom',
    Component: (props)=>{

      console.log(props);
      return (<span>xin chao</span>)
    }
  },
};

export default spacePlugin;
// export default {
//   Component: Space,
//   //Icon display on toolbar
// //   IconComponent: <StarIcon />,
//   //component name to check when use in editor
//   name: 'Space',
//   version: '0.0.1',
//   //text display on toolbar
//   text: 'Space',
//   description:'Space',
//   IconComponent:<img src={''} style={{}} />

// }