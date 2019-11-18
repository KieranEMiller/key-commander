import React from 'react';
import ReactDOM from 'react-dom';

class Stopwatch extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            StopwatchIntervalId: -1,
            TimerVal: 0,
            IsRunning: this.props.IsRunning
        }
    }

    componentDidUpdate = () => {
        if (this.props.IsRunning != this.state.IsRunning) {

            if (this.props.IsRunning == false) {
                this.stopwatchStop();
            }
            else if (this.props.IsRunning == true) {
                this.stopwatchStart();
            }
            this.setState({ IsRunning: this.props.IsRunning });
        }
    }

    componentWillUnmount() {
        this.stopwatchStop();
    }

    stopwatchStart = () => {
        var intervalId = setInterval(this.stopwatchTick, 250);
        this.setState({ TimerVal: 0, StopwatchIntervalId: intervalId});
    }

    stopwatchStop = () => {
        clearInterval(this.state.StopwatchIntervalId);
    }

    stopwatchTick = () => {
        this.setState({ TimerVal: this.state.TimerVal + 250 });
    }

    formatMsToFriendly = (ms) => {
        let min = Math.floor((ms / (1000 * 60)) % 60);
        let sec = Math.floor((ms / 1000) % 60);
        let milli = parseInt(ms % 1000);

        let result = "";
        if (min > 0) {
            if (min <= 9) result += "0";
            result += (min + "m ");
        }
        if (sec > 0) {
            if (sec <= 9) result += "0";
            result += (sec + "s ");
        }
        
        result += (("000" + milli).slice(-3) + "ms");

        return result;
    }

    render() {
        return (
            <div id="stopwatch">
                {this.formatMsToFriendly(this.state.TimerVal)}
            </div>
        );
    }
};
export default Stopwatch;