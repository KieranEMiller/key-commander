import BaseChart from './base_chart.jsx';

export default class WordsPerMinuteChart extends BaseChart {
    constructor(props) {
        super(props);
    }

    getChartDisplayName() {
        return "Words Per Minute";
    }

    getDateFrom(data) { return data.SpeedDateFrom; }
    getDateTo(data) { return data.SpeedDateFrom; }
    getTotalCount(data) { return data.SpeedMeasurementCount; }

    transformDataToPoints = (data) => {
        var points = data.SpeedMeasurements.map((item, index) => {
            return { t: (item.CreateDateDisplayFriendly), y: item.WordPerMin };
        });
        return points;
    }

    transformPointsToLabels = (points) => {
        //use a counter and only show the even labels
        //in order to show the datapoints but not the labels use
        //the hack of setting the label to empty string
        var counter = 1;
        var labels = points.reduce(function (result, item) {
            if (counter % 2 == 0) {
                result.push(item.t);
            }
            else { result.push("");}
            counter++;
            return result;
        }, []);
        return labels;
    }
}