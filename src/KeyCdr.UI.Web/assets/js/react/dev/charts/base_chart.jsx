export default class BaseChart {
    constructor(props) {
    }

    //absract members
    //getChartDisplayName() { }
    //transformDataToPoints(data) { }
    //transformPointsToLabels(points) { }

    initChart = (title, data) => {

        var points = this.transformDataToPoints(data);
        var labels = this.transformPointsToLabels(points);

        var ctx = document.getElementById('chart');

        var myChart = new Chart(ctx, {
            type: 'line',

            data: {
                labels: labels,
                datasets: [{
                    label: title,
                    data: points,
                    borderWidth: 2,
                    borderColor: "#000000",
                    backgroundColor: "#106fbf"
                }]
            },
            options: {}
        });
    }
}