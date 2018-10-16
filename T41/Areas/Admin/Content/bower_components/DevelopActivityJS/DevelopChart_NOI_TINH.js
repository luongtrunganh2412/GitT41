function DevelopChartSL() {
    var data = [];

    var dataSeries = { type: "line", showInLegend: true, legendText: "SL Đến", };
    var dataPoints = [];
    var count = $('#example1 tr').length;
    count -= 2;

    for (var i = 2; i < count; i += 1) {
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var bi = parseInt(document.getElementById("example1").rows[i].cells[9].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[23].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[13].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[11].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[25].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);
    CanvasJS.addColorSet("setcolor",
               [//colorSet Array

               "#ffff00",
               "#33cc33",
               "#ff3300",
               "#ff9900",
               "#00ffff"
               ]);

    
    var chart = new CanvasJS.Chart("chartContainer", {
        colorSet: "setcolor",
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
            text: "Báo cáo hoạt động sàn khai thác nội tỉnh theo ngày xác nhận đến" + "-" + checktinhchapnhan + "-" + checktinhphattra,
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var bi = parseInt(document.getElementById("example1").rows[i].cells[10].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[24].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[14].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[12].innerText);
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
        var ai = document.getElementById("example1").rows[i].cells[31].innerText;
        var ci = parseInt(document.getElementById("example1").rows[i].cells[26].innerText);
        dataPoints.push({
            label: ai,
            y: ci
        });
    }


    dataSeries.dataPoints = dataPoints;
    data.push(dataSeries);

    CanvasJS.addColorSet("setcolor",
               [//colorSet Array

               "#ffff00",
               "#33cc33",
               "#ff3300",
               "#ff9900",
               "#33ccff"
               ]);

    
    var chart = new CanvasJS.Chart("chartContainer1", {
        colorSet: "setcolor",
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
            text: "Báo cáo hoạt động sàn khai thác nội tỉnh theo ngày xác nhận đến" + "-" + checktinhchapnhan + "-" + checktinhphattra,
            fontFamily: "roboto",
        },
        axisY: {
            minimum: 0,
        },
        data: data  // random generator below
    });


    chart.render();
}