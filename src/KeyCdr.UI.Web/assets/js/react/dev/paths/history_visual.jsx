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
        this.loadScript("https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.bundle.min.js")
            .then(() => {
                this.getUserHistory()
                    .then(resp => {
                        console.log('visual history resp', resp);
                        this.setState({ isLoading: false, data: resp });
                        this.initGrid();
                    });
            });
        
    }

    initGrid = () => {

        var ctx = document.getElementById('myChart');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
                datasets: [{
                    label: '# of Votes',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
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

    componentDidUpdate() {
        if (this.state && this.state.data && this.state.data.length > 0) {
            this.initGrid();
        }
    }

    render() {
        if (this.authenticationComplete()) {
            return (
                <ContentContainer authed={true}>
                    <h2>Visual History</h2>

                    <div className="content_row">
                        <Loading showLoading={this.state.isLoading} />
                        <canvas id="myChart" width="400" height="400"></canvas>



                        {/*
                        <table id="grid" className="display cell-border stripe hover">
                            <thead>
                                <tr>
                                    <th>CreateDate</th>
                                    <th>SourceType</th>
                                    <th>SourceKey</th>
                                    <th>Details</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            </table>
                            */}
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
