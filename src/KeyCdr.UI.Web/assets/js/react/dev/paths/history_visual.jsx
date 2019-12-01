import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from    '../content.jsx';
import Loading from             '../components/loading.jsx';
import SecureComponent from     './secure_component.jsx';
import { Urls } from            '../constants.jsx';

export default class HistoryVisual extends SecureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            data: null
        }

        this.loadDependencies();
    }

    loadDependencies() {
        this.loadScript("/assets/js/external/Chart.bundle.min.js")
            .then(() => {
                this.getUserHistory()
                    .then(resp => {
                        this.setState({ isLoading: false, data: resp });
                    });
            });
    }

    componentDidUpdate() {
        if (this.state && this.state.data) {
            this.initGrid();
        }
    }

    transformDataToPoints = (data) => {
        var points = data.SpeedMeasurements.map((item, index) => {
            return { t: (item.CreateDateDisplayFriendly), y: item.CharsPerSec };
        });
        return points;
    }

    transformPointsToLabels = (points) => {
        var labels = points.map((item, index) => {
            return item.t;
        });
        return labels;
    }

    initGrid = (points) => {

        var points = this.transformDataToPoints(this.state.data);
        var labels = this.transformPointsToLabels(points);

        var ctx = document.getElementById('chart');

        var myChart = new Chart(ctx, {
            type: 'line',
            borderColor: "#bae755",
            borderDash: [5, 5],
            backgroundColor: "#e755ba",
            pointBackgroundColor: "#55bae7",
            pointBorderColor: "#55bae7",
            pointHoverBackgroundColor: "#55bae7",
            pointHoverBorderColor: "#55bae7",
            data: {
                labels: labels,
                datasets: [{
                    label: 'Characters per Second',
                    data: points,
                    borderWidth: 1
                }]
            }
        });
    }

    getUserHistory() {
        var token = this.auth.getCurrentToken();
        return fetch(Urls.API_HISTORY_VISUAL, {
                method: "Post",
                headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
                , body: JSON.stringify(token)
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
                    <h2>Visual History</h2>

                    <div className="content_row">
                        <Loading showLoading={this.state.isLoading} />

                        <div className="chart_container">
                            <canvas id="chart"></canvas>
                        </div>

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
