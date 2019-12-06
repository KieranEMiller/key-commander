export default class BaseChart {
    constructor(props) {
    }

    //abstract members
    //getChartDisplayName() {}
    //getDateFrom() {}
    //getDateTo() {}
    //getTotalCount() {}
    //transformDataToPoints(data) {}
    //transformPointsToLabels(points) {}

    getDateFromDisplay(data) {
        var dt = new Date(this.getDateFrom(data));
        return dt.toDateString();
    }
    getDateToDisplay(data) {
        var dt = new Date(this.getDateTo(data));
        return dt.toDateString();
    }
    getTotalCountDisplay(data) { return this.getTotalCount(data);}

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