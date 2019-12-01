import React from 'react';
import ReactDOM from 'react-dom';

import ContentContainer from    '../content.jsx';
import Loading from             '../components/loading.jsx';
import SecureComponent from     './secure_component.jsx';
import { Urls } from            '../constants.jsx';
import ChartDisplayer from      '../charts/chart_component.jsx';

export default class HistoryVisual extends SecureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true
        }

        this.loadDependancies();
    }

    loadDependancies() {
        this.loadScript("/assets/js/external/Chart.bundle.min.js")
            .then(() => {
                this.setState({ isLoading: false });
            });
    }

    render() {
        if (this.authenticationComplete()) {
            return (
                <ContentContainer authed={true}>
                    <h2>Visual History</h2>

                    <div className="content_row">
                        <ChartDisplayer />
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
