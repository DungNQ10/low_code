import React, {Component} from 'react'
import GridComponentConfig from './components/GridComponentConfig';
import GridComponent from './components/GridComponent';

export default  {
  Renderer: (props) => {
    return (<GridComponent {...props} />)
  },
  id: 'Grid',
  title: 'Grid',
  description: 'Grid component',
  version: 1,
  controls: {
    type: 'custom',
    Component: (props)=>(
      <GridComponentConfig {...props} />
    )
  },
};
 