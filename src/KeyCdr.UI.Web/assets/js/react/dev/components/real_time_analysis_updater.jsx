import React from 'react';

import { Runtime } from '../constants.jsx';

export default class RealTimeAnalysis extends React.Component{
    constructor(props) {
        super(props);

        this.state = {
            LastTextUsed: this.props.TextEntered,
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
        if(Runtime.IS_DEBUG) console.log("starting real time analysis");

        if(this.state.IntervalId == -1)
            this.state.IntervalId = setInterval(this.tick, 3000);
    }

    stop = () => {
        if(Runtime.IS_DEBUG) console.log("stopping real time analysis");
        clearInterval(this.state.IntervalId);
        this.setState({ IntervalId: -1 });
    }

    tick = () => {
        if(Runtime.IS_DEBUG) console.log("real time analysis tick fired", this.state, this.props);
        if (this.hasChangedSignificantly()) {
            this.setState({ LastTextUsed: this.props.TextEntered });
            this.props.runAnalyze();
        }
    }

    hasChangedSignificantly = () => {
        if (this.props.TextEntered) {
            if (this.props.TextEntered.length > this.state.LastTextUsed.length + 5) {
                return true;
            }
        }
        return false;
    }

    render() {
        return (<React.Fragment></React.Fragment>)
    }
}