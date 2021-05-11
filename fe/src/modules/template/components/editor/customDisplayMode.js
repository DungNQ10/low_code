import * as React from 'react';
import ToggleEdit from '@react-page/ui/lib/DisplayModeToggle/ToggleEdit/index';
import ToggleInsert from '@react-page/ui/lib/DisplayModeToggle/ToggleInsert/index';
import ToggleLayout from '@react-page/ui/lib/DisplayModeToggle/ToggleLayout/index';
import TogglePreview from '@react-page/ui/lib/DisplayModeToggle/TogglePreview/index';
import ToggleResize from '@react-page/ui/lib/DisplayModeToggle/ToggleResize/index';


const defaultTranslations = {
    edit: 'Edit things',
    insert: 'Add things',
    layout: 'Move things',
    resize: 'Resize things',
    preview: 'Preview result',
};

const defaultDisplay = {
    edit: true,
    insert: true,
    move: true,
    resize: true,
    preview: true
}

const getStickyNessstyle = (stickyness) => {
    if (
        !stickyness ||
        (!stickyness.shouldStickToBottom && !stickyness.shouldStickToTop)
    ) {
        return {
            position: 'fixed',
        };
    }

    return {
        position: 'absolute',
        bottom: stickyness.shouldStickToBottom ? 0 : 'auto',
        top: stickyness.shouldStickToTop ? 0 : 'auto',
        right: -stickyness.rightOffset || 0,
    };
};


const Inner = function ({ stickyNess, translations = defaultTranslations, display = defaultDisplay, ...props }) {
    return (
        <div
            className="ory-controls-mode-toggle-control-group"
            style={{
                position: 'fixed',
                zIndex: 10001,
                bottom: 0,
                right: 0,
                display: 'flex',
                maxHeight: '100%',
                ...getStickyNessstyle(stickyNess),
            }}
        >
            <div
                ref={stickyNess.stickyElRef}
                style={{
                    padding: 16,
                    position: 'relative',

                    flexFlow: 'column wrap',
                    direction: 'rtl',

                    display: 'flex',
                }}
            >
                {display.edit &&
                    <div className="ory-controls-mode-toggle-control">
                        <ToggleEdit label={translations.edit} />
                        <div className="ory-controls-mode-toggle-clearfix" />
                    </div>
                }

                {display.insert &&
                    <div className="ory-controls-mode-toggle-control">
                        <ToggleInsert label={translations.insert} />
                        <div className="ory-controls-mode-toggle-clearfix" />
                    </div>
                }

                {display.move &&
                    <div className="ory-controls-mode-toggle-control">
                        <ToggleLayout label={translations.layout} />
                        <div className="ory-controls-mode-toggle-clearfix" />
                    </div>
                }
                {display.resize &&
                    <div className="ory-controls-mode-toggle-control">
                        <ToggleResize label={translations.resize} />
                        <div className="ory-controls-mode-toggle-clearfix" />
                    </div>}

                {display.preview &&
                    <div className="ory-controls-mode-toggle-control">
                        <TogglePreview label={translations.preview} />
                        <div className="ory-controls-mode-toggle-clearfix" />
                    </div>}


            </div>
        </div>
    )
};

export default Inner;
