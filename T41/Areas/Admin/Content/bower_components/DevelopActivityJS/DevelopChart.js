function DevelopChartSL() {
    var data = [];

    var dataSeries = { type: "line", showInLegend: true, legendText: "SL Đến", };
    var dataPoints = [];
    var count = $('#example1 tr').length;
    count -= 2;

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var bi = parseInt(document.getElementById("example1").rows[i].cells[6].innerText);
        dataPoints.push({
            label: ai,
            y: bi
        });
    }

    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);

    var dataSeries = { type: "line", showInLegend: true, legendText: "SL Đi", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[17].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);

    var dataSeries = { type: "line", showInLegend: true, legendText: "SL Tồn Lũy kế", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[10].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);



    var dataSeries = { type: "line", showInLegend: true, legendText: "SL Đến Lũy Kế", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[8].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);

    var dataSeries = { type: "line", showInLegend: true, legendText: "SL Đi Lũy Kế", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[19].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);


    var chart = new CanvasJS.Chart("chartContainer", {
        exportEnabled: true,
        animationEnabled: true,
        theme: "dark1",
        legend: {
            verticalAlign: "top",
            cursor: "pointer",
            itemclick: function (e) {
                //console.log("legend click: " + e.dataPointIndex);
                //console.log(e);
                if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                    e.dataSeries.visible = false;
                } else {
                    e.dataSeries.visible = true;
                }

                e.chart.render();
            }
        },
        title: {
            text: "Báo cáo hoạt động sàn khai thác nội tỉnh theo ngày xác nhận đến",
            fontFamily: "roboto",
        },
        axisY: {
            minimum: 0,
        },
        data: data  // random generator below
    });


    chart.render();
}

function DevelopChartKL() {
    var data = [];

    var dataSeries = { type: "line", showInLegend: true, legendText: "KL Đến", };
    var dataPoints = [];
    var count = $('#example1 tr').length;
    count -= 2;

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var bi = parseInt(document.getElementById("example1").rows[i].cells[7].innerText);
        dataPoints.push({
            label: ai,
            y: bi
        });
    }

    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);

    var dataSeries = { type: "line", showInLegend: true, legendText: "KL Đi", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[18].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);

    var dataSeries = { type: "line", showInLegend: true, legendText: "KL Tồn Lũy kế", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[11].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);



    var dataSeries = { type: "line", showInLegend: true, legendText: "KL Đến Lũy Kế", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[9].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);

    var dataSeries = { type: "line", showInLegend: true, legendText: "KL Đi Lũy Kế", };
    var dataPoints = [];

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[24].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[20].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);


    var chart = new CanvasJS.Chart("chartContainer1", {
        exportEnabled: true,
        animationEnabled: true,
        theme: "dark1",
        legend: {
            verticalAlign: "top",
            cursor: "pointer",
            itemclick: function (e) {
                //console.log("legend click: " + e.dataPointIndex);
                //console.log(e);
                if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                    e.dataSeries.visible = false;
                } else {
                    e.dataSeries.visible = true;
                }

                e.chart.render();
            }
        },
        title: {
            text: "Báo cáo hoạt động sàn khai thác nội tỉnh theo ngày xác nhận đến",
            fontFamily: "roboto",
        },
        axisY: {
            minimum: 0,
        },
        data: data  // random generator below
    });


    chart.render();
}