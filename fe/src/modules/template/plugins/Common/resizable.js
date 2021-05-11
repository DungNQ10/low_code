import React, {  } from 'react';
import PropTypes from 'prop-types';
import './style.scss';

class Resizable extends React.Component {
    constructor(props) {
      super(props);
      this.resizer = React.createRef();
      this.xPos = 0;
    }
    componentDidMount(){
      window.addEventListener('mousemove', this.onMouseMove.bind(this), false);
      window.addEventListener('mouseup', this.onMouseUp.bind(this), false);
    }
    componentWillUnmount(){
      window.removeEventListener('mousemove', this.onMouseMove.bind(this), false);
      window.removeEventListener('mouseup', this.onMouseUp.bind(this), false);
    }
   
    onMouseDown(e) {
    this.xPos =e.clientX;
    this.yPos = e.clientY;
      this.props.updateStateResizing( this.props.id, true);
    }
    onMouseMove(e) {
      if( this.props.isResizing ){
          var moveX =  e.clientX - this.xPos;
          var moveY = e.clientX - this.yPos;
        this.props.funcResizing( this.props.id,  moveX, moveY);
      }
    }
    onMouseUp() {
      if( this.props.isResizing ){
        this.props.updateStateResizing( this.props.id, false);
      }
    }
    render() {
      const style = {
        width:  this.props.resizerWidth||200,
        height: this.props.resizerHeight||200,
      };
      return (
        <div className="resizer" ref={this.resizer}
              style={style}
              onMouseDown={this.onMouseDown.bind(this)}
          >
             {this.props.children}
          </div>
      );
    }
  };
  Resizable.propTypes = {
    id:                   PropTypes.string.isRequired,
    // isResizing:           React.PropTypes.bool.isRequired,
    // funcResizing:         React.PropTypes.func.isRequired,
    // updateStateResizing:  React.PropTypes.func.isRequired,
    // resizerWidth: PropTypes.number.isRequired,
    // resizerHeight: PropTypes.number.isRequired
  };

export default Resizable;