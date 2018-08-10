
$(document).ready(function () {
    chartContainer1();
    chartContainer2();
    chartContainer3();
    chartContainer4();
    chartContainer5();
    chartContainer6();
    chartContainer7();
    chartContainer8();
});

//Phần Xuất Ra Biểu Đồ Hình Cột
//Phần Xuất Ra Biểu Đồ Dòng 1
function chartContainer1() {
    
    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[1].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[1].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[1].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[1].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[1].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[1].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[1].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


    var chart = new CanvasJS.Chart("chartContainer1",
    {
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart.render();
}



function chartContainer2() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[1].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[1].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[1].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


    var chart = new CanvasJS.Chart("chartContainer2", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart.render();
}



//Phần Xuất Ra Biểu Đồ Dòng 2
function chartContainer3() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[2].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[2].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[2].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[2].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[2].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[2].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[2].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


    var chart = new CanvasJS.Chart("chartContainer3",
    {
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart.render();
}



function chartContainer4() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[2].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[2].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[2].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


    var chart = new CanvasJS.Chart("chartContainer4", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 3
function chartContainer5() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[3].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[3].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[3].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[3].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[3].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[3].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[3].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


    var chart = new CanvasJS.Chart("chartContainer5",
    {
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart.render();
}



function chartContainer6() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[3].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[3].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[3].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


    var chart = new CanvasJS.Chart("chartContainer6", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart.render();
}

//Phần Xuất Ra Biểu Đồ Dòng 4
function chartContainer7() {

    //Phần lấy ra Tiêu Đề
    var title = document.getElementById("ExcelView").rows[4].cells[3].innerText;
    var title1 = "Biểu Đồ Tổng Hợp SL Đi Phát Của " + title;

    //Lấy Thông Tin SL BƯU GỬI ĐẾN
    //Lấy tiêu đề của biểu đồ
    try {
        var a = document.getElementById("ExcelView").rows[0].cells[4].innerText;
    }
    catch (e) {
        var a = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var a1 = parseInt(document.getElementById("ExcelView").rows[4].cells[4].innerText);
    }
    catch (e) {
        var a1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT THÀNH CÔNG
    //Lấy tiêu đề của biểu đồ
    try {
        var b = document.getElementById("ExcelView").rows[0].cells[5].innerText;
    }
    catch (e) {
        var b = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var b1 = parseInt(document.getElementById("ExcelView").rows[4].cells[5].innerText);
    }
    catch (e) {
        var b1 = "Null";
    }

    //Lấy Thông Tin SL PHÁT CHƯA CÓ THÔNG TIN
    //Lấy tiêu đề của biểu đồ
    try {
        var c = document.getElementById("ExcelView").rows[0].cells[6].innerText;
    }
    catch (e) {
        var c = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var c1 = parseInt(document.getElementById("ExcelView").rows[4].cells[6].innerText);
    }
    catch (e) {
        var c1 = "Null";
    }

    //Lấy Thông Tin SL PTC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var d = document.getElementById("ExcelView").rows[0].cells[7].innerText;
    }
    catch (e) {
        var d = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var d1 = parseInt(document.getElementById("ExcelView").rows[4].cells[7].innerText);

    }
    catch (e) {
        var d1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var e = document.getElementById("ExcelView").rows[0].cells[8].innerText;
    }
    catch (e) {
        var e = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var e1 = parseInt(document.getElementById("ExcelView").rows[4].cells[8].innerText);

    }
    catch (e) {
        var e1 = "Null";

    }

    //Lấy Thông Tin SL PTC KHÔNG XÁC ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var f = document.getElementById("ExcelView").rows[0].cells[11].innerText;
    }
    catch (e) {
        var f = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var f1 = parseInt(document.getElementById("ExcelView").rows[4].cells[11].innerText);

    }
    catch (e) {
        var f1 = "Null";

    }


    var chart = new CanvasJS.Chart("chartContainer7",
    {
        title: {
            text: title1,
            fontFamily: "tahoma",
        },

        axisY: {
            title: title,
            //titleMaxWidth: 150 //**change the value of titleMaxWidth
        },

        data: [
        {
            type: "column",
            indexLabel: "{y}",
            dataPoints: [
            { label: a, y: a1 },
            { label: b, y: b1 },
            { label: c, y: c1 },
            { label: d, y: d1 },
            { label: e, y: e1 },
            { label: f, y: f1 },
            ]
        }
        ]
    });

    chart.render();
}



function chartContainer8() {
    //Phần Xuất Ra Biểu Đồ Hình Tròn

    //Phần Lấy Ra Tiêu Đề Biểu Đồ Hình Tròn
    var titleTron = document.getElementById("ExcelView").rows[4].cells[3].innerText;
    var titleTron1 = "Tỉ Lệ PTC Của " + titleTron;

    //Lấy Thông Tin Tỉ Lệ TC ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeTRUE = document.getElementById("ExcelView").rows[0].cells[9].innerText;
    }
    catch (e) {
        var tieudeTRUE = "Null";
    }

    //Lấy giá trị của biểu đồ
    try {
        var giatriTRUE = parseInt(document.getElementById("ExcelView").rows[4].cells[9].innerText);
    }
    catch (e) {
        var giatriTRUE = "Null";
    }

    //Lấy Thông Tin Tỉ Lệ TC KHÔNG ĐÚNG QUY ĐỊNH
    //Lấy tiêu đề của biểu đồ
    try {
        var tieudeFALSE = document.getElementById("ExcelView").rows[0].cells[10].innerText;
    }
    catch (e) {
        var tieudeFALSE = "Null";
    }

    //Lấy gía trị của biểu đồ
    try {
        var giatriFALSE = parseInt(document.getElementById("ExcelView").rows[4].cells[10].innerText);
    }
    catch (e) {
        var giatriFALSE = "Null";
    }


    var chart = new CanvasJS.Chart("chartContainer8", {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleTron1,
            fontFamily: "tahoma",
        },
        legend: {
            cursor: "pointer",
            itemclick: explodePie
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} - {y}%",
            dataPoints: [
                { y: giatriTRUE, name: tieudeTRUE, exploded: true },
                { y: giatriFALSE, name: tieudeFALSE },


            ]
        }]
    });
    chart.render();
}

function explodePie(e) {
    if (typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = true;
    } else {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
    }
    e.chart.render();

}
