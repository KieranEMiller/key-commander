import React from 'react';
import ReactDOM from 'react-dom';

import Loading from     './loading.jsx';
import Auth from        '../auth.jsx';
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
        this.getErrorAnalysis();
    }

    getErrorAnalysis() {
        var data = { SequenceId: this.props.sequenceId };

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
        return (
            <React.Fragment>
                <Loading showLoading={this.state.isLoading} />
                <form>
                    <div className="error_analysis">
                        {this.state.isLoading === false &&
                           this.state.data &&
                           this.state.isError === false &&
                           this.state.data.map((highlight, index) => {
                                var css = "highlight-" + HighlightType[highlight.HighlightType];
                                return (
                                    <span key={index} className={css}>{highlight.Text}</span>
                                )
                            })
                        }

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