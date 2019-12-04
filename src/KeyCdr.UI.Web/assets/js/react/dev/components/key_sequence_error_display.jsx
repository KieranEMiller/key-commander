import React from 'react';
import ReactDOM from 'react-dom';

import Loading from             './loading.jsx';
import Auth from                '../auth.jsx';
import RealTimeAnalysis from    './real_time_analysis_updater.jsx';

import { Urls, HighlightType, HttpErrorHandler } from    '../constants.jsx';

export default class KeySequenceErrorDisplay extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            isError: false,
            data: null
        }
    }

    componentDidMount() {
        if (this.props.peekInRealTime === true) {
            this.setState({isLoading: false});
        }
        else {
            if (this.props.sequenceId != null && this.props.sequenceId != -1)
                this.getErrorAnalysisForExistingSequence();
        }
    }

    reanalyze = () => {
        console.log('reanalyze fired');
        this.getErrorAnalysisForExistingSequence();
    }

    getErrorAnalysisForExistingSequence = () => {
        var data = {
            SequenceId: this.props.sequenceId,
            TextEntered: (this.props.TextEntered) ? this.props.TextEntered : ""
        };

        return fetch(Urls.API_RUN_ANALYSIS_FOR_SEQ, {
            method: "Post",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
            , body: JSON.stringify(data)
        })
            .then(resp => HttpErrorHandler(resp))
            .then(resp => resp.json())
            .then(data => {
                this.setState({ isLoading: false, data: data });
                return Promise.resolve(data);
            })
            .catch(error => {
                console.log(error);
                this.setState({ isLoading: false, isError: true });
                return Promise.reject();
            });
    }

    render() {

        var textOrAnalysisToShow;

        if (this.state.isLoading === false
            && this.state.isError === false
            && this.state.data) {
            textOrAnalysisToShow = this.state.data.map((highlight, index) => {
                var css = "highlight-" + HighlightType[highlight.HighlightType];
                return (
                    <span key={index} className={css}>{highlight.Text}</span>
                )
            });
        }
        else if (this.props.peekInRealTime == true) {
            textOrAnalysisToShow = (
                <span>{this.props.TextShown}</span>
            )
        }
            
        return (
            <React.Fragment>
                <Loading showLoading={this.state.isLoading} />

                <RealTimeAnalysis
                    runAnalyze={this.reanalyze}
                    TextEntered={this.props.TextEntered}
                    IsRunning={this.props.IsRunning}
                />

                <form>
                    <div className="error_analysis extra_line_spacing">

                        {textOrAnalysisToShow}

                        {this.state.isError === true &&
                            <p className="position_center error">
                            A problem was encountered processing your input
                            </p>
                        }
                    </div>
                    <div className="error_analysis_legend">
                        <ul>
                            <li>
                                <div className="highlight-IncorrectChar"></div>
                               Incorrect
                            </li>
                            <li>
                                <div className="highlight-ExtraChars"></div>
                                <span>Extra Chars</span>
                            </li>
                            <li>
                                <div className="highlight-ShortChars"></div>
                                <span>Short Chars</span>
                            </li>
                            <li>
                                <div className="highlight-Normal"></div>
                                <span>Normal</span>
                            </li>
                            <li>
                                <div className="highlight-Unevaluated"></div>
                                <span>Unevaluated</span>
                            </li>
                        </ul>
                    </div>
                </form>
            </React.Fragment>
        )
    }
}