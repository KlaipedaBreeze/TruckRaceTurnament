function setup() {
    createCanvas(1205, 805);
    getMaze();
}

function draw() {
    background(0);
    if (mazeGraph == null) return;

    var cellSize = 40;
    var wallSize = 5;
    mazeGraph.StopsList.forEach(function(item) {
        fill('white');
        noStroke();
        rect(item.CordX * cellSize + wallSize, item.CordY * cellSize + wallSize,
            cellSize - wallSize, cellSize - wallSize);
    });

    mazeGraph.RoadsList.forEach(function (item) {
        //copy.push(item * item);
        x1 = item.FromStop.CordX * cellSize + (wallSize/2) + (cellSize/2);
        y1 = item.FromStop.CordY * cellSize + (wallSize / 2) + (cellSize / 2);
        x2 = item.ToStop.CordX * cellSize + (wallSize / 2) + (cellSize / 2);
        y2 = item.ToStop.CordY * cellSize + (wallSize / 2) + (cellSize / 2);

        strokeWeight(cellSize/2);
        stroke('white');
        
        line(x1, y1, x2, y2);
    });


}

var mazeGraph;
function getMaze() {
    var jqxhr = $.getJSON("api/GameWorld/GetMaze", function () {
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
