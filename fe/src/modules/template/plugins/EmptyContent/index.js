import React, {Component} from 'react'
import EmptyContent from './components';

export default  {
  Renderer: (props) => {
    return (<EmptyContent {...props} />)
  },
  id: 'emptyContent',
  title: 'Empty Content',
  description: 'Empty Content',
  version: 1 
};
 