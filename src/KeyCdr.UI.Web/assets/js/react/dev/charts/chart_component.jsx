import React from 'react';
import ReactDOM from 'react-dom';
import Select from 'react-select';

import Auth from                    '../auth.jsx';
import Loading from                 '../components/loading.jsx'
import { Urls } from                '../constants.jsx';
import CharsPerSecondChart from     './chars_per_second_chart.jsx';
import WordsPerMinuteChart from     './words_per_minute_chart.jsx';

const Charts = {
    DefaultChartType: 'cps',
    VisualHistoryChartTypes: [
        { value: 'cps', label: 'Speed - Characters Per Second'},
        { value: 'wpm', label: 'Speed - Words Per Minute'}
    ]
};

export default class ChartDisplayer extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: null,
            isLoading: true,
            SelectedChartType: Charts.VisualHistoryChartTypes.filter(option => option.value === Charts.DefaultChartType)[0]
        }

        this.getUserHistory()
            .then(data => {
                this.setState({data: data, isLoading: false})
            });

        this.currentChart = null;
    }

    componentDidUpdate() {
        if (this.state && this.state.data) {
            this.handleChartTypeChange();
        }
    }

    handleChartTypeChange = (selection) => {
        if(selection)
            this.setState({ SelectedChartType: selection });

        if (this.state.SelectedChartType.value == 'cps')
            this.currentChart = new CharsPerSecondChart();

        else if(this.state.SelectedChartType.value == 'wpm')
            this.currentChart = new WordsPerMinuteChart();

        else
            this.currentChart = null;

        if (this.currentChart != null && this.state.data) {
            var displayName = this.currentChart.getChartDisplayName();
            this.currentChart.initChart(displayName, this.state.data);

            document.getElementById('chartDateFrom').innerText = this.currentChart.getDateFromDisplay(this.state.data);
            document.getElementById('chartDateTo').innerText = this.currentChart.getDateToDisplay(this.state.data);
            document.getElementById('chartTotalCount').innerText = this.currentChart.getTotalCountDisplay(this.state.data);
        }
    }

    getUserHistory() {
        var auth = new Auth();
        var token = auth.getCurrentToken();
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
        return (
            <React.Fragment>
                <Loading showLoading={this.state.isLoading} />

                {this.state.isLoading === false &&
                    <React.Fragment>
                        <div className="react_select_container">
                            <Select
                                value={this.state.SelectedChartType}
                                onChange={this.handleChartTypeChange}
                                options={Charts.VisualHistoryChartTypes}
                                defaultValue={Charts.DefaultChartType}
                            />
                        </div>

                        <div className="centered_list">
                            <ul>
                            <li>
                                <span className="strong">Total Count: </span>
                                <span id="chartTotalCount"></span>
                            </li>
                            <li>
                                <span className="strong">Dates From: </span>
                                <span id="chartDateFrom"></span>
                            </li>
                            <li>
                                <span className="strong">Dates To: </span>
                                <span id="chartDateTo"></span>
                            </li>
                            </ul>
                        </div>

                        <div className="chart_container">
                            <canvas id="chart"></canvas>
                        </div> 
                    </React.Fragment>
                }
            </React.Fragment>
        );
    }
}