import React from 'react';

import { Runtime, RealTimeAnalysisSettings } from '../constants.jsx';

export default class RealTimeAnalysis extends React.Component{
    constructor(props) {
        super(props);

        this.state = {
            //used when measuring changes in what the user typed
            LastTextUsed: this.props.TextEntered,

            //used when measuring "has the text not changed in x ticks"
            LastTextEvaluated: this.props.TextEntered,

            TicksWithSameText: 0,
            IntervalId: -1
        }
    }

    componentDidUpdate() {
        if (this.props.IsRunning == true)
            this.start();

        else if (this.props.IsRunning == false && this.state.IntervalId != -1)
            this.stop();
    }

    componentWillUnmount() { this.stop();}

    start = () => {
        if (this.state.IntervalId == -1) {
            if(Runtime.IS_DEBUG) console.log("real time analysis: starting");

            this.state.IntervalId = setInterval(this.tick, RealTimeAnalysisSettings.TICK_INTERVAL_IN_MS);
        }
    }

    stop = () => {
        if(Runtime.IS_DEBUG) console.log("real time analysis: stopping");

        clearInterval(this.state.IntervalId);
        this.setState({ IntervalId: -1 });
    }

    tick = () => {
        if (Runtime.IS_DEBUG) console.log("real time analysis: tick fired", this.state, this.props);
        this.setState({ LastTextEvaluated: this.props.TextEntered });

        if (this.props.TextEntered === this.state.LastTextEvaluated)
            this.setState({ TicksWithSameText: this.state.TicksWithSameText + 1 });
        else
            this.setState({ TicketsWithSameText: 0 });

        if (this.hasChangedSignificantly()) {
            if (Runtime.IS_DEBUG) console.log("real time analysis: changed significantly", this.state, this.props);

            this.setState({ LastTextUsed: this.props.TextEntered });
            this.props.runAnalyze();
        }
    }

    hasChangedSignificantly = () => {
        if (this.props.TextEntered) {

            //if the length of text entered has changed by more than +- 5, fire
            if (this.props.TextEntered.length > this.state.LastTextUsed.length + RealTimeAnalysisSettings.LENGTH_BUFFER
                || this.props.TextEntered.length < this.state.LastTextUsed - RealTimeAnalysisSettings.LENGTH_BUFFER) {
                return true;
            }

            //if the text has not changed in the last 5 ticks, fire
            if (this.state.TicksWithSameText > RealTimeAnalysisSettings.UNCHANGED_TEXT_TICK_THRESHOLD) {
                this.setState({ TicksWithSameText: 0 });
                return true;
            }
        }
        return false;
    }

    //this component is not indended to render anything, so this seems uneccessary
    //because it is a component though render is required.
    //is there a better way to structure this class?
    render() {
        return (<React.Fragment></React.Fragment>)
    }
}