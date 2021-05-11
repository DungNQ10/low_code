
import React from 'react';
import { Drawer } from '@material-ui/core';
import OutsideClick from './outsideClick';
import './style.scss';

const TextAreaContent = (props) => {
    const {
        value
    } = props;

    let t = value||"";
    t = t.replace(/\n/g, "<br/>");


    return (
        <React.Fragment>
          <div dangerouslySetInnerHTML={{__html:t}}></div>
        </React.Fragment>

    )
}

export default TextAreaContent