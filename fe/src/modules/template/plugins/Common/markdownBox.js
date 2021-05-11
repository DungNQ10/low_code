import React from 'react';
import './style.scss';

const MarkDownBox = ({
    label,
    labelClass,
    boxClass,
    ...props
}) => {
    const {
        children,
        
    } = props;



    return (
        <React.Fragment>
            <section className="code-box-meta markdown">
                <div  className={`code-box-title ${labelClass||''}`}>
                    <a>{label}</a>
                </div>
                <div className={`code-box-description ${boxClass||''}`}>
                    {children}
                </div>
            </section>
        </React.Fragment>

    );
};

export default MarkDownBox;