function setup() {
    //createCanvas(1205, 805);
    var myCanvas = createCanvas(1205, 805);
    myCanvas.parent("draw");
    getMaze();
}

function draw() {
    background(0);
    if (mazeGraph == null) return;

    var cellSize = 40;
    var wallSize = 5;
    mazeGraph.StopsList.forEach(function (item) {
        fill('white');
        noStroke();
        rect(item.CordX * cellSize + wallSize, item.CordY * cellSize + wallSize,
            cellSize - wallSize, cellSize - wallSize);
    });

    mazeGraph.RoadsList.forEach(function (item) {
        //copy.push(item * item);
        x1 = item.FromStop.CordX * cellSize + (wallSize / 2) + (cellSize / 2);
        y1 = item.FromStop.CordY * cellSize + (wallSize / 2) + (cellSize / 2);
        x2 = item.ToStop.CordX * cellSize + (wallSize / 2) + (cellSize / 2);
        y2 = item.ToStop.CordY * cellSize + (wallSize / 2) + (cellSize / 2);

        strokeWeight(cellSize * 0.9);
        stroke('white');
        line(x1, y1, x2, y2);
    });

    mazeGraph.MineList.forEach(function (item) {
        fill('yellow');
        noStroke();
        rect(item.CordX * cellSize + wallSize, item.CordY * cellSize + wallSize,
            cellSize - wallSize, cellSize - wallSize);
    });


    fill('lightblue');
    noStroke();
    rect(mazeGraph.StartStop.CordX * cellSize + wallSize, mazeGraph.StartStop.CordY * cellSize + wallSize,
        cellSize - wallSize, cellSize - wallSize);



    if (trucks != null) {
        var count = 0;
        trucks.forEach(function (item) {
            xdev = count % 4 * (cellSize / 4) + (cellSize / 4 / 2);
            ydev = Math.floor(count / 4) * (cellSize / 4) + (cellSize / 4 / 2);

            count++;
            x = item.Location.CordX * cellSize + (wallSize / 2) + xdev;
            y = item.Location.CordY * cellSize + (wallSize / 2) + ydev;

            strokeWeight(cellSize * 0.2);
            stroke(item.Color);
            point(x, y);
        });
    }
}

var mazeGraph;
function getMaze() {
    var jqxhr = $.getJSON("/api/GameWorld/GetMaze", function () {
        console.log("success");
    })
        .done(function (data) {
            console.log("second success");
            console.log(data);
            mazeGraph = data;
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
}
var trucks;
function getTrucks() {
    var jqxhr = $.getJSON("/api/GameWorld/GetTrucks", function () {
        console.log("success");
    })
        .done(function (data) {
            console.log("second success");
            console.log(data);
            trucks = data;
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
}

function initCall() {

    var jqxhr = $.getJSON("/api/GameControl/InitGame", function () {
        console.log("success");
    });
    getMaze();
}
var myVar = setInterval(getMaze, 10000);
var myVar = setInterval(getTrucks, 1000);