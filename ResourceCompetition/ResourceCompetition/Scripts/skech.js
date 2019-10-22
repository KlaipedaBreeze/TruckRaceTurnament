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
            DisplayResults(trucks);
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

function DisplayResults(trucks) {
    if (trucks != null) {
        var count = 0;
        var output = "";
        trucks.forEach(function (item) {

            output += item.Id + " ";
            output += item.Color + " "; 
            output += item.Fuel + " ";
            output += item.Score + " ";
            output += item.Token + " ";
            //output += item.Id;
            //output += item.Id;

            //Cargo: []
            //Color: "red"
            //Fuel: 15000
            //Id: 0
            //LastMove: "2019-10-22T14:29:56.4978829+03:00"
            //Location: { Name: "Stop-0-0", Id: 1, CordX: 0, CordY: 0, IsHidden: false }
            //MaxWeight: 20
            //MinTimePeriod: "00:00:00.2500000"
            //Mines: [{ Id: 1, Location: { Name: "Stop-19-1", Id: 50, CordX: 19, CordY: 1, IsHidden: true }, … }, …]
            //Score: 0
            //Token: "0"
            output += "<br>";
        });
        $("#results").html(output);
        //console.log("dd");
    }
}

var myVar = setInterval(getMaze, 10000);
var myVar = setInterval(getTrucks, 1000);