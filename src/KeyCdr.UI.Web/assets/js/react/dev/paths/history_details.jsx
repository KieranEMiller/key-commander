import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from    '../content.jsx';
import Loading from             '../components/loading.jsx';
import SecureComponent from     './secure_component.jsx';
import SpeedAnalysis from       '../components/speed_analysis.jsx';
import AccuracyAnalysis from    '../components/accuracy_analysis.jsx';
import { Urls } from            '../constants.jsx';
import KeySequenceErrorDisplay from  '../components/key_sequence_error_display.jsx';

class HistoryDetails extends SecureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            data: null
        }
    }

    componentDidMount() {
        this.getHistoryDetails()
            .then(resp => {
                this.setState({ isLoading: false, data: resp });
            });
    }

    componentDidUpdate() {
        if (this.state && this.state.data && this.state.data.length > 0) {
            this.initGrid();
        }
    }

    getHistoryDetails() {
        var token = this.auth.getCurrentToken();
        var data = { token: token, sequenceId: this.props.match.params.sequenceId };
        return fetch(Urls.API_HISTORY_DETAILS, {
                method: "Post",
                headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
                , body: JSON.stringify(data)
            })
            .then(resp => resp.json())
            .then(data => {
                return Promise.resolve(data);
            });
    }

    render() {
        if (this.authenticationComplete()) {

            return (
                <ContentContainer authed={true}>
                    <h2>History Details</h2>

                    <div className="content_row_sm">
                        <Loading showLoading={this.state.isLoading} />
                        <h3>Text Presented to You</h3>

                        {(this.state.data) &&
                            <KeySequenceErrorDisplay
                                TextEntered={this.state.data.TextEntered}
                                sequenceId={this.props.match.params.sequenceId}
                            />
                        }

                    </div>

                    <div className="content_row_sm">
                        <h3>Text You Entered</h3>

                        <form>
                            <textarea name="textEntered"
                                className="extra_line_spacing"
                                value={
                                    (this.state.data != null) ?
                                        this.state.data.TextEntered : ""
                                }
                                readOnly
                            />
                        </form> 
                    </div>

                    <div className="content_row_sm">
                        <h3>Text Source (AKA: "Where did this text come from")</h3>

                            {this.state.isLoading == false && 
                                this.state.data &&
                                <table className="static_grid">
                                    <thead>
                                        <tr>
                                            <th className="grid_header_sm">SourceType</th>
                                            <th>Source Key</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>{this.state.data.SourceType}</td>
                                            <td>
                                                <a href={this.state.data.SourceKey}> {this.state.data.SourceKey}</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            }
                    </div>

                    <div className="content_row_sm">
                        <Loading showLoading={this.state.isLoading} />
                        <h3>Speed Analysis</h3>

                        {this.state.isLoading == false && 
                            this.state.data &&
                            this.state.data.AnalysisSpeed &&
                            this.state.data.AnalysisSpeedAllTime &&
                            <SpeedAnalysis
                                AnalysisSpeed={this.state.data.AnalysisSpeed}
                                AnalysisSpeedAllTime={this.state.data.AnalysisSpeedAllTime}
                            />
                        }
                    </div>

                    <div className="content_row_sm">
                        <Loading showLoading={this.state.isLoading} />
                        <h3>Accuracy Analysis</h3>

                        {this.state.isLoading == false && 
                            this.state.data &&
                            this.state.data.AnalysisAccuracy &&
                            this.state.data.AnalysisAccuracyAllTime &&

                            <AccuracyAnalysis
                                AnalysisAccuracy={this.state.data.AnalysisAccuracy}
                                AnalysisAccuracyAllTime={this.state.data.AnalysisAccuracyAllTime}
                            />
                        }
                    </div>
                </ContentContainer>
            );
        }
        else {
            return (
                super.render()
            )
        }
    }
}

export default HistoryDetails;