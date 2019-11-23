import React from 'react';
import ReactDOM from 'react-dom';
import DeltaIndicator from './delta_indicator.jsx';

class SpeedAnalysis extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            SpeedThisSeq: this.props.AnalysisSpeed,
            SpeedAllTime: this.props.AnalysisSpeedAllTime
        };
    }

    render() {
        return (
            <table className="static_grid">
                <thead>
                    <tr>
                        <th className="grid_header_sm">Metric</th>
                        <th>This Session</th>
                        <th>All Time ({this.state.SpeedAllTime.NumEntitiesRepresented} sessions)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Total Time</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.SpeedThisSeq.TotalTimeInMilliSec}
                                ValToDisplay={this.state.SpeedThisSeq.TotalTimeDisplayFriendly}
                                Val2={this.state.SpeedAllTime.TotalTimeInMilliSec}
                            />
                        </td>
                        <td>{this.state.SpeedAllTime.TotalTimeDisplayFriendly}</td>
                    </tr>
                    <tr>
                        <td>Chars/sec</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.SpeedThisSeq.CharsPerSec}
                                Val2={this.state.SpeedAllTime.CharsPerSec}
                            />
                        </td>
                        <td>{this.state.SpeedAllTime.CharsPerSec}</td>
                    </tr>
                    <tr>
                        <td>Words/min</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.SpeedThisSeq.WordPerMin}
                                Val2={this.state.SpeedAllTime.WordPerMin}
                            />
                        </td>
                        <td>{this.state.SpeedAllTime.WordPerMin}</td>
                    </tr>
                </tbody>
            </table>
        );
    }
}

export default SpeedAnalysis;
