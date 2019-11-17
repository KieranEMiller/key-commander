import React from 'react';
import ReactDOM from 'react-dom';
import DeltaIndicator from '../components/delta_indicator.jsx';

class AccuracyAnalysis extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            AccuracyThisSeq: this.props.AnalysisAccuracy,
            AccuracyAllTime: this.props.AnalysisAccuracyAllTime
        };
    }

    render() {
        return (
            <table className="static_grid">
                <thead>
                    <tr>
                        <th>Metric</th>
                        <th>This Session</th>
                        <th>All Time ({this.state.AccuracyAllTime.NumEntitiesRepresented} sessions)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Accuracy</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.AccuracyThisSeq.Accuracy}
                                Val2={this.state.AccuracyAllTime.Accuracy}
                            />
                        </td>
                        <td>{this.state.AccuracyAllTime.Accuracy}</td>
                    </tr>
                    <tr>
                        <td>Num Chars</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.AccuracyThisSeq.NumChars}
                                Val2={this.state.AccuracyAllTime.NumChars}
                            />
                        </td>
                        <td>{this.state.AccuracyAllTime.NumChars}</td>
                    </tr>
                    <tr>
                        <td>Num Words</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.AccuracyThisSeq.NumWords}
                                Val2={this.state.AccuracyAllTime.NumWords}
                            />
                        </td>
                        <td>{this.state.AccuracyAllTime.NumWords}</td>
                    </tr>
                    <tr>
                        <td>Num Correct Chars</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.AccuracyThisSeq.NumCorrectChars}
                                Val2={this.state.AccuracyAllTime.NumCorrectChars}
                            />
                        </td>
                        <td>{this.state.AccuracyAllTime.NumCorrectChars}</td>
                    </tr>
                    <tr>
                        <td>Num Incorrect Chars</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.AccuracyThisSeq.NumIncorrectChars}
                                Val2={this.state.AccuracyAllTime.NumIncorrectChars}
                            />
                        </td>
                        <td>{this.state.AccuracyAllTime.NumIncorrectChars}</td>
                    </tr>
                    <tr>
                        <td>Num Extra Chars</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.AccuracyThisSeq.NumExtraChars}
                                Val2={this.state.AccuracyAllTime.NumExtraChars}
                            />
                        </td>
                        <td>{this.state.AccuracyAllTime.NumExtraChars}</td>
                    </tr>
                    <tr>
                        <td>Num Short Chars</td>
                        <td>
                            <DeltaIndicator
                                Val1={this.state.AccuracyThisSeq.NumShortChars}
                                Val2={this.state.AccuracyAllTime.NumShortChars}
                            />
                        </td>
                        <td>{this.state.AccuracyAllTime.NumShortChars}</td>
                    </tr>
                </tbody>
            </table>
        );
    }
}

export default AccuracyAnalysis;
