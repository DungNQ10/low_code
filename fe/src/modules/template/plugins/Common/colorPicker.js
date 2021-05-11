import React from 'react';
import reactCSS from 'reactcss';
import { ChromePicker } from 'react-color';

class ColorPicker extends React.Component {
 constructor(props){
     super(props);
     this.state = {
        displayColorPicker: false,
        color: {
          
        },
      };
 }
  

  handleClick = () => {
    this.setState({ displayColorPicker: !this.state.displayColorPicker });
  };

  handleClose = () => {
    this.setState({ displayColorPicker: false });
  };

  handleChange = (color) => {
    this.setState({ color: color.rgb });
    if(this.props.onChange){
        this.props.onChange(color);
    }
  };

  render() {
    
    let color = {hex:this.props.color||''};
    console.log(color);
    const styles = reactCSS({
      'default': {
        color: {
          width: '36px',
          height: '14px',
          borderRadius: '2px',
          background: `${color.hex}`,
        },
        swatch: {
          padding: '5px',
          background: '#fff',
          borderRadius: '1px',
          boxShadow: '0 0 0 1px rgba(0,0,0,.1)',
          display: 'inline-block',
          cursor: 'pointer',
        },
        popover: {
          position: 'absolute',
          zIndex: '2',
        },
        cover: {
          position: 'fixed',
          top: '0px',
          right: '0px',
          bottom: '0px',
          left: '0px',
        },
      },
    });
  
    return (
      <div>
        <div style={ styles.swatch } onClick={ this.handleClick }>
          <div style={ styles.color } className="color-picker-content" />
        </div>
        { this.state.displayColorPicker ? <div style={ styles.popover }>
          <div style={ styles.cover } onClick={ this.handleClose }/>
          <ChromePicker color={ color } onChange={ this.handleChange } disableAlpha={true} width={120} height={120} />
        </div> : null }

      </div>
    );
  }
}

export default ColorPicker;