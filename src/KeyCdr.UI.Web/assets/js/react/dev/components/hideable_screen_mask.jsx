import React from 'react';
import ReactDOM from 'react-dom';

class HideableScreenMask extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        var css = (this.props.MaskIsVisible) ? "loading_mask_screen" : "loading_mask_screen_hide";
        return (
            <div id="screen_mask" className={css}></div>
        )
    }
}

export default HideableScreenMask;
