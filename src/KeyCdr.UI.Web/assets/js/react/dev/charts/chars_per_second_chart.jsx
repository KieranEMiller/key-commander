import BaseChart from './base_chart.jsx';

export default class CharsPerSecondChart extends BaseChart {
    constructor(props) {
        super(props);
    }

    getChartDisplayName() {
        return "Characters Per Second";
    }

    transformDataToPoints = (data) => {
        var points = data.SpeedMeasurements.map((item, index) => {
            return { t: (item.CreateDateDisplayFriendly), y: item.CharsPerSec };
        });
        return points;
    }

    transformPointsToLabels = (points) => {
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