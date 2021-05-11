import React from 'react';
const InlineError = (props) => {
    const {
        errors,
        name,
        style
    } = props;
    if (!!errors && !!errors[name]) {
        return (
            <React.Fragment>
                <div>
                    <span style={style||{}} className="label-error">{errors[name].msg}</span>
                </div>
            </React.Fragment>

        );
    }
    else {
        return (
            <React.Fragment></React.Fragment>
        );
    }

};

export default InlineError;