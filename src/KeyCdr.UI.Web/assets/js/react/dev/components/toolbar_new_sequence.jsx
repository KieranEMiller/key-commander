import React from 'react';
import ReactDOM from 'react-dom';

import Stopwatch from './stopwatch.jsx';

class ToolbarNewSequence extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        var buttonText = (this.props.MaskIsVisible == true)
            ? "Theatre Mode Off" : "Theatre Mode On";

        var toolbarVisibilityCss = (this.props.IsRunning == true)
            ? "toolbar_top_center" : "toolbar_top_center_hide";

        return (
            <div id="screen_mask_toolbar" className={toolbarVisibilityCss}>
                <input onClick={this.props.onToggleMask} className="button-size-small" type="button" value={buttonText} />
                <input onClick={this.props.onStopSequence} className="button-size-small" type="button" value="Stop Sequence" />
                <Stopwatch IsRunning={this.props.IsRunning} />
            </div>
        );
    }
};
export default ToolbarNewSequence;